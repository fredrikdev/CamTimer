using System.IO;
using System.Drawing;
using System.Threading;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using DirectShowLib;
using System.Drawing.Imaging;
using System.Windows.Forms;
using CamTimer.Controls;

namespace CamTimer {
	internal delegate Bitmap TakePicture();
	internal delegate Bitmap TakePictureWithPreview(SynchronizedPictureBox pictureControl);
	internal delegate void ErrorEventHandler(string message);

	static class WebcamManager {
		internal static Webcam[] Enumerate() {
			List<Webcam> result = new List<Webcam>();

			// directshow enumeration
			try {
				DsDevice[] dsDevices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
				for (int x = 0; x < dsDevices.Length; x++) {
					result.Add(new WebcamDirectShow(x, dsDevices[x].Name));
				}
			} catch (Exception) { }

			// wia enumeration
			try {
				WIA.DeviceManager deviceManager = new WIA.DeviceManagerClass();
				foreach (WIA.DeviceInfo deviceInfo in deviceManager.DeviceInfos) {
					if ((deviceInfo.Type == WIA.WiaDeviceType.CameraDeviceType) || (deviceInfo.Type == WIA.WiaDeviceType.VideoDeviceType)) {
						result.Add(new WebcamWIA(deviceInfo));
					}
				}
			} catch (Exception) { }

			// avicap32 enumeration
			try {
				for (short x = 0; x < 10; x++) {
					string name = new string(' ', 255);
					string version = new string(' ', 255);
					if (NativeMethods.capGetDriverDescriptionA(x, ref name, name.Length, ref version, version.Length)) {
						if (name.IndexOf('\x00') > -1) name = name.Substring(0, name.IndexOf('\x00'));
						if (version.IndexOf('\x00') > -1) version = version.Substring(0, version.IndexOf('\x00'));
						result.Add(new WebcamAVICap(x, name.Trim()));
					}
				}
			} catch (Exception) { }

			return result.ToArray();
		}

		internal static Webcam FindByName(string name) {
			Webcam[] cams = Enumerate();
			for (int x = 0; x < cams.Length; x++) {
				if (cams[x].ToString() == name) {
					return cams[x];
				}
			}
			return null;
		}

		static class NativeMethods {
			[DllImport("avicap32.dll")]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool capGetDriverDescriptionA(short wDriverIndex, [MarshalAs(UnmanagedType.VBByRefStr)]ref String lpszName, int cbName, [MarshalAs(UnmanagedType.VBByRefStr)] ref String lpszVer, int cbVer);
		}
	}

	abstract class Webcam {
		internal abstract Bitmap TakePicture();
		internal abstract void Config(WebcamConfiguration configuration);
		internal abstract WebcamConfiguration[] QueryFormats();
		public abstract override string ToString();
	}

	abstract class WebcamWithPreview : Webcam {
		protected bool m_takePictureEnd = false;
		protected Bitmap m_displacementMap = null;

		internal abstract Bitmap TakePicture(SynchronizedPictureBox pictureControl);
		internal virtual void TakePictureEnd() {
			m_takePictureEnd = true;
		}
		internal virtual Bitmap DisplacementMap {
			set {
				m_displacementMap = value;
			}
		}

