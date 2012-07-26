using CamTimer.Controls;
using System.Windows.Forms;

namespace CamTimer {
	partial class MainForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.confFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.takePictureSaveDialog = new System.Windows.Forms.SaveFileDialog();
			this.takePicturePanel = new System.Windows.Forms.Panel();
			this.ui5 = new System.Windows.Forms.Label();
			this.ui2 = new System.Windows.Forms.Label();
			this.confPanel = new System.Windows.Forms.Panel();
			this.confFilenamePanel = new CamTimer.Controls.MousePanel();
			this.confFilenameLabel = new System.Windows.Forms.Label();
			this.confFilename = new System.Windows.Forms.TextBox();
			this.confFolderPanel = new CamTimer.Controls.MousePanel();
			this.confFolderButton = new System.Windows.Forms.Button();
			this.confFolder = new System.Windows.Forms.TextBox();
			this.confFoldernameLabel = new System.Windows.Forms.Label();
			this.confWebcamPanel = new CamTimer.Controls.MousePanel();
			this.confWebcamLabel = new System.Windows.Forms.Label();
			this.confWebcam = new System.Windows.Forms.ComboBox();
			this.confSchedulePanel = new CamTimer.Controls.MousePanel();
			this.confScheduleIntervalHoursLabel = new System.Windows.Forms.Label();
			this.confScheduleIntervalLabel = new System.Windows.Forms.Label();
			this.confScheduleInterval = new System.Windows.Forms.DomainUpDown();
			this.confScheduleEnabled = new System.Windows.Forms.CheckBox();
			this.confScheduleBetweenAnd = new System.Windows.Forms.Label();
			this.confScheduleTimeEnd = new System.Windows.Forms.DateTimePicker();
			this.confScheduleTimeStart = new System.Windows.Forms.DateTimePicker();
			this.confScheduleBetweenLabel = new System.Windows.Forms.Label();
			this.confScheduleSun = new System.Windows.Forms.CheckBox();
			this.confScheduleSat = new System.Windows.Forms.CheckBox();
			this.confScheduleFri = new System.Windows.Forms.CheckBox();
			this.confScheduleThu = new System.Windows.Forms.CheckBox();
			this.confScheduleWed = new System.Windows.Forms.CheckBox();
			this.confScheduleTue = new System.Windows.Forms.CheckBox();
			this.confScheduleMon = new System.Windows.Forms.CheckBox();
			this.confScheduleDaysLabel = new System.Windows.Forms.Label();
			this.confScheduleMaxRes = new System.Windows.Forms.CheckBox();
			this.confScheduleDisableOnScreensaver = new System.Windows.Forms.CheckBox();
			this.confAutoStart = new System.Windows.Forms.CheckBox();
			this.ui4 = new System.Windows.Forms.Label();
			this.ui1 = new System.Windows.Forms.Label();
			this.infoPanel = new System.Windows.Forms.Panel();
			this.infoCopyright2Label = new System.Windows.Forms.Label();
			this.infoWebsiteLabel = new System.Windows.Forms.LinkLabel();
			this.ui6 = new System.Windows.Forms.Label();
			this.ui3 = new System.Windows.Forms.Label();
			this.infoPictureBox = new System.Windows.Forms.PictureBox();
			this.infoCopyright1Label = new System.Windows.Forms.Label();
			this.takePicturePanel.SuspendLayout();
			this.confPanel.SuspendLayout();
			this.confFilenamePanel.SuspendLayout();
			this.confFolderPanel.SuspendLayout();
			this.confWebcamPanel.SuspendLayout();
			this.confSchedulePanel.SuspendLayout();
			this.infoPanel.SuspendLayout();
			confResolution = new WebcamConfigurationPicker();
			confRejectButton = new FlatButton();
			confAcceptButton = new FlatButton();
			takePictureSaveButton = new FlatButton();
			takePictureBackButton = new FlatButton();
			confTakePictureButton = new FlatButton();
			confInfoButton = new FlatButton();
			helpPanel = new HelpLabel();
			infoBackButton = new FlatButton();
			takePicturePicture = new SynchronizedPictureBox();
			takePictureDisplacement = new HorizontalSelector();
			((System.ComponentModel.ISupportInitialize)(this.infoPictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// takePictureSaveDialog
			// 
			this.takePictureSaveDialog.DefaultExt = "jpg";
			this.takePictureSaveDialog.Filter = "JPEG (*.JPG;*.JPEG;*.JPE)|*.JPG;*.JPEG;*.JPE";
			// 
			// takePicturePanel
			// 
			this.takePicturePanel.Controls.Add(this.takePictureDisplacement);
			this.takePicturePanel.Controls.Add(this.takePictureSaveButton);
			this.takePicturePanel.Controls.Add(this.takePictureBackButton);
			this.takePicturePanel.Controls.Add(this.ui5);
			this.takePicturePanel.Controls.Add(this.ui2);
			this.takePicturePanel.Controls.Add(this.takePicturePicture);
			this.takePicturePanel.Location = new System.Drawing.Point(565, 0);
			this.takePicturePanel.Name = "takePicturePanel";
			this.takePicturePanel.Size = new System.Drawing.Size(554, 580);
			this.takePicturePanel.TabIndex = 41;
			this.takePicturePanel.Visible = false;
			// 
			// takePictureDisplacement
			// 
			this.takePictureDisplacement.Cursor = System.Windows.Forms.Cursors.Hand;
			this.takePictureDisplacement.Location = new System.Drawing.Point(22, 475);
			this.takePictureDisplacement.Name = "takePictureDisplacement";
			this.takePictureDisplacement.Size = new System.Drawing.Size(512, 93);
			this.takePictureDisplacement.TabIndex = 51;
			this.takePictureDisplacement.PictureClicked += new CamTimer.Controls.HorizontalSelector.PictureClickedDelegate(this.displacementSelector_PictureClicked);
			// 
			// takePictureSaveButton
			// 
			this.takePictureSaveButton.BackColor = System.Drawing.Color.White;
			this.takePictureSaveButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.takePictureSaveButton.ImageDisabled = ((System.Drawing.Image)(resources.GetObject("takePictureSaveButton.ImageDisabled")));
			this.takePictureSaveButton.ImageHot = ((System.Drawing.Image)(resources.GetObject("takePictureSaveButton.ImageHot")));
			this.takePictureSaveButton.ImageNormal = ((System.Drawing.Image)(resources.GetObject("takePictureSaveButton.ImageNormal")));
			this.takePictureSaveButton.Location = new System.Drawing.Point(72, 5);
			this.takePictureSaveButton.MaximumSize = new System.Drawing.Size(52, 52);
			this.takePictureSaveButton.MinimumSize = new System.Drawing.Size(52, 52);
			this.takePictureSaveButton.Name = "takePictureSaveButton";
			this.takePictureSaveButton.Size = new System.Drawing.Size(52, 52);
			this.takePictureSaveButton.TabIndex = 2;
			this.takePictureSaveButton.Click += new System.EventHandler(this.savePictureButton_Click);
			// 
			// takePictureBackButton
			// 
			this.takePictureBackButton.BackColor = System.Drawing.Color.White;
			this.takePictureBackButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.takePictureBackButton.ImageDisabled = ((System.Drawing.Image)(resources.GetObject("takePictureBackButton.ImageDisabled")));
			this.takePictureBackButton.ImageHot = ((System.Drawing.Image)(resources.GetObject("takePictureBackButton.ImageHot")));
			this.takePictureBackButton.ImageNormal = ((System.Drawing.Image)(resources.GetObject("takePictureBackButton.ImageNormal")));
			this.takePictureBackButton.Location = new System.Drawing.Point(14, 5);
			this.takePictureBackButton.MaximumSize = new System.Drawing.Size(52, 52);
			this.takePictureBackButton.MinimumSize = new System.Drawing.Size(52, 52);
			this.takePictureBackButton.Name = "takePictureBackButton";
			this.takePictureBackButton.Size = new System.Drawing.Size(52, 52);
			this.takePictureBackButton.TabIndex = 1;
			this.takePictureBackButton.Click += new System.EventHandler(this.animatePanelOut_Click);
			// 
			// ui5
			// 
			this.ui5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.ui5.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ui5.Location = new System.Drawing.Point(0, 62);
			this.ui5.Name = "ui5";
			this.ui5.Size = new System.Drawing.Size(555, 1);
			this.ui5.TabIndex = 42;
			// 
			// ui2
			// 
			this.ui2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.ui2.BackColor = System.Drawing.Color.White;
			this.ui2.Location = new System.Drawing.Point(0, 0);
			this.ui2.Name = "ui2";
			this.ui2.Size = new System.Drawing.Size(554, 64);
			this.ui2.TabIndex = 0;
			// 
			// takePicturePicture
			// 
			this.takePicturePicture.Cursor = System.Windows.Forms.Cursors.Hand;
			this.takePicturePicture.DisplayBitmap = null;
			this.takePicturePicture.Location = new System.Drawing.Point(22, 90);
			this.takePicturePicture.Name = "takePicturePicture";
			this.takePicturePicture.Size = new System.Drawing.Size(512, 384);
			this.takePicturePicture.TabIndex = 50;
			this.takePicturePicture.Click += new System.EventHandler(this.TakePicture);
			// 
			// confPanel
			// 
			this.confPanel.Controls.Add(this.confFilenamePanel);
			this.confPanel.Controls.Add(this.confFolderPanel);
			this.confPanel.Controls.Add(this.confWebcamPanel);
			this.confPanel.Controls.Add(this.confSchedulePanel);
			this.confPanel.Controls.Add(this.confInfoButton);
			this.confPanel.Controls.Add(this.confTakePictureButton);
			this.confPanel.Controls.Add(this.confRejectButton);
			this.confPanel.Controls.Add(this.confAcceptButton);
			this.confPanel.Controls.Add(this.confResolution);
			this.confPanel.Controls.Add(this.confAutoStart);
			this.confPanel.Controls.Add(this.ui4);
			this.confPanel.Controls.Add(this.ui1);
			this.confPanel.Location = new System.Drawing.Point(0, 0);
			this.confPanel.Name = "confPanel";
			this.confPanel.Size = new System.Drawing.Size(554, 514);
			this.confPanel.TabIndex = 42;
			// 
			// confFilenamePanel
			// 
			this.confFilenamePanel.Controls.Add(this.confFilenameLabel);
			this.confFilenamePanel.Controls.Add(this.confFilename);
			this.confFilenamePanel.Location = new System.Drawing.Point(14, 221);
			this.confFilenamePanel.Name = "confFilenamePanel";
			this.confFilenamePanel.Size = new System.Drawing.Size(522, 25);
			this.confFilenamePanel.TabIndex = 3;
			// 
			// confFilenameLabel
			// 
			this.confFilenameLabel.AutoSize = true;
			this.confFilenameLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.confFilenameLabel.Location = new System.Drawing.Point(5, 3);
			this.confFilenameLabel.Name = "confFilenameLabel";
			this.confFilenameLabel.Size = new System.Drawing.Size(0, 13);
			this.confFilenameLabel.TabIndex = 0;
			// 
			// confFilename
			// 
			this.confFilename.BackColor = System.Drawing.SystemColors.Window;
			this.confFilename.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.confFilename.Location = new System.Drawing.Point(142, 0);
			this.confFilename.Name = "confFilename";
			this.confFilename.Size = new System.Drawing.Size(349, 21);
			this.confFilename.TabIndex = 1;
			// 
			// confFolderPanel
			// 
			this.confFolderPanel.Controls.Add(this.confFolderButton);
			this.confFolderPanel.Controls.Add(this.confFolder);
			this.confFolderPanel.Controls.Add(this.confFoldernameLabel);
			this.confFolderPanel.Location = new System.Drawing.Point(17, 192);
			this.confFolderPanel.Name = "confFolderPanel";
			this.confFolderPanel.Size = new System.Drawing.Size(519, 27);
			this.confFolderPanel.TabIndex = 2;
			// 
			// confFolderButton
			// 
			this.confFolderButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.confFolderButton.Location = new System.Drawing.Point(492, 4);
			this.confFolderButton.Name = "confFolderButton";
			this.confFolderButton.Size = new System.Drawing.Size(27, 21);
			this.confFolderButton.TabIndex = 2;
			this.confFolderButton.Text = "...";
			this.confFolderButton.UseVisualStyleBackColor = true;
			this.confFolderButton.Click += new System.EventHandler(this.destinationFolderBrowse_Click);
			// 
			// confFolder
			// 
			this.confFolder.BackColor = System.Drawing.SystemColors.Window;
			this.confFolder.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.confFolder.Location = new System.Drawing.Point(139, 4);
			this.confFolder.MaxLength = 1024;
			this.confFolder.Name = "confFolder";
			this.confFolder.ReadOnly = true;
			this.confFolder.Size = new System.Drawing.Size(349, 21);
			this.confFolder.TabIndex = 1;
			// 
			// confFoldernameLabel
			// 
			this.confFoldernameLabel.AutoSize = true;
			this.confFoldernameLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.confFoldernameLabel.Location = new System.Drawing.Point(2, 7);
			this.confFoldernameLabel.Name = "confFoldernameLabel";
			this.confFoldernameLabel.Size = new System.Drawing.Size(0, 13);
			this.confFoldernameLabel.TabIndex = 0;
			// 
			// confWebcamPanel
			// 
			this.confWebcamPanel.Controls.Add(this.confWebcamLabel);
			this.confWebcamPanel.Controls.Add(this.confWebcam);
			this.confWebcamPanel.Location = new System.Drawing.Point(17, 90);
			this.confWebcamPanel.Name = "confWebcamPanel";
			this.confWebcamPanel.Size = new System.Drawing.Size(519, 22);
			this.confWebcamPanel.TabIndex = 0;
			// 
			// confWebcamLabel
			// 
			this.confWebcamLabel.AutoSize = true;
			this.confWebcamLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.confWebcamLabel.Location = new System.Drawing.Point(2, 4);
			this.confWebcamLabel.Name = "confWebcamLabel";
			this.confWebcamLabel.Size = new System.Drawing.Size(0, 13);
			this.confWebcamLabel.TabIndex = 0;
			// 
			// confWebcam
			// 
			this.confWebcam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.confWebcam.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.confWebcam.FormattingEnabled = true;
			this.confWebcam.Location = new System.Drawing.Point(139, 1);
			this.confWebcam.Name = "confWebcam";
			this.confWebcam.Size = new System.Drawing.Size(380, 21);
			this.confWebcam.TabIndex = 1;
			this.confWebcam.SelectedIndexChanged += new System.EventHandler(this.webCamList_SelectedIndexChanged);
			// 
			// confSchedulePanel
			// 
			this.confSchedulePanel.Controls.Add(this.confScheduleIntervalHoursLabel);
			this.confSchedulePanel.Controls.Add(this.confScheduleIntervalLabel);
			this.confSchedulePanel.Controls.Add(this.confScheduleInterval);
			this.confSchedulePanel.Controls.Add(this.confScheduleEnabled);
			this.confSchedulePanel.Controls.Add(this.confScheduleBetweenAnd);
			this.confSchedulePanel.Controls.Add(this.confScheduleTimeEnd);
			this.confSchedulePanel.Controls.Add(this.confScheduleTimeStart);
			this.confSchedulePanel.Controls.Add(this.confScheduleBetweenLabel);
			this.confSchedulePanel.Controls.Add(this.confScheduleSun);
			this.confSchedulePanel.Controls.Add(this.confScheduleSat);
			this.confSchedulePanel.Controls.Add(this.confScheduleFri);
			this.confSchedulePanel.Controls.Add(this.confScheduleThu);
			this.confSchedulePanel.Controls.Add(this.confScheduleWed);
			this.confSchedulePanel.Controls.Add(this.confScheduleTue);
			this.confSchedulePanel.Controls.Add(this.confScheduleMon);
			this.confSchedulePanel.Controls.Add(this.confScheduleDaysLabel);
			this.confSchedulePanel.Controls.Add(this.confScheduleMaxRes);
			this.confSchedulePanel.Controls.Add(this.confScheduleDisableOnScreensaver);
			this.confSchedulePanel.Location = new System.Drawing.Point(17, 266);
			this.confSchedulePanel.Name = "confSchedulePanel";
			this.confSchedulePanel.Size = new System.Drawing.Size(519, 162);
			this.confSchedulePanel.TabIndex = 4;
			// 
			// confScheduleIntervalHoursLabel
			// 
			this.confScheduleIntervalHoursLabel.AutoSize = true;
			this.confScheduleIntervalHoursLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.confScheduleIntervalHoursLabel.Location = new System.Drawing.Point(212, 129);
			this.confScheduleIntervalHoursLabel.Name = "confScheduleIntervalHoursLabel";
			this.confScheduleIntervalHoursLabel.Size = new System.Drawing.Size(0, 13);
			this.confScheduleIntervalHoursLabel.TabIndex = 18;
			// 
			// confScheduleIntervalLabel
			// 
			this.confScheduleIntervalLabel.AutoSize = true;
			this.confScheduleIntervalLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.confScheduleIntervalLabel.Location = new System.Drawing.Point(2, 128);
			this.confScheduleIntervalLabel.Name = "confScheduleIntervalLabel";
			this.confScheduleIntervalLabel.Size = new System.Drawing.Size(0, 13);
			this.confScheduleIntervalLabel.TabIndex = 16;
			// 
			// confScheduleInterval
			// 
			this.confScheduleInterval.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.confScheduleInterval.Location = new System.Drawing.Point(139, 124);
			this.confScheduleInterval.Name = "confScheduleInterval";
			this.confScheduleInterval.Size = new System.Drawing.Size(66, 21);
			this.confScheduleInterval.TabIndex = 17;
			this.confScheduleInterval.Leave += new System.EventHandler(this.scheduleInterval_Leave);
			// 
			// confScheduleEnabled
			// 
			this.confScheduleEnabled.CheckAlign = System.Drawing.ContentAlignment.TopRight;
			this.confScheduleEnabled.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.confScheduleEnabled.Location = new System.Drawing.Point(0, 0);
			this.confScheduleEnabled.Name = "confScheduleEnabled";
			this.confScheduleEnabled.Size = new System.Drawing.Size(153, 17);
			this.confScheduleEnabled.TabIndex = 1;
			this.confScheduleEnabled.UseVisualStyleBackColor = true;
			this.confScheduleEnabled.CheckedChanged += new System.EventHandler(this.scheduleEnabled_CheckedChanged);
			// 
			// confScheduleBetweenAnd
			// 
			this.confScheduleBetweenAnd.AutoSize = true;
			this.confScheduleBetweenAnd.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.confScheduleBetweenAnd.Location = new System.Drawing.Point(265, 100);
			this.confScheduleBetweenAnd.Name = "confScheduleBetweenAnd";
			this.confScheduleBetweenAnd.Size = new System.Drawing.Size(0, 13);
			this.confScheduleBetweenAnd.TabIndex = 14;
			// 
			// confScheduleTimeEnd
			// 
			this.confScheduleTimeEnd.CustomFormat = "";
			this.confScheduleTimeEnd.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.confScheduleTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.confScheduleTimeEnd.Location = new System.Drawing.Point(296, 96);
			this.confScheduleTimeEnd.Name = "confScheduleTimeEnd";
			this.confScheduleTimeEnd.ShowUpDown = true;
			this.confScheduleTimeEnd.Size = new System.Drawing.Size(120, 21);
			this.confScheduleTimeEnd.TabIndex = 15;
			this.confScheduleTimeEnd.Value = new System.DateTime(2008, 4, 9, 15, 36, 0, 0);
			// 
			// confScheduleTimeStart
			// 
			this.confScheduleTimeStart.CustomFormat = "";
			this.confScheduleTimeStart.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.confScheduleTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.confScheduleTimeStart.Location = new System.Drawing.Point(139, 96);
			this.confScheduleTimeStart.Name = "confScheduleTimeStart";
			this.confScheduleTimeStart.ShowUpDown = true;
			this.confScheduleTimeStart.Size = new System.Drawing.Size(120, 21);
			this.confScheduleTimeStart.TabIndex = 13;
			this.confScheduleTimeStart.Value = new System.DateTime(2008, 4, 9, 15, 36, 0, 0);
			// 
			// confScheduleBetweenLabel
			// 
			this.confScheduleBetweenLabel.AutoSize = true;
			this.confScheduleBetweenLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.confScheduleBetweenLabel.Location = new System.Drawing.Point(2, 100);
			this.confScheduleBetweenLabel.Name = "confScheduleBetweenLabel";
			this.confScheduleBetweenLabel.Size = new System.Drawing.Size(0, 13);
			this.confScheduleBetweenLabel.TabIndex = 12;
			// 
			// confScheduleSun
			// 
			this.confScheduleSun.AutoSize = true;
			this.confScheduleSun.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.confScheduleSun.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.confScheduleSun.Location = new System.Drawing.Point(438, 72);
			this.confScheduleSun.Name = "confScheduleSun";
			this.confScheduleSun.Size = new System.Drawing.Size(15, 14);
			this.confScheduleSun.TabIndex = 11;
			this.confScheduleSun.UseVisualStyleBackColor = true;
			// 
			// confScheduleSat
			// 
			this.confScheduleSat.AutoSize = true;
			this.confScheduleSat.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.confScheduleSat.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.confScheduleSat.Location = new System.Drawing.Point(390, 72);
			this.confScheduleSat.Name = "confScheduleSat";
			this.confScheduleSat.Size = new System.Drawing.Size(15, 14);
			this.confScheduleSat.TabIndex = 10;
			this.confScheduleSat.UseVisualStyleBackColor = true;
			// 
			// confScheduleFri
			// 
			this.confScheduleFri.AutoSize = true;
			this.confScheduleFri.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.confScheduleFri.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.confScheduleFri.Location = new System.Drawing.Point(347, 72);
			this.confScheduleFri.Name = "confScheduleFri";
			this.confScheduleFri.Size = new System.Drawing.Size(15, 14);
			this.confScheduleFri.TabIndex = 9;
			this.confScheduleFri.UseVisualStyleBackColor = true;
			// 
			// confScheduleThu
			// 
			this.confScheduleThu.AutoSize = true;
			this.confScheduleThu.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.confScheduleThu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.confScheduleThu.Location = new System.Drawing.Point(296, 72);
			this.confScheduleThu.Name = "confScheduleThu";
			this.confScheduleThu.Size = new System.Drawing.Size(15, 14);
			this.confScheduleThu.TabIndex = 8;
			this.confScheduleThu.UseVisualStyleBackColor = true;
			// 
			// confScheduleWed
			// 
			this.confScheduleWed.AutoSize = true;
			this.confScheduleWed.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.confScheduleWed.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.confScheduleWed.Location = new System.Drawing.Point(241, 72);
			this.confScheduleWed.Name = "confScheduleWed";
			this.confScheduleWed.Size = new System.Drawing.Size(15, 14);
			this.confScheduleWed.TabIndex = 7;
			this.confScheduleWed.UseVisualStyleBackColor = true;
			// 
			// confScheduleTue
			// 
			this.confScheduleTue.AutoSize = true;
			this.confScheduleTue.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.confScheduleTue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.confScheduleTue.Location = new System.Drawing.Point(192, 72);
			this.confScheduleTue.Name = "confScheduleTue";
			this.confScheduleTue.Size = new System.Drawing.Size(15, 14);
			this.confScheduleTue.TabIndex = 6;
			this.confScheduleTue.UseVisualStyleBackColor = true;
			// 
			// confScheduleMon
			// 
			this.confScheduleMon.AutoSize = true;
			this.confScheduleMon.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.confScheduleMon.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.confScheduleMon.Location = new System.Drawing.Point(139, 72);
			this.confScheduleMon.Name = "confScheduleMon";
			this.confScheduleMon.Size = new System.Drawing.Size(15, 14);
			this.confScheduleMon.TabIndex = 5;
			this.confScheduleMon.UseVisualStyleBackColor = true;
			// 
			// confScheduleDaysLabel
			// 
			this.confScheduleDaysLabel.AutoSize = true;
			this.confScheduleDaysLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.confScheduleDaysLabel.Location = new System.Drawing.Point(2, 74);
			this.confScheduleDaysLabel.Name = "confScheduleDaysLabel";
			this.confScheduleDaysLabel.Size = new System.Drawing.Size(0, 13);
			this.confScheduleDaysLabel.TabIndex = 4;
			// 
			// confScheduleMaxRes
			// 
			this.confScheduleMaxRes.CheckAlign = System.Drawing.ContentAlignment.TopRight;
			this.confScheduleMaxRes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.confScheduleMaxRes.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			this.confScheduleMaxRes.Location = new System.Drawing.Point(0, 24);
			this.confScheduleMaxRes.Name = "confScheduleMaxRes";
			this.confScheduleMaxRes.Size = new System.Drawing.Size(153, 17);
			this.confScheduleMaxRes.TabIndex = 2;
			this.confScheduleMaxRes.UseVisualStyleBackColor = true;
			// 
			// confScheduleDisableOnScreensaver
			// 
			this.confScheduleDisableOnScreensaver.CheckAlign = System.Drawing.ContentAlignment.TopRight;
			this.confScheduleDisableOnScreensaver.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.confScheduleDisableOnScreensaver.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			this.confScheduleDisableOnScreensaver.Location = new System.Drawing.Point(0, 48);
			this.confScheduleDisableOnScreensaver.Name = "confScheduleDisableOnScreensaver";
			this.confScheduleDisableOnScreensaver.Size = new System.Drawing.Size(153, 17);
			this.confScheduleDisableOnScreensaver.TabIndex = 3;
			this.confScheduleDisableOnScreensaver.UseVisualStyleBackColor = true;
			// 
			// confInfoButton
			// 
			this.confInfoButton.BackColor = System.Drawing.Color.White;
			this.confInfoButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.confInfoButton.ImageDisabled = ((System.Drawing.Image)(resources.GetObject("confInfoButton.ImageDisabled")));
			this.confInfoButton.ImageHot = ((System.Drawing.Image)(resources.GetObject("confInfoButton.ImageHot")));
			this.confInfoButton.ImageNormal = ((System.Drawing.Image)(resources.GetObject("confInfoButton.ImageNormal")));
			this.confInfoButton.Location = new System.Drawing.Point(490, 5);
			this.confInfoButton.MaximumSize = new System.Drawing.Size(52, 52);
			this.confInfoButton.MinimumSize = new System.Drawing.Size(52, 52);
			this.confInfoButton.Name = "confInfoButton";
			this.confInfoButton.Size = new System.Drawing.Size(52, 52);
			this.confInfoButton.TabIndex = 10;
			this.confInfoButton.Click += new System.EventHandler(this.animatePanelIn_Click);
			// 
			// confTakePictureButton
			// 
			this.confTakePictureButton.BackColor = System.Drawing.Color.White;
			this.confTakePictureButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.confTakePictureButton.ImageDisabled = ((System.Drawing.Image)(resources.GetObject("confTakePictureButton.ImageDisabled")));
			this.confTakePictureButton.ImageHot = ((System.Drawing.Image)(resources.GetObject("confTakePictureButton.ImageHot")));
			this.confTakePictureButton.ImageNormal = ((System.Drawing.Image)(resources.GetObject("confTakePictureButton.ImageNormal")));
			this.confTakePictureButton.Location = new System.Drawing.Point(14, 5);
			this.confTakePictureButton.MaximumSize = new System.Drawing.Size(52, 52);
			this.confTakePictureButton.MinimumSize = new System.Drawing.Size(52, 52);
			this.confTakePictureButton.Name = "confTakePictureButton";
			this.confTakePictureButton.Size = new System.Drawing.Size(52, 52);
			this.confTakePictureButton.TabIndex = 7;
			this.confTakePictureButton.Click += new System.EventHandler(this.animatePanelIn_Click);
			// 
			// confRejectButton
			// 
			this.confRejectButton.BackColor = System.Drawing.Color.White;
			this.confRejectButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.confRejectButton.ImageDisabled = ((System.Drawing.Image)(resources.GetObject("confRejectButton.ImageDisabled")));
			this.confRejectButton.ImageHot = ((System.Drawing.Image)(resources.GetObject("confRejectButton.ImageHot")));
			this.confRejectButton.ImageNormal = ((System.Drawing.Image)(resources.GetObject("confRejectButton.ImageNormal")));
			this.confRejectButton.Location = new System.Drawing.Point(131, 5);
			this.confRejectButton.MaximumSize = new System.Drawing.Size(52, 52);
			this.confRejectButton.MinimumSize = new System.Drawing.Size(52, 52);
			this.confRejectButton.Name = "confRejectButton";
			this.confRejectButton.Size = new System.Drawing.Size(52, 52);
			this.confRejectButton.TabIndex = 9;
			this.confRejectButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// confAcceptButton
			// 
			this.confAcceptButton.BackColor = System.Drawing.Color.White;
			this.confAcceptButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.confAcceptButton.ImageDisabled = ((System.Drawing.Image)(resources.GetObject("confAcceptButton.ImageDisabled")));
			this.confAcceptButton.ImageHot = ((System.Drawing.Image)(resources.GetObject("confAcceptButton.ImageHot")));
			this.confAcceptButton.ImageNormal = ((System.Drawing.Image)(resources.GetObject("confAcceptButton.ImageNormal")));
			this.confAcceptButton.Location = new System.Drawing.Point(72, 5);
			this.confAcceptButton.MaximumSize = new System.Drawing.Size(52, 52);
			this.confAcceptButton.MinimumSize = new System.Drawing.Size(52, 52);
			this.confAcceptButton.Name = "confAcceptButton";
			this.confAcceptButton.Size = new System.Drawing.Size(52, 52);
			this.confAcceptButton.TabIndex = 8;
			this.confAcceptButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// confResolution
			// 
			this.confResolution.Location = new System.Drawing.Point(144, 118);
			this.confResolution.Name = "confResolution";
			this.confResolution.Size = new System.Drawing.Size(402, 74);
			this.confResolution.TabIndex = 1;
			// 
			// confAutoStart
			// 
			this.confAutoStart.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.confAutoStart.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.confAutoStart.Location = new System.Drawing.Point(22, 442);
			this.confAutoStart.Name = "confAutoStart";
			this.confAutoStart.Size = new System.Drawing.Size(514, 69);
			this.confAutoStart.TabIndex = 5;
			this.confAutoStart.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.confAutoStart.UseVisualStyleBackColor = true;
			// 
			// ui4
			// 
			this.ui4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.ui4.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ui4.Location = new System.Drawing.Point(0, 62);
			this.ui4.Name = "ui4";
			this.ui4.Size = new System.Drawing.Size(565, 1);
			this.ui4.TabIndex = 42;
			// 
			// ui1
			// 
			this.ui1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.ui1.BackColor = System.Drawing.Color.White;
			this.ui1.Location = new System.Drawing.Point(0, 0);
			this.ui1.Name = "ui1";
			this.ui1.Size = new System.Drawing.Size(566, 64);
			this.ui1.TabIndex = 6;
			// 
			// infoPanel
			// 
			this.infoPanel.Controls.Add(this.infoCopyright2Label);
			this.infoPanel.Controls.Add(this.infoWebsiteLabel);
			this.infoPanel.Controls.Add(this.infoBackButton);
			this.infoPanel.Controls.Add(this.ui6);
			this.infoPanel.Controls.Add(this.ui3);
			this.infoPanel.Controls.Add(this.infoPictureBox);
			this.infoPanel.Controls.Add(this.infoCopyright1Label);
			this.infoPanel.Location = new System.Drawing.Point(1135, 0);
			this.infoPanel.Name = "infoPanel";
			this.infoPanel.Size = new System.Drawing.Size(554, 580);
			this.infoPanel.TabIndex = 61;
			this.infoPanel.Visible = false;
			// 
			// infoCopyright2Label
			// 
			this.infoCopyright2Label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.infoCopyright2Label.Location = new System.Drawing.Point(19, 427);
			this.infoCopyright2Label.Name = "infoCopyright2Label";
			this.infoCopyright2Label.Size = new System.Drawing.Size(517, 130);
			this.infoCopyright2Label.TabIndex = 59;
			// 
			// infoWebsiteLabel
			// 
			this.infoWebsiteLabel.AutoSize = true;
			this.infoWebsiteLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.infoWebsiteLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.infoWebsiteLabel.Location = new System.Drawing.Point(19, 267);
			this.infoWebsiteLabel.Name = "infoWebsiteLabel";
			this.infoWebsiteLabel.Size = new System.Drawing.Size(0, 13);
			this.infoWebsiteLabel.TabIndex = 57;
			this.infoWebsiteLabel.UseMnemonic = false;
			this.infoWebsiteLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.websiteButton_LinkClicked);
			// 
			// infoBackButton
			// 
			this.infoBackButton.BackColor = System.Drawing.Color.White;
			this.infoBackButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.infoBackButton.ImageDisabled = ((System.Drawing.Image)(resources.GetObject("infoBackButton.ImageDisabled")));
			this.infoBackButton.ImageHot = ((System.Drawing.Image)(resources.GetObject("infoBackButton.ImageHot")));
			this.infoBackButton.ImageNormal = ((System.Drawing.Image)(resources.GetObject("infoBackButton.ImageNormal")));
			this.infoBackButton.Location = new System.Drawing.Point(14, 5);
			this.infoBackButton.MaximumSize = new System.Drawing.Size(52, 52);
			this.infoBackButton.MinimumSize = new System.Drawing.Size(52, 52);
			this.infoBackButton.Name = "infoBackButton";
			this.infoBackButton.Size = new System.Drawing.Size(52, 52);
			this.infoBackButton.TabIndex = 0;
			this.infoBackButton.Click += new System.EventHandler(this.animatePanelOut_Click);
			// 
			// ui6
			// 
			this.ui6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.ui6.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ui6.Location = new System.Drawing.Point(0, 62);
			this.ui6.Name = "ui6";
			this.ui6.Size = new System.Drawing.Size(555, 1);
			this.ui6.TabIndex = 42;
			// 
			// ui3
			// 
			this.ui3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.ui3.BackColor = System.Drawing.Color.White;
			this.ui3.Location = new System.Drawing.Point(0, 0);
			this.ui3.Name = "ui3";
			this.ui3.Size = new System.Drawing.Size(554, 64);
			this.ui3.TabIndex = 0;
			// 
			// infoPictureBox
			// 
			this.infoPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.infoPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("infoPictureBox.Image")));
			this.infoPictureBox.Location = new System.Drawing.Point(209, 83);
			this.infoPictureBox.Name = "infoPictureBox";
			this.infoPictureBox.Size = new System.Drawing.Size(128, 128);
			this.infoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.infoPictureBox.TabIndex = 58;
			this.infoPictureBox.TabStop = false;
			// 
			// infoCopyright1Label
			// 
			this.infoCopyright1Label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.infoCopyright1Label.Location = new System.Drawing.Point(19, 226);
			this.infoCopyright1Label.Name = "infoCopyright1Label";
			this.infoCopyright1Label.Size = new System.Drawing.Size(511, 40);
			this.infoCopyright1Label.TabIndex = 55;
			this.infoCopyright1Label.UseMnemonic = false;
			// 
			// helpPanel
			// 
			this.helpPanel.BackColor = System.Drawing.SystemColors.Info;
			this.helpPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.helpPanel.Location = new System.Drawing.Point(0, 501);
			this.helpPanel.Name = "helpPanel";
			this.helpPanel.Opacity = 0D;
			this.helpPanel.Size = new System.Drawing.Size(554, 96);
			this.helpPanel.TabIndex = 60;
			this.helpPanel.Title = "";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1694, 581);
			this.Controls.Add(this.helpPanel);
			this.Controls.Add(this.infoPanel);
			this.Controls.Add(this.confPanel);
			this.Controls.Add(this.takePicturePanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.mainForm_FormClosed);
			this.takePicturePanel.ResumeLayout(false);
			this.confPanel.ResumeLayout(false);
			this.confFilenamePanel.ResumeLayout(false);
			this.confFilenamePanel.PerformLayout();
			this.confFolderPanel.ResumeLayout(false);
			this.confFolderPanel.PerformLayout();
			this.confWebcamPanel.ResumeLayout(false);
			this.confWebcamPanel.PerformLayout();
			this.confSchedulePanel.ResumeLayout(false);
			this.confSchedulePanel.PerformLayout();
			this.infoPanel.ResumeLayout(false);
			this.infoPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.infoPictureBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FolderBrowserDialog confFolderDialog;
		private System.Windows.Forms.SaveFileDialog takePictureSaveDialog;
		private System.Windows.Forms.Panel takePicturePanel;
		private System.Windows.Forms.Label ui5;
		private System.Windows.Forms.Label ui2;
		private System.Windows.Forms.Panel confPanel;
		private System.Windows.Forms.CheckBox confScheduleDisableOnScreensaver;
		private System.Windows.Forms.CheckBox confAutoStart;
		private System.Windows.Forms.Label confScheduleIntervalHoursLabel;
		private System.Windows.Forms.Label confScheduleIntervalLabel;
		private System.Windows.Forms.DomainUpDown confScheduleInterval;
		private System.Windows.Forms.CheckBox confScheduleEnabled;
		private System.Windows.Forms.Label confScheduleBetweenAnd;
		private System.Windows.Forms.DateTimePicker confScheduleTimeEnd;
		private System.Windows.Forms.DateTimePicker confScheduleTimeStart;
		private System.Windows.Forms.Label confScheduleBetweenLabel;
		private System.Windows.Forms.CheckBox confScheduleSun;
		private System.Windows.Forms.CheckBox confScheduleSat;
		private System.Windows.Forms.CheckBox confScheduleFri;
		private System.Windows.Forms.CheckBox confScheduleThu;
		private System.Windows.Forms.CheckBox confScheduleWed;
		private System.Windows.Forms.CheckBox confScheduleTue;
		private System.Windows.Forms.CheckBox confScheduleMon;
		private System.Windows.Forms.Label confScheduleDaysLabel;
		private System.Windows.Forms.Label confFilenameLabel;
		private System.Windows.Forms.TextBox confFilename;
		private System.Windows.Forms.TextBox confFolder;
		private System.Windows.Forms.Label confFoldernameLabel;
		private System.Windows.Forms.Label confWebcamLabel;
		private System.Windows.Forms.ComboBox confWebcam;
		private System.Windows.Forms.Label ui4;
		private System.Windows.Forms.Label ui1;
		private System.Windows.Forms.Button confFolderButton;
		private WebcamConfigurationPicker confResolution;
		private FlatButton confRejectButton;
		private FlatButton confAcceptButton;
		private FlatButton takePictureSaveButton;
		private FlatButton takePictureBackButton;
		private FlatButton confTakePictureButton;
		private FlatButton confInfoButton;
		private CamTimer.Controls.HelpLabel helpPanel;
		private System.Windows.Forms.Panel infoPanel;
		private FlatButton infoBackButton;
		private System.Windows.Forms.Label ui6;
		private System.Windows.Forms.Label ui3;
		private System.Windows.Forms.Label infoCopyright1Label;
		private System.Windows.Forms.LinkLabel infoWebsiteLabel;
		private System.Windows.Forms.PictureBox infoPictureBox;
		private CamTimer.Controls.MousePanel confSchedulePanel;
		private CamTimer.Controls.MousePanel confWebcamPanel;
		private CamTimer.Controls.MousePanel confFolderPanel;
		private CamTimer.Controls.MousePanel confFilenamePanel;
		private System.Windows.Forms.Label infoCopyright2Label;
		private SynchronizedPictureBox takePicturePicture;
		private HorizontalSelector takePictureDisplacement;
		private System.Windows.Forms.CheckBox confScheduleMaxRes;

	}
}

