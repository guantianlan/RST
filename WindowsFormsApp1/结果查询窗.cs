using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class 结果查询窗 : Form
    {
        public 结果查询窗()
        {
            InitializeComponent();
        }

        public 结果查询窗(Image currentPicture)
        {
            InitializeComponent();
            pictureBox2.Image = currentPicture;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void 结果查询窗_Load(object sender, EventArgs e)
        {
           
        }
    }
}