		internal static unsafe void DistortedDraw(byte* sourceBuffer, Bitmap displacementMap, Bitmap destBitmap) {
			int sourceWidth = destBitmap.Width, sourceHeight = destBitmap.Height;
			BitmapData previewData = destBitmap.LockBits(new Rectangle(0, 0, sourceWidth, sourceHeight), ImageLockMode.WriteOnly, destBitmap.PixelFormat);
			int sourceDataStride = previewData.Stride;
			int sourceBPP = destBitmap.PixelFormat.ToString().Contains("16") ? 2 : destBitmap.PixelFormat.ToString().Contains("24") ? 3 : destBitmap.PixelFormat.ToString().Contains("32") ? 4 : 0;

			if (displacementMap == null) {
				unsafe {
					byte* previewScan0 = (byte*)previewData.Scan0;
					byte* sourceScan0 = (byte*)sourceBuffer;
					for (int y = 0, yofs = 0, invyofs = (sourceHeight - 1 - y) * sourceDataStride; y < sourceHeight; y++, yofs += sourceDataStride, invyofs -= sourceDataStride) {
						for (int x = 0; x < sourceDataStride; x += 4) {
							*(int*)(previewScan0 + yofs + x) = *(int*)(sourceScan0 + invyofs + x);
						}
					}
				}
			} else {
				BitmapData mapData = displacementMap.LockBits(new Rectangle(0, 0, displacementMap.Width, displacementMap.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
				int mapDataStride = mapData.Stride;
				int mapDeltaY = (768 << 16) / sourceHeight;
				int mapDeltaX = (1024 << 16) / sourceWidth;

				unsafe {
					byte* sourceDataScan0 = (byte*)sourceBuffer;
					byte* destDataScan0 = (byte*)previewData.Scan0;
					byte* mapDataScan0 = (byte*)mapData.Scan0;
					int value, mapOffset, mapOffsetY, sourceOffset;

					for (int y = 0, mapY = 0, destOffsetY = 0; y < sourceHeight; y++, destOffsetY += sourceDataStride, mapY += mapDeltaY) {
						mapOffsetY = (mapY >> 16) * mapDataStride;
						for (int x = 0, mapX = 0, destOffset = destOffsetY; x < sourceWidth; x++, destOffset += sourceBPP, mapX += mapDeltaX) {
							// read 24bit value from displacement map
							mapOffset = mapOffsetY + ((mapX >> 16 << 2) + (mapX >> 16 << 1) >> 1); //(mapX >> 16) * 3; 
							value = *(ushort*)(mapDataScan0 + mapOffset) + (*(mapDataScan0 + mapOffset + 2) << 16);

							// calculate offset relative to this image (x = map.x/1024*x*sourceBPP, y = map.y/768*sourceDataStride)
							sourceOffset =
								(((767 - ((value & 0xFFF000) >> 12)) << 16) / 768 * sourceHeight >> 16) * sourceDataStride +
								((((value & 0xFFF)) << 16) / 1024 * sourceWidth >> 16) * sourceBPP;

							if (sourceBPP == 3) {
								*(ushort*)(destDataScan0 + destOffset) = *(ushort*)(sourceDataScan0 + sourceOffset);
								*(destDataScan0 + destOffset + 2) = *(sourceDataScan0 + sourceOffset + 2);
							} else if (sourceBPP == 2) {
								*(ushort*)(destDataScan0 + destOffset) = *(ushort*)(sourceDataScan0 + sourceOffset);
							} else if (sourceBPP == 4) {
								*(int*)(destDataScan0 + destOffset) = *(int*)(sourceDataScan0 + sourceOffset);
							}
						}
					}
				}

				displacementMap.UnlockBits(mapData);
			}
			destBitmap.UnlockBits(previewData);
		}
	}

	sealed class WebcamDirectShow : WebcamWithPreview, ISampleGrabberCB {
		private readonly int m_cameraDeviceIndex;
		private readonly string m_name;

		private int m_callbackState;
		private ManualResetEvent m_callbackCompleted;
		private WebcamConfiguration m_configuration = WebcamConfiguration.Empty;
		private Bitmap m_capturedBitmap;
		private SynchronizedPictureBox m_pictureControl;

		internal WebcamDirectShow(int cameraDeviceIndex, string name) {
			m_cameraDeviceIndex = cameraDeviceIndex;
			m_name = name;
		}

		internal override void Config(WebcamConfiguration configuration) {
			m_configuration = configuration;
		}

		internal override WebcamConfiguration[] QueryFormats() {
			List<WebcamConfiguration> result = new List<WebcamConfiguration>();

			DsDevice cameraDevice = cameraDevice = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice)[m_cameraDeviceIndex];
			IFilterGraph2 filterGraph = null; IBaseFilter cam = null; IPin camOutPin = null;
			try {
				filterGraph = (IFilterGraph2)new FilterGraph();
				DsError.ThrowExceptionForHR(filterGraph.AddSourceFilterForMoniker(cameraDevice.Mon, null, cameraDevice.Name, out cam));
				camOutPin = DsFindPin.ByCategory(cam, PinCategory.Capture, 0);

				if (camOutPin != null) {
					IAMStreamConfig config = (IAMStreamConfig)camOutPin;

					int piCount, piSize;
					config.GetNumberOfCapabilities(out piCount, out piSize);

					byte[] temp = new byte[piSize];
					GCHandle tempHandle = GCHandle.Alloc(temp, GCHandleType.Pinned);
					try {
						for (int x = 0; x < piCount; x++) {
							AMMediaType mediaType = null;
							try {
								DsError.ThrowExceptionForHR(config.GetStreamCaps(x, out mediaType, tempHandle.AddrOfPinnedObject()));
								VideoInfoHeader v = (VideoInfoHeader)Marshal.PtrToStructure(mediaType.formatPtr, typeof(VideoInfoHeader));
								
								if (BPPIsValid(v.BmiHeader.BitCount)) {
									result.Add(new WebcamConfiguration(new Size(v.BmiHeader.Width, v.BmiHeader.Height), v.BmiHeader.BitCount, mediaType.subType));
								} else {
									//System.Diagnostics.Debug.WriteLine("BPP " + v.BmiHeader.BitCount + " was not accepted!");
								}
							} finally {
								if (mediaType != null) {
									DsUtils.FreeAMMediaType(mediaType);
									mediaType = null;
								}
							}
						}
					} finally {
						tempHandle.Free();
					}
				}
			} finally {
				if (camOutPin != null) {
					Marshal.ReleaseComObject(camOutPin);
					camOutPin = null;
				}
				if (filterGraph != null) {
					Marshal.ReleaseComObject(filterGraph);
					filterGraph = null;
				}
			}

			result.Sort();
			return result.ToArray();
		}

		internal override Bitmap TakePicture() {
			return TakePicture(null);
		}

		internal override Bitmap TakePicture(SynchronizedPictureBox pictureControl) {
			if (m_callbackCompleted != null) {
				return null;
			}
			m_pictureControl = pictureControl;
			m_takePictureEnd = false;

			DsDevice cameraDevice = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice)[m_cameraDeviceIndex];

			IFilterGraph2 filterGraph = null;
			IBaseFilter cam = null; IPin camCapture = null;										// cam
			ISampleGrabber sg = null; IPin sgIn = null;											// samplegrabber

			try {
				// setup filterGraph & connect camera
				filterGraph = (IFilterGraph2)new FilterGraph();
				DsError.ThrowExceptionForHR(filterGraph.AddSourceFilterForMoniker(cameraDevice.Mon, null, cameraDevice.Name, out cam));

				// setup smarttee and connect so that cam(PinCategory.Capture)->st(PinDirection.Input)
				camCapture = DsFindPin.ByCategory(cam, PinCategory.Capture, 0);					// output
				ConfStreamDimensions((IAMStreamConfig)camCapture);

				// connect Camera output to SampleGrabber input
				sg = (ISampleGrabber)new SampleGrabber();

				// configure
				AMMediaType media = new AMMediaType();
				try {
					media.majorType = MediaType.Video;
					media.subType = BPP2MediaSubtype(m_configuration.BPP);	// this will ask samplegrabber to do convertions for us
					media.formatType = FormatType.VideoInfo;
					DsError.ThrowExceptionForHR(sg.SetMediaType(media));
				} finally {
					DsUtils.FreeAMMediaType(media);
					media = null;
				}

				DsError.ThrowExceptionForHR(sg.SetCallback(this, 1));							// 1 = BufferCB
				DsError.ThrowExceptionForHR(filterGraph.AddFilter((IBaseFilter)sg, "SG"));
				sgIn = DsFindPin.ByDirection((IBaseFilter)sg, PinDirection.Input, 0);			// input
				DsError.ThrowExceptionForHR(filterGraph.Connect(camCapture, sgIn));
				GetSizeInfo(sg);

				// wait until timeout - or picture has been taken 
				if (m_callbackCompleted == null) {
					m_callbackCompleted = new ManualResetEvent(false);

					// start filter
					DsError.ThrowExceptionForHR(((IMediaControl)filterGraph).Run());
					m_callbackState = 5;
					if (m_pictureControl != null) {
						m_callbackCompleted.WaitOne();
					} else {
						if (!m_callbackCompleted.WaitOne(15000, false)) {
							throw new Exception(); //"Timeout while waiting for Picture");
						}
					}
					return m_capturedBitmap;
				} else {
					return null;
				}
			} finally {
				// release allocated objects
				if (m_callbackCompleted != null) {
					m_callbackCompleted.Close();
					m_callbackCompleted = null;
				}
				if (sgIn != null) {
					Marshal.ReleaseComObject(sgIn);
					sgIn = null;
				}
				if (sg != null) {
					Marshal.ReleaseComObject(sg);
					sg = null;
				}
				if (camCapture != null) {
					Marshal.ReleaseComObject(camCapture);
					camCapture = null;
				}
				if (cam != null) {
					Marshal.ReleaseComObject(cam);
					cam = null;
				}
				if (filterGraph != null) {
					try {
						((IMediaControl)filterGraph).Stop();
					} catch (Exception) { }
					Marshal.ReleaseComObject(filterGraph);
					filterGraph = null;
				}
				m_capturedBitmap = null;
				m_callbackCompleted = null;
			}
		}

