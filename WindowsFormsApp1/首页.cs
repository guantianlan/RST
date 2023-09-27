using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

using MaterialSkin.Controls;//**
using MaterialSkin;//**
using System.Globalization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Net.WebRequestMethods;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Security.Cryptography;
using DAL;
using System.Data.SqlClient;
using System.Reflection;

namespace WindowsFormsApp1
{
    public partial class 首页 : MaterialForm   //45行this
    {

        private string folderDirPath;                            //图片文件夹地址
        private string picDirPath = null;                        //图片路径
        private string path = null;                        //空白图片路径
        private List<string> imagePathList = new List<string>(); //获取列表图片路径
        private List<string> imagePredictList = new List<string>(); //获取列表图片预测结果
        private int index;                                       //获取选中列表图片序号

        //** **
        private readonly MaterialSkinManager materialSkinManager;

        int j = 0;  //第0行起
        int k = 0;  //第1列起
        int l = -1;  //第几张图片
        int x = 0;

        public static string[] image_item;
        public static int count_image = 0;
        public static string[,] submit_result;//二维数组存结果
        public static string submit_retain;//暂存结果
        public static bool import_data = false;//是否导入
        public static bool history_import_data = false;//是否导入历史记录
        public static bool submit_or_not = false;//是否提交
        public static bool history_submit_or_not = false;//是否提交历史记录
        public static bool record_list = false;//是否打开record列表
        public static int i = 0;
        public string history_path = "./history";
        private List<PictureBox> PictureBoxList = new List<PictureBox>();
        private List<PictureBox> PictureBoxList_sharp = new List<PictureBox>();

        PictureBox old = null;

        DataTable Major_list;

        string pathname = null;  //数据库中存放的图片目录
        public static string[] photoname;  //数据库中存放的图片的名称
        public static string[] news;  //数据库中存放的图的名称
        string photo = null;
        public 首页()
        {
            InitializeComponent();


            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.EnforceBackcolorOnAllComponents = true;
            materialSkinManager.AddFormToManage(this);

            //更改主题颜色参数的位置
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                       Primary.Cyan700,
                       Primary.Cyan900,
                       Primary.Cyan500,
                       Accent.DeepOrange200,
                       TextShade.WHITE);

        }

        
        //关闭已经存在的窗体方法
        public void CloseParForm()
        {
            //判断当前容器中是否已经存在窗体
            foreach (Control item in this.splitContainer1.Panel2.Controls)
            {
                if (item is Form)
                {
                    Form objControl = (Form)item;
                    objControl.Close();
                }
            }
        }

