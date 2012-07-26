using System;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Globalization;
using CamTimer.Controls;

namespace CamTimer {
	partial class MainForm : Form {
		private static MainForm s_instance;
		private TakePicture m_takePictureAsync;
		private TakePictureWithPreview m_takePictureWithPreviewAsync;
		private int m_lastEnumTick;
		private Animator m_animator = new Animator();

		internal static void ShowInstance(int initScreenIndex) {
			if (s_instance == null) {
				s_instance = new MainForm();
			}
			if (initScreenIndex == 0) {
				s_instance.confPanel.Left = 0;
				s_instance.confPanel.Visible = true;
				s_instance.takePicturePanel.Visible = false;
				s_instance.Text = Application.ProductName + " - " + Language.FormatString(Language.LanguageString.MainForm_Configuration_Title);
			} else {
				s_instance.takePictureSaveButton.Enabled = false;
				s_instance.takePicturePanel.Left = 0;
				s_instance.takePicturePanel.Visible = true;
				s_instance.confPanel.Visible = false;
				s_instance.Text = Application.ProductName + " - " + Language.FormatString(Language.LanguageString.MainForm_TakePicture_Title);
				s_instance.TakePicture(null, null);
			}

			s_instance.Show();
			s_instance.BringToFront();
		}

		private MainForm() {
			InitializeComponent();
			Width = 560;
			confScheduleInterval.Items.Clear();
			for (int x = 23; x > 0; x--) {
				confScheduleInterval.Items.Add(x);
			}
			EnumerateCams();

			// query settings
			if (Settings.CamName.Length > 0) {
				confWebcam.SelectedIndex = confWebcam.FindStringExact(Settings.CamName);
				webCamList_SelectedIndexChanged(null, null);

				confResolution.FocusItem(Settings.CamConfigSize, Settings.CamConfigBPP);
			} else {
				webCamList_SelectedIndexChanged(null, null);
			}

			confFolder.Text = Settings.OutputFolder;
			confFilename.Text = Settings.OutputFilename;
			confScheduleEnabled.Checked = Settings.ScheduleEnabled;
			scheduleEnabled_CheckedChanged(null, null);
			confScheduleMaxRes.Checked = Settings.ScheduleMaxResultionAndQuality;
			confScheduleMon.Checked = Settings.ScheduleMon;
			confScheduleTue.Checked = Settings.ScheduleTue;
			confScheduleWed.Checked = Settings.ScheduleWed;
			confScheduleThu.Checked = Settings.ScheduleThu;
			confScheduleFri.Checked = Settings.ScheduleFri;
			confScheduleSat.Checked = Settings.ScheduleSat;
			confScheduleSun.Checked = Settings.ScheduleSun;
			confScheduleTimeStart.Value = Settings.ScheduleTimeStart;
			confScheduleTimeEnd.Value = Settings.ScheduleTimeEnd;
			confScheduleInterval.SelectedItem = Settings.ScheduleInterval;
			confScheduleDisableOnScreensaver.Checked = Settings.ScheduleDisabledOnScreensaver;
			confAutoStart.Checked = Settings.AutoStart;

			// bind help
			BindHelp(confTakePictureButton, Language.FormatString(Language.LanguageString.MainForm_Help_TakePictureButtonTitle), Language.FormatString(Language.LanguageString.MainForm_Help_TakePictureButtonText));
			BindHelp(confAcceptButton, Language.FormatString(Language.LanguageString.MainForm_Help_AcceptConfigurationButtonTitle), Language.FormatString(Language.LanguageString.MainForm_Help_AcceptConfigurationButtonText));
			BindHelp(confRejectButton, Language.FormatString(Language.LanguageString.MainForm_Help_CancelConfigurationButtonTitle), Language.FormatString(Language.LanguageString.MainForm_Help_CancelConfigurationButtonText));
			BindHelp(confInfoButton, Language.FormatString(Language.LanguageString.MainForm_Help_InformationButtonTitle), Language.FormatString(Language.LanguageString.MainForm_Help_InformationButtonText));
			BindHelp(confWebcamPanel, Language.FormatString(Language.LanguageString.MainForm_Help_PickWebCamTitle), Language.FormatString(Language.LanguageString.MainForm_Help_PickWebCamText));
			BindHelp(confFolderPanel, Language.FormatString(Language.LanguageString.MainForm_Help_FoldernameTitle), Language.FormatString(Language.LanguageString.MainForm_Help_FoldernameText));
			BindHelp(confFilenamePanel, Language.FormatString(Language.LanguageString.MainForm_Help_FilenameTitle), Language.FormatString(Language.LanguageString.MainForm_Help_FilenameText, "webcam " + DateTime.Now.ToString("yyyy-MM-dd HH.mm", CultureInfo.CurrentCulture) + ".jpg"));
			BindHelp(confResolution, Language.FormatString(Language.LanguageString.MainForm_Help_ChangeResolutionTitle), Language.FormatString(Language.LanguageString.MainForm_Help_ChangeResolutionText));
			BindHelp(confSchedulePanel, Language.FormatString(Language.LanguageString.MainForm_Help_ScheduleTitle), Language.FormatString(Language.LanguageString.MainForm_Help_ScheduleText));
			BindHelp(confAutoStart, Language.FormatString(Language.LanguageString.MainForm_Help_AutoStartTitle), Language.FormatString(Language.LanguageString.MainForm_Help_AutoStartText));
			BindHelp(takePictureBackButton, Language.FormatString(Language.LanguageString.MainForm_Help_TakePictureBackButtonTitle), Language.FormatString(Language.LanguageString.MainForm_Help_TakePictureBackButtonText));
			BindHelp(takePictureSaveButton, Language.FormatString(Language.LanguageString.MainForm_Help_SavePictureButtonTitle), Language.FormatString(Language.LanguageString.MainForm_Help_SavePictureButtonText));
			BindHelp(infoBackButton, Language.FormatString(Language.LanguageString.MainForm_Help_InformationButtonBackTitle), Language.FormatString(Language.LanguageString.MainForm_Help_InformationButtonBackText));

			// set texts
			confWebcamLabel.Text = Language.FormatString(Language.LanguageString.MainForm_Configuration_PickWebCam);
			confFoldernameLabel.Text = Language.FormatString(Language.LanguageString.MainForm_Configuration_Foldername);
			confFilenameLabel.Text = Language.FormatString(Language.LanguageString.MainForm_Configuration_Filename); 
			confScheduleEnabled.Text = Language.FormatString(Language.LanguageString.MainForm_Configuration_ScheduleEnabled);
			confScheduleMaxRes.Text = Language.FormatString(Language.LanguageString.MainForm_Configuration_ScheduleMaxRes);
			confScheduleDisableOnScreensaver.Text = Language.FormatString(Language.LanguageString.MainForm_Configuration_ScheduleDisableOnScreensaver);
			confScheduleDaysLabel.Text = Language.FormatString(Language.LanguageString.MainForm_Configuration_Days);
			confScheduleMon.Text = Language.FormatString(Language.LanguageString.MainForm_Configuration_Mon);
			confScheduleTue.Text = Language.FormatString(Language.LanguageString.MainForm_Configuration_Tue);
			confScheduleWed.Text = Language.FormatString(Language.LanguageString.MainForm_Configuration_Wed);
			confScheduleThu.Text = Language.FormatString(Language.LanguageString.MainForm_Configuration_Thu);
			confScheduleFri.Text = Language.FormatString(Language.LanguageString.MainForm_Configuration_Fri);
			confScheduleSat.Text = Language.FormatString(Language.LanguageString.MainForm_Configuration_Sat);
			confScheduleSun.Text = Language.FormatString(Language.LanguageString.MainForm_Configuration_Sun);
			confScheduleBetweenLabel.Text = Language.FormatString(Language.LanguageString.MainForm_Configuration_Between);
			confScheduleBetweenAnd.Text = Language.FormatString(Language.LanguageString.MainForm_Configuration_BetweenAnd);
			confScheduleIntervalLabel.Text = Language.FormatString(Language.LanguageString.MainForm_Configuration_Every);
			confScheduleIntervalHoursLabel.Text = Language.FormatString(Language.LanguageString.MainForm_Configuration_EveryHours);
			confAutoStart.Text = Language.FormatString(Language.LanguageString.MainForm_Configuration_AutoStart);
			infoWebsiteLabel.Text = Language.FormatString(Language.LanguageString.MainForm_Information_VisitWebSite);
			infoCopyright1Label.Text = Language.FormatString(Language.LanguageString.MainForm_Information_About);

			infoCopyright2Label.Text =
				Application.ProductName + " Copyright © 2008 by Fredrik Johansson Robotics AB. Please refer to LICENSE.txt in the installation folder.\r\n\r\n" +
				"This application uses (links) to DirectShowLib, which is licensed under LGPL (Lesser General Public License). Copyright © http://directshownet.sourceforge.net. Please refer to LGPL.txt in the installation folder.";

			// load displacement filters
			takePictureDisplacement.Items.Add(new PictureAndThumbnailPair(string.Empty, string.Empty));
			string filterPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Filters");
			if (Directory.Exists(filterPath)) {
				string[] filterThumbs = Directory.GetFiles(filterPath, "*-thumb.*");
				for (int x = 0; x < filterThumbs.Length; x++) {
					string filterFile = Path.Combine(filterPath, Path.GetFileNameWithoutExtension(filterThumbs[x]).Replace("-thumb", string.Empty) + ".png");
					if (File.Exists(filterFile)) {
						takePictureDisplacement.Items.Add(new PictureAndThumbnailPair(filterFile, filterThumbs[x]));
					}
				}
			}
		}

