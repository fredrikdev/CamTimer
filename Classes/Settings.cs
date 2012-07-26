using System.Collections.Generic;
using System;
using System.Drawing;
using System.IO;
using System.Globalization;
using System.Windows.Forms;

namespace CamTimer {
	static class Settings {
		private static string s_keyName = @"Software\Fredrik Johansson Robotics AB\" + Application.ProductName;
		private static Dictionary<string, object> s_valueCache = new Dictionary<string, object>();

		private static bool QuerySetting(string name, bool defaultValue) {
			if (s_valueCache.ContainsKey(name)) {
				return (((int)s_valueCache[name]) == 1);
			} else {
				return QuerySetting(name, defaultValue ? 1 : 0) == 1;
			}
		}

		private static int QuerySetting(string name, int defaultValue) {
			if (s_valueCache.ContainsKey(name)) {
				return (int)s_valueCache[name];
			} else {
				int value = (int)Microsoft.Win32.Registry.CurrentUser.CreateSubKey(s_keyName).GetValue(name, defaultValue);
				s_valueCache.Add(name, value);
				return value;
			}
		}

		private static string QuerySetting(string name, string defaultValue) {
			if (s_valueCache.ContainsKey(name)) {
				string temp = (string)s_valueCache[name];
				if (temp.Trim().Length == 0) {
					temp = defaultValue;
				}
				return temp;
			} else {
				string temp = (string)Microsoft.Win32.Registry.CurrentUser.CreateSubKey(s_keyName).GetValue(name, defaultValue);
				if (temp.Trim().Length == 0) {
					temp = defaultValue;
				}
				s_valueCache.Add(name, temp);
				return temp;
			}
		}

		private static void SaveSetting(string name, bool value) {
			SaveSetting(name, value ? 1 : 0);
			if (s_valueCache.ContainsKey(name)) {
				s_valueCache[name] = value ? 1 : 0;
			} else {
				s_valueCache.Add(name, value ? 1 : 0);
			}
		}

		private static void SaveSetting(string name, int value) {
			Microsoft.Win32.Registry.CurrentUser.CreateSubKey(s_keyName).SetValue(name, value);
			if (s_valueCache.ContainsKey(name)) {
				s_valueCache[name] = value;
			} else {
				s_valueCache.Add(name, value);
			}
		}

		private static void SaveSetting(string name, string value) {
			Microsoft.Win32.Registry.CurrentUser.CreateSubKey(s_keyName).SetValue(name, value);
			if (s_valueCache.ContainsKey(name)) {
				s_valueCache[name] = value;
			} else {
				s_valueCache.Add(name, value);
			}
		}

		// tweak/by installer
		internal static int JPEGQuality {
			get {
				return QuerySetting("JPEGQuality", 95);
			}
		}

		// tweak/by installer
		internal static string Language {
			get {
				return QuerySetting("Language", "en-US").Replace("_","-");
			}
		}

		// tweak/by installer
		internal static bool ShowFPS {
			get {
				return QuerySetting("ShowFPS", false);
			}
		}

		internal static string CamName {
			get {
				return QuerySetting("CamName", string.Empty);
			}
			set {
				SaveSetting("CamName", value);
			}
		}

		internal static Size CamConfigSize {
			get {
				string[] result = QuerySetting("CamConfigSize", "0x0").Split('x');
				return new Size(int.Parse(result[0], CultureInfo.InvariantCulture), int.Parse(result[1], CultureInfo.InvariantCulture));
			}
			set {
				SaveSetting("CamConfigSize", value.Width + "x" + value.Height);
			}
		}

		internal static short CamConfigBPP {
			get {
				return (short)QuerySetting("CamConfigBPP", 0);
			}
			set {
				SaveSetting("CamConfigBPP", value);
			}
		}

		internal static Guid CamConfigMediaSubType {
			get {
				return new Guid(QuerySetting("CamConfigMediaSubType", Guid.Empty.ToString("D")));
			}
			set {
				SaveSetting("CamConfigMediaSubType", value.ToString("D"));
			}
		}

