using System;
using System.Windows.Forms;

namespace CamTimer.Controls {
	class MousePanel : Panel {
		protected override void OnMouseLeave(EventArgs e) {
			if (DisplayRectangle.Contains(PointToClient(Cursor.Position))) {
				return;
			}
			base.OnMouseLeave(e);
		}

		protected override void OnControlAdded(ControlEventArgs e) {
			base.OnControlAdded(e);

			e.Control.GotFocus += new EventHandler(Control_GotFocus);
			e.Control.LostFocus += new EventHandler(Control_LostFocus);
			e.Control.MouseEnter += new EventHandler(Control_MouseEnter);
			e.Control.MouseLeave += new EventHandler(Control_MouseLeave);
		}

		void Control_LostFocus(object sender, EventArgs e) {
			OnLostFocus(e);
		}

		void Control_GotFocus(object sender, EventArgs e) {
			OnGotFocus(e);
		}

		void Control_MouseLeave(object sender, EventArgs e) {
			OnMouseLeave(e);
		}

		void Control_MouseEnter(object sender, EventArgs e) {
			OnMouseEnter(e);
		}
	}
}