		private void BindHelp(Control ctl, string title, string text) {
			ctl.GotFocus -= (EventHandler)delegate(object sender, EventArgs e) {
				helpPanel.AnimateIn(title, text);
			};
			ctl.LostFocus -= (EventHandler)delegate(object sender, EventArgs e) {
				helpPanel.AnimateOut();
			};
			ctl.MouseEnter -= (EventHandler)delegate(object sender, EventArgs e) {
				helpPanel.AnimateIn(title, text);
			};
			ctl.MouseLeave -= (EventHandler)delegate(object sender, EventArgs e) {
				helpPanel.AnimateOut();
			};
	
			ctl.GotFocus += (EventHandler)delegate(object sender, EventArgs e) {
				helpPanel.AnimateIn(title, text);
			};
			ctl.LostFocus += (EventHandler)delegate(object sender, EventArgs e) {
				helpPanel.AnimateOut();
			};
			ctl.MouseEnter += (EventHandler)delegate(object sender, EventArgs e) {
				helpPanel.AnimateIn(title, text);
			};
			ctl.MouseLeave += (EventHandler)delegate(object sender, EventArgs e) {
				helpPanel.AnimateOut();
			};
		}

		private void EnumerateCams() {
			int currentTick = System.Environment.TickCount;
			if ((currentTick - m_lastEnumTick > 5000) || (m_lastEnumTick == 0)) {
				m_lastEnumTick = currentTick;
				confWebcam.Items.Clear();
				confWebcam.Items.AddRange(WebcamManager.Enumerate());
				confWebcam.Items.Add(Language.FormatString(Language.LanguageString.MainForm_Configuration_RefreshWebcamList));
				confWebcam.SelectedIndex = 0;
			}
		}

