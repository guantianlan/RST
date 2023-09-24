using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class 染色体分类 : Form
    {
        private string folderDirPath;                            //图片文件夹地址
        private string picDirPath = null;                        //图片路径
        private List<string> imagePathList = new List<string>(); //获取列表图片路径
        private int index;                                       //获取选中列表图片序号

        public static 染色体分类 chromosome;
        public 染色体分类()
        {
            InitializeComponent();//************
            chromosome = this;
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listView1.Items.Count == 0)
                return;
            index = this.listView1.SelectedItems[0].Index;
            Image iii = 染色体分类.chromosome.imageList1.Images[index];
            结果查询窗 结果查询窗 = new 结果查询窗(iii);
            结果查询窗.Show();
        }

        private void 染色体分类_Load(object sender, EventArgs e)
        {
            首页 first = new 首页();
            first.Owner = this;
        }
    }
}