		#region TakePicture helpers
		private void GetSizeInfo(ISampleGrabber sampleGrabber) {
			// Get the media type from the SampleGrabber
			AMMediaType media = new AMMediaType();
			try {
				DsError.ThrowExceptionForHR(sampleGrabber.GetConnectedMediaType(media));
				if ((media.formatType != FormatType.VideoInfo) || (media.formatPtr == IntPtr.Zero)) {
					throw new NotSupportedException(); //"Unknown Grabber Media Format");
				}

				VideoInfoHeader v = (VideoInfoHeader)Marshal.PtrToStructure(media.formatPtr, typeof(VideoInfoHeader));
				m_configuration.Size = new Size(v.BmiHeader.Width, v.BmiHeader.Height);
				m_configuration.BPP = v.BmiHeader.BitCount;
				//m_configuration.MediaSubtype = media.subType;
			} finally {
				DsUtils.FreeAMMediaType(media);
				media = null;
			}
		}

		private void ConfStreamDimensions(IAMStreamConfig streamConfig) {
			AMMediaType media = null;
			DsError.ThrowExceptionForHR(streamConfig.GetFormat(out media));

			try {
				VideoInfoHeader v = (VideoInfoHeader)Marshal.PtrToStructure(media.formatPtr, typeof(VideoInfoHeader));
				if (m_configuration.Size.Width > 0) {
					v.BmiHeader.Width = m_configuration.Size.Width;
				}
				if (m_configuration.Size.Height > 0) {
					v.BmiHeader.Height = m_configuration.Size.Height;
				}
				if (m_configuration.BPP > 0) {
					v.BmiHeader.BitCount = m_configuration.BPP;
				}
				if (m_configuration.MediaSubtype != Guid.Empty) {
					media.subType = m_configuration.MediaSubtype;
				}
				//v.AvgTimePerFrame = 10000000 / 30; // 30 fps. FPS might be controlled by the camera, because of lightning exposure may increase and FPS decrease. 

				Marshal.StructureToPtr(v, media.formatPtr, false);
				DsError.ThrowExceptionForHR(streamConfig.SetFormat(media));
			} finally {
				DsUtils.FreeAMMediaType(media);
				media = null;
			}
		}
		#endregion