		#region Animation
		private void animatePanelIn_Click(object sender, EventArgs ev) {
			// animate in :)
			if ((confWebcam.SelectedItem is Webcam) || (sender != confTakePictureButton)) {
				if (!m_animator.IsAnimating) {
					Control panelIn = (sender == confTakePictureButton ? takePicturePanel : infoPanel);
					takePictureSaveButton.Enabled = false;
					panelIn.Location = new Point(ClientRectangle.Width, 0);
					panelIn.Visible = true;
					panelIn.Focus();
					if (panelIn == takePicturePanel) {
						takePictureDisplacement.Visible = false;
						if (confWebcam.SelectedItem is WebcamWithPreview) {
							takePictureDisplacement.Visible = true;
							takePictureDisplacement.ResetPosition();
						}
					}

					m_animator.Run((EventHandler<AnimationEventArgs>)delegate(object sendr, AnimationEventArgs e) {
						Invoke((MethodInvoker)delegate() {
							int newLeft = (int)(Math.Min(e.PercentRemaining * e.PercentRemaining * e.PercentRemaining, 1) * ClientRectangle.Width);
							panelIn.Left = newLeft;
							confPanel.Left = -(ClientRectangle.Width - newLeft);
							if (e.IsLastCall) {
								if (panelIn == takePicturePanel) {
									Text = Application.ProductName + " - " + Language.FormatString(Language.LanguageString.MainForm_TakePicture_Title);
									TakePicture(null, null);
								} else {
									Text = Application.ProductName + " - " + Language.FormatString(Language.LanguageString.MainForm_Information_Title);
								}
								confPanel.Visible = false;
							}
						});
					});
				}
			}
		}

