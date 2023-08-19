using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace EncryptTool
{
    public partial class MainForm : Form
    {
        
        private string type = "";
        private string[] files = null;
        public MainForm()
        {
            InitializeComponent();
            IList<SelectItem> list = new List<SelectItem>();
            list.Add(new SelectItem() { Key = "AES_ECB", Value = "AES加解密_ECB模式" });
            cmbxEncryptType.DataSource = list;
            cmbxEncryptType.ValueMember = "Key";
            cmbxEncryptType.DisplayMember = "Value";
            progress_backgroundWorker.WorkerReportsProgress = true;//设置能报告进度更新
            progress_backgroundWorker.WorkerSupportsCancellation = true;//设置支持异步取消

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConfigData configData = getConfigData();
            if (configData != null)
            {
                if (configData.Settings.TryGetValue("encryptKey", out string value))
                {
                    txtEncryptKey.Text = value;
                }
            }
        }

        private static ConfigData getConfigData()
        {
            // 获取 exe 程序的目录
            string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // 构造配置文件的完整路径
            string configFilePath = Path.Combine(exeDirectory, "config.json");

            // 读取配置文件中的设置
            ConfigData configData = ReadConfigFromFile(configFilePath);
            return configData;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
           
            this.type = "encrypt";
            //秘钥非空校验
            string encryptKey = txtEncryptKey.Text;
            if (txtEncryptKey.Text == "" || encryptKey == null)
            {
                MessageBox.Show("请输入秘钥");
                return;
            }
            //加密文件，打开资源管理器，选择文件
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件";
            dialog.Filter = "所有文件(*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (dialog.FileNames.Length > 0) {
                    files = dialog.FileNames;
                    this.progress_backgroundWorker.RunWorkerAsync();//运行backgroundWorker组件
                    ProgressForm form = new ProgressForm(this.progress_backgroundWorker);  //显示进度条窗体
                    form.ShowDialog(this);
                    form.Close();
                }
            }
            
        }

        //在另一个线程上开始运行(处理进度条)
        private void progress_backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (this.type.Equals("encrypt")) {
                doEncrypt(sender, e);
            } if (this.type.Equals("decrypt"))
            {
                doEncrypt(sender, e);
            }

        }

        private void doEncrypt(object sender, DoWorkEventArgs e)
        {
            string encryptKey = txtEncryptKey.Text;
            BackgroundWorker worker = sender as BackgroundWorker;
            long totalSize = 0;
            for (int i = 0; i < files.Length; i++)
            {
                totalSize += new FileStream(files[i], FileMode.Open, FileAccess.Read).Length;
            }
            Console.WriteLine($"总大小={totalSize}");
            for (int i = 0; i < files.Length; i++)
            {
                string fileName = files[i];
                string path = Path.GetDirectoryName(fileName);//扩展名
                string extension = Path.GetExtension(fileName);//扩展名
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);// 没有扩展名的文件名
                                                                                             // 读取文件为字节流
                FileStream sr = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                // 解密后路径
                string savePath = path + "//" + fileNameWithoutExtension + "_" + this.type + extension;
                FileStream sw = new FileStream(savePath, FileMode.Create, FileAccess.Write);
                Console.WriteLine("保存路径=" + path);
                const int V = 1 * 1024 * 1024;
                if (sr.Length > V)//如果文件大于50M，采取分块加密，按50MB读写
                {
                    byte[] mybyte = new byte[V];//每50MB加密一次                  
                    int numBytesRead = V;//每次加密的流大小
                    long leftBytes = sr.Length;//剩余需要加密的流大小
                    long readBytes = 0;//已经读取的流大小
                                       //每50MB加密后会变成50MB+16B
                    byte[] encrpy = new byte[V+16];
                    while (true)
                    {
                        if (leftBytes > numBytesRead)
                        {
                            sr.Read(mybyte, 0, mybyte.Length);
                            if (this.type.Equals("encrypt"))
                            {
                                encrpy = AES_ECB_EnorDecrypt.AESEncrypt(mybyte, encryptKey);
                            }
                            if (this.type.Equals("decrypt"))
                            {
                                encrpy = AES_ECB_EnorDecrypt.AESDecrypt(mybyte, encryptKey);
                            }
                            Console.WriteLine("已处理：" + readBytes);
                            sw.Write(encrpy, 0, encrpy.Length);
                            leftBytes -= numBytesRead;
                            readBytes += numBytesRead;

                            int percentProgress = (int)((double)readBytes / totalSize * 100);
                            worker.ReportProgress(percentProgress);
                            if (worker.CancellationPending) //获取程序是否已请求取消后台操作
                            {
                                e.Cancel = true;
                                break;
                            }
                        }
                        else//重新设定读取流大小，避免最后多余空值
                        {
                            byte[] newByte = new byte[leftBytes];
                            sr.Read(newByte, 0, newByte.Length);
                            byte[] newWriteByte = null;
                            if(this.type.Equals("encrypt"))
                            {
                                newWriteByte = AES_ECB_EnorDecrypt.AESEncrypt(newByte, encryptKey);
                            }
                            if (this.type.Equals("decrypt"))
                            {
                                newWriteByte = AES_ECB_EnorDecrypt.AESDecrypt(newByte, encryptKey);
                            }
                            
                            sw.Write(newWriteByte, 0, newWriteByte.Length);
                            readBytes += leftBytes;
                            worker.ReportProgress((int)((double)readBytes / totalSize * 100));
                            if (worker.CancellationPending) //获取程序是否已请求取消后台操作
                            {
                                e.Cancel = true;
                                break;
                            }
                            break;
                        }
                    }
                }
                else
                {
                    byte[] mybyte = new byte[sr.Length];
                    sr.Read(mybyte, 0, (int)sr.Length);
                    if (this.type.Equals("encrypt"))
                    {
                        mybyte = AES_ECB_EnorDecrypt.AESEncrypt(mybyte, encryptKey);
                    }
                    if (this.type.Equals("decrypt"))
                    {
                        mybyte = AES_ECB_EnorDecrypt.AESDecrypt(mybyte, encryptKey);
                    }
                    sw.Write(mybyte, 0, mybyte.Length);
                    worker.ReportProgress((int)((int)sr.Length / totalSize * 100));
                    if (worker.CancellationPending) //获取程序是否已请求取消后台操作
                    {
                        e.Cancel = true;
                        break;
                    }
                }

                sr.Close();
                sw.Close();
            }

            //保存秘钥
            ConfigData configData = getConfigData();
            if (configData == null) {
                configData = new ConfigData();
            }
            configData.Settings["encryptKey"] = encryptKey;
            // 获取 exe 程序的目录
            string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
            // 构造配置文件的完整路径
            string configFilePath = Path.Combine(exeDirectory, "config.json");
            // 写入配置文件
            WriteConfigToFile(configFilePath, configData);
        }

        private void progress_backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                MessageBox.Show("取消");
            }
            else
            {
                MessageBox.Show("完成");
            }
        }

        private void progress_backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //Console.WriteLine($"进度更新={e.ProgressPercentage}");
            //this.progressBar1.Value = e.ProgressPercentage; // 更新进度条值
        }
        private void btnDecrypt_Click(object sender, EventArgs e)
        {

            this.type = "decrypt";
            //秘钥非空校验
            string encryptKey = txtEncryptKey.Text;
            if (txtEncryptKey.Text == "" || encryptKey == null)
            {
                MessageBox.Show("请输入秘钥");
                return;
            }
            //加密文件，打开资源管理器，选择文件
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件";
            dialog.Filter = "所有文件(*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (dialog.FileNames.Length > 0)
                {
                    files = dialog.FileNames;
                    progress_backgroundWorker.RunWorkerAsync();//运行backgroundWorker组件
                    ProgressForm form = new ProgressForm(this.progress_backgroundWorker);  //显示进度条窗体
                    form.ShowDialog(this);
                    form.Close();
                }
            }
        }

        static ConfigData ReadConfigFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<ConfigData>(json);
            }
            return new ConfigData();
        }

        static void WriteConfigToFile(string filePath, ConfigData configData)
        {
            string json = JsonConvert.SerializeObject(configData, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
        class ConfigData
        {
            public Dictionary<string, string> Settings { get; set; } = new Dictionary<string, string>();
        }
    }
}