		#region ISampleGrabberCB Callbacks
		int ISampleGrabberCB.SampleCB(double SampleTime, IMediaSample pSample) {
			Marshal.ReleaseComObject(pSample);
			return 0;
		}

		int ISampleGrabberCB.BufferCB(double SampleTime, IntPtr pBuffer, int BufferLen) {
			if (m_callbackState > 0) {
				/*
				#region Just as if BufferCB was called
				IntPtr pBuffer;

				pSample.GetPointer(out pBuffer);
				int BufferLen = pSample.GetActualDataLength();
				#endregion
				*/
				if (m_pictureControl != null) {
					lock (m_pictureControl.SynchronizationObject) {
						unsafe {
							DistortedDraw((byte*)pBuffer, m_displacementMap, m_pictureControl.DisplayBitmap);
						}
					}

					m_pictureControl.Refresh();

					if (m_takePictureEnd) {
						m_callbackState = 0;
						m_capturedBitmap = (Bitmap)m_pictureControl.DisplayBitmap;
						m_callbackCompleted.Set();
					}
				} else {
					m_callbackState--;
					if (m_callbackState <= 0) {
						using (Bitmap b = new Bitmap(m_configuration.Size.Width, m_configuration.Size.Height, BufferLen / m_configuration.Size.Height, m_configuration.PixelFormat, pBuffer)) {
							m_capturedBitmap = new Bitmap(b);	// copy
							m_capturedBitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
						}

						m_callbackCompleted.Set();
					}
				}
			}						
	
			return 0;
		}
		#endregion