		private void animatePanelOut_Click(object sender, EventArgs ev) {
			if (!m_animator.IsAnimating) {
				Control panelOut = (sender == takePictureBackButton ? takePicturePanel : infoPanel);
				if (m_takePictureWithPreviewAsync != null) {
					WebcamWithPreview camp = confWebcam.SelectedItem as WebcamWithPreview;
					if (camp != null) {
						camp.TakePictureEnd();
					}
				}

				confPanel.Location = new Point(-ClientRectangle.Width, 0);
				confPanel.Visible = true;
				confPanel.Focus();

				m_animator.Run((EventHandler<AnimationEventArgs>)delegate(object sendr, AnimationEventArgs e) {
					Invoke((MethodInvoker)delegate() {
						int newLeft = (int)(Math.Min(e.PercentRemaining * e.PercentRemaining * e.PercentRemaining, 1) * -ClientRectangle.Width);
						confPanel.Left = newLeft;
						panelOut.Left = newLeft + ClientRectangle.Width;
						if (e.IsLastCall) {
							Text = Application.ProductName + " - " + Language.FormatString(Language.LanguageString.MainForm_Configuration_Title);
							panelOut.Visible = false;
						}
					});
				});
			}
		}
		#endregion

		private void mainForm_FormClosing(object sender, FormClosingEventArgs e) {
			if (m_takePictureWithPreviewAsync != null) {
				WebcamWithPreview camp = confWebcam.SelectedItem as WebcamWithPreview;
				if (camp != null) {
					camp.TakePictureEnd();
				}
			}
		}

		private void mainForm_FormClosed(object sender, FormClosedEventArgs e) {
			m_animator.Abort();
			s_instance = null;
		}

		#region Settings
		private void cancelButton_Click(object sender, EventArgs e) {
			Close();
		}

