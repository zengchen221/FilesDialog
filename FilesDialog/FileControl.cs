using System;
using System.Windows.Forms;
using System.IO;

namespace FilesDialog
{
    public partial class FileControl : UserControl
    {
        public bool isChoosed = false;     //是否被选中
        public bool isDirectory = false;   //是否为目录        

        public FileControl()
        {
            InitializeComponent();

            this.Width = 125;
            this.Height = 130;
            this.Margin = new Padding(10);

            this.lab_fileName.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.filePanel.BackColor = System.Drawing.Color.Transparent;

            this.pictureBox1.Cursor = Cursors.Hand;
            this.lab_fileName.Cursor = Cursors.Hand;
            this.filePanel.Cursor = Cursors.Hand;
            
            this.lab_fileFullName.Visible = false;                        
        }

        /// <summary>
        /// 构造方法，对于文件夹或者文件
        /// </summary>        
        /// <param name="fileName"></param>
        public FileControl(string filePath) : this()
        {
            
            ToolTip tooltip = new ToolTip();
            string caption = "";

            FileInfo fileInfo = new FileInfo(filePath);
            string fileName = fileInfo.Name;
            
            if (Directory.Exists(filePath))             //该项是文件夹
            {               
                this.isDirectory = true;
                caption = "文件夹: " + fileName;
                SetPanelStyle(Resource1._f_folder);
            }
            else                                         //该项是文件
            {   
                this.isDirectory = false;
                switch (fileInfo.Extension)
                {
                    case ".doc":
                    case ".docx":
                        SetPanelStyle(Resource1._f_word);
                        break;

                    case ".htm":
                    case ".html":
                        SetPanelStyle(Resource1._f_word);
                        break;

                    case ".txt":
                        SetPanelStyle(Resource1._f_txt);
                        break;

                    case ".mp3":
                        SetPanelStyle(Resource1._f_word);
                        break;

                    case ".jpg":
                        SetPanelStyle(Resource1._f_word);
                        break;
                }
                fileName = fileName.Substring(0, fileName.LastIndexOf("."));
                caption = "文件: " + fileName;
                caption += "\r\n" + "大小: " + getsizeStr(fileInfo.Length);                  
            }
            
            
          //  fileName = fileName.Length > 12 ? fileName.Substring(0, 11) + "..." : fileName;
            this.lab_fileName.Text = fileName;
            this.lab_fileFullName.Text = filePath;
            
            string createTime = fileInfo.CreationTime.ToShortDateString() + " " + fileInfo.CreationTime.ToShortTimeString();
            caption += "\r\n" + "创建日期: " + createTime;

            tooltip.SetToolTip(this.pictureBox1, caption);
            tooltip.SetToolTip(this.lab_fileName, caption);
            tooltip.SetToolTip(this.filePanel, caption);
        }



        /// <summary>
        /// 构造方法，对于驱动器目录
        /// </summary>
        /// <param name="driveName">驱动器名称</param>
        /// <param name="volumeLabel">驱动器卷标</param>
        public FileControl(string driveName, string volumeLabel) : this()
        {
            this.isDirectory = true;
            this.lab_fileName.Text = volumeLabel + "(" + driveName.Substring(0, 2) + ")";
            this.lab_fileFullName.Text = driveName.Substring(0, 2);            
            SetPanelStyle(Resource1._f_disk);
        }

        /// <summary>
        /// 设置Panel样式
        /// </summary>
        /// <param name="resource"></param>
        private void SetPanelStyle(System.Drawing.Bitmap resource)
        {
            this.filePanel.Width = 121;   //固定，大小同背景图像
            this.filePanel.Height = 126;
            this.filePanel.Left = (this.Width - this.filePanel.Width) / 2;
            this.filePanel.Top = (this.Height - this.filePanel.Height) / 2;

            this.pictureBox1.Width = resource.Width;
            this.pictureBox1.Height = resource.Height;
            this.pictureBox1.Image = resource;

            this.pictureBox1.Left = (this.filePanel.Width - pictureBox1.Width) / 2;
            this.pictureBox1.Top = 20;

            this.lab_fileName.AutoSize = false;
            this.lab_fileName.AutoEllipsis = true;
            this.lab_fileName.Width = 90;
            this.lab_fileName.Height = 26;
            this.lab_fileName.Left = (this.filePanel.Width - this.lab_fileName.Width) / 2;
            this.lab_fileName.Top = this.pictureBox1.Bottom + 5;            
            this.lab_fileName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        }


        /// <summary>
        /// 获取文件大小字符串标识【1GB以内】
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public string getsizeStr(long Lsize)
        {
            if (Lsize < Math.Pow(2, 10) && Lsize >= 0)
            {
                return Lsize + "字节";
            }
            else if (Lsize >= Math.Pow(2, 10) && Lsize < Math.Pow(2, 20))
            {
                return Convert.ToString(Math.Round(Lsize / Math.Pow(2, 10), 2)) + "KB";
            }
            else if (Lsize >= Math.Pow(2, 20) && Lsize < Math.Pow(2, 30))
            {
                return Convert.ToString(Math.Round(Lsize / Math.Pow(2, 20), 2)) + "MB";
            }
            else
            {
                return "超出限制";
            }
        }


        private void FileControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.OnMouseDoubleClick(e);
        }
        
        private void FileControl_MouseClick(object sender, MouseEventArgs e)
        {
            this.OnMouseClick(e);
        }

        private void FileControl_MouseEnter(object sender, EventArgs e)
        {
            SetFileControlStyle(true);
        }


        private void FileControl_MouseLeave(object sender, EventArgs e)
        {
            SetFileControlStyle(false);
        }


        public void SetFileControlStyle(bool style)
        {
            if (this.isChoosed == false)
            {
                if (style)      //设置背景图像
                {
                    this.filePanel.BackgroundImage = Resource1._f_layer_1;
                }
                else              //清除背景图像
                {
                    this.filePanel.BackgroundImage = null;
                    this.filePanel.BackColor = System.Drawing.Color.Transparent;
                }
            }
            else
            {
                this.filePanel.BackgroundImage = Resource1._f_layer_2;
            }
        }
    }
}