		internal static Guid BPP2MediaSubtype(short bpp) {
			switch (bpp) {
				case 16:
					return MediaSubType.RGB565;
				case 24:
					return MediaSubType.RGB24;
				case 32:
					return MediaSubType.RGB32;
			}
			return MediaSubType.RGB24;
		}

		internal static bool BPPIsValid(short bpp) {
			switch (bpp) {
				case 16:
				case 24:
				case 32:
					return true;
			}
			return false;
		}

		public override string ToString() {
			return m_name + " [DirectShow]";
		}
	}

	sealed class WebcamWIA : Webcam {
		private readonly WIA.DeviceInfo m_deviceInfo;
		private readonly string m_name;

		internal WebcamWIA(WIA.DeviceInfo deviceInfo) {
			m_deviceInfo = deviceInfo;
			foreach (WIA.Property property in deviceInfo.Properties) {
				if (property.Name.Equals("Name", StringComparison.OrdinalIgnoreCase)) {
					m_name = (string)property.get_Value();
					break;
				}
			}
		}

		internal override void Config(WebcamConfiguration configuration) {
		}

		internal override WebcamConfiguration[] QueryFormats() {
			return new WebcamConfiguration[0];
		}

		internal override Bitmap TakePicture() {
			Bitmap result = null;

			// take a picture
			WIA.Device device = m_deviceInfo.Connect();
			WIA.Item item = device.ExecuteCommand(WIA.CommandID.wiaCommandTakePicture);

			foreach (string format in item.Formats) {
				// transfer
				WIA.ImageFile imageFile = item.Transfer(format) as WIA.ImageFile;

				// save
				string tempFile = System.IO.Path.GetTempFileName();
				File.Delete(tempFile);
				imageFile.SaveFile(tempFile);

				// delete from cam
				DeleteItem(device.Items, item.ItemID);

				// return image
				MemoryStream ms = new MemoryStream(File.ReadAllBytes(tempFile));
				File.Delete(tempFile);
				result = (Bitmap)Bitmap.FromStream(ms);

				break;
			}

			return result;
		}

		//TODO: possibly reverse iteration to improve performance
		private bool DeleteItem(WIA.Items items, string itemId) {
			for (int x = 1; x <= items.Count; x++) {
				if (items[x].ItemID == itemId) {
					items.Remove(x);
					return true;
				} else if (items[x].Items.Count > 0) {
					if (DeleteItem(items[x].Items, itemId)) {
						return true;
					}
				}
			}
			return false;
		}