		internal static string OutputFolder {
			get {
				return QuerySetting("OutputFolder", System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
			}
			set {
				SaveSetting("OutputFolder", value);
			}
		}

		internal static string OutputFilename {
			get {
				return QuerySetting("OutputFilename", "webcam <yyyy-MM-dd HH.mm>.jpg");
			}
			set {
				SaveSetting("OutputFilename", value);
			}
		}

		internal static DateTime FirstRun {
			get {
				return DateTime.ParseExact(QuerySetting("FirstRun", DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
			}
			set {
				SaveSetting("FirstRun", value.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
			}
		}

		internal static bool ScheduleEnabled {
			get {
				return QuerySetting("ScheduleEnabled", false);
			}
			set {
				SaveSetting("ScheduleEnabled", value);
			}
		}

		internal static bool ScheduleMaxResultionAndQuality {
			get {
				return QuerySetting("ScheduleMaxResolutionAndQuality", true);
			}
			set {
				SaveSetting("ScheduleMaxResolutionAndQuality", value);
			}
		}

		internal static Size CamConfigSizeMax {
			get {
				string[] result = QuerySetting("CamConfigSizeMax", "0x0").Split('x');
				return new Size(int.Parse(result[0], CultureInfo.InvariantCulture), int.Parse(result[1], CultureInfo.InvariantCulture));
			}
			set {
				SaveSetting("CamConfigSizeMax", value.Width + "x" + value.Height);
			}
		}

		internal static short CamConfigBPPMax {
			get {
				return (short)QuerySetting("CamConfigBPPMax", 0);
			}
			set {
				SaveSetting("CamConfigBPPMax", value);
			}
		}

		internal static Guid CamConfigMediaSubTypeMax {
			get {
				return new Guid(QuerySetting("CamConfigMediaSubTypeMax", Guid.Empty.ToString("D")));
			}
			set {
				SaveSetting("CamConfigMediaSubTypeMax", value.ToString("D"));
			}
		}

		internal static bool ScheduleMon {
			get {
				return QuerySetting("ScheduleMon", true);
			}
			set {
				SaveSetting("ScheduleMon", value);
			}
		}

		internal static bool ScheduleTue {
			get {
				return QuerySetting("ScheduleTue", true);
			}
			set {
				SaveSetting("ScheduleTue", value);
			}
		}

		internal static bool ScheduleWed {
			get {
				return QuerySetting("ScheduleWed", true);
			}
			set {
				SaveSetting("ScheduleWed", value);
			}
		}

		internal static bool ScheduleThu {
			get {
				return QuerySetting("ScheduleThu", true);
			}
			set {
				SaveSetting("ScheduleThu", value);
			}
		}

		internal static bool ScheduleFri {
			get {
				return QuerySetting("ScheduleFri", true);
			}
			set {
				SaveSetting("ScheduleFri", value);
			}
		}

		internal static bool ScheduleSat {
			get {
				return QuerySetting("ScheduleSat", true);
			}
			set {
				SaveSetting("ScheduleSat", value);
			}
		}

		internal static bool ScheduleSun {
			get {
				return QuerySetting("ScheduleSun", true);
			}
			set {
				SaveSetting("ScheduleSun", value);
			}
		}

		internal static DateTime ScheduleTimeStart {
			get {
				return DateTime.ParseExact(QuerySetting("ScheduleTimeStart", "08:00:00"), "HH:mm:ss", CultureInfo.InvariantCulture);
			}
			set {
				SaveSetting("ScheduleTimeStart", value.ToString("HH:mm:ss", CultureInfo.InvariantCulture));
			}
		}

		internal static DateTime ScheduleTimeEnd {
			get {
				return DateTime.ParseExact(QuerySetting("ScheduleTimeEnd", "18:00:00"), "HH:mm:ss", CultureInfo.InvariantCulture);
			}
			set {
				SaveSetting("ScheduleTimeEnd", value.ToString("HH:mm:ss", CultureInfo.InvariantCulture));
			}
		}

		internal static DateTime ScheduleLastRun {
			get {
				return DateTime.ParseExact(QuerySetting("ScheduleLastRun", DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
			}
			set {
				SaveSetting("ScheduleLastRun", value.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
			}
		}

		internal static int ScheduleInterval {
			get {
				return QuerySetting("ScheduleInterval", 3);
			}
			set {
				SaveSetting("ScheduleInterval", value);
			}
		}

		internal static bool ScheduleDisabledOnScreensaver {
			get {
				return QuerySetting("ScheduleDisabledOnScreensaver", true);
			}
			set {
				SaveSetting("ScheduleDisabledOnScreensaver", value);
			}
		}

		internal static bool AutoStart {
			get {
				string assemblyNameAndPath = System.IO.Path.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().Location);
				string startupShortcut = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Startup), Path.GetFileNameWithoutExtension(assemblyNameAndPath) + ".lnk");
				return File.Exists(startupShortcut);
			}
			set {
				string assemblyNameAndPath = System.IO.Path.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().Location);
				string startupShortcut = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Startup), Path.GetFileNameWithoutExtension(assemblyNameAndPath) + ".lnk");
				if (value) {
					// autostart
					if (!File.Exists(startupShortcut)) {
						IWshRuntimeLibrary.WshShellClass shell = new IWshRuntimeLibrary.WshShellClass();
						IWshRuntimeLibrary.IWshShortcut_Class shortcut = (IWshRuntimeLibrary.IWshShortcut_Class)shell.CreateShortcut(startupShortcut);
						shortcut.TargetPath = assemblyNameAndPath;
						shortcut.Arguments = "/autostart";
						shortcut.Save();
					}
				} else {
					// don't autostart
					if (File.Exists(startupShortcut)) {
						File.Delete(startupShortcut);
					}
				}
			}
		}
	}
}
