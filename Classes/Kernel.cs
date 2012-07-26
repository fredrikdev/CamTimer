using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CamTimer {
	class Kernel {
		private NotifyIcon m_notifyIcon;
		private System.Windows.Forms.Timer m_scheduleThread;

		internal Kernel(string[] args) {
			m_notifyIcon = new NotifyIcon();
			m_notifyIcon.Icon = new System.Drawing.Icon(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("CamTimer.Graphics.mainIcon.ico"));
			m_notifyIcon.DoubleClick += new EventHandler(m_notifyIcon_Show);
			m_notifyIcon.ContextMenu = new ContextMenu(new MenuItem[] { 
				new MenuItem("&" + Application.ProductName, m_notifyIcon_Show), 
				new MenuItem(Language.FormatString(Language.LanguageString.NotifyMenu_TakePicture), m_notifyIcon_TakePicture), 
				new MenuItem("-"), 
				new MenuItem(Language.FormatString(Language.LanguageString.NotifyMenu_Exit), new EventHandler(m_notifyIcon_Exit)) });
			m_notifyIcon.ContextMenu.MenuItems[0].DefaultItem = true;
			m_notifyIcon.Text = Application.ProductName;
			m_notifyIcon.Visible = true;

			if (Settings.FirstRun == DateTime.MinValue) {
				Settings.FirstRun = DateTime.Now;
				m_notifyIcon.ShowBalloonTip(5000, 
					Language.FormatString(Language.LanguageString.NotifyMenu_FirstRunBalloonTitle),
					Language.FormatString(Language.LanguageString.NotifyMenu_FirstRunBalloonText), 
					ToolTipIcon.Info);
			}

			bool showInterface = true;
			for (int x = 0; x < args.Length; x++) {
				if ((args[x].Trim().Equals("/autostart", StringComparison.OrdinalIgnoreCase)) || (args[x].Trim().Equals("-autostart", StringComparison.OrdinalIgnoreCase))) {
					showInterface = false;
					break;
				}
			}
			if (showInterface) {
				MainForm.ShowInstance(0);
			}

			m_scheduleThread = new System.Windows.Forms.Timer();
			#if (DEBUG)
			m_scheduleThread.Interval = 10000;	// start after 10 seconds
			#else
			m_scheduleThread.Interval = 60000*2;	// start after 2 minutes (to prevent annoying user at system startup).
			#endif
			m_scheduleThread.Tick += (EventHandler)delegate(object sender, EventArgs e) {
				if (m_scheduleThread.Interval != 1000) {
					m_scheduleThread.Interval = 1000;
				}

				// check if we should take a picture
				if (Settings.ScheduleEnabled) {
					DateTime now = DateTime.Now;

					if (RunToday(now.DayOfWeek)) {
						if ((now.TimeOfDay >= Settings.ScheduleTimeStart.TimeOfDay) && (now.TimeOfDay <= Settings.ScheduleTimeEnd.TimeOfDay)) {
							bool takePicture = false;

							for (TimeSpan x = Settings.ScheduleTimeStart.TimeOfDay; x <= Settings.ScheduleTimeEnd.TimeOfDay; x = x.Add(new TimeSpan(Settings.ScheduleInterval, 0, 0))) {
								DateTime calculatedRunAt = new DateTime(now.Year, now.Month, now.Day, x.Hours, x.Minutes, x.Seconds);
								if (now > calculatedRunAt) {
									if (calculatedRunAt > Settings.ScheduleLastRun) {
										takePicture = true;
										break;
									}
								}
							}

							// check screensaver if specified
							if ((takePicture) && (Settings.ScheduleDisabledOnScreensaver)) {
								bool screenSaverRunning = false;
								if (NativeMethods.SystemParametersInfo(NativeMethods.SPI_GETSCREENSAVERRUNNING, 0, ref screenSaverRunning, 0)) {
									if (screenSaverRunning) takePicture = false;
								}
							}

							// check if fullscreen directX window (game or such) is running
							if (takePicture) {
								IntPtr fgHwnd = NativeMethods.GetForegroundWindow();
								if ((fgHwnd != null) && (fgHwnd != IntPtr.Zero)) {
									if ((fgHwnd != NativeMethods.GetDesktopWindow()) && (fgHwnd != NativeMethods.GetShellWindow())) {
										NativeMethods.RECT appRect;
										if (NativeMethods.GetWindowRect(fgHwnd, out appRect) != 0) {
											System.Drawing.Rectangle screenRect = Screen.FromHandle(fgHwnd).Bounds;

											if (((appRect.Bottom - appRect.Top) == screenRect.Height) && ((appRect.Right - appRect.Left) == screenRect.Width)) {
												takePicture = false;
											}
										}
									}
								}
							}

							if (takePicture) {
								Settings.ScheduleLastRun = now;
								NotifyForm.ShowInstance();
							}
						}
					}
				}
			};
			m_scheduleThread.Start();
		}

		static bool RunToday(DayOfWeek dow) {
			return
				((dow == DayOfWeek.Monday) && (Settings.ScheduleMon)) ||
				((dow == DayOfWeek.Tuesday) && (Settings.ScheduleTue)) ||
				((dow == DayOfWeek.Wednesday) && (Settings.ScheduleWed)) ||
				((dow == DayOfWeek.Thursday) && (Settings.ScheduleThu)) ||
				((dow == DayOfWeek.Friday) && (Settings.ScheduleFri)) ||
				((dow == DayOfWeek.Saturday) && (Settings.ScheduleSat)) ||
				((dow == DayOfWeek.Sunday) && (Settings.ScheduleSun));
		}

		void m_notifyIcon_Show(object sender, EventArgs e) {
			MainForm.ShowInstance(0);
		}

		void m_notifyIcon_TakePicture(object sender, EventArgs e) {
			MainForm.ShowInstance(1);
		}

		void m_notifyIcon_Exit(object sender, EventArgs e) {
			m_scheduleThread.Stop();
			m_scheduleThread.Dispose();
			m_notifyIcon.Dispose();
			Application.Exit();
		}

		static class NativeMethods {
			internal const int SPI_GETSCREENSAVERRUNNING = 114;

			[StructLayout(LayoutKind.Sequential)]
			internal struct RECT {
				internal int Left;
				internal int Top;
				internal int Right;
				internal int Bottom;
			}

			[DllImport("user32.dll")]
			internal static extern IntPtr GetForegroundWindow();

			[DllImport("user32.dll")]
			internal static extern IntPtr GetDesktopWindow();

			[DllImport("user32.dll")]
			internal static extern IntPtr GetShellWindow();

			[DllImport("user32.dll", SetLastError = true)]
			internal static extern int GetWindowRect(IntPtr hwnd, out RECT rc);

			[DllImport("user32.dll", CharSet = CharSet.Auto)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool SystemParametersInfo(int uAction, int uParam, [MarshalAs(UnmanagedType.Bool)]ref bool lpvParam, int flags);
		}
	}
}