		public override string ToString() {
			return m_name + " [WIA]";
		}
	}

	sealed class WebcamAVICap : Webcam {
		private readonly int m_index;
		private readonly string m_name;

		private int m_captureHwnd;
		private string m_captureTemp = string.Empty;

		internal WebcamAVICap(int index, string name) {
			m_index = index;
			m_name = name;
		}

		internal override void Config(WebcamConfiguration configuration) {
		}

		internal override WebcamConfiguration[] QueryFormats() {
			return new WebcamConfiguration[0];
		}

		public override string ToString() {
			return m_name + " [AVICap]";
		}

		internal override Bitmap TakePicture() {
			int timeoutMSec = 20000;

			Bitmap result = null;

			// setup & connect
			string captureWTitle = Application.ProductName + " " + m_index;
			m_captureHwnd = NativeMethods.capCreateCaptureWindowA(ref captureWTitle, 0, 0, 0, 320, 240, 0, 0);

			if (NativeMethods.SendMessage(m_captureHwnd, NativeMethods.WM_CAP_DRIVER_CONNECT, m_index, 0) != 0) {
				NativeMethods.FrameReceivedEventHandler eventHandler = new NativeMethods.FrameReceivedEventHandler(FrameReceived);

				NativeMethods.SendMessage(m_captureHwnd, NativeMethods.WM_CAP_SET_SCALE, 0, 0);
				NativeMethods.SendMessageCallback(m_captureHwnd, NativeMethods.WM_CAP_SET_CALLBACK_FRAME, 0, eventHandler);

				// get frames until we've saved one
				m_captureTemp = string.Empty;
				int tickStart = Environment.TickCount;
				while ((m_captureTemp.Length == 0) && (Environment.TickCount - tickStart < timeoutMSec)) {
					NativeMethods.SendMessage(m_captureHwnd, NativeMethods.WM_CAP_GRAB_FRAME, 0, 0);
					if (m_captureTemp.Length == 0) {
						Thread.Sleep(100);
					}
				}

				if (m_captureTemp.Length > 0) {
					MemoryStream ms = new MemoryStream(File.ReadAllBytes(m_captureTemp));
					File.Delete(m_captureTemp);
					result = (Bitmap)Bitmap.FromStream(ms);
				} 

				NativeMethods.SendMessageCallback(m_captureHwnd, NativeMethods.WM_CAP_SET_CALLBACK_FRAME, 0, null);
				NativeMethods.SendMessage(m_captureHwnd, NativeMethods.WM_CAP_DRIVER_DISCONNECT, m_index, 0);
			}

			// destroy
			NativeMethods.DestroyWindow(m_captureHwnd);

			if (m_captureTemp.Length == 0) {
				throw new Exception();
			}

			return result;
		}

		private void FrameReceived(IntPtr lwnd, IntPtr lpVHdr) {
			NativeMethods.VIDEOHDR videoHeader = (NativeMethods.VIDEOHDR)Marshal.PtrToStructure(lpVHdr, typeof(NativeMethods.VIDEOHDR));
			if ((videoHeader.dwBytesUsed > 0) && (m_captureTemp.Length == 0)) {
				string tempFile = System.IO.Path.GetTempFileName();
				if (NativeMethods.SendMessage(m_captureHwnd, NativeMethods.WM_CAP_FILE_SAVEDIB, 0, tempFile) == 1) {
					m_captureTemp = tempFile;
					NativeMethods.SendMessageCallback(m_captureHwnd, NativeMethods.WM_CAP_SET_CALLBACK_FRAME, 0, null);
				}
			}
		}

		static class NativeMethods {
			internal delegate void FrameReceivedEventHandler(IntPtr lwnd, IntPtr lpVHdr);

