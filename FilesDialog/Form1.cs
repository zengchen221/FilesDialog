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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog oFileDialog = new OpenFileDialog("选择图片",0,true);
            
            //设置默认选中项
            List<string> list = new List<string>();
            list.Add(@"D:\Work\DXK\多学科试卷\2012年安徽省高考语文试卷（答案紧跟试题后）.doc");
            oFileDialog.wishToSelectFileList = list;

            oFileDialog.ShowDialog();
            List<FileInfo> f = oFileDialog.GetSelectedFileList();            
        }
    }
}