		private void saveButton_Click(object sender, EventArgs e) {
			// save settings
			Settings.CamName = confWebcam.SelectedItem.ToString();
			Settings.CamConfigSize = Size.Empty;
			Settings.CamConfigBPP = 0;
			Settings.CamConfigMediaSubType = Guid.Empty;
			Settings.CamConfigSizeMax = Size.Empty;
			Settings.CamConfigBPPMax = 0;
			Settings.CamConfigMediaSubTypeMax = Guid.Empty;

			if (confWebcam.SelectedItem is Webcam) {
				Settings.CamConfigSize = confResolution.Resolution.Size;
				Settings.CamConfigBPP = confResolution.Resolution.BPP;
				Settings.CamConfigMediaSubType = confResolution.Resolution.MediaSubtype;

				Settings.CamConfigSizeMax = confResolution.MaxResolution.Size;
				Settings.CamConfigBPPMax = confResolution.MaxResolution.BPP;
				Settings.CamConfigMediaSubTypeMax = confResolution.MaxResolution.MediaSubtype;
			}
			Settings.OutputFolder = confFolder.Text;
			Settings.OutputFilename = confFilename.Text;
			Settings.ScheduleEnabled = confScheduleEnabled.Checked;
			Settings.ScheduleMaxResultionAndQuality = confScheduleMaxRes.Checked;
			Settings.ScheduleMon = confScheduleMon.Checked;
			Settings.ScheduleTue = confScheduleTue.Checked;
			Settings.ScheduleWed = confScheduleWed.Checked;
			Settings.ScheduleThu = confScheduleThu.Checked;
			Settings.ScheduleFri = confScheduleFri.Checked;
			Settings.ScheduleSat = confScheduleSat.Checked;
			Settings.ScheduleSun = confScheduleSun.Checked;
			if (confScheduleTimeStart.Value > confScheduleTimeEnd.Value) {
				Settings.ScheduleTimeStart = confScheduleTimeEnd.Value;
				Settings.ScheduleTimeEnd = confScheduleTimeStart.Value;
			} else {
				Settings.ScheduleTimeStart = confScheduleTimeStart.Value;
				Settings.ScheduleTimeEnd = confScheduleTimeEnd.Value;
			}
			scheduleInterval_Leave(null, null);
			Settings.ScheduleInterval = (int)confScheduleInterval.SelectedItem;
			Settings.ScheduleDisabledOnScreensaver = confScheduleDisableOnScreensaver.Checked;
			Settings.AutoStart = confAutoStart.Checked;

			Close();
		}
	
		private void destinationFolderBrowse_Click(object sender, EventArgs e) {
			if (Directory.Exists(confFolder.Text)) {
				confFolderDialog.SelectedPath = confFolder.Text;
			} else {
				confFolderDialog.SelectedPath = string.Empty;
			}
			if (confFolderDialog.ShowDialog(this) == DialogResult.OK) {
				confFolder.Text = confFolderDialog.SelectedPath;
				if (confFolder.Text.Length > 0) {
					confFolder.Select(confFolder.Text.Length, 0);
				}
			}
		}

		private void webCamList_SelectedIndexChanged(object sender, EventArgs e) {
			confResolution.Enabled = false;
			confTakePictureButton.Enabled = false;
			if (confWebcam.SelectedItem is string) {
				EnumerateCams();
			} else if (confWebcam.SelectedItem is Webcam) {
				confResolution.SetResolutions(((Webcam)confWebcam.SelectedItem).QueryFormats());
				confTakePictureButton.Enabled = true;
			} 			
		}

		private void scheduleEnabled_CheckedChanged(object sender, EventArgs e) {
			foreach (Control ctl in confSchedulePanel.Controls) {
				if ((ctl.Name.StartsWith("schedule", StringComparison.OrdinalIgnoreCase)) && (ctl != confScheduleEnabled)) {
					ctl.Enabled = confScheduleEnabled.Checked;
				}
			}
		}
		#endregion

		#region Take Picture
		private void savePictureButton_Click(object sender, EventArgs e) {
			takePictureSaveDialog.Title = Language.FormatString(Language.LanguageString.MainForm_TakePicture_SaveDialogTitle);
			takePictureSaveDialog.FileName = Path.Combine(confFolder.Text, Common.GenerateFilename(confFilename.Text));
			if (takePictureSaveDialog.ShowDialog(this) == DialogResult.OK) {
				Common.SaveJPEG(takePicturePicture.DisplayBitmap, takePictureSaveDialog.FileName);
			}
		}

