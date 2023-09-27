using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal class Tools
    {
        public static string[,] submit_result;//二维数组存结果
        public static string submit_retain;//暂存结果
        public static int count_image = 0;
        public static int i = 0;
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
        public static string[,] RunPythonScript(string sArgName, int count, string args = "", params string[] teps)
        {
            count_image = count;
            submit_result = new string[count_image, 3];
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
            return submit_result; 
        }
        public static string[,] RunPythonScript_D(string sArgName, List<string> teps, string args = "")//图像增强
        {

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
            i = 0;
            p.WaitForExit();
            p.Close();
            return submit_result;
        }

        /// <summary>
        /// 获取imageList里面图片，保存到指定路径文件夹中
        /// </summary>
        /// <param name="imageList">待提取图片的imageList</param>
        /// <param name="path">想要保存图片的文件夹路径，注意文件夹想要存在</param>
        /// <returns></returns>
        public static bool GetImageListSave(string path, List<PictureBox> PictureBoxList)
        {
            if (PictureBoxList.Count <= 0)
            {
                return false;
            }
            for (int i = 0; i < PictureBoxList.Count; i++)
            {
                try
                {

                    DirectoryInfo root = new DirectoryInfo(path);
                    FileInfo[] files = root.GetFiles();
                    int count = files.Length + i + 1;

                    PictureBoxList[i].Image.Save(path + "\\" + PictureBoxList[i].Name);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("保存图片错误：" + ex.Message);
                    return false;
                }
            }
            return true;
        }

    }

}
