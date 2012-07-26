using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CamTimer.Controls {
	partial class WebcamConfigurationPicker : UserControl {
		private WebcamConfiguration[] m_resolutions = new WebcamConfiguration[] { };
		private List<Size> m_distinctResolutions = new List<Size>();
		private List<VisualBitDepth> m_bitDepths = new List<VisualBitDepth>();

		internal WebcamConfigurationPicker() {
			InitializeComponent();
		}

		internal void FocusItem(Size size, short bpp) {
			if (Enabled) {
				int x = m_distinctResolutions.IndexOf(size);
				if (x > -1) {
					webCamResolution.Value = x;
					webCamResolution_Scroll(null, null);

					x = m_bitDepths.IndexOf(new VisualBitDepth(bpp));
					if (x > -1) {
						webCamBitDepth.SelectedIndex = x;
					}
				}
			}
		}

		internal WebcamConfiguration Resolution {
			get {
				if ((Enabled) && (webCamResolution.Value > -1)) {
					Size selectedSize = m_distinctResolutions[webCamResolution.Value];
					for (int x = 0; x < m_resolutions.Length; x++) {
						if ((m_resolutions[x].Size == selectedSize) && (m_resolutions[x].BPP == ((VisualBitDepth)webCamBitDepth.SelectedItem).BBP)) {
							return m_resolutions[x];
						}
					}
				}
				return WebcamConfiguration.Empty;
			}
		}

		internal WebcamConfiguration MaxResolution {
			get {
				if (Enabled) {
					Size maxSize = Size.Empty;
					int maxBPP = -1;
					int bestBetIndex = -1;
					for (int x = 0; x < m_resolutions.Length; x++) {
						if ((m_resolutions[x].Size.Width >= maxSize.Width) || (m_resolutions[x].Size.Height >= maxSize.Height)) {
							maxSize = m_resolutions[x].Size;
							if (m_resolutions[x].BPP >= maxBPP) {
								maxBPP = m_resolutions[x].BPP;
								bestBetIndex = x;
							}
						}
					}
					if (bestBetIndex > -1) {
						return m_resolutions[bestBetIndex];
					}
				}
				return WebcamConfiguration.Empty;
			}
		}

		internal void SetResolutions(WebcamConfiguration[] resolutions) {
			if (resolutions.Length == 0) {
				Enabled = false;
				return;
			} else {
				Enabled = true;
			}

			m_resolutions = resolutions;

			// gather a distinct list of resolutions and sort it
			m_distinctResolutions.Clear();
			for (int x = 0; x < m_resolutions.Length; x++) {
				if (!m_distinctResolutions.Contains(m_resolutions[x].Size)) {
					m_distinctResolutions.Add(m_resolutions[x].Size);
				}
			}
			m_distinctResolutions.Sort((Comparison<Size>)delegate(Size x, Size y) {
				if ((x.Width == y.Width) && (x.Height == y.Height)) {
					return 0;
				} else if ((x.Width < y.Width) && (x.Height < y.Height)) {
					return -2;
				} else if ((y.Width < x.Width) && (y.Height < x.Height)) {
					return 2;
				} else if (x.Width < y.Width) {
					return -1;
				} else {
					return 1;
				}
			});

			webCamResolution.Minimum = 0;
			webCamResolution.Maximum = m_distinctResolutions.Count - 1;
			webCamResolution.SmallChange = 1;
			webCamResolution.LargeChange = 1;
			webCamResolution.Value = m_distinctResolutions.Count - 1;
			webCamResolution_Scroll(null, null);

			// try default to 640x480x24
			FocusItem(new Size(640, 480), 24);
		}

		private void webCamResolution_Scroll(object sender, EventArgs e) {
			// display selected resolution (based on the distinct list of resolutions)
			if ((webCamResolution.Value > -1) && (webCamResolution.Value < m_distinctResolutions.Count)) {
				webCamResolutionLabel.Text = Language.FormatString(Language.LanguageString.MainForm_Configuration_Resolution, m_distinctResolutions[webCamResolution.Value].Width, m_distinctResolutions[webCamResolution.Value].Height);

				// list available bitdepths for the selected resolution
				m_bitDepths.Clear();
				for (int x = 0; x < m_resolutions.Length; x++) {
					if (m_resolutions[x].Size == m_distinctResolutions[webCamResolution.Value]) {
						if (!m_bitDepths.Contains(new VisualBitDepth(m_resolutions[x].BPP))) {
							m_bitDepths.Add(new VisualBitDepth(m_resolutions[x].BPP));
						}
					}
				}
				m_bitDepths.Sort((Comparison<VisualBitDepth>)delegate(VisualBitDepth x, VisualBitDepth y) {
					return x.BBP.CompareTo(y.BBP);
				});

				VisualBitDepth? lastBitDepth = webCamBitDepth.SelectedItem as VisualBitDepth?;
				webCamBitDepth.Items.Clear();
				for (int x = 0; x < m_bitDepths.Count; x++) {
					webCamBitDepth.Items.Add(m_bitDepths[x]);
				}

				if ((webCamBitDepth.SelectedIndex < 0) && (webCamBitDepth.Items.Count > 0)) {
					int x = -1;
					if (lastBitDepth != null) {
						x = webCamBitDepth.Items.IndexOf(lastBitDepth);
					}
					if (x > -1) {
						webCamBitDepth.SelectedIndex = x;
					} else {
						webCamBitDepth.SelectedIndex = webCamBitDepth.Items.Count - 1;
					}
				}
			}
		}

		protected override void OnEnabledChanged(EventArgs e) {
			if (!Enabled) {
				webCamResolution.Value = 0;
				webCamResolutionLabel.Text = Language.FormatString(Language.LanguageString.MainForm_Configuration_ResolutionDisabled);
				webCamBitDepth.Items.Clear();
				webCamBitDepth.Items.Add(Language.FormatString(Language.LanguageString.MainForm_Configuration_ResolutionDisabled));
				webCamBitDepth.SelectedIndex = 0;
			}
			base.OnEnabledChanged(e);
		}

		protected override void OnMouseLeave(EventArgs e) {
			if (DisplayRectangle.Contains(PointToClient(Cursor.Position))) {
				return;
			}
			base.OnMouseLeave(e);
		}

		internal struct VisualBitDepth {
			private short m_bpp;

			internal VisualBitDepth(short bpp) {
				m_bpp = bpp;
			}

			internal int BBP {
				get {
					return m_bpp;
				}
			}

			public override string ToString() {
				switch (m_bpp) {
					case 8:
						return Language.FormatString(Language.LanguageString.MainForm_Configuration_Color8bit);
					case 15:
						return Language.FormatString(Language.LanguageString.MainForm_Configuration_Color15bit);
					case 16:
						return Language.FormatString(Language.LanguageString.MainForm_Configuration_Color16bit); 
					case 24:
						return Language.FormatString(Language.LanguageString.MainForm_Configuration_Color24bit);
					case 32:
						return Language.FormatString(Language.LanguageString.MainForm_Configuration_Color32bit);
				}
				return Language.FormatString(Language.LanguageString.MainForm_Configuration_ColorNbits, m_bpp);
			}
		}

		private void sub_MouseEnter(object sender, EventArgs e) {
			OnMouseEnter(e);
		}

		private void sub_MouseLeave(object sender, EventArgs e) {
			OnMouseLeave(e);
		}

		private void sub_GotFocus(object sender, EventArgs e) {
			OnGotFocus(e);
		}

		private void sub_LostFocus(object sender, EventArgs e) {
			OnLostFocus(e);
		}
	}
}
