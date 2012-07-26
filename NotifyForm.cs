using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;

namespace CamTimer {
	partial class NotifyForm : Form {
		private static NotifyForm s_instance;
		private Webcam m_cam;
		private const int s_animationDurationMSec = 1000;
		private Thread m_windowAnimator;
		private Thread m_countdownTimer;
		private TakePicture m_takePictureAsync;
		private string m_fileName;

		internal static void ShowInstance() {
			Webcam cam = WebcamManager.FindByName(Settings.CamName);

			if (cam != null) {
				if (s_instance == null) {
					s_instance = new NotifyForm();
				}

				if (Settings.ScheduleMaxResultionAndQuality) {
					cam.Config(new WebcamConfiguration(Settings.CamConfigSizeMax, Settings.CamConfigBPPMax, Settings.CamConfigMediaSubTypeMax));
				} else {
					cam.Config(new WebcamConfiguration(Settings.CamConfigSize, Settings.CamConfigBPP, Settings.CamConfigMediaSubType));
				}
				s_instance.m_cam = cam;
				s_instance.AnimateIn();
			}
		}

		private NotifyForm() {
			InitializeComponent();
			m_fileName = Common.GenerateFilename(Settings.OutputFilename);
			takePictureButton.Text = Language.FormatString(Language.LanguageString.NotifyForm_TakePictureAgain);
			hideButton.Text = Language.FormatString(Language.LanguageString.NotifyForm_HideNow);
			settingsButton.Text = Language.FormatString(Language.LanguageString.NotifyForm_Configuration);
		}

		private void notifyForm_FormClosed(object sender, FormClosedEventArgs e) {
			s_instance = null;
		}

		private void AnimateIn() {
			if (m_windowAnimator == null) {
				Rectangle screenBounds = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
	
				m_windowAnimator = new Thread((ThreadStart)delegate() {
					int startMSec = System.Environment.TickCount;
					while (true) {
						int elapsedMSec = System.Environment.TickCount - startMSec;
						double percentRemaining = 1 - (Math.Min(elapsedMSec, s_animationDurationMSec) / (double)s_animationDurationMSec);
						int newLeft = (int)(Math.Min(percentRemaining * percentRemaining * percentRemaining, 1) * Width);
						try {
							Invoke((MethodInvoker)delegate() {
								Left = screenBounds.Right-(Width-newLeft);
							});
						} catch (Exception) { }
						if (elapsedMSec >= s_animationDurationMSec) {
							break;
						}

						Thread.Sleep((int)(1000 / 33d));	// 33 fps
					}

					m_windowAnimator = null;
					try {
						Invoke((MethodInvoker)delegate() {
							TakePicture();
						});
					} catch (Exception) { }
				});
				NativeMethods.SetWindowPos(this.Handle, NativeMethods.HWND_TOPMOST, screenBounds.Right, screenBounds.Top + 100, 0, 0, NativeMethods.SWP_NOACTIVATE | NativeMethods.SWP_NOSIZE | NativeMethods.SWP_SHOWWINDOW);
				Visible = true;
				m_windowAnimator.Start();
			}
		}

		private void CountdownTimerSetup() {
			if (m_countdownTimer == null) {
				Rectangle screenBounds = System.Windows.Forms.Screen.PrimaryScreen.Bounds;

				m_countdownTimer = new Thread((ThreadStart)delegate() {
					int x = 0;
					while (true) {
						if (!Visible) {
							break;
						} else if (Bounds.Contains(Cursor.Position)) {
							x = 0;
							Thread.Sleep(1000);
						} else if (x < 5) {
							x++;
							Thread.Sleep(1000);
						} else {
							break;
						}
					}
				
					int startMSec = System.Environment.TickCount;
					while (true) {
						int elapsedMSec = System.Environment.TickCount - startMSec;
						double percentRemaining = 1 - (Math.Min(elapsedMSec, s_animationDurationMSec) / (double)s_animationDurationMSec);
						int newLeft = (int)(Math.Min(percentRemaining * percentRemaining * percentRemaining, 1) * Width);
						try {
							Invoke((MethodInvoker)delegate() {
								Left = screenBounds.Right - newLeft;
							});
						} catch (Exception) { }
						if (elapsedMSec >= s_animationDurationMSec) {
							break;
						}

						Thread.Sleep((int)(1000 / 33d));	// 33 fps
					}

					m_countdownTimer = null;
					try {
						Invoke((MethodInvoker)delegate() {
							Close();
						});
					} catch (Exception) { }
				});
				m_countdownTimer.Start();
			}
		}

		internal void TakePicture() {
			if ((m_takePictureAsync == null) && (m_cam != null)) {
				if (m_countdownTimer != null) {
					m_countdownTimer.Abort();
					m_countdownTimer = null;
				}

				pictureBox.Text = Language.FormatString(Language.LanguageString.MainForm_TakePicture_TakingPicture);
				m_takePictureAsync = new TakePicture(m_cam.TakePicture);
				m_takePictureAsync.BeginInvoke((AsyncCallback)delegate(IAsyncResult result) {
					try {
						pictureBox.DisplayBitmap = m_takePictureAsync.EndInvoke(result);
						Invoke((MethodInvoker)delegate() {
							try {
								Common.SaveJPEG(pictureBox.DisplayBitmap, Path.Combine(Settings.OutputFolder, m_fileName));
								pictureBox.Text = string.Empty;
							} catch (Exception) {
								pictureBox.Text = Language.FormatString(Language.LanguageString.NotifyForm_FailedToSave) + "\r\n" + m_fileName;
							}
						});
					} catch (Exception) {
						Invoke((MethodInvoker)delegate() {
							pictureBox.Text = Language.FormatString(Language.LanguageString.NotifyForm_FailedToTakePicture);
						});
					} finally {
						m_takePictureAsync = null;
						CountdownTimerSetup();
					}
				}, null);
			}
		}

		private void pictureBox_Click(object sender, EventArgs e) {
			TakePicture();
		}

		private void takePictureButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			TakePicture();
		}

		private void hideNowButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			Visible = false;
		}

		private void settingsButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			Visible = false;			
			MainForm.ShowInstance(0);
		}

		static class NativeMethods {
			internal static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
			internal const UInt32 SWP_NOSIZE = 0x0001;
			internal const UInt32 SWP_NOMOVE = 0x0002;
			internal const UInt32 SWP_NOZORDER = 0x0004;
			internal const UInt32 SWP_NOREDRAW = 0x0008;
			internal const UInt32 SWP_NOACTIVATE = 0x0010;
			internal const UInt32 SWP_FRAMECHANGED = 0x0020;  /* The frame changed: send WM_NCCALCSIZE */
			internal const UInt32 SWP_SHOWWINDOW = 0x0040;
			internal const UInt32 SWP_HIDEWINDOW = 0x0080;
			internal const UInt32 SWP_NOCOPYBITS = 0x0100;
			internal const UInt32 SWP_NOOWNERZORDER = 0x0200;  /* Don't do owner Z ordering */
			internal const UInt32 SWP_NOSENDCHANGING = 0x0400;  /* Don't send WM_WINDOWPOSCHANGING */

			[DllImport("user32.dll")]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
		}
	}
}
