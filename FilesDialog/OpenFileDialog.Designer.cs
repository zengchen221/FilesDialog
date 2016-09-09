namespace FilesDialog
{
    partial class OpenFileDialog
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
            this.button1 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_lastStep = new System.Windows.Forms.Label();
            this.btn_nextStep = new System.Windows.Forms.Button();
            this.btn_desktop = new System.Windows.Forms.Button();
            this.cBox_checkAll = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(37, 65);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "本地磁盘";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(118, 39);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(428, 286);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // btn_lastStep
            // 
            this.btn_lastStep.AutoSize = true;
            this.btn_lastStep.Location = new System.Drawing.Point(590, 591);
            this.btn_lastStep.Name = "btn_lastStep";
            this.btn_lastStep.Size = new System.Drawing.Size(41, 12);
            this.btn_lastStep.TabIndex = 5;
            this.btn_lastStep.Text = "上一步";
            this.btn_lastStep.Click += new System.EventHandler(this.btn_lastStep_Click);
            // 
            // btn_nextStep
            // 
            this.btn_nextStep.Location = new System.Drawing.Point(731, 586);
            this.btn_nextStep.Name = "btn_nextStep";
            this.btn_nextStep.Size = new System.Drawing.Size(75, 23);
            this.btn_nextStep.TabIndex = 6;
            this.btn_nextStep.Text = "确定";
            this.btn_nextStep.UseVisualStyleBackColor = true;
            this.btn_nextStep.Click += new System.EventHandler(this.btn_nextStep_Click);
            // 
            // btn_desktop
            // 
            this.btn_desktop.Location = new System.Drawing.Point(37, 135);
            this.btn_desktop.Name = "btn_desktop";
            this.btn_desktop.Size = new System.Drawing.Size(75, 23);
            this.btn_desktop.TabIndex = 7;
            this.btn_desktop.Text = "桌面";
            this.btn_desktop.UseVisualStyleBackColor = true;
            this.btn_desktop.Click += new System.EventHandler(this.btn_desktop_Click);
            // 
            // cBox_checkAll
            // 
            this.cBox_checkAll.AutoSize = true;
            this.cBox_checkAll.Location = new System.Drawing.Point(34, 326);
            this.cBox_checkAll.Name = "cBox_checkAll";
            this.cBox_checkAll.Size = new System.Drawing.Size(48, 16);
            this.cBox_checkAll.TabIndex = 11;
            this.cBox_checkAll.Text = "全选";
            this.cBox_checkAll.UseVisualStyleBackColor = true;
            this.cBox_checkAll.CheckedChanged += new System.EventHandler(this.cBox_checkAll_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 474);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // OpenFileDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 645);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.cBox_checkAll);
            this.Controls.Add(this.btn_desktop);
            this.Controls.Add(this.btn_nextStep);
            this.Controls.Add(this.btn_lastStep);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.button1);
            this.Name = "OpenFileDialog";
            this.Text = "OpenFileDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label btn_lastStep;
        private System.Windows.Forms.Button btn_nextStep;
        private System.Windows.Forms.Button btn_desktop;
        private System.Windows.Forms.CheckBox cBox_checkAll;
        private System.Windows.Forms.Button button2;
    }
}