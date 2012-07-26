using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace CamTimer {
	class Language {
		private static Dictionary<string, string> m_strings = null;

		static Language() {
			m_strings = new Dictionary<string,string>();
			#if (DEBUG)
			string languageFilename = @"..\..\Languages\" + Settings.Language + ".txt";
			#else
			string languageFilename = Path.Combine(Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Languages"), Settings.Language + ".txt");
			#endif
			
			// load language file
			StreamReader reader = new StreamReader(languageFilename);
			while (!reader.EndOfStream) {
				string line = reader.ReadLine();
				int x = line.IndexOf("=");
				if (x > -1) {
					if ((line.Substring(0, x) == LanguageString.MainForm_Information_About.ToString()) && (line.IndexOf("%ProductManufacturer%") < 0)) {
						MessageBox.Show("Language file " + Path.GetFileName(languageFilename) + "/" + LanguageString.MainForm_Information_About.ToString() + " must contain %ProductManufacturer%", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
						Application.Exit();
					}
					m_strings.Add(line.Substring(0, x), line.Substring(x + 1).Replace("%ProductName%", Application.ProductName).Replace("%ProductManufacturer%", Application.CompanyName));
				}
			}

			// validate language file
			LanguageString[] items = (LanguageString[])Enum.GetValues(typeof(LanguageString));
			for (int x = 0; x < items.Length; x++) {
				if (!m_strings.ContainsKey(items[x].ToString())) {
					MessageBox.Show("Language file " + Path.GetFileName(languageFilename) + " does not contain a definition for " + items[x].ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					Application.Exit();
				}
			}
			
		}

		internal static string FormatString(LanguageString stringName, params object[] value) {
			return string.Format(m_strings[stringName.ToString()], value);
		}

		internal enum LanguageString {
			App_AlreadyRunning,
			MainForm_Configuration_AutoStart,
			MainForm_Configuration_Between,
			MainForm_Configuration_BetweenAnd,
			MainForm_Configuration_Color15bit,
			MainForm_Configuration_Color16bit,
			MainForm_Configuration_Color24bit,
			MainForm_Configuration_Color32bit,
			MainForm_Configuration_Color8bit,
			MainForm_Configuration_ColorNbits,
			MainForm_Configuration_Days,
			MainForm_Configuration_Every,
			MainForm_Configuration_EveryHours,
			MainForm_Configuration_Filename,
			MainForm_Configuration_Foldername,
			MainForm_Configuration_Fri,
			MainForm_Configuration_Mon,
			MainForm_Configuration_PickWebCam,
			MainForm_Configuration_RefreshWebcamList,
			MainForm_Configuration_Resolution,
			MainForm_Configuration_ResolutionDisabled,
			MainForm_Configuration_Sat,
			MainForm_Configuration_ScheduleDisableOnScreensaver,
			MainForm_Configuration_ScheduleEnabled,
			MainForm_Configuration_ScheduleMaxRes,
			MainForm_Configuration_Sun,
			MainForm_Configuration_Thu,
			MainForm_Configuration_Title,
			MainForm_Configuration_Tue,
			MainForm_Configuration_Wed,
			MainForm_FailedToTakePicture,
			MainForm_Help_AcceptConfigurationButtonText,
			MainForm_Help_AcceptConfigurationButtonTitle,
			MainForm_Help_AutoStartText,
			MainForm_Help_AutoStartTitle,
			MainForm_Help_CancelConfigurationButtonText,
			MainForm_Help_CancelConfigurationButtonTitle,
			MainForm_Help_ChangeResolutionText,
			MainForm_Help_ChangeResolutionTitle,
			MainForm_Help_FilenameText,
			MainForm_Help_FilenameTitle,
			MainForm_Help_FoldernameText,
			MainForm_Help_FoldernameTitle,
			MainForm_Help_InformationButtonBackText,
			MainForm_Help_InformationButtonBackTitle,
			MainForm_Help_InformationButtonText,
			MainForm_Help_InformationButtonTitle,
			MainForm_Help_PickWebCamText,
			MainForm_Help_PickWebCamTitle,
			MainForm_Help_PictureText,
			MainForm_Help_PictureTitle,
			MainForm_Help_SavePictureButtonText,
			MainForm_Help_SavePictureButtonTitle,
			MainForm_Help_ScheduleText,
			MainForm_Help_ScheduleTitle,
			MainForm_Help_TakePictureBackButtonText,
			MainForm_Help_TakePictureBackButtonTitle,
			MainForm_Help_TakePictureButtonText,
			MainForm_Help_TakePictureButtonTitle,
			MainForm_Help_VideoStreamFreezeText,
			MainForm_Help_VideoStreamFreezeTitle,
			MainForm_Information_Title,
			MainForm_Information_VisitWebSite,
			MainForm_TakePicture_NoDisplacementFilter,
			MainForm_TakePicture_SaveDialogTitle,
			MainForm_TakePicture_TakingPicture,
			MainForm_TakePicture_Title,
			NotifyForm_Configuration,
			NotifyForm_FailedToSave,
			NotifyForm_FailedToTakePicture,
			NotifyForm_HideNow,
			NotifyForm_TakePictureAgain,
			NotifyMenu_Exit,
			NotifyMenu_FirstRunBalloonText,
			NotifyMenu_FirstRunBalloonTitle,
			NotifyMenu_TakePicture,
			MainForm_Information_About
		}
	}
}
