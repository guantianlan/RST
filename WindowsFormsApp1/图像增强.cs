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
    public partial class 图像增强 : Form
    {
        private string folderDirPath;                            //图片文件夹地址
        private string picDirPath = null;                        //图片路径
        private List<string> imagePathList = new List<string>(); //获取列表图片路径
        private int index;                                       //获取选中列表图片序号
        DataTable dt;
        public static 图像增强 strong;


        public 图像增强()
        {
            InitializeComponent();
            strong = this;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void 图像增强_Load(object sender, EventArgs e)
        {
            首页 first = new 首页();
            first.Owner = this;
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)//表格前面的数字
        {
            Rectangle rect = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y,
            dataGridView1.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
            dataGridView1.RowHeadersDefaultCellStyle.Font, rect,
            dataGridView1.RowHeadersDefaultCellStyle.ForeColor,
            TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.dataGridView1.Rows.Count == 0)
                return;
            try
            {
                if(首页.strong_submit == false)
                {
                    DataGridViewRow dgr = dataGridView1.CurrentRow;
                    图像增强个人 图像增强_个人窗 = new 图像增强个人(dgr);
                    图像增强_个人窗.Show();
                }
                else
                {
                    DataGridViewRow dgr = dataGridView1.CurrentRow;
                    图像增强个人 图像增强_个人窗 = new 图像增强个人(dgr);
                    图像增强_个人窗.Show();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
