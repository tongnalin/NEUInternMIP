namespace MIP
{
    partial class PictureWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PictureWindow));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.windowStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.WriteBackButton = new System.Windows.Forms.Button();
            this.saveToDiskButton = new System.Windows.Forms.Button();
            this.drawLineButton = new System.Windows.Forms.Button();
            this.EllipseButton = new System.Windows.Forms.Button();
            this.RectangleButton = new System.Windows.Forms.Button();
            this.drawGroupBox = new System.Windows.Forms.GroupBox();
            this.drawPolygonButton = new System.Windows.Forms.Button();
            this.textButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.undoButton = new System.Windows.Forms.Button();
            this.pictureBoxPanel = new System.Windows.Forms.Panel();
            this.setWindowButton = new System.Windows.Forms.Button();
            this.rotateButton = new System.Windows.Forms.Button();
            this.transformGroupBox = new System.Windows.Forms.GroupBox();
            this.zoomButton = new System.Windows.Forms.Button();
            this.windowWidthTrackBar = new System.Windows.Forms.TrackBar();
            this.windowCenterTrackBar = new System.Windows.Forms.TrackBar();
            this.windowWidthLabel = new System.Windows.Forms.Label();
            this.windowCenterLabel = new System.Windows.Forms.Label();
            this.previousPicButton = new System.Windows.Forms.Button();
            this.nextPicButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.drawGroupBox.SuspendLayout();
            this.pictureBoxPanel.SuspendLayout();
            this.transformGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.windowWidthTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.windowCenterTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pictureBox1.Location = new System.Drawing.Point(33, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(512, 512);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.windowStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 577);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(930, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // windowStatusLabel
            // 
            this.windowStatusLabel.Name = "windowStatusLabel";
            this.windowStatusLabel.Size = new System.Drawing.Size(135, 17);
            this.windowStatusLabel.Text = "Image Window Status";
            // 
            // WriteBackButton
            // 
            this.WriteBackButton.Image = ((System.Drawing.Image)(resources.GetObject("WriteBackButton.Image")));
            this.WriteBackButton.Location = new System.Drawing.Point(770, 488);
            this.WriteBackButton.Name = "WriteBackButton";
            this.WriteBackButton.Size = new System.Drawing.Size(75, 23);
            this.WriteBackButton.TabIndex = 2;
            this.WriteBackButton.Text = "保存";
            this.WriteBackButton.UseVisualStyleBackColor = true;
            this.WriteBackButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // saveToDiskButton
            // 
            this.saveToDiskButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToDiskButton.Image")));
            this.saveToDiskButton.Location = new System.Drawing.Point(770, 517);
            this.saveToDiskButton.Name = "saveToDiskButton";
            this.saveToDiskButton.Size = new System.Drawing.Size(75, 23);
            this.saveToDiskButton.TabIndex = 3;
            this.saveToDiskButton.Text = "保存到磁盘";
            this.saveToDiskButton.UseVisualStyleBackColor = true;
            this.saveToDiskButton.Click += new System.EventHandler(this.saveToDiskButton_Click);
            // 
            // drawLineButton
            // 
            this.drawLineButton.Image = ((System.Drawing.Image)(resources.GetObject("drawLineButton.Image")));
            this.drawLineButton.Location = new System.Drawing.Point(26, 20);
            this.drawLineButton.Name = "drawLineButton";
            this.drawLineButton.Size = new System.Drawing.Size(75, 23);
            this.drawLineButton.TabIndex = 4;
            this.drawLineButton.Text = "直线";
            this.drawLineButton.UseVisualStyleBackColor = true;
            this.drawLineButton.Click += new System.EventHandler(this.ShapeButton_Click);
            // 
            // EllipseButton
            // 
            this.EllipseButton.Image = ((System.Drawing.Image)(resources.GetObject("EllipseButton.Image")));
            this.EllipseButton.Location = new System.Drawing.Point(26, 52);
            this.EllipseButton.Name = "EllipseButton";
            this.EllipseButton.Size = new System.Drawing.Size(75, 23);
            this.EllipseButton.TabIndex = 5;
            this.EllipseButton.Text = "椭圆";
            this.EllipseButton.UseVisualStyleBackColor = true;
            this.EllipseButton.Click += new System.EventHandler(this.ShapeButton_Click);
            // 
            // RectangleButton
            // 
            this.RectangleButton.Image = ((System.Drawing.Image)(resources.GetObject("RectangleButton.Image")));
            this.RectangleButton.Location = new System.Drawing.Point(26, 83);
            this.RectangleButton.Name = "RectangleButton";
            this.RectangleButton.Size = new System.Drawing.Size(75, 23);
            this.RectangleButton.TabIndex = 6;
            this.RectangleButton.Text = "矩形";
            this.RectangleButton.UseVisualStyleBackColor = true;
            this.RectangleButton.Click += new System.EventHandler(this.ShapeButton_Click);
            // 
            // drawGroupBox
            // 
            this.drawGroupBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.drawGroupBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("drawGroupBox.BackgroundImage")));
            this.drawGroupBox.Controls.Add(this.drawPolygonButton);
            this.drawGroupBox.Controls.Add(this.textButton);
            this.drawGroupBox.Controls.Add(this.clearButton);
            this.drawGroupBox.Controls.Add(this.undoButton);
            this.drawGroupBox.Controls.Add(this.drawLineButton);
            this.drawGroupBox.Controls.Add(this.RectangleButton);
            this.drawGroupBox.Controls.Add(this.EllipseButton);
            this.drawGroupBox.Location = new System.Drawing.Point(40, 146);
            this.drawGroupBox.Name = "drawGroupBox";
            this.drawGroupBox.Size = new System.Drawing.Size(124, 241);
            this.drawGroupBox.TabIndex = 7;
            this.drawGroupBox.TabStop = false;
            this.drawGroupBox.Text = "标记";
            // 
            // drawPolygonButton
            // 
            this.drawPolygonButton.Image = ((System.Drawing.Image)(resources.GetObject("drawPolygonButton.Image")));
            this.drawPolygonButton.Location = new System.Drawing.Point(26, 200);
            this.drawPolygonButton.Name = "drawPolygonButton";
            this.drawPolygonButton.Size = new System.Drawing.Size(75, 23);
            this.drawPolygonButton.TabIndex = 10;
            this.drawPolygonButton.Text = "多边形";
            this.drawPolygonButton.UseVisualStyleBackColor = true;
            this.drawPolygonButton.Click += new System.EventHandler(this.drawPolygonButton_Click);
            // 
            // textButton
            // 
            this.textButton.Image = ((System.Drawing.Image)(resources.GetObject("textButton.Image")));
            this.textButton.Location = new System.Drawing.Point(26, 112);
            this.textButton.Name = "textButton";
            this.textButton.Size = new System.Drawing.Size(75, 23);
            this.textButton.TabIndex = 9;
            this.textButton.Text = "文字";
            this.textButton.UseVisualStyleBackColor = true;
            this.textButton.Click += new System.EventHandler(this.textButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Image = ((System.Drawing.Image)(resources.GetObject("clearButton.Image")));
            this.clearButton.Location = new System.Drawing.Point(26, 171);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 8;
            this.clearButton.Text = "清除";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // undoButton
            // 
            this.undoButton.Image = ((System.Drawing.Image)(resources.GetObject("undoButton.Image")));
            this.undoButton.Location = new System.Drawing.Point(26, 141);
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(75, 23);
            this.undoButton.TabIndex = 7;
            this.undoButton.Text = "撤销";
            this.undoButton.UseVisualStyleBackColor = true;
            this.undoButton.Click += new System.EventHandler(this.undoButton_Click);
            // 
            // pictureBoxPanel
            // 
            this.pictureBoxPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pictureBoxPanel.Controls.Add(this.pictureBox1);
            this.pictureBoxPanel.Location = new System.Drawing.Point(170, 8);
            this.pictureBoxPanel.Name = "pictureBoxPanel";
            this.pictureBoxPanel.Size = new System.Drawing.Size(579, 512);
            this.pictureBoxPanel.TabIndex = 8;
            // 
            // setWindowButton
            // 
            this.setWindowButton.Image = ((System.Drawing.Image)(resources.GetObject("setWindowButton.Image")));
            this.setWindowButton.Location = new System.Drawing.Point(23, 29);
            this.setWindowButton.Name = "setWindowButton";
            this.setWindowButton.Size = new System.Drawing.Size(75, 23);
            this.setWindowButton.TabIndex = 9;
            this.setWindowButton.Text = "调窗";
            this.setWindowButton.UseVisualStyleBackColor = true;
            this.setWindowButton.Click += new System.EventHandler(this.setWindowButton_Click);
            // 
            // rotateButton
            // 
            this.rotateButton.Image = ((System.Drawing.Image)(resources.GetObject("rotateButton.Image")));
            this.rotateButton.Location = new System.Drawing.Point(23, 58);
            this.rotateButton.Name = "rotateButton";
            this.rotateButton.Size = new System.Drawing.Size(75, 23);
            this.rotateButton.TabIndex = 11;
            this.rotateButton.Text = "旋转";
            this.rotateButton.UseVisualStyleBackColor = true;
            this.rotateButton.Click += new System.EventHandler(this.rotateButton_1);
            // 
            // transformGroupBox
            // 
            this.transformGroupBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.transformGroupBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("transformGroupBox.BackgroundImage")));
            this.transformGroupBox.Controls.Add(this.zoomButton);
            this.transformGroupBox.Controls.Add(this.setWindowButton);
            this.transformGroupBox.Controls.Add(this.rotateButton);
            this.transformGroupBox.Location = new System.Drawing.Point(40, 422);
            this.transformGroupBox.Name = "transformGroupBox";
            this.transformGroupBox.Size = new System.Drawing.Size(124, 137);
            this.transformGroupBox.TabIndex = 12;
            this.transformGroupBox.TabStop = false;
            this.transformGroupBox.Text = "变换";
            // 
            // zoomButton
            // 
            this.zoomButton.Image = ((System.Drawing.Image)(resources.GetObject("zoomButton.Image")));
            this.zoomButton.Location = new System.Drawing.Point(23, 87);
            this.zoomButton.Name = "zoomButton";
            this.zoomButton.Size = new System.Drawing.Size(75, 23);
            this.zoomButton.TabIndex = 12;
            this.zoomButton.Text = "放缩";
            this.zoomButton.UseVisualStyleBackColor = true;
            this.zoomButton.Click += new System.EventHandler(this.zoomButton_Click);
            // 
            // windowWidthTrackBar
            // 
            this.windowWidthTrackBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.windowWidthTrackBar.Location = new System.Drawing.Point(755, 41);
            this.windowWidthTrackBar.Maximum = 1000;
            this.windowWidthTrackBar.Name = "windowWidthTrackBar";
            this.windowWidthTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.windowWidthTrackBar.Size = new System.Drawing.Size(45, 388);
            this.windowWidthTrackBar.TabIndex = 13;
            this.windowWidthTrackBar.Value = 600;
            this.windowWidthTrackBar.Scroll += new System.EventHandler(this.windowTrackbar_Scroll);
            this.windowWidthTrackBar.ValueChanged += new System.EventHandler(this.windowTrackbar_Scroll);
            // 
            // windowCenterTrackBar
            // 
            this.windowCenterTrackBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.windowCenterTrackBar.Location = new System.Drawing.Point(830, 41);
            this.windowCenterTrackBar.Maximum = 3000;
            this.windowCenterTrackBar.Name = "windowCenterTrackBar";
            this.windowCenterTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.windowCenterTrackBar.Size = new System.Drawing.Size(45, 388);
            this.windowCenterTrackBar.TabIndex = 14;
            this.windowCenterTrackBar.Value = 400;
            this.windowCenterTrackBar.Scroll += new System.EventHandler(this.windowTrackbar_Scroll);
            this.windowCenterTrackBar.ValueChanged += new System.EventHandler(this.windowTrackbar_Scroll);
            // 
            // windowWidthLabel
            // 
            this.windowWidthLabel.AutoSize = true;
            this.windowWidthLabel.Location = new System.Drawing.Point(756, 450);
            this.windowWidthLabel.Name = "windowWidthLabel";
            this.windowWidthLabel.Size = new System.Drawing.Size(29, 12);
            this.windowWidthLabel.TabIndex = 15;
            this.windowWidthLabel.Text = "窗宽";
            // 
            // windowCenterLabel
            // 
            this.windowCenterLabel.AutoSize = true;
            this.windowCenterLabel.Location = new System.Drawing.Point(827, 450);
            this.windowCenterLabel.Name = "windowCenterLabel";
            this.windowCenterLabel.Size = new System.Drawing.Size(29, 12);
            this.windowCenterLabel.TabIndex = 15;
            this.windowCenterLabel.Text = "窗位";
            // 
            // previousPicButton
            // 
            this.previousPicButton.Image = ((System.Drawing.Image)(resources.GetObject("previousPicButton.Image")));
            this.previousPicButton.Location = new System.Drawing.Point(381, 532);
            this.previousPicButton.Name = "previousPicButton";
            this.previousPicButton.Size = new System.Drawing.Size(52, 32);
            this.previousPicButton.TabIndex = 10;
            this.previousPicButton.UseVisualStyleBackColor = true;
            this.previousPicButton.Click += new System.EventHandler(this.navigatePicButton_Click);
            // 
            // nextPicButton
            // 
            this.nextPicButton.Image = ((System.Drawing.Image)(resources.GetObject("nextPicButton.Image")));
            this.nextPicButton.Location = new System.Drawing.Point(472, 532);
            this.nextPicButton.Name = "nextPicButton";
            this.nextPicButton.Size = new System.Drawing.Size(52, 32);
            this.nextPicButton.TabIndex = 10;
            this.nextPicButton.UseVisualStyleBackColor = true;
            this.nextPicButton.Click += new System.EventHandler(this.navigatePicButton_Click);
            // 
            // PictureWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(930, 599);
            this.Controls.Add(this.windowCenterLabel);
            this.Controls.Add(this.windowWidthLabel);
            this.Controls.Add(this.windowCenterTrackBar);
            this.Controls.Add(this.windowWidthTrackBar);
            this.Controls.Add(this.transformGroupBox);
            this.Controls.Add(this.nextPicButton);
            this.Controls.Add(this.previousPicButton);
            this.Controls.Add(this.pictureBoxPanel);
            this.Controls.Add(this.drawGroupBox);
            this.Controls.Add(this.saveToDiskButton);
            this.Controls.Add(this.WriteBackButton);
            this.Controls.Add(this.statusStrip1);
            this.Name = "PictureWindow";
            this.Text = "PictureWindow";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureWindow_Paint);
            this.Resize += new System.EventHandler(this.PictureWindow_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.drawGroupBox.ResumeLayout(false);
            this.pictureBoxPanel.ResumeLayout(false);
            this.transformGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.windowWidthTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.windowCenterTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel windowStatusLabel;
        private System.Windows.Forms.Button WriteBackButton;
        private System.Windows.Forms.Button saveToDiskButton;
        private System.Windows.Forms.Button drawLineButton;
        private System.Windows.Forms.Button EllipseButton;
        private System.Windows.Forms.Button RectangleButton;
        private System.Windows.Forms.GroupBox drawGroupBox;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button undoButton;
        private System.Windows.Forms.Panel pictureBoxPanel;
        private System.Windows.Forms.Button setWindowButton;
        private System.Windows.Forms.Button rotateButton;
        private System.Windows.Forms.GroupBox transformGroupBox;
        private System.Windows.Forms.Button zoomButton;
        private System.Windows.Forms.TrackBar windowWidthTrackBar;
        private System.Windows.Forms.TrackBar windowCenterTrackBar;
        private System.Windows.Forms.Label windowWidthLabel;
        private System.Windows.Forms.Label windowCenterLabel;
        private System.Windows.Forms.Button textButton;
        private System.Windows.Forms.Button previousPicButton;
        private System.Windows.Forms.Button nextPicButton;
        private System.Windows.Forms.Button drawPolygonButton;
    }
}