using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
                                         //获取选中列表图片序号

    public partial class 历史记录 : Form
    {
        private string folderDirPath;                            //图片文件夹地址
        private string picDirPath = null;                        //图片路径
        private List<string> imagePathList = new List<string>(); //获取列表图片路径
        private int index;

        public static 历史记录 history;
        public 历史记录()
        {
            InitializeComponent();
            history = this;
        }

        private void listView1_Click(object sender, EventArgs e)
        {

        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            结果查询窗 结果查询窗 = new 结果查询窗();
            结果查询窗.Show();
        }

        private void 历史记录_Load(object sender, EventArgs e)
        {
            首页 first = new 首页();
            first.Owner = this;
        }
    }
}
