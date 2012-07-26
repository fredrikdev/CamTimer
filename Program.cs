using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CamTimer {
	static class Program {
		[STAThread]
		static void Main(string[] args) {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			#region create thumbnail option
			for (int x = 0; x < args.Length; x++) {
				if ((args[x].Trim().Equals("/displace", StringComparison.OrdinalIgnoreCase)) || (args[x].Trim().Equals("-displace", StringComparison.OrdinalIgnoreCase))) {
					OpenFileDialog openDialog = new OpenFileDialog();
					openDialog.Title = "Select displacement map";
					openDialog.Filter = "All files (*.*)|*.*";
					openDialog.FilterIndex = 1;
					openDialog.CheckFileExists = true;
					openDialog.DereferenceLinks = true;
					openDialog.ShowReadOnly = false;
					openDialog.FileName = string.Empty;
					if (openDialog.ShowDialog() == DialogResult.OK) {
						// load displacement map
						string mapName = openDialog.FileName;
						Bitmap mapBitmap;
						try {
							mapBitmap = new Bitmap(mapName);
						} catch (Exception) {
							MessageBox.Show("Error loading displacement map. Application ended.", Application.ProductName, MessageBoxButtons.OK);
							return;
						}
						if ((mapBitmap.PixelFormat != System.Drawing.Imaging.PixelFormat.Format24bppRgb) || (mapBitmap.Size.Width != 1024) || (mapBitmap.Size.Height != 768)) {
							MessageBox.Show("The displacement map must be 1024x768 and 24bpp. Application ended.", Application.ProductName, MessageBoxButtons.OK);
							return;
						}

						openDialog.Title = "Select image to displace";
						openDialog.FileName = string.Empty;
						if (openDialog.ShowDialog() == DialogResult.OK) {
							// load source bitmap
							string sourceName = openDialog.FileName;
							Bitmap sourceBitmap;
							try {
								sourceBitmap = new Bitmap(sourceName);
							} catch (Exception) {
								MessageBox.Show("Error loading image. Application ended.", Application.ProductName, MessageBoxButtons.OK);
								return;
							}
							if ((sourceBitmap.PixelFormat != System.Drawing.Imaging.PixelFormat.Format16bppRgb565) && (sourceBitmap.PixelFormat != System.Drawing.Imaging.PixelFormat.Format24bppRgb) && (sourceBitmap.PixelFormat != System.Drawing.Imaging.PixelFormat.Format32bppRgb)) {
								MessageBox.Show("This image to displace must be 16bpp(565), 24bpp(888) or 32bpp. Application ended.", Application.ProductName, MessageBoxButtons.OK);
								return;
							}

							// create distorted bitmap
							Bitmap destBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height, sourceBitmap.PixelFormat);
							Bitmap destThumb = new Bitmap((int) (320*0.5), (int) (240*0.5), sourceBitmap.PixelFormat);
							unsafe {
								BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, sourceBitmap.PixelFormat);
								byte* sourceScan0 = (byte*)sourceData.Scan0;
								WebcamWithPreview.DistortedDraw((byte*)sourceScan0, mapBitmap, destBitmap);
								sourceBitmap.UnlockBits(sourceData);
							}
							using (Graphics g = Graphics.FromImage(destThumb)) {
								g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
								g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
								g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
								g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
								g.DrawImage(destBitmap, g.VisibleClipBounds);
							}

							// write distorted jpeg
							EncoderParameters eargs = new EncoderParameters();
							eargs.Param = new EncoderParameter[] { new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 95L) };

							ImageCodecInfo[] arrCodecs = ImageCodecInfo.GetImageEncoders();
							foreach (ImageCodecInfo i in arrCodecs) {
								if (i.FormatID.Equals(ImageFormat.Jpeg.Guid)) {
									destThumb.Save(Path.Combine(Path.GetDirectoryName(mapName), Path.GetFileNameWithoutExtension(mapName)) + "-thumb.jpg", i, eargs);
									break;
								}
							}

							MessageBox.Show("Displaced bitmap created successfully! Application ended.", Application.ProductName, MessageBoxButtons.OK);
						}
					}
					return;
				}
			}
			#endregion

			// one instance limit
			IntPtr mutexPtr = NativeMethods.CreateMutexA(IntPtr.Zero, false, "FJRCamTimerMutex");
			if (mutexPtr != IntPtr.Zero) {
				if (Marshal.GetLastWin32Error() == NativeMethods.ERROR_ALREADY_EXISTS) {
					NativeMethods.ReleaseMutex(mutexPtr);
					MessageBox.Show(Language.FormatString(Language.LanguageString.App_AlreadyRunning), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
			}

			Kernel kernel = new Kernel(args);
			Application.Run();
			kernel = null;

			if (mutexPtr != IntPtr.Zero) {
				NativeMethods.ReleaseMutex(mutexPtr);
			}
		}

		static class NativeMethods {
			internal const int ERROR_ALREADY_EXISTS = 183;

			[DllImport("kernel32.dll", SetLastError = true)]
			internal static extern IntPtr CreateMutexA(IntPtr lpMutexAttributes, bool bInitialOwner, string lpName);

			[DllImport("kernel32.dll")]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool ReleaseMutex(IntPtr hMutex);
		}
	}

}
