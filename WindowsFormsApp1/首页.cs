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
            if(panel2.Visible == true)
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

        private void materialLabel1_Click(object sender, EventArgs e)  // 首页-染色体分类
        {
            this.CloseParForm();
            染色体分类 染色体分类 = new 染色体分类();
            this.OpenForm(染色体分类);
            panel2.Visible = !panel2.Visible;
        }

        private void materialLabel2_Click(object sender, EventArgs e)  //首页-图像增强
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
                    //获取当前目录JPG文件列表 GetFiles获取指定目录中文件的名称(包括其路径)
                    FileInfo[] fileInfo = dir.GetFiles("*.jpg");
                    //防止图片失真
                    染色体分类.chromosome.imageList1.ColorDepth = ColorDepth.Depth32Bit;

                    //****
                    int count = fileInfo.Length;
                    image_item = new string[count];
                    count_image = count;
                    submit_result = new string[count_image, 2];
                    for (int i = 0; i < fileInfo.Length; i++)
                    {
                        //获取文件完整目录
                        picDirPath = fileInfo[i].FullName;
                        image_item[i] = picDirPath;
                        //记录图片源路径 双击显示图片时使用
                        imagePathList.Add(picDirPath);
                        //图片加载到ImageList控件和imageList图片列表
                        染色体分类.chromosome.imageList1.Images.Add(Image.FromFile(picDirPath));
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
            RunPythonScript(sArguments, "-u", image_item);
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
            }
            染色体分类.chromosome.listView1.EndUpdate();
            //将数据添加至数据库当中

            MessageBox.Show("提交成功！");
        }

        public static void RunPythonScript(string sArgName, string args = "", params string[] teps)
        {
            submit_or_not = true;
            history_submit_or_not = true;
            Process p = new Process();
            string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + sArgName;// 获得python文件的绝对路径（将文件放在c#的debug文件夹中可以这样操作）
            path = @"./" + sArgName;
            //path = @"C:\Users\user\Desktop\test\"+sArgName;//(因为我没放debug下，所以直接写的绝对路径,替换掉上面的路径了)
            //p.StartInfo.FileName = @"D:\Python\envs\python3\python.exe";//没有配环境变量的话，可以像我这样写python.exe的绝对路径。如果配了，直接写"python.exe"即可
            p.StartInfo.FileName = @"python.exe";
            string sArguments = path;
            foreach (string sigstr in teps)
            {
                sArguments += " " + sigstr;//传递参数
            }

            sArguments += " " + args;
            foreach (string sigstr in teps)
            {
                sArguments += " " + sigstr;//传递参数
            }

            sArguments += " " + args;

            p.StartInfo.Arguments = sArguments;

            p.StartInfo.UseShellExecute = false;

            p.StartInfo.RedirectStandardOutput = true;

            p.StartInfo.RedirectStandardInput = true;

            p.StartInfo.RedirectStandardError = false;

            p.StartInfo.CreateNoWindow = true;

            p.Start();
            p.BeginOutputReadLine();
            p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            i = 0;
            Console.ReadLine();
            p.WaitForExit();
            p.Close();
        }
        public static void RunPythonScript_D(string sArgName, string args = "", params string[] teps)//图像增强
        {
            submit_or_not = true;
            history_submit_or_not = true;

            Process p = new Process();
            string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + sArgName;// 获得python文件的绝对路径（将文件放在c#的debug文件夹中可以这样操作）
            path = @"./" + sArgName;
            p.StartInfo.FileName = @"python.exe";

            string sArguments = path;

            foreach (string sigstr in teps)
            {
                sArguments += " " + sigstr;//传递参数
            }

            sArguments += " " + args;

            p.StartInfo.Arguments = sArguments;

            p.StartInfo.UseShellExecute = false;

            p.StartInfo.RedirectStandardOutput = true;

            p.StartInfo.RedirectStandardInput = true;

            p.StartInfo.RedirectStandardError = false;

            p.StartInfo.CreateNoWindow = true;

            p.Start();
            Console.ReadLine();
            p.WaitForExit();
            p.Close();
        }

        public static void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            //返回n条语句执行n次

            if (!string.IsNullOrEmpty(e.Data))
            {
                submit_retain += e.Data + ",";
                string b = e.Data.ToString();
                if (i < count_image)
                {
                    string[] a = b.Split(new string[] { "," }, StringSplitOptions.None);
                    submit_result[i, 0] = a[0];//预测结果
                    submit_result[i, 1] = a[1];//预测概率
                    i++;
                }
            }
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
                        path = @"../img/R-C.jpg";
                        //记录图片源路径 双击显示图片时使用
                        imagePathList.Add(picDirPath);
                        //imagePathList.Add(path);
                        //图片加载到ImageList控件和imageList图片列表
                        图像增强.strong.imageList1.Images.Add(Image.FromFile(picDirPath));
                        //图像增强.strong.imageList1.Images.Add(Image.FromFile(path));
                    }
               
                    //增加图片至datagridview控件中     
                    for (int i = 0; i < 图像增强.strong.imageList1.Images.Count; i++)
                    {
                        k++;
                        l++;                
                        if(i <= 46) //datagridview里的第一行
                        {
                            DataGridViewRow dr = new DataGridViewRow();
                            dr.Height = 40;  //行高
                            dr.CreateCells(图像增强.strong.dataGridView1);
                            for (int m = 0; m < 47; m++)
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
                        if(i > 46)
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
                            for(int m = 0; m < 47; m++)
                            {
                                if(m == 0)
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

        }

        private void materialButton12_Click(object sender, EventArgs e)  //历史记录-查询结果
        {
            //string date = dateTimePicker1.Text.ToString();
            //string name = textBox2.Text.ToString();
            //string room = comboBox4.Text.ToString();
            //string filter = $"时间='{date}' and 姓名='{name}' and 科室='{room}' and Table_1.id={textBox1.Text}";

            //dt：患者列表
            DataTable dt = GetList();
            if(dt.Rows.Count == 0)
            {
                MessageBox.Show("暂无此消息！");
                return;
            }
            for(int i = 1; i < 47; i++)
            {
                string imagepath = dt.Rows[0]["图片"+i].ToString();
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
            //string filter = $"时间='{date}' and 姓名='{name}' and 科室='{room}' and Table_1.id={textBox1.Text}";
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
            if(历史记录.history.listView1.Items.Count != 0)
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