		// take picture according to settings right now
		private void TakePicture(object sender, EventArgs e) {
			Webcam cam = confWebcam.SelectedItem as Webcam;
			WebcamWithPreview camp = cam as WebcamWithPreview;

			if ((m_takePictureWithPreviewAsync != null) && (sender == takePicturePicture) && (camp != null)) {
				camp.TakePictureEnd();
			}

			if ((m_takePictureAsync == null) && (m_takePictureWithPreviewAsync == null) && (cam != null)) {
				takePictureSaveButton.Enabled = false;
				cam.Config(confResolution.Resolution);

				if (camp != null) {
					takePicturePicture.Text = string.Empty;
					BindHelp(takePicturePicture, Language.FormatString(Language.LanguageString.MainForm_Help_VideoStreamFreezeTitle), Language.FormatString(Language.LanguageString.MainForm_Help_VideoStreamFreezeText));

					camp.DisplacementMap = null;
					if ((takePictureDisplacement.CurrentPicture != null) && (takePictureDisplacement.CurrentPicture.Picture.Length > 0)) {
						camp.DisplacementMap = new Bitmap(takePictureDisplacement.CurrentPicture.Picture);
					}
					takePicturePicture.DisplayBitmap = new Bitmap(confResolution.Resolution.Size.Width, confResolution.Resolution.Size.Height, confResolution.Resolution.PixelFormat);

					m_takePictureWithPreviewAsync = new TakePictureWithPreview(camp.TakePicture);
					m_takePictureWithPreviewAsync.BeginInvoke(takePicturePicture, (AsyncCallback)delegate(IAsyncResult result) {
						try {
							m_takePictureWithPreviewAsync.EndInvoke(result);
							confAcceptButton.Invoke((MethodInvoker)delegate() {
								takePictureSaveButton.Enabled = true;
							});
						} catch (Exception ex) {
							try {
								confAcceptButton.Invoke((MethodInvoker)delegate() {
									takePicturePicture.Text = Language.FormatString(Language.LanguageString.MainForm_FailedToTakePicture);
								});
							} catch (Exception) { }
						} finally {
							m_takePictureWithPreviewAsync = null;
						}
					}, null);

				} else {
					BindHelp(takePicturePicture, Language.FormatString(Language.LanguageString.MainForm_Help_PictureTitle), Language.FormatString(Language.LanguageString.MainForm_Help_PictureText));

					takePicturePicture.Text = Language.FormatString(Language.LanguageString.MainForm_TakePicture_TakingPicture);
					m_takePictureAsync = new TakePicture(cam.TakePicture);
					m_takePictureAsync.BeginInvoke((AsyncCallback)delegate(IAsyncResult result) {
						try {
							takePicturePicture.DisplayBitmap = m_takePictureAsync.EndInvoke(result);
							confAcceptButton.Invoke((MethodInvoker)delegate() {
								takePictureSaveButton.Enabled = true;
								takePicturePicture.Text = string.Empty;
							});
						} catch (Exception ex) {
							try {
								confAcceptButton.Invoke((MethodInvoker)delegate() {
									takePicturePicture.Text = Language.FormatString(Language.LanguageString.MainForm_FailedToTakePicture);
								});
							} catch (Exception) { }
						} finally {
							m_takePictureAsync = null;
						}
					}, null);
				}
			}
		}

		private void displacementSelector_PictureClicked(PictureAndThumbnailPair e) {
			WebcamWithPreview camp = confWebcam.SelectedItem as WebcamWithPreview;
			if ((m_takePictureWithPreviewAsync != null) && (camp != null)) {
				camp.DisplacementMap = e.Picture.Length == 0 ? null : new Bitmap(e.Picture);
			}
		}
		#endregion

		private void scheduleInterval_Leave(object sender, EventArgs e) {
			int selectedInt;
			if (confScheduleInterval.SelectedItem == null) {
				if (int.TryParse(confScheduleInterval.Text, out selectedInt)) {
					for (int x = 0; x < confScheduleInterval.Items.Count; x++) {
						if ((int)confScheduleInterval.Items[x] == selectedInt) {
							confScheduleInterval.SelectedIndex = x;
							return;
						}
					}
				}
				confScheduleInterval.SelectedIndex = confScheduleInterval.Items.Count - 3;
			}
			 
		}

		private void websiteButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			System.Diagnostics.Process.Start("http://www.johanssonrobotics.com");
		}
	}
}
