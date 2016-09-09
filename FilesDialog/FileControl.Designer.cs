namespace FilesDialog
{
    partial class FileControl
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lab_fileFullName = new System.Windows.Forms.Label();
            this.filePanel = new System.Windows.Forms.Panel();
            this.lab_fileName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.filePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(22, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(101, 90);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FileControl_MouseClick);
            this.pictureBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FileControl_MouseDoubleClick);
            this.pictureBox1.MouseEnter += new System.EventHandler(this.FileControl_MouseEnter);
            this.pictureBox1.MouseLeave += new System.EventHandler(this.FileControl_MouseLeave);
            // 
            // lab_fileFullName
            // 
            this.lab_fileFullName.AutoSize = true;
            this.lab_fileFullName.Location = new System.Drawing.Point(98, 234);
            this.lab_fileFullName.Name = "lab_fileFullName";
            this.lab_fileFullName.Size = new System.Drawing.Size(77, 12);
            this.lab_fileFullName.TabIndex = 2;
            this.lab_fileFullName.Text = "fileFullName";
            // 
            // filePanel
            // 
            this.filePanel.Controls.Add(this.lab_fileName);
            this.filePanel.Controls.Add(this.pictureBox1);
            this.filePanel.Location = new System.Drawing.Point(43, 15);
            this.filePanel.Name = "filePanel";
            this.filePanel.Size = new System.Drawing.Size(200, 175);
            this.filePanel.TabIndex = 3;
            this.filePanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FileControl_MouseClick);
            this.filePanel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FileControl_MouseDoubleClick);
            this.filePanel.MouseEnter += new System.EventHandler(this.FileControl_MouseEnter);
            this.filePanel.MouseLeave += new System.EventHandler(this.FileControl_MouseLeave);
            // 
            // lab_fileName
            // 
            this.lab_fileName.AutoEllipsis = true;
            this.lab_fileName.AutoSize = true;
            this.lab_fileName.Location = new System.Drawing.Point(33, 126);
            this.lab_fileName.Name = "lab_fileName";
            this.lab_fileName.Size = new System.Drawing.Size(41, 12);
            this.lab_fileName.TabIndex = 1;
            this.lab_fileName.Text = "label1";
            this.lab_fileName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FileControl_MouseClick);
            this.lab_fileName.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FileControl_MouseDoubleClick);
            this.lab_fileName.MouseEnter += new System.EventHandler(this.FileControl_MouseEnter);
            this.lab_fileName.MouseLeave += new System.EventHandler(this.FileControl_MouseLeave);
            // 
            // FileControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.filePanel);
            this.Controls.Add(this.lab_fileFullName);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "FileControl";
            this.Size = new System.Drawing.Size(280, 288);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.filePanel.ResumeLayout(false);
            this.filePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Label lab_fileFullName;
        public System.Windows.Forms.Panel filePanel;
        private System.Windows.Forms.Label lab_fileName;
    }
}
