namespace MIP
{
    partial class CTBox
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CTBox));
            this.ctPictureBox = new System.Windows.Forms.PictureBox();
            this.patientGroupBox = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.ctPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ctPictureBox
            // 
            this.ctPictureBox.Location = new System.Drawing.Point(3, 3);
            this.ctPictureBox.Name = "ctPictureBox";
            this.ctPictureBox.Size = new System.Drawing.Size(245, 245);
            this.ctPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ctPictureBox.TabIndex = 0;
            this.ctPictureBox.TabStop = false;
            this.ctPictureBox.DoubleClick += new System.EventHandler(this.ctPictureBox_DoubleClick);
            // 
            // patientGroupBox
            // 
            this.patientGroupBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("patientGroupBox.BackgroundImage")));
            this.patientGroupBox.Location = new System.Drawing.Point(256, 3);
            this.patientGroupBox.Name = "patientGroupBox";
            this.patientGroupBox.Size = new System.Drawing.Size(134, 244);
            this.patientGroupBox.TabIndex = 1;
            this.patientGroupBox.TabStop = false;
            this.patientGroupBox.Text = "Name";
            // 
            // CTBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.patientGroupBox);
            this.Controls.Add(this.ctPictureBox);
            this.Name = "CTBox";
            this.Size = new System.Drawing.Size(395, 250);
            ((System.ComponentModel.ISupportInitialize)(this.ctPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ctPictureBox;
        private System.Windows.Forms.GroupBox patientGroupBox;
    }
}
