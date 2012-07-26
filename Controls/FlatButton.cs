using System;
using System.Windows.Forms;
using System.Drawing;

namespace CamTimer.Controls {
	class FlatButton : Control {
		private Image m_imageNormal, m_imageHot, m_imageDisabled;
		private bool m_mouseOver, m_mouseDown;
		private readonly static Image s_imageOverBg = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("CamTimer.Graphics.FlatButtonOver.gif"));
		private readonly static Image s_imageDownBg = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("CamTimer.Graphics.FlatButtonDown.gif"));
		private const int s_padding = 2;

		internal FlatButton() {
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
			Cursor = Cursors.Hand;

		}

		internal void SetSize() {
			if (m_imageNormal != null) {
				Size = MaximumSize = MinimumSize = new Size(m_imageNormal.Size.Width + s_padding * 2, m_imageNormal.Size.Height + s_padding * 2);
			}
			Refresh();
		}

		public Image ImageNormal {
			get {
				return m_imageNormal;
			}
			set {
				m_imageNormal = value;
				SetSize();
			}
		}

		public Image ImageHot {
			get {
				return m_imageHot;
			}
			set {
				m_imageHot = value;
			}
		}

		public Image ImageDisabled {
			get {
				return m_imageDisabled;
			}
			set {
				m_imageDisabled = value;
			}
		}

		protected override void OnResize(EventArgs e) {
			base.OnResize(e);
			SetSize();
		}

		protected override void OnMouseEnter(EventArgs e) {
			base.OnMouseEnter(e);
			m_mouseOver = true;
			Refresh();
		}

		protected override void OnMouseLeave(EventArgs e) {
			base.OnMouseLeave(e);
			m_mouseOver = false;
			Refresh();
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			base.OnMouseDown(e);
			if (e.Button == MouseButtons.Left) {
				m_mouseDown = true;
			}
			Refresh();
		}

		protected override void OnMouseUp(MouseEventArgs e) {
			base.OnMouseUp(e);
			if (e.Button == MouseButtons.Left) {
				m_mouseDown = false;
			}
			Refresh();
		}

		protected override void OnMouseMove(MouseEventArgs e) {
			base.OnMouseMove(e);
			if (m_mouseDown) {
				if ((e.X < 0) || (e.Y < 0) || (e.X > Width) || (e.Y > Height)) {
					if (m_mouseOver != false) {
						m_mouseOver = false;
						Refresh();
					}
				} else {
					if (m_mouseOver != true) {
						m_mouseOver = true;
						Refresh();
					}
				}
			}
		}

		protected override void OnKeyDown(KeyEventArgs e) {
			if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Space) || (e.KeyCode == Keys.Return)) {
				OnClick(new EventArgs());
			}
			base.OnKeyDown(e);
		}

		protected override void OnGotFocus(EventArgs e) {
			base.OnGotFocus(e);
			Refresh();
		}

		protected override void OnLostFocus(EventArgs e) {
			base.OnLostFocus(e);
			Refresh();
		}

		protected override void OnEnabledChanged(EventArgs e) {
			base.OnEnabledChanged(e);
			Refresh();
		}

		protected override void OnPaint(PaintEventArgs e) {
			Image drawImage = null;
			Image drawBgImage = null;
			int offset = 0;

			if (!Enabled) {
				drawImage = m_imageDisabled;
			} else if ((m_mouseOver) || (m_mouseDown) || (Focused)) {
				if ((m_mouseDown) && (m_mouseOver)) {
					drawImage = m_imageHot;
					drawBgImage = s_imageDownBg;
					offset = 1;
				} else {
					drawImage = m_imageHot;
					drawBgImage = s_imageOverBg;
				}
			} else {
				drawImage = m_imageNormal;
			}
			if (drawBgImage != null) {
				e.Graphics.DrawImage(drawBgImage, 0, 0, drawBgImage.Width, drawBgImage.Height);
			} else {
				e.Graphics.Clear(BackColor);
			}
			if (drawImage != null) {
				e.Graphics.DrawImage(drawImage, s_padding + offset, s_padding + offset, drawImage.Width, drawImage.Height);
			}
		}

		protected override void OnPaintBackground(PaintEventArgs pevent) {
			//base.OnPaintBackground(pevent);
		}


	}
}