			internal const int WM_USER = 0x400;
			internal const int WM_CAP_SET_SCALE = WM_USER + 53;
			internal const int WM_CAP_DRIVER_CONNECT = WM_USER + 10;
			internal const int WM_CAP_DRIVER_DISCONNECT = WM_USER + 11;
			internal const int WM_CAP_SET_CALLBACK_FRAME = WM_USER + 5;
			internal const int WM_CAP_GRAB_FRAME = WM_USER + 60;
			internal const int WM_CAP_FILE_SAVEDIB = WM_USER + 25;

			[StructLayout(LayoutKind.Sequential)]
			internal struct VIDEOHDR {
				[MarshalAs(UnmanagedType.I4)]
				internal int lpData;
				[MarshalAs(UnmanagedType.I4)]
				internal int dwBufferLength;
				[MarshalAs(UnmanagedType.I4)]
				internal int dwBytesUsed;
				[MarshalAs(UnmanagedType.I4)]
				internal int dwTimeCaptured;
				[MarshalAs(UnmanagedType.I4)]
				internal int dwUser;
				[MarshalAs(UnmanagedType.I4)]
				internal int dwFlags;
				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
				internal int[] dwReserved;
			}

			[DllImport("avicap32.dll")]
			internal static extern int capCreateCaptureWindowA([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, int hWndParent, int nID);

			[DllImport("user32.dll", EntryPoint = "SendMessageA")]
			internal static extern int SendMessage(int hwnd, int wMsg, int wParam, [MarshalAs(UnmanagedType.AsAny)] object lParam);

			[DllImport("user32.dll", EntryPoint = "SendMessageA")]
			internal static extern int SendMessageCallback(int hWnd, int wMsg, short wParam, FrameReceivedEventHandler lParam);

			[DllImport("user32.dll")]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool DestroyWindow(int hwnd);
		}
	}

	internal struct WebcamConfiguration : IComparable {
		internal static readonly WebcamConfiguration Empty = new WebcamConfiguration(Size.Empty, 0, Guid.Empty);
		private Size m_size;
		private short m_bpp;
		private Guid m_mediaSubtype;

		internal WebcamConfiguration(Size size, short bpp, Guid mediaSubtype) {
			m_size = size;
			m_bpp = bpp;
			m_mediaSubtype = mediaSubtype;
		}

		internal PixelFormat PixelFormat {
			get {
				switch (m_bpp) {
					case 32:
						return PixelFormat.Format32bppRgb;
					case 16:
						return PixelFormat.Format16bppRgb565;
					default:
						return PixelFormat.Format24bppRgb;
				}
			}
		}

		internal Size Size {
			get {
				return m_size;
			}
			set {
				m_size = value;
			}
		}

		internal short BPP {
			get {
				return m_bpp;
			}
			set {
				m_bpp = value;
			}
		}

		internal Guid MediaSubtype {
			get {
				return m_mediaSubtype;
			}
			set {
				m_mediaSubtype = value;
			}
		}

		public override int GetHashCode() {
			return m_size.Width * m_size.Height * m_bpp;
		}

		public int CompareTo(object obj) {
			if (obj is WebcamConfiguration) {
				WebcamConfiguration y = (WebcamConfiguration)obj;
				return this.GetHashCode() - y.GetHashCode();
			} else {
				return int.MinValue;
			}
		}

		public override bool Equals(object obj) {
			return this.CompareTo(obj) == 0;
		}

		public static bool operator ==(WebcamConfiguration obj1, WebcamConfiguration obj2) {
			return obj1.Equals(obj2);
		}

		public static bool operator !=(WebcamConfiguration obj1, WebcamConfiguration obj2) {
			return !obj1.Equals(obj2);
		}

		public static bool operator <(WebcamConfiguration obj1, WebcamConfiguration obj2) {
			return obj1.GetHashCode() < obj2.GetHashCode();
		}

		public static bool operator >(WebcamConfiguration obj1, WebcamConfiguration obj2) {
			return obj1.GetHashCode() > obj2.GetHashCode();
		}
	}
}
