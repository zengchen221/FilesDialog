using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FilesDialog
{
    public partial class OpenFileDialog : Form
    {
        #region 初始化

        string currentPath;            //记录当前目录路径
        bool isMultiple;               //是否支持多选（圈选）        
        string fileExtension;          //文件后缀筛选
        List<int> selectedIndexList;   //记录被选中项序号
        bool isDirectory ;             //当前选择的是否为文件夹
        public List<string> wishToSelectFileList;   //欲选中的文件(文件全路径)
        List<FileInfo> fileList;              

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="index">表示文件类型（0为文本，1为图片，2为音频）</param>
        /// isMultiple          是否支持多选
        public OpenFileDialog(string title,int index,bool isMultiple)
        {
            CreatePanelControl();
            InitializeComponent();

            //初始化对话框样式
            this.Text = title;
            this.TopMost = true;
            this.Width = 740;
            this.Height = 560;

            InitializeAttributeValue(index, isMultiple);
            InitializPanelStyle(title);
            button1_Click(null, null);
        }

        /// <summary>
        /// 初始化Panel样式
        /// </summary>
        private void InitializPanelStyle(string title)
        {            
            this.flowLayoutPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.flowLayoutPanel1_MouseDown);
            this.flowLayoutPanel1.MouseEnter += new System.EventHandler(this.flowLayoutPanel1_MouseEnter);
            this.flowLayoutPanel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.flowLayoutPanel1_MouseUp);
            this.flowLayoutPanel1.Width = 580;
            this.flowLayoutPanel1.Height = 500;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.flowLayoutPanel1.AutoScroll = true;                       
        }


        /// <summary>
        /// 初始化属性值
        /// </summary>
        private void InitializeAttributeValue(int index, bool isMultiple)
        {
            //设置文件后缀
            switch (index)
            {
                case 0: fileExtension = "*.html;*.htm;*.doc;*.docx;*.txt"; break;
                case 1: fileExtension = "*.jpg;*.jpeg;*.png;*.bmp;*.gif;"; break;
                case 2: fileExtension = ".mp3"; break;
            }

            //是否支持多选
            this.isMultiple = isMultiple;
            
            selectedIndexList = new List<int>();
            wishToSelectFileList = new List<string>();
            isDirectory = false;            
        }

        //获取焦点，支持滚轮滑动
        private void flowLayoutPanel1_MouseEnter(object sender, EventArgs e)
        {
            this.flowLayoutPanel1.Focus();
        }

        #endregion

        /// <summary>
        /// 点击“我的电脑”
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {            
            currentPath = "";
            ShowFilesInDirectory();
        }

        /// <summary>
        /// 点击“桌面”
        /// </summary>
        private void btn_desktop_Click(object sender, EventArgs e)
        {
            currentPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            ShowFilesInDirectory();
        }

        /// <summary>
        /// 点击“上一步”
        /// </summary>
        private void btn_lastStep_Click(object sender, EventArgs e)
        {
            currentPath = currentPath.TrimEnd('\\');
            currentPath = currentPath.Substring(0, currentPath.LastIndexOf('\\') + 1);
            ShowFilesInDirectory();
        }

        #region 双击、确定

        /// <summary>
        /// 点击“确定”
        /// </summary>
        private void btn_nextStep_Click(object sender, EventArgs e)
        {
            if (isDirectory)  
            {
                FileControl fc = (FileControl)this.flowLayoutPanel1.Controls[selectedIndexList[0]];
                currentPath = fc.lab_fileFullName.Text;
                ShowFilesInDirectory();
            }
            else           
            {
                FileSelected();
                this.Close();
            }
        }
        
        /// <summary>
        /// 双击文件夹或者文件时
        /// </summary>
        private void FileSelected_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FileControl fc = (FileControl)sender;
            if (fc.isDirectory)
            {
                currentPath = fc.lab_fileFullName.Text + "\\";
                ShowFilesInDirectory();
            }
            else
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control ) 
                {
                    return;
                }
                FileSelected(); 
                this.Close();
            }
        }
        
        /// <summary>
        /// 显示当前目录下的所有文件和目录
        /// </summary>
        private void ShowFilesInDirectory()
        {
            this.selectedIndexList.Clear();
            this.flowLayoutPanel1.Controls.Clear();
            this.cBox_checkAll.Visible = isMultiple && !string.IsNullOrEmpty(currentPath) ? true : false;
            this.cBox_checkAll.Checked = false;
            this.btn_nextStep.Text = "确定";

            if (string.IsNullOrEmpty(currentPath))
            {
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                foreach (DriveInfo driver in allDrives)
                {
                    if (driver.DriveType == DriveType.Fixed || driver.DriveType == DriveType.Removable)
                    {
                        AddControlToPanel(new FileControl(driver.Name, driver.VolumeLabel));
                    }
                }
            }
            else
            {
                DirectoryInfo dir = new DirectoryInfo(currentPath);
                FileSystemInfo[] files = dir.GetFileSystemInfos();
                foreach (FileSystemInfo file in files)
                {
                    string filePath = file.FullName;
                    if ((File.GetAttributes(filePath) & FileAttributes.Hidden) == FileAttributes.Hidden) //隐藏文件
                    {
                        continue;
                    }
                    
                    if (string.IsNullOrEmpty(file.Extension) && !Directory.Exists(filePath)) //没有后缀但不是文件夹
                    {
                        continue;
                    }
                    
                    if (!string.IsNullOrEmpty(file.Extension) && fileExtension.IndexOf(file.Extension) < 0) //后缀过滤
                    {
                        continue;
                    }

                    AddControlToPanel(new FileControl(filePath));
                }
            }
            SetFilesWishedToSelect();
        }

        private void FileSelected()
        {
            fileList = new List<FileInfo>();
            foreach (int i in selectedIndexList)
            {
                FileControl fc = (FileControl)this.flowLayoutPanel1.Controls[i];
                string file = fc.lab_fileFullName.Text;
                fileList.Add(new FileInfo(file));
            }
        }

        public void AddControlToPanel(FileControl fileControl)
        {
            fileControl.MouseDoubleClick += FileSelected_MouseDoubleClick;
            fileControl.MouseClick += FileControl_MouseClick;
            flowLayoutPanel1.Controls.Add(fileControl);
            fileControl.BackColor = Color.Transparent;
            fileControl.SetFileControlStyle(false);
        }


        /// <summary>
        /// 通过文件名设置某个文件夹下的选中文件
        /// </summary>
        /// <param name="fileFullName">文件全路径集合</param>
        private void SetFilesWishedToSelect()
        {
            if (wishToSelectFileList.Count == 0)
            {
                return;
            }
            foreach (FileControl fc in this.flowLayoutPanel1.Controls)
            {
                if (!fc.isDirectory)
                {
                    string file = fc.lab_fileFullName.Text;
                    if (wishToSelectFileList.IndexOf(file) > -1)
                    {
                        SetSelectedItem(fc);
                    }
                }
            }
            isDirectory = false;
        }


        /// <summary>
        /// 返回选中文件
        /// </summary>
        /// <returns></returns>
        public List<FileInfo> GetSelectedFileList()
        {
            return fileList;
        }


        #endregion

        #region 单击
        /// <summary>
        /// 单击文件或者文件夹时
        /// </summary>
        private void FileControl_MouseClick(object sender, MouseEventArgs e)
        {
            this.cBox_checkAll.Checked = false;

            FileControl fc = (FileControl)sender;
            if (!fc.isChoosed)  
            {
                if (fc.isDirectory == false && (Control.ModifierKeys & Keys.Control) == Keys.Control && isMultiple) //CTRL同时被按下，多选
                {
                    if (isDirectory)
                    {
                        ClearAllSelectedItems();
                    }
                    SetSelectedItem(fc);
                }
                else
                {
                    ClearAllSelectedItems();
                    SetSelectedItem(fc);
                }
                isDirectory = fc.isDirectory;
                this.btn_nextStep.Text = fc.isDirectory ? "打开" : "确定";
            }
            else
            {
                if (fc.isDirectory == false && (Control.ModifierKeys & Keys.Control) == Keys.Control && isMultiple) //CTRL被按下
                {
                    ClearSelectedItem(fc);
                }
                else
                {
                    ClearAllSelectedItems();
                    SetSelectedItem(fc);
                }                
            }            
        }

        private void SetSelectedItem(FileControl fc)
        {
            int index = this.flowLayoutPanel1.Controls.IndexOf(fc);
            selectedIndexList.Add(index);
            fc.isChoosed = true;
            fc.SetFileControlStyle(true);            
        }

        private void ClearAllSelectedItems()
        {
            foreach (int index in selectedIndexList)
            {
                FileControl fc = (FileControl)this.flowLayoutPanel1.Controls[index];
                fc.isChoosed = false;
                fc.SetFileControlStyle(false);
                CleaerItemInWishToSelectFileList(fc.lab_fileFullName.Text);
            }
            selectedIndexList.Clear();
        }
        

        private void ClearSelectedItem(FileControl fc)
        {
            fc.isChoosed = false;
            int index = this.flowLayoutPanel1.Controls.IndexOf(fc);
            selectedIndexList.Remove(index);
            fc.SetFileControlStyle(false);
            CleaerItemInWishToSelectFileList(fc.lab_fileFullName.Text);
        }

        private void CleaerItemInWishToSelectFileList(string file)
        {
            if (wishToSelectFileList.Count > 0)
            {
                if (wishToSelectFileList.IndexOf(file) > -1)
                {
                    wishToSelectFileList.Remove(file);
                }
            }
        }

        #endregion

        #region 圈中


        Timer timer = new Timer();
        Point start;  
        List<int> tempSelectedIndexList;         //临时集合-记录被当前圈选中的项

        private void flowLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            this.cBox_checkAll.Checked = false;
            if (!((Control.ModifierKeys & Keys.Control) == Keys.Control)) //CTRL没有按下时
            {
                ClearAllSelectedItems();
            }

            if (isMultiple)  //如果不能多选，就不能圈选
            {               
                tempSelectedIndexList = new List<int>();
                start = e.Location;

                timer = new Timer();
                timer.Tick += TimeEvent;
                timer.Interval = 100;
                timer.Start();
            }                       
        }

        private void flowLayoutPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (isMultiple)  
            {
                timer.Dispose();
                ShowRectanglePanels(false);
                if (tempSelectedIndexList.Count > 0)
                {
                    isDirectory = false;
                    this.btn_nextStep.Text = "确定";
                    tempSelectedIndexList.Clear();
                }
            }
        }

        private void TimeEvent(object sender, EventArgs e)
        {                    
            Point end = this.flowLayoutPanel1.PointToClient(Control.MousePosition);

            //坐标点不可超出flowLayoutPanel控件
            int endX = end.X > 5 ? end.X : 5;
            endX = endX > flowLayoutPanel1.Width ? flowLayoutPanel1.Width - 5 : endX;
            int endY = end.Y > 5 ? end.Y : 5;
            endY = endY > flowLayoutPanel1.Height ? flowLayoutPanel1.Height - 5 : endY;

            //设置矩形坐标
            int pX = endX > start.X ? start.X : endX;
            int pY = endY > start.Y ? start.Y : endY;
            int pWidth  = Math.Abs(endX - start.X);
            int pHeigth = Math.Abs(endY - start.Y);
            Rectangle ret2 = new Rectangle(pX, pY, pWidth, pHeigth);
            
            //画矩形
            DrawRectangleByPanel(ret2);

            //获取圈中项
            foreach (FileControl fc in this.flowLayoutPanel1.Controls)
            {                
                if (!fc.isDirectory)  //文件夹不可被圈中
                {
                    Rectangle ret1 = new Rectangle(fc.Location, new Size(fc.Width, fc.Height));
                    int index = this.flowLayoutPanel1.Controls.IndexOf(fc);                                       
                    bool intersect = ret2.IntersectsWith(ret1);
                    if (intersect && !fc.isChoosed)
                    {
                        tempSelectedIndexList.Add(index);    //将圈中项加入临时集合
                        SetSelectedItem(fc);
                    }
                    else if (!intersect && fc.isChoosed && tempSelectedIndexList.Contains(index))
                    {
                        ClearSelectedItem(fc);
                        tempSelectedIndexList.Remove(index);
                    }
                }
            }
        }
        
        
        private System.Windows.Forms.Panel panel_X1;
        private System.Windows.Forms.Panel panel_Y2;
        private System.Windows.Forms.Panel panel_Y1;
        private System.Windows.Forms.Panel panel_X2;        

        /// <summary>
        /// 创建四个panel对象，为圈文件时的四条边
        /// </summary>
        private void CreatePanelControl()
        {              
            this.panel_X2 = new System.Windows.Forms.Panel();
            this.panel_X1 = new System.Windows.Forms.Panel();
            this.panel_Y2 = new System.Windows.Forms.Panel();
            this.panel_Y1 = new System.Windows.Forms.Panel();

            panel_X2.Location = new System.Drawing.Point(10,10);
            panel_X2.Name = "panel_X2";
            panel_X2.Size = new System.Drawing.Size(83, 56);
            panel_X2.BackColor = Color.Red;

            this.panel_X1.Location = new System.Drawing.Point(37, 405);
            this.panel_X1.Name = "panel_X1";
            this.panel_X1.Size = new System.Drawing.Size(79, 56);

            this.panel_Y2.Location = new System.Drawing.Point(377, 405);
            this.panel_Y2.Name = "panel_Y2";
            this.panel_Y2.Size = new System.Drawing.Size(67, 56);

            this.panel_Y1.Location = new System.Drawing.Point(271, 405);
            this.panel_Y1.Name = "panel_Y1";
            this.panel_Y1.Size = new System.Drawing.Size(78, 56);

            this.Controls.Add(this.panel_Y1);
            this.Controls.Add(this.panel_Y2);
            this.Controls.Add(this.panel_X1);
            this.Controls.Add(panel_X2);

            ShowRectanglePanels(false);
        }

        /// <summary>
        /// 画矩形
        /// </summary>
        /// <param name="rect"></param>
        private void DrawRectangleByPanel(Rectangle rect)
        {
            ShowRectanglePanels(true);     
            Color bColor = Color.FromArgb(77, 200, 220);
            panel_X1.BackColor = bColor;
            panel_X2.BackColor = bColor;
            panel_Y1.BackColor = bColor;
            panel_Y2.BackColor = bColor;

            panel_X1.SetBounds(rect.X + this.flowLayoutPanel1.Location.X, rect.Y + this.flowLayoutPanel1.Location.Y, rect.Width, 1);
            panel_X2.SetBounds(rect.X + this.flowLayoutPanel1.Location.X, rect.Y + this.flowLayoutPanel1.Location.Y + rect.Height, rect.Width, 1);
            panel_Y1.SetBounds(rect.X + this.flowLayoutPanel1.Location.X, rect.Y + this.flowLayoutPanel1.Location.Y, 1, rect.Height);
            panel_Y2.SetBounds(rect.X + this.flowLayoutPanel1.Location.X + rect.Width, rect.Y + this.flowLayoutPanel1.Location.Y , 1, rect.Height);
        }

        private void ShowRectanglePanels(bool visible)
        {
            this.panel_X1.Visible = visible;
            this.panel_X2.Visible = visible;
            this.panel_Y1.Visible = visible;
            this.panel_Y2.Visible = visible;
        }

        #endregion

        #region 全选
        private void cBox_checkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cBox_checkAll.Checked)
            {
                foreach (FileControl fc in this.flowLayoutPanel1.Controls)
                {
                    if (!fc.isDirectory && !fc.isChoosed)  
                    {
                        SetSelectedItem(fc);
                    }
                }
                isDirectory = false;
                this.btn_nextStep.Text = "确定";
            }
            else
            {
                ClearAllSelectedItems();
            }
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            File.Move("D:\\DxkLog.txt", "D:\\DxkLog1.txt");
           // this.flowLayoutPanel1.VerticalScroll.Value += 1;
        }
    }
}
