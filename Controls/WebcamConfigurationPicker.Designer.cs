namespace CamTimer.Controls {
	partial class WebcamConfigurationPicker {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.webCamBitDepth = new System.Windows.Forms.ComboBox();
			this.webCamResolution = new System.Windows.Forms.TrackBar();
			this.webCamResolutionLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.webCamResolution)).BeginInit();
			this.SuspendLayout();
			// 
			// webCamBitDepth
			// 
			this.webCamBitDepth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.webCamBitDepth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.webCamBitDepth.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.webCamBitDepth.FormattingEnabled = true;
			this.webCamBitDepth.Location = new System.Drawing.Point(169, 6);
			this.webCamBitDepth.Name = "webCamBitDepth";
			this.webCamBitDepth.Size = new System.Drawing.Size(233, 21);
			this.webCamBitDepth.TabIndex = 48;
			this.webCamBitDepth.Leave += new System.EventHandler(this.sub_LostFocus);
			this.webCamBitDepth.Enter += new System.EventHandler(this.sub_GotFocus);
			this.webCamBitDepth.MouseEnter += new System.EventHandler(this.sub_MouseEnter);
			this.webCamBitDepth.MouseLeave += new System.EventHandler(this.sub_MouseLeave);
			// 
			// webCamResolution
			// 
			this.webCamResolution.Location = new System.Drawing.Point(4, 4);
			this.webCamResolution.Name = "webCamResolution";
			this.webCamResolution.Size = new System.Drawing.Size(135, 42);
			this.webCamResolution.TabIndex = 46;
			this.webCamResolution.MouseLeave += new System.EventHandler(this.sub_MouseLeave);
			this.webCamResolution.Leave += new System.EventHandler(this.sub_LostFocus);
			this.webCamResolution.Scroll += new System.EventHandler(this.webCamResolution_Scroll);
			this.webCamResolution.Enter += new System.EventHandler(this.sub_GotFocus);
			this.webCamResolution.MouseEnter += new System.EventHandler(this.sub_MouseEnter);
			// 
			// webCamResolutionLabel
			// 
			this.webCamResolutionLabel.AutoEllipsis = true;
			this.webCamResolutionLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.webCamResolutionLabel.Location = new System.Drawing.Point(12, 46);
			this.webCamResolutionLabel.Name = "webCamResolutionLabel";
			this.webCamResolutionLabel.Size = new System.Drawing.Size(120, 22);
			this.webCamResolutionLabel.TabIndex = 47;
			this.webCamResolutionLabel.Text = string.Empty;
			this.webCamResolutionLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.webCamResolutionLabel.MouseLeave += new System.EventHandler(this.sub_MouseLeave);
			this.webCamResolutionLabel.Leave += new System.EventHandler(this.sub_LostFocus);
			this.webCamResolutionLabel.Enter += new System.EventHandler(this.sub_GotFocus);
			this.webCamResolutionLabel.MouseEnter += new System.EventHandler(this.sub_MouseEnter);
			// 
			// WebcamConfigurationPicker
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.webCamBitDepth);
			this.Controls.Add(this.webCamResolution);
			this.Controls.Add(this.webCamResolutionLabel);
			this.Name = "WebcamConfigurationPicker";
			this.Size = new System.Drawing.Size(411, 74);
			((System.ComponentModel.ISupportInitialize)(this.webCamResolution)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox webCamBitDepth;
		private System.Windows.Forms.TrackBar webCamResolution;
		private System.Windows.Forms.Label webCamResolutionLabel;
	}
}
