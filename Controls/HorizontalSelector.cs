using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace CamTimer.Controls {
	class HorizontalSelector : Control {
		public delegate void PictureClickedDelegate(PictureAndThumbnailPair e);
		public event PictureClickedDelegate PictureClicked = null;

		private List<PictureAndThumbnailPair> m_items = new List<PictureAndThumbnailPair>();
		private List<RectangleAndIndex> m_rects = new List<RectangleAndIndex>();
		private Animator m_animator = new Animator();
		private int m_rectHover = -1;

		private int m_itemWidth = 90;
		private int m_itemSpacing = 4;
		private int m_offset = -1;
		private int m_itemIndexCenter = -1;

		internal HorizontalSelector() {
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
			m_animator.TotalDuration = 100;
			m_animator.FPS = 28;
		}

		internal List<PictureAndThumbnailPair> Items {
			get {
				return m_items;
			}
		}

		private int CenterPoint {
			get {
				return ClientSize.Width / 2;
			}
		}

		private int MaxOffset {
			get {
				return (m_itemWidth + m_itemSpacing) * m_items.Count;
			}
		}

		private void AdjustNegativeOffset(ref int value) {
			while (value < 0) value += MaxOffset;
		}

		internal void ResetPosition() {
			m_offset = -1;
		}

		internal PictureAndThumbnailPair CurrentPicture {
			get {
				if ((m_itemIndexCenter > -1) && (m_itemIndexCenter < m_items.Count)) {
					return m_items[m_itemIndexCenter];
				}
				return null;
			}
		}

		protected override void OnClick(EventArgs e) {
			if ((m_rectHover > -1) && (m_rectHover < m_rects.Count)) {
				RectangleAndIndex r = m_rects[m_rectHover];

				int startOffset = m_offset;
				int endOffset = (startOffset + r.Rectangle.Left - CenterPoint + m_itemWidth / 2);
				if (r.Rectangle.Left > CenterPoint) AdjustNegativeOffset(ref endOffset);

				m_animator.Run((EventHandler<AnimationEventArgs>)delegate(object s, AnimationEventArgs aea) {
					int newOffset = (int)(startOffset + (endOffset - startOffset) * aea.PercentComplete) % MaxOffset;
					if (r.Rectangle.Left < CenterPoint) AdjustNegativeOffset(ref newOffset);
					m_offset = newOffset;
					Invoke((MethodInvoker)delegate() {
						Refresh();
					});

					if (aea.IsLastCall) {
						m_itemIndexCenter = r.Index;
						if (PictureClicked != null) {
							Invoke((MethodInvoker)delegate() {
								PictureClicked(m_items[m_itemIndexCenter]);
							});
						}
					}
				});
			}
			base.OnClick(e);
		}

		// note the rectangle the user is hovering
		protected override void OnMouseMove(MouseEventArgs e) {
			m_rectHover = -1;
			for (int x = 0; x < m_rects.Count; x++) {
				if (m_rects[x].Rectangle.Contains(e.Location)) {
					m_rectHover = x;
					break;
				}
			}
			base.OnMouseMove(e);
		}

		// calculate new offset to the current element
		protected override void OnResize(EventArgs e) {
			if ((m_items.Count > 0) && (m_itemIndexCenter > -1) && (m_itemIndexCenter < m_items.Count)) {
				int newOffset = (0 + (m_itemSpacing + m_itemWidth) * m_itemIndexCenter - CenterPoint + m_itemSpacing + m_itemWidth / 2) % MaxOffset;
				AdjustNegativeOffset(ref newOffset);
				m_offset = newOffset;
			}
			Refresh();
			base.OnResize(e);
		}

		protected override void OnPaint(PaintEventArgs e) {
			const int borderSpacing = 10;
			int clientWidth = ClientSize.Width, clientHeight = ClientSize.Height;
			int heightMin = clientHeight - borderSpacing * 2 - 2 - m_itemSpacing * 2;
			int heightMax = clientHeight;

			// draw white area with border
			e.Graphics.Clear(BackColor);
			e.Graphics.FillRectangle(Brushes.White, new Rectangle(1, borderSpacing + 1, clientWidth - 2, clientHeight - borderSpacing * 2 - 2));
			e.Graphics.DrawRectangle(SystemPens.ControlDark, new Rectangle(0, borderSpacing, clientWidth - 1, clientHeight - borderSpacing * 2 - 1));

			// based on m_offset, calculate the first item that we need to draw
			m_rects.Clear();
			if (m_items.Count > 0) {
				// if m_offset isn't set, calculate it to the first element
				if (m_offset == -1) {
					m_offset = (0 + 0 - CenterPoint + m_itemSpacing + m_itemWidth / 2) % MaxOffset;
					AdjustNegativeOffset(ref m_offset);
					m_itemIndexCenter = 0;
				}

				// calculate where drawing starts
				int firstIndex = (int)Math.Floor((double)m_offset / (m_itemSpacing + m_itemWidth)) % m_items.Count;
				int firstIndexOfs = firstIndex * (m_itemSpacing + m_itemWidth) - m_offset;

				// draw
				int index = firstIndex;
				int drawOffset = firstIndexOfs + m_itemSpacing;
				while (true) {
					int drawIndex = index % m_items.Count;

					// draw into this rectangle
					Rectangle bounds = new Rectangle(drawOffset, clientHeight / 2 - heightMin / 2, m_itemWidth, heightMin);
					//e.Graphics.DrawRectangle(Pens.Magenta, bounds);
					m_rects.Add(new RectangleAndIndex(bounds, drawIndex));

					// scale b to fit in bounds
					Bitmap b = m_items[drawIndex].Thumbnail;
					if (b != null) {
						double distanceFromCenter = (int)(bounds.Left + bounds.Width / 2.0);
						distanceFromCenter = 1 - ((distanceFromCenter < CenterPoint) ? CenterPoint - distanceFromCenter : distanceFromCenter - CenterPoint) / (double)CenterPoint;
						double scale = Math.Min((double)bounds.Width / b.Width, ((double)bounds.Height + heightMax * Math.Pow(distanceFromCenter, 5)) / b.Height);

						Rectangle s = new Rectangle(0, 0, b.Width, b.Height);
						Rectangle d = new Rectangle(0, 0, (int)(s.Width * scale), (int)(s.Height * scale));
						if (d.Height % 2 == 1) d.Height--;
						d.X = (int)(bounds.X + bounds.Width / 2 - d.Width / 2);
						d.Y = (int)(bounds.Y + bounds.Height / 2.0 - d.Height / 2.0);

						e.Graphics.DrawImage(b, d, s, GraphicsUnit.Pixel);
						//e.Graphics.DrawString(drawIndex.ToString() + "," + distanceFromCenter, Font, Brushes.Black, bounds);
					} else {
						TextRenderer.DrawText(e.Graphics, Language.FormatString(Language.LanguageString.MainForm_TakePicture_NoDisplacementFilter), Font, bounds, ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
					}

					if (bounds.Right > clientWidth) break;

					index++;
					drawOffset += m_itemWidth + m_itemSpacing;
				}
				/*
				e.Graphics.DrawLine(Pens.Red, clientWidth / 2, 0, clientWidth / 2, clientHeight);
				e.Graphics.DrawLine(Pens.Red, clientWidth / 2-60, 0, clientWidth / 2-60, clientHeight);
				e.Graphics.DrawLine(Pens.Red, clientWidth / 2 + 60, 0, clientWidth / 2 + 60, clientHeight);
				 * */
			}
			e.Graphics.DrawLine(SystemPens.ControlDark, 0, borderSpacing, 0, clientHeight - borderSpacing * 2 - 1);
			e.Graphics.DrawLine(SystemPens.ControlDark, clientWidth - 1, borderSpacing, clientWidth - 1, clientHeight - borderSpacing * 2 - 1);
		}

		protected override void OnPaintBackground(PaintEventArgs pevent) {
			//base.OnPaintBackground(pevent);
		}
	}

	class PictureAndThumbnailPair {
		private string m_picture;
		private Bitmap m_thumbnail;

		internal PictureAndThumbnailPair(string picture, string thumbnail) {
			m_picture = picture;
			if (thumbnail.Length == 0) {
				m_thumbnail = null;
			} else {
				m_thumbnail = new Bitmap(thumbnail);
			}
		}

		internal string Picture {
			get {
				return m_picture;
			}
		}

		internal Bitmap Thumbnail {
			get {
				return m_thumbnail;
			}
		}
	}

	class RectangleAndIndex {
		private Rectangle m_rect;
		private int m_index;

		internal RectangleAndIndex(Rectangle rect, int index) {
			m_rect = rect;
			m_index = index;
		}

		internal Rectangle Rectangle {
			get {
				return m_rect;
			}
		}

		internal int Index {
			get {
				return m_index;
			}
		}
	}
}
