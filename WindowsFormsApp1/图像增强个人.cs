using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class 图像增强个人 : Form
    {
        DataGridViewRow dgr;
        public static 图像增强个人 people;
        int m = 1;
        int j;
        int k = -1;
        int a = 0;
        private string path = null;                        //空白图片路径
        private string path1 = null;                        //灰色图片路径
        int count = 0;  //组数，3行为一组
        Image img = null;
        int time = 0;  
        int pic = 0;//第几张图片
        int count1 = 0;  //组数，3行为一组
        int time1 = 0;
        int pic1 = 0;//第几张图片
        int length = 0;
        int x;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        private int LVM_SETICONSPACING = 0x1035;

        public 图像增强个人(DataGridViewRow dgr)
        {
            InitializeComponent();
            people = this;
            this.dgr = dgr;
            path = @"../img/R-C.jpg";
            path1 = @"../img/R-C1.jpg";
            materialLabel8.Text = dgr.Cells[0].Value.ToString();
            for (x = 1; x < dgr.Cells.Count; )
            {
                while(count % 3 == 0 && x < dgr.Cells.Count)//第一组
                {
                    if(time != 11 && pic%3 == 0 && x < dgr.Cells.Count)
                    {
                        img = (Image)dgr.Cells[x].Value;
                        imageList1.Images.Add(img);  //图像增强前
                        x++;
                        time++;
                        pic++;
                    }
                    if (time != 11 && pic % 3 == 1 && x < dgr.Cells.Count)
                    {
                        imageList1.Images.Add(Image.FromFile(path));  //图像增强后
                        time++; pic++;
                    }
                    if (time != 11 && pic % 3 == 2 && x < dgr.Cells.Count)
                    {   
                        imageList1.Images.Add(Image.FromFile(path1));  //间隔
                        time++; pic++;
                    }
                    if(time == 11)
                    {
                        count++;
                        time = 0;
                        continue;
                    }
                }
                while (count % 3 == 1 && x < dgr.Cells.Count)//第二组
                {
                    if (time != 11 && pic % 3 == 2 && x < dgr.Cells.Count)
                    {
                        img = (Image)dgr.Cells[x].Value;
                        imageList1.Images.Add(img);  //图像增强前
                        x++;
                        time++; pic++;
                    }
                    if (time != 11 && pic % 3 == 0 && x <= dgr.Cells.Count)
                    {
                        imageList1.Images.Add(Image.FromFile(path));  //图像增强后
                        time++; pic++;
                    }
                    if (time != 11 && pic % 3 == 1 && x < dgr.Cells.Count)
                    {
                        imageList1.Images.Add(Image.FromFile(path1));  //间隔
                        time++; pic++;
                    }
                    if (time == 11)
                    {
                        count++;
                        time = 0;
                        continue;
                    }
                }
                while (count % 3 == 2 && x < dgr.Cells.Count)//第三组
                {
                    if (time != 11 && pic % 3 == 1 && x < dgr.Cells.Count)
                    {
                        img = (Image)dgr.Cells[x].Value;
                        imageList1.Images.Add(img);  //图像增强前
                        x++;
                        time++; pic++;
                    }
                    if (time != 11 && pic % 3 == 2 && x <= dgr.Cells.Count)
                    {
                        imageList1.Images.Add(Image.FromFile(path));  //图像增强后
                        time++; pic++;
                    }
                    if (time != 11 && pic % 3 == 0 && x < dgr.Cells.Count)
                    {
                        imageList1.Images.Add(Image.FromFile(path1));  //间隔
                        time++; pic++;
                    }
                    if (time == 11)
                    {
                        count++;
                        time = 0;
                        continue;
                    }
                }
            }
            length = imageList1.Images.Count;
        }


        private void 图像增强_个人_Load(object sender, EventArgs e)
        {
            SendMessage(this.listView1.Handle, LVM_SETICONSPACING, 0, 0x10000 * 140 + 110);  //140控制行距，110控制列距
            图像增强 strong = new 图像增强();
            strong.Owner = this;
            //显示文件列表
            listView1.Items.Clear();
            listView1.LargeImageList = imageList1;
            listView1.View = View.LargeIcon;        //大图标显示


            //开始绑定
            listView1.BeginUpdate();
            //增加图片至ListView控件中
            for (int i = 0; i < imageList1.Images.Count; i++)
            {
                while (count1 % 3 == 0 && i < imageList1.Images.Count)//第一组
                {
                    if (time1 != 11 && pic1 % 3 == 0 && i < imageList1.Images.Count)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.ImageIndex = i;
                        lvi.Text = "图像增强前" + a;//图片名称
                        listView1.Items.Add(lvi);
                        i++; time1++; pic1++; k++;
                        listView1.EndUpdate();
                    }
                    if (time1 != 11 && pic1 % 3 == 1 && i < imageList1.Images.Count)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.ImageIndex = i;
                        lvi.Text = "图像增强后" + k;//图片名称
                        listView1.Items.Add(lvi);
                        a++; i++; time1++; pic1++;
                        listView1.EndUpdate();
                    }
                    if (time1 != 11 && pic1 % 3 == 2 && i < imageList1.Images.Count)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.ImageIndex = i;
                        lvi.Text = "";//图片名称
                        listView1.Items.Add(lvi);
                        i++; time1++; pic1++;
                        listView1.EndUpdate();
                    }
                    if (time1 == 11)
                    {
                        count1++;
                        time1 = 0;
                        continue;
                    }
                }
                while (count1 % 3 == 1 && i < imageList1.Images.Count)//第二组
                {
                    if (time1 != 11 && pic1 % 3 == 2 && i < imageList1.Images.Count)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.ImageIndex = i;
                        lvi.Text = "图像增强前" + a;//图片名称
                        listView1.Items.Add(lvi);
                        i++; time1++; pic1++; k++;
                        listView1.EndUpdate();
                    }
                    if (time1 != 11 && pic1 % 3 == 0 && i <= imageList1.Images.Count)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.ImageIndex = i;
                        lvi.Text = "图像增强后" + k;//图片名称
                        listView1.Items.Add(lvi);
                        a++; i++;
                        time1++; pic1++;
                        listView1.EndUpdate();
                    }
                    if (time1 != 11 && pic1 % 3 == 1 && i < imageList1.Images.Count)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.ImageIndex = i;
                        lvi.Text = "";//图片名称
                        listView1.Items.Add(lvi);
                        time1++; pic1++; i++;
                        listView1.EndUpdate();
                    }
                    if (time1 == 11)
                    {
                        count1++;
                        time1 = 0;
                        continue;
                    }
                }
                while (count1 % 3 == 2 && i < imageList1.Images.Count)//第三组
                {
                    if (time1 != 11 && pic1 % 3 == 1 && i < imageList1.Images.Count)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.ImageIndex = i;
                        lvi.Text = "图像增强前" + a;//图片名称
                        listView1.Items.Add(lvi);
                        i++; time1++; pic1++; k++;
                        listView1.EndUpdate();
                    }
                    if (time1 != 11 && pic1 % 3 == 2 && i < imageList1.Images.Count)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.ImageIndex = i;
                        lvi.Text = "图像增强后" + k;//图片名称
                        listView1.Items.Add(lvi);
                        a++;time1++; pic1++;
                        if (time1 != 11)
                        {
                            i++;
                        }
                        listView1.EndUpdate();
                    }
                    if (time1 != 11 && pic1 % 3 == 0 && i < imageList1.Images.Count)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.ImageIndex = i;
                        lvi.Text = "";//图片名称
                        listView1.Items.Add(lvi);
                        time1++; pic1++; i++;
                        listView1.EndUpdate();
                    }
                    if (time1 == 11)
                    {
                        count1++;
                        time1 = 0;
                        continue;
                    }
                }
                //listView1.EndUpdate();
            }
        }
    }
}
