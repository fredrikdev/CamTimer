using CamTimer.Controls;

namespace CamTimer {
	partial class NotifyForm {
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {

			this.pictureBox = new SynchronizedPictureBox();
			this.takePictureButton = new System.Windows.Forms.LinkLabel();
			this.hideButton = new System.Windows.Forms.LinkLabel();
			this.settingsButton = new System.Windows.Forms.LinkLabel();
			this.ui1 = new System.Windows.Forms.Label();
			this.ui2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// pictureBox
			// 
			this.pictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pictureBox.DisplayBitmap = null;
			this.pictureBox.Location = new System.Drawing.Point(11, 11);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(242, 182);
			this.pictureBox.TabIndex = 56;
			this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
			// 
			// takePictureButton
			// 
			this.takePictureButton.AutoSize = true;
			this.takePictureButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.takePictureButton.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.takePictureButton.Location = new System.Drawing.Point(9, 202);
			this.takePictureButton.Name = "takePictureButton";
			this.takePictureButton.Size = new System.Drawing.Size(0, 13);
			this.takePictureButton.TabIndex = 51;
			this.takePictureButton.TabStop = true;
			this.takePictureButton.UseMnemonic = false;
			this.takePictureButton.VisitedLinkColor = System.Drawing.Color.Blue;
			this.takePictureButton.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.takePictureButton_LinkClicked);
			// 
			// hideButton
			// 
			this.hideButton.AutoSize = true;
			this.hideButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.hideButton.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.hideButton.Location = new System.Drawing.Point(9, 253);
			this.hideButton.Name = "hideButton";
			this.hideButton.Size = new System.Drawing.Size(0, 13);
			this.hideButton.TabIndex = 52;
			this.hideButton.TabStop = true;
			this.hideButton.UseMnemonic = false;
			this.hideButton.VisitedLinkColor = System.Drawing.Color.Blue;
			this.hideButton.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.hideNowButton_LinkClicked);
			// 
			// settingsButton
			// 
			this.settingsButton.AutoSize = true;
			this.settingsButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.settingsButton.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.settingsButton.Location = new System.Drawing.Point(9, 219);
			this.settingsButton.Name = "settingsButton";
			this.settingsButton.Size = new System.Drawing.Size(0, 13);
			this.settingsButton.TabIndex = 53;
			this.settingsButton.TabStop = true;
			this.settingsButton.UseMnemonic = false;
			this.settingsButton.VisitedLinkColor = System.Drawing.Color.Blue;
			this.settingsButton.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.settingsButton_LinkClicked);
			// 
			// ui1
			// 
			this.ui1.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ui1.Location = new System.Drawing.Point(0, 0);
			this.ui1.Name = "ui1";
			this.ui1.Size = new System.Drawing.Size(265, 282);
			this.ui1.TabIndex = 54;
			// 
			// ui2
			// 
			this.ui2.BackColor = System.Drawing.SystemColors.Control;
			this.ui2.Location = new System.Drawing.Point(1, 1);
			this.ui2.Name = "ui2";
			this.ui2.Size = new System.Drawing.Size(263, 280);
			this.ui2.TabIndex = 55;
			// 
			// NotifyForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(265, 282);
			this.ControlBox = false;
			this.Controls.Add(this.pictureBox);
			this.Controls.Add(this.settingsButton);
			this.Controls.Add(this.hideButton);
			this.Controls.Add(this.takePictureButton);
			this.Controls.Add(this.ui2);
			this.Controls.Add(this.ui1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NotifyForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.notifyForm_FormClosed);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.LinkLabel takePictureButton;
		private System.Windows.Forms.LinkLabel hideButton;
		private System.Windows.Forms.LinkLabel settingsButton;
		private System.Windows.Forms.Label ui1;
		private System.Windows.Forms.Label ui2;
		private SynchronizedPictureBox pictureBox;
	}
}