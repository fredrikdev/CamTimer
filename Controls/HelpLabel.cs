using System;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace CamTimer.Controls {
	class HelpLabel : Control, IDisposable {
		private double m_opacity = 0.5;	// 0 Totally Transparent, 1 Totally Opaque
		private string m_title = string.Empty;
		private Font m_boldFont;
		private ManualResetEvent m_animationContinue = new ManualResetEvent(false);
		private Thread m_animationThread;
		private double m_animateTo;
		private const int s_animationDurationMSec = 250;

		internal HelpLabel() {
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
			m_boldFont = new Font(Font, FontStyle.Bold);
			m_animationThread = new Thread((ThreadStart)Animator);
			m_animationThread.Start();
		}

		public double Opacity {
			get {
				return m_opacity;
			}
			set {
				if ((m_opacity != value) && (value >= 0) && (value <= 1)) {
					m_opacity = value;
					Refresh();
				}
			}
		}

		internal void AnimateIn(string title, string text) {
			Title = title;
			Text = text;
			m_animateTo = 1;
			m_animationContinue.Set();
		}

		internal void AnimateOut() {
			m_animateTo = 0;
			m_animationContinue.Set();
		}

		internal void Animator() {
			try {
				while (true) {
					m_animationContinue.WaitOne();
					m_animationContinue.Reset();
					double animateFrom = Opacity;
					double animateTo = m_animateTo;
					if (m_animateTo == 1) {
						Invoke((MethodInvoker)delegate() {
							Visible = true;
						});
					}
					int startMSec = System.Environment.TickCount;

					while (true) {
						int elapsedMSec = System.Environment.TickCount - startMSec;
						double percentComplete = (Math.Min(elapsedMSec, s_animationDurationMSec) / (double)s_animationDurationMSec);

						try {
							Invoke((MethodInvoker)delegate() {
								Opacity = animateFrom + (animateTo - animateFrom) * percentComplete;
							});
						} catch (Exception) { }
						if (elapsedMSec >= s_animationDurationMSec) {
							break;
						}

						Thread.Sleep((int)(1000 / 33d));	// 33 fps
					}

					if (m_animateTo == 0) {
						Invoke((MethodInvoker)delegate() {
							Visible = false;
						});
					}
				}
			} catch (Exception) {
				//System.Diagnostics.Debug.WriteLine("Animator ended!");
			}
		}

		public string Title {
			get {
				return m_title;
			}
			set {
				if (m_title != value) {
					m_title = value;
					Refresh();
				}
			}
		}

		public override string Text {
			get {
				return base.Text;
			}
			set {
				if (base.Text != value) {
					base.Text = value;
					Refresh();
				}
			}
		}

		protected override void OnFontChanged(EventArgs e) {
			base.OnFontChanged(e);
			m_boldFont = new Font(Font, FontStyle.Bold);
		}

		protected override void OnPaint(PaintEventArgs e) {
			double transparency = 1-m_opacity;
			try {
				Color pbc = Parent.BackColor;

				// BackColor to Parent.BackColor (add or remove from individual components until they reach Parent.BackColor)
				Color bc = Color.FromArgb(BackColor.R + (int)(((int)pbc.R - (int)BackColor.R) * transparency), BackColor.G + (int)(((int)pbc.G - (int)BackColor.G) * transparency), BackColor.B + (int)(((int)pbc.B - (int)BackColor.B) * transparency));

				// ForeColor to Parent.BackColor
				Color fc = Color.FromArgb(ForeColor.R + (int)(((int)pbc.R - (int)ForeColor.R) * transparency), ForeColor.G + (int)(((int)pbc.G - (int)ForeColor.G) * transparency), ForeColor.B + (int)(((int)pbc.B - (int)ForeColor.B) * transparency));

				e.Graphics.Clear(bc);
				TextRenderer.DrawText(e.Graphics, Title.Replace("&", "&&"), m_boldFont, Rectangle.FromLTRB(17, 11, Width - 17, 11 + 100), fc, TextFormatFlags.EndEllipsis | TextFormatFlags.SingleLine);
				TextRenderer.DrawText(e.Graphics, Text.Replace("&", "&&"), Font, Rectangle.FromLTRB(17, 37, Width - 17, 37 + 100), fc, TextFormatFlags.EndEllipsis | TextFormatFlags.WordBreak);
			} catch (Exception) { }
		}

		protected override void OnPaintBackground(PaintEventArgs pevent) {
			//base.OnPaintBackground(pevent);
		}

		protected override void Dispose(bool disposing) {
			if (disposing) {
				try {
					m_animationThread.Abort();
					m_animationContinue.Close();
					m_boldFont.Dispose();
				} catch (Exception) { }
			}
			base.Dispose(disposing);
		}
	}
}
