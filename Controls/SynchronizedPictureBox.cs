using System;
using System.Windows.Forms;
using System.Drawing;

namespace CamTimer.Controls {
	class SynchronizedPictureBox : Control {
		private const int s_padding = 1;
		private Bitmap m_bitmap = null;
		private object m_synchronizationObject = new object();
		private string m_text = string.Empty;
		private int m_frameCountStartTickCount = 0;
		private int m_framesRendered = 0;
		private string m_lastFPS = string.Empty;

		internal SynchronizedPictureBox() {
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
			Cursor = Cursors.Hand;
		}

		public Bitmap DisplayBitmap {
			get {
				return m_bitmap;
			}
			set {
				m_bitmap = value;
				Refresh();
			}
		}

		public override void Refresh() {
			if (InvokeRequired) {
				Invoke((MethodInvoker)delegate() {
					base.Refresh();
				});
			} else {
				base.Refresh();
			}
		}

		internal object SynchronizationObject {
			get {
				return m_synchronizationObject;
			}
		}

		public override string Text {
			get {
				return m_text ?? string.Empty;
			}
			set {
				m_text = value ?? string.Empty;
				Refresh();
			}
		}

		protected override void OnKeyDown(KeyEventArgs e) {
			if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Space) || (e.KeyCode == Keys.Return)) {
				OnClick(new EventArgs());
			}
			base.OnKeyDown(e);
		}

		protected override void OnPaint(PaintEventArgs e) {
			Graphics g = e.Graphics;

			g.Clear(SystemColors.ControlDark);
			if (m_bitmap == null) {
				g.FillRectangle(Brushes.White, s_padding, s_padding, ClientSize.Width - s_padding * 2, ClientSize.Height - s_padding * 2);
			} else {
				lock (SynchronizationObject) {
					double bw = m_bitmap.Width, bh = m_bitmap.Height;
					int cw = ClientSize.Width, ch = ClientSize.Height;
					double scale = Math.Min((cw - s_padding*2) / bw, (ch-s_padding*2) / bh);
					int newWidth = (int)Math.Ceiling(bw * scale), newHeight = (int)(bh * scale);
					g.DrawImage(m_bitmap, (cw >> 1) - (newWidth >> 1), (ch >> 1) - (newHeight >> 1), newWidth, newHeight);
				}
			}
			if (m_text.Length > 0) {
				const int textPadding = 4;

				Size size = TextRenderer.MeasureText(g, m_text, Font);
				Rectangle rect = new Rectangle(ClientSize.Width / 2 - size.Width / 2 - textPadding, ClientSize.Height - size.Height * 2 - textPadding, size.Width + textPadding*2, size.Height + textPadding*2);
				g.FillRectangle(SystemBrushes.Window, rect);
				TextRenderer.DrawText(g, m_text, Font, rect, ForeColor, SystemColors.Window, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
			}
			if ((Settings.ShowFPS) && (Parent.GetType() != typeof(NotifyForm))) {
				m_framesRendered++;
				int currentTickCount = Environment.TickCount;
				int ticksElapsed = currentTickCount - m_frameCountStartTickCount;
				if (ticksElapsed >= 2000) {
					m_lastFPS = string.Format("FPS: {0:0.00}", m_framesRendered*1000 / (double)ticksElapsed);
				}
				TextRenderer.DrawText(g, m_lastFPS, Font, new Point(4, 6), ForeColor, SystemColors.Window);

				if ((ticksElapsed >= 2000) || (m_frameCountStartTickCount == 0)) {
					// reset
					m_framesRendered = 0;
					m_frameCountStartTickCount = currentTickCount;
				}
			}
		}

		protected override void OnPaintBackground(PaintEventArgs pevent) {
			//base.OnPaintBackground(pevent);
		}


	}
}
