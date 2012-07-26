using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace CamTimer {
	static class Common {
		internal static string GenerateFilename(string pattern) {
			DateTime now = DateTime.Now;
			System.Text.StringBuilder result = new System.Text.StringBuilder();
			int expressionStart = -1;
			for (int x = 0; x < pattern.Length; x++) {
				switch (pattern.Substring(x, 1)) {
					case "<":
						expressionStart = x;
						break;
					case ">":
						if (expressionStart != -1) {
							result.Append(
								now.ToString(pattern.Substring(expressionStart + 1, x - expressionStart - 1), CultureInfo.CurrentCulture)
							);
							expressionStart = -1;
						}
						break;
					default:
						if (expressionStart == -1) {
							result.Append(pattern.Substring(x, 1));
						}
						break;
				}
			}

			// remove invalid path chars
			char[] invalidChars = Path.GetInvalidPathChars();
			foreach (char c in invalidChars) {
				result = result.Replace(c, '-');
			}
			char[] moreInvalidChars = new char[] { '\\', '/', ':', '*', '?', '"', '<', '>', '|' };
			foreach (char c in moreInvalidChars) {
				result = result.Replace(c, '-');
			}

			string temp = result.ToString().Trim();
			if (temp.Length == 0) {
				temp = DateTime.Now.ToString("webcam <yyyy-MM-dd HH.mm>.jpg", CultureInfo.InvariantCulture);
			}

			return temp;
		}

		internal static bool SaveJPEG(Image image, string jpegFileName) {
			long jpegQuality = Settings.JPEGQuality;
	
			if (image != null) {
				EncoderParameters args = new EncoderParameters();
				args.Param = new EncoderParameter[] { new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, jpegQuality) };

				ImageCodecInfo[] arrCodecs = ImageCodecInfo.GetImageEncoders();
				foreach (ImageCodecInfo i in arrCodecs) {
					if (i.FormatID.Equals(ImageFormat.Jpeg.Guid)) {
						image.Save(jpegFileName, i, args);
						return true;
					}
				}
			}
			return false;
		}
	}
}