        //嵌入父容器方法
        public void OpenForm(Form objFrm)
        {
            objFrm.TopLevel = false;//子窗体设为非顶级控件
            //让窗体最大化显示
            objFrm.WindowState = FormWindowState.Maximized;
            //去掉窗体的边框
            //objFrm.FormBorderStyle = FormBorderStyle.None;
            //指定子窗体显示的容器
            objFrm.Parent = this.splitContainer1.Panel2;
            objFrm.Show();//显示窗体
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            this.CloseParForm();
            if (panel2.Visible == true)
                panel2.Visible = !panel2.Visible;
            if (panel3.Visible == true)
                panel3.Visible = !panel3.Visible;
            if (panel4.Visible == true)
                panel4.Visible = !panel4.Visible;
        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {
            this.CloseParForm();
            染色体分类 染色体分类 = new 染色体分类();
            this.OpenForm(染色体分类);
            panel2.Visible = !panel2.Visible;
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            this.CloseParForm();
            染色体分类 染色体分类 = new 染色体分类();
            this.OpenForm(染色体分类);
            panel2.Visible = !panel2.Visible;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.CloseParForm();
            图像增强 图像增强 = new 图像增强();
            this.OpenForm(图像增强);
            panel3.Visible = !panel3.Visible;
        }

        private void groupBox1_MouseCaptureChanged_1(object sender, EventArgs e)
        {
            this.CloseParForm();
            染色体分类 染色体分类 = new 染色体分类();
            this.OpenForm(染色体分类);
            panel2.Visible = !panel2.Visible;
        }

        private void groupBox2_MouseCaptureChanged(object sender, EventArgs e)
        {
            this.CloseParForm();
            图像增强 图像增强 = new 图像增强();
            this.OpenForm(图像增强);
            panel3.Visible = !panel3.Visible;
        }

        private void 首页_Load(object sender, EventArgs e)
        {
            //asc.controllInitializeSize(this);
            panel2.Visible = !panel2.Visible;
            panel3.Visible = !panel3.Visible;
            panel4.Visible = !panel4.Visible;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.CloseParForm();
            染色体分类 染色体分类 = new 染色体分类();
            this.OpenForm(染色体分类);
            panel2.Visible = !panel2.Visible;
            panel3.Visible = false;
            panel4.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.CloseParForm();
            图像增强 图像增强 = new 图像增强();
            this.OpenForm(图像增强);
            panel3.Visible = !panel3.Visible;
            panel2.Visible = false;
            panel4.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.CloseParForm();
            历史记录 历史记录 = new 历史记录();
            this.OpenForm(历史记录);
            panel4.Visible = !panel4.Visible;
            panel3.Visible = false;
            panel2.Visible = false;
        }

        private void materialLabel1_Click(object sender, EventArgs e)  // 首页-显示染色体分类界面
        {
            this.CloseParForm();
            染色体分类 染色体分类 = new 染色体分类();
            this.OpenForm(染色体分类);
            panel2.Visible = !panel2.Visible;
        }

        private void materialLabel2_Click(object sender, EventArgs e)  //首页-显示图像增强界面
        {
            this.CloseParForm();
            图像增强 图像增强 = new 图像增强();
            this.OpenForm(图像增强);
            panel3.Visible = !panel3.Visible;
        }

        private void materialButton1_Click(object sender, EventArgs e)  //染色体分类-导入图片
        {
            if (import_data == true)
            {
                MessageBox.Show("已导入图片，请先删除图片库！");
                return;
            }
            染色体分类 chromosome = (染色体分类)this.Owner;
            try
            {
                //打开选择文件夹对话框
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    //获取用户选择的文件夹路径
                    this.folderDirPath = folderBrowserDialog.SelectedPath;

                    //获取目录与子目录
                    DirectoryInfo dir = new DirectoryInfo(folderDirPath);
                    char[] separators = { '\\' };
                    string[] strings = folderDirPath.Split(separators);
                    int legth = strings.Length;
                    pathname = "../" + strings[legth - 2] + '/' + strings[legth - 1];
                    //获取当前目录JPG文件列表 GetFiles获取指定目录中文件的名称(包括其路径)
                    FileInfo[] fileInfo = dir.GetFiles("*.jpg");
                    //防止图片失真
                    染色体分类.chromosome.imageList1.ColorDepth = ColorDepth.Depth32Bit;

                    //****
                    int count = fileInfo.Length;
                    image_item = new string[count];
                    photoname = new string[count];
                    news = new string[count];
                    count_image = count;
                    submit_result = new string[count_image, 3];
                    for (int i = 0; i < fileInfo.Length; i++)
                    {
                        //获取文件完整目录
                        picDirPath = fileInfo[i].FullName;
                        photo = fileInfo[i].Name;
                        photoname[i] = photo;
                        image_item[i] = picDirPath;
                        //记录图片源路径 双击显示图片时使用
                        imagePathList.Add(picDirPath);
                        PictureBox pic = new PictureBox();
                        pic.Name = string.Format(Convert.ToString(photo));
                        pic.Load(picDirPath);
                        PictureBoxList.Add(pic);
                        //图片加载到ImageList控件和imageList图片列表
                        染色体分类.chromosome.imageList1.Images.Add(photo,Image.FromFile(picDirPath));
                    }

                    //显示文件列表
                    染色体分类.chromosome.listView1.Items.Clear();
                    染色体分类.chromosome.listView1.LargeImageList = 染色体分类.chromosome.imageList1;
                    染色体分类.chromosome.listView1.View = View.LargeIcon;        //大图标显示
                    //imageList1.ImageSize = new Size(40, 40);   //不能设置ImageList的图像大小 属性处更改

                    //开始绑定
                    染色体分类.chromosome.listView1.BeginUpdate();
                    //增加图片至ListView控件中
                    for (int i = 0; i < 染色体分类.chromosome.imageList1.Images.Count; i++)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.ImageIndex = i;
                        //lvi.Text = "预测结果：" + "x号染色体" + "预测概率：" + "xx%";//图片名称***********pic X
                        lvi.Text = "待检测";

                        染色体分类.chromosome.listView1.Items.Add(lvi);
                    }
                    染色体分类.chromosome.listView1.EndUpdate();
                }
                else if (result == DialogResult.Cancel)
                {
                    MessageBox.Show("取消显示图片列表");
                }
            }
            catch (Exception msg)
            {
                //报错提示 未将对象引用设置到对象的实例
                throw msg;
            }
            import_data = true;
        }

        private void materialButton2_Click(object sender, EventArgs e)  //染色体分类-清空图片
        {
            if (染色体分类.chromosome.imageList1.Images.Count == 0)
            {
                MessageBox.Show("请先导入图像！");
                return;
            }
            else
            {
                染色体分类 chromosome = (染色体分类)this.Owner;
                染色体分类.chromosome.listView1.Items.Clear();
                染色体分类.chromosome.imageList1.Images.Clear();
                image_item = null;
                count_image = 0;
                submit_or_not = false;
                import_data = false;
                history_submit_or_not = false;
                history_import_data = false;
                submit_retain = null;
                submit_result = null;
            }
        }

        private void materialButton3_Click(object sender, EventArgs e)  //染色体分类-提交图片
        {
            if (submit_or_not == true)
            {
                MessageBox.Show("已经提交过了！");
                return;
            }
            if (history_submit_or_not == true)
            {
                MessageBox.Show("已经提交过了！");
                return;
            }
            if (import_data == false)
            {
                MessageBox.Show("请先导入图像！");
                return;
            }
            string[] strArr = new string[2];//参数列表
            string sArguments;

            if (materialComboBox1.Text != "Mobilenet" && materialComboBox1.Text != "Resnet" && materialComboBox1.Text != "VGG" && materialComboBox1.Text != "Densenet")
            {
                sArguments = @"Mobilenet.py";
            }
            else
            {
                sArguments = materialComboBox1.Text + ".py";  //这里是python的文件名字
            }

            strArr[0] = "2";
            strArr[1] = "2";
            submit_result=Tools.RunPythonScript(sArguments,count_image, "-u", image_item);
            //显示文件列表
            染色体分类.chromosome.listView1.Items.Clear();
            染色体分类.chromosome.listView1.LargeImageList = 染色体分类.chromosome.imageList1;
            染色体分类.chromosome.listView1.View = View.LargeIcon;        //大图标显示
            //imageList1.ImageSize = new Size(40, 40);   //不能设置ImageList的图像大小 属性处更改

            //开始绑定
            染色体分类.chromosome.listView1.BeginUpdate();

            for (int i = 0; i < 染色体分类.chromosome.imageList1.Images.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.ImageIndex = i;
                //lvi.Text = "预测结果：" + "x号染色体" + "预测概率：" + "xx%";//图片名称***********pic X
                lvi.Text = "   预测结果:" + submit_result[i, 0] +
                    "             预测概率:" + submit_result[i, 1]; ;

                染色体分类.chromosome.listView1.Items.Add(lvi);

                string news1 = photoname[i] + "+" + submit_result[i, 0];
                news[i] = news1;

            }
            染色体分类.chromosome.listView1.EndUpdate();
            string id = 染色体分类.chromosome.materialLabel8.Text;  //存入数据库中的id号
            string keshi = 染色体分类.chromosome.materialLabel9.Text;
            string time = System.DateTime.Now.ToString("d");
            //将数据添加至数据库当中
            string sql = string.Format("insert into Table_1(id,时间,目录,科室,图片1,图片2,图片3,图片4,图片5,图片6,图片7,图片8,图片9,图片10," +
                "图片11,图片12,图片13,图片14,图片15,图片16,图片17,图片18,图片19,图片20,图片21,图片22,图片23,图片24,图片25," +
                "图片26,图片27,图片28,图片29,图片30,图片31,图片32,图片33,图片34,图片35,图片36,图片37,图片38,图片39,图片40," +
                "图片41,图片42,图片43,图片44,图片45,图片46) values(@id,@time,@path,@keshi,@图1,@图2,@图3,@图4,@图5,@图6,@图7,@图8,@图9,@图10," +
                "@图11,@图12,@图13,@图14,@图15,@图16,@图17,@图18,@图19,@图20,@图21,@图22,@图23,@图24,@图25,@图26,@图27,@图28,@图29,@图30," +
                "@图31,@图32,@图33,@图34,@图35,@图36,@图37,@图38,@图39,@图40,@图41,@图42,@图43,@图44,@图45,@图46)");
            SqlParameter[] scs = new SqlParameter[50];
            scs[0] = new SqlParameter("@id", id);
            scs[1] = new SqlParameter("@time", time);
            scs[2] = new SqlParameter("@path", pathname);
            scs[3] = new SqlParameter("@keshi", keshi);
            scs[4] = new SqlParameter("@图1", news[0]);
            scs[5] = new SqlParameter("@图2", news[1]);
            scs[6] = new SqlParameter("@图3", news[2]);
            scs[7] = new SqlParameter("@图4", news[3]);
            scs[8] = new SqlParameter("@图5", news[4]);
            scs[9] = new SqlParameter("@图6", news[5]);
            scs[10] = new SqlParameter("@图7", news[6]);
            scs[11] = new SqlParameter("@图8", news[7]);
            scs[12] = new SqlParameter("@图9", news[8]);
            scs[13] = new SqlParameter("@图10", news[9]);
            scs[14] = new SqlParameter("@图11", news[10]);
            scs[15] = new SqlParameter("@图12", news[11]);
            scs[16] = new SqlParameter("@图13", news[12]);
            scs[17] = new SqlParameter("@图14", news[13]);
            scs[18] = new SqlParameter("@图15", news[14]);
            scs[19] = new SqlParameter("@图16", news[15]);
            scs[20] = new SqlParameter("@图17", news[16]);
            scs[21] = new SqlParameter("@图18", news[17]);
            scs[22] = new SqlParameter("@图19", news[18]);
            scs[23] = new SqlParameter("@图20", news[19]);
            scs[24] = new SqlParameter("@图21", news[20]);
            scs[25] = new SqlParameter("@图22", news[21]);
            scs[26] = new SqlParameter("@图23", news[22]);
            scs[27] = new SqlParameter("@图24", news[23]);
            scs[28] = new SqlParameter("@图25", news[24]);
            scs[29] = new SqlParameter("@图26", news[25]);
            scs[30] = new SqlParameter("@图27", news[26]);
            scs[31] = new SqlParameter("@图28", news[27]);
            scs[32] = new SqlParameter("@图29", news[28]);
            scs[33] = new SqlParameter("@图30", news[29]);
            scs[34] = new SqlParameter("@图31", news[30]);
            scs[35] = new SqlParameter("@图32", news[31]);
            scs[36] = new SqlParameter("@图33", news[32]);
            scs[37] = new SqlParameter("@图34", news[33]);
            scs[38] = new SqlParameter("@图35", news[34]);
            scs[39] = new SqlParameter("@图36", news[35]);
            scs[40] = new SqlParameter("@图37", news[36]);
            scs[41] = new SqlParameter("@图38", news[37]);
            scs[42] = new SqlParameter("@图39", news[38]);
            scs[43] = new SqlParameter("@图40", news[39]);
            scs[44] = new SqlParameter("@图41", news[40]);
            scs[45] = new SqlParameter("@图42", news[41]);
            scs[46] = new SqlParameter("@图43", news[42]);
            scs[47] = new SqlParameter("@图44", news[43]);
            scs[48] = new SqlParameter("@图45", news[44]);
            scs[49] = new SqlParameter("@图46", news[45]);
            int dt = SqlDbHelper.ExecuteNonQuery(sql, CommandType.Text, scs);

            MessageBox.Show("提交成功！");
        }

        private void materialButton7_Click(object sender, EventArgs e)  //图像增强-导入图片
        {
            图像增强 strong = (图像增强)this.Owner;
            图像增强个人 people = (图像增强个人)this.Owner;
            try
            {
                //打开选择文件夹对话框
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    //获取用户选择的文件夹路径
                    this.folderDirPath = folderBrowserDialog.SelectedPath;

                    //获取目录与子目录
                    DirectoryInfo dir = new DirectoryInfo(folderDirPath);
                    //获取当前目录JPG文件列表 GetFiles获取指定目录中文件的名称(包括其路径)
                    FileInfo[] fileInfo = dir.GetFiles("*.jpg");

                    //防止图片失真
                    图像增强.strong.imageList1.ColorDepth = ColorDepth.Depth32Bit;     //****
                    int length = (fileInfo.Length) + (fileInfo.Length);
                    for (int i = 0; i < fileInfo.Length; i++)
                    {
                        //获取文件完整目录
                        picDirPath = fileInfo[i].FullName;
                        photo = fileInfo[i].Name;
                        path = @"../img/R-C.jpg";
                        //记录图片源路径 双击显示图片时使用
                        imagePathList.Add(picDirPath);
                        PictureBox pic = new PictureBox();
                        pic.Name = string.Format(Convert.ToString(photo));
                        pic.Load(picDirPath);
                        PictureBoxList.Add(pic);
                        //imagePathList.Add(path);
                        //图片加载到ImageList控件和imageList图片列表
                        图像增强.strong.imageList1.Images.Add(photo,Image.FromFile(picDirPath));

                        //图像增强.strong.imageList1.Images.Add(Image.FromFile(path));
                    }

                    //增加图片至datagridview控件中     
                    for (int i = 0; i < 图像增强.strong.imageList1.Images.Count; i++)
                    {
                        k++;
                        l++;
                        if (i <= 46) //datagridview里的第一行
                        {
                            DataGridViewRow dr = new DataGridViewRow();
                            dr.Height = 40;  //行高
                            dr.CreateCells(图像增强.strong.dataGridView1);
                            for (int m = 0; m < fileInfo.Count() + 1; m++)
                            {
                                if (m == 0)
                                {
                                    dr.Cells[m].Value = j.ToString();
                                }
                                if (m > 0)
                                {
                                    dr.Cells[m].Value = Image.FromFile(imagePathList[i]);
                                    i++;
                                }
                            }
                            图像增强.strong.dataGridView1.Rows.Add(dr);
                            //i += 46;
                        }
                        if (i > 46)
                        {
                            j++;
                            //图像增强.strong.dataGridView1.Rows[j].Cells[0].Value = j.ToString();
                            //if (l % 46 == 0)
                            //{
                            //    k = 1;
                            //}
                            DataGridViewRow dr = new DataGridViewRow();
                            dr.Height = 40;  //行高
                            dr.CreateCells(图像增强.strong.dataGridView1);
                            for (int m = 0; m < fileInfo.Count() + 1; m++)
                            {
                                if (m == 0)
                                {
                                    dr.Cells[m].Value = j.ToString();
                                }
                                if (m > 0)
                                {
                                    dr.Cells[m].Value = Image.FromFile(imagePathList[m]);
                                }
                            }
                            图像增强.strong.dataGridView1.Rows.Add(dr);
                            i += 46;
                        }
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    MessageBox.Show("取消显示图片列表");
                }
            }
            catch (Exception msg)
            {
                //报错提示 未将对象引用设置到对象的实例
                throw msg;
            }
            import_data = true;
        }

        private void materialButton8_Click(object sender, EventArgs e)  //图像增强-提交图片
        {
            if (submit_or_not == true)
            {
                MessageBox.Show("已经提交过了！");
                return;
            }
            if (history_submit_or_not == true)
            {
                MessageBox.Show("已经提交过了！");
                return;
            }
            if (import_data == false)
            {
                MessageBox.Show("请先导入图像！");
                return;
            }
            string[] strArr = new string[2];//参数列表
            string sArguments;
            sArguments = @"predict.py";
            submit_result=Tools.RunPythonScript_D(sArguments, imagePathList, "-u");

            图像增强.strong.dataGridView1.Rows.Clear();

            for (int i = 0; i < 图像增强.strong.imageList1.Images.Count; i++)
            {
                k++;
                l++;
                if (i <= 46) //datagridview里的第一行
                {
                    DataGridViewRow dr = new DataGridViewRow();
                    dr.Height = 40;  //行高
                    dr.CreateCells(图像增强.strong.dataGridView1);
                    for (int m = 0; m < imagePathList.Count(); m++)
                    {
                        if (m == 0)
                        {
                            dr.Cells[m].Value = j.ToString();
                        }
                        if (m > 0)
                        {
                            dr.Cells[m].Value = Image.FromFile(imagePathList[i]);
                            i++;
                        }
                    }
                    图像增强.strong.dataGridView1.Rows.Add(dr);
                    //i += 46;
                }
                if (i > 46)
                {
                    j++;
                    //图像增强.strong.dataGridView1.Rows[j].Cells[0].Value = j.ToString();
                    //if (l % 46 == 0)
                    //{
                    //    k = 1;
                    //}
                    DataGridViewRow dr = new DataGridViewRow();
                    dr.Height = 40;  //行高
                    dr.CreateCells(图像增强.strong.dataGridView1);
                    for (int m = 0; m < imagePathList.Count(); m++)
                    {
                        if (m == 0)
                        {
                            dr.Cells[m].Value = j.ToString();
                        }
                        if (m > 0)
                        {
                            dr.Cells[m].Value = Image.FromFile(imagePathList[m]);
                        }
                    }
                    图像增强.strong.dataGridView1.Rows.Add(dr);
                    i += 46;
                }
            }
            string path = "./submit";
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            directoryInfo.Create();
            FileInfo[] files = directoryInfo.GetFiles();
            int count = files.Length;

            for (int k = 0; k < count; k++)
            {

                string img_path = path + "/" + files[k].ToString();


            }
            try
            {
                if (Directory.Exists("./submit"))
                {
                    MessageBox.Show("提交成功！");
                }
            }
            catch
            {
                MessageBox.Show("提交失败！请检查染色体目录是否含有中文");
            }

            DirectoryInfo dir = new DirectoryInfo("./submit");
            FileInfo[] fileInfo = dir.GetFiles("*.jpg");
            int length = (fileInfo.Length) + (fileInfo.Length);
            for (int i = 0; i < fileInfo.Length; i++)
            {
                //获取文件完整目录
                picDirPath = fileInfo[i].FullName;
                photo = fileInfo[i].Name;
                path = @"../img/R-C.jpg";
                //记录图片源路径 双击显示图片时使用
                //imagePathList.Add(picDirPath);
                //imagePathList.Add(path);
                //图片加载到ImageList控件和imageList图片列表
                图像增强.strong.imageList2.Images.Add(photo, Image.FromFile(picDirPath));
                //图像增强.strong.imageList1.Images.Add(Image.FromFile(path));
            }
        }

        private void materialButton9_Click(object sender, EventArgs e)  //图像增强-清空图片
        {
            图像增强 strong = (图像增强)this.Owner;
            if (图像增强.strong.dataGridView1.Rows.Count == 0)
            {
                this.CloseParForm();
                图像增强 图像增强 = new 图像增强();
                this.OpenForm(图像增强);
                //panel3.Visible = !panel3.Visible;
            }
            else
            {
                图像增强.strong.dataGridView1.Rows.Clear();
                图像增强.strong.imageList1.Images.Clear();
                图像增强.strong.dataGridView1.Refresh();

            }
        }

        private void materialButton10_Click(object sender, EventArgs e)  //图像增强-保存图片
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();//用户选择需要保存的地址
            dialog.Description = "请选择染色体图像所在的文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = dialog.SelectedPath + "\\save_rst_picture";

                if (!Directory.Exists(path))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(path);
                    directoryInfo.Create();
                }

                Tools.GetImageListSave(path, PictureBoxList);
            }
        }

        private void materialButton12_Click(object sender, EventArgs e)  //历史记录-查询结果
        {
            DataTable dt = GetList();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("暂无此消息！");
                return;
            }
            for (int i = 1; i < 47; i++)
            {
                string imagepath = dt.Rows[0]["图片" + i].ToString();
                char[] separators = { '+' };
                string[] strings = imagepath.Split(separators);
                string path = dt.Rows[0]["目录"].ToString() + "/" + strings[0];
                //string path = Path.Join(dt.Rows[0]["目录"].ToString(), strings[0]);
                imagePredictList.Add(strings[1]);
                历史记录.history.imageList1.Images.Add(Image.FromFile(path));
            }
            //显示文件列表
            历史记录.history.listView1.Items.Clear();
            历史记录.history.listView1.LargeImageList = 历史记录.history.imageList1;
            历史记录.history.listView1.View = View.LargeIcon;        //大图标显示

            //开始绑定
            历史记录.history.listView1.BeginUpdate();
            //增加图片至ListView控件中
            for (int i = 0; i < 历史记录.history.imageList1.Images.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.ImageIndex = i;
                lvi.Text = "预测结果：" + imagePredictList[i] + "号染色体";//图片名称***********pic X

                历史记录.history.listView1.Items.Add(lvi);
            }
            历史记录.history.listView1.EndUpdate();
        }
        public DataTable GetList()
        {
            string date = dateTimePicker1.Text.ToString();
            string name = textBox2.Text.ToString();
            string room = comboBox4.Text.ToString();
            string id = textBox1.Text.ToString();
            string sql = string.Format("select * from Table_1,Table_2 where 时间=@时间 and 姓名=@姓名 and 科室=@科室 and Table_1.id=@id");
            SqlParameter[] scs = new SqlParameter[4];
            scs[0] = new SqlParameter("@时间", date);
            scs[1] = new SqlParameter("@姓名", name);
            scs[2] = new SqlParameter("@科室", room);
            scs[3] = new SqlParameter("@id", id);
            DataTable dt = SqlDbHelper.ExecuteDataTable(sql, CommandType.Text, scs);
            return dt;
        }

        private void materialButton11_Click(object sender, EventArgs e)  //历史记录-清空图片
        {
            if (历史记录.history.listView1.Items.Count != 0)
            {
                历史记录 history = (历史记录)this.Owner;
                历史记录.history.listView1.Clear();
            }
            else
            {
                MessageBox.Show("还未导入历史记录！");
                return;
            }
        }

    }
}
