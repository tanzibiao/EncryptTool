using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EncryptTool
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            IList<SelectItem> list = new List<SelectItem>();
            list.Add(new SelectItem() { Key = "AES_ECB", Value = "AES加解密_ECB模式" });
            cmbxEncryptType.DataSource = list;
            cmbxEncryptType.ValueMember = "Key";
            cmbxEncryptType.DisplayMember = "Value";

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            //秘钥非空校验
            string encryptKey = txtEncryptKey.Text;
            if (txtEncryptKey.Text == "" || encryptKey == null) {
                MessageBox.Show("请输入秘钥");
                return;
            }
            //加密文件，打开资源管理器，选择文件
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件";
            dialog.Filter = "所有文件(*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = dialog.FileName;
                string path = Path.GetDirectoryName(fileName);//扩展名
                string extension = Path.GetExtension(fileName);//扩展名
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);// 没有扩展名的文件名
                // 读取文件为字节流
                FileStream sr = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                // 解密后路径
                string savePath = path + "//" + fileNameWithoutExtension + "_encrypt" + extension;
                FileStream sw = new FileStream(savePath, FileMode.Create, FileAccess.Write);
                Console.WriteLine("保存路径=" + path);
                if (sr.Length > 50 * 1024 * 1024)//如果文件大于50M，采取分块加密，按50MB读写
                {
                    byte[] mybyte = new byte[52428800];//每50MB加密一次                  
                    int numBytesRead = 52428800;//每次加密的流大小
                    long leftBytes = sr.Length;//剩余需要加密的流大小
                    long readBytes = 0;//已经读取的流大小
                                       //每50MB加密后会变成50MB+16B
                    byte[] encrpy = new byte[52428816];
                    while (true)
                    {
                        if (leftBytes > numBytesRead)
                        {
                            sr.Read(mybyte, 0, mybyte.Length);
                            encrpy = AES_ECB_EnorDecrypt.AESEncrypt(mybyte, encryptKey);
                            sw.Write(encrpy, 0, encrpy.Length);
                            leftBytes -= numBytesRead;
                            readBytes += numBytesRead;
                        }
                        else//重新设定读取流大小，避免最后多余空值
                        {
                            byte[] newByte = new byte[leftBytes];
                            sr.Read(newByte, 0, newByte.Length);
                            byte[] newWriteByte;
                            newWriteByte = AES_ECB_EnorDecrypt.AESEncrypt(newByte, encryptKey);
                            sw.Write(newWriteByte, 0, newWriteByte.Length);
                            readBytes += leftBytes;
                            break;
                        }
                    }
                }
                else
                {
                    byte[] mybyte = new byte[sr.Length];
                    sr.Read(mybyte, 0, (int)sr.Length);
                    mybyte = AES_ECB_EnorDecrypt.AESEncrypt(mybyte, encryptKey);
                    sw.Write(mybyte, 0, mybyte.Length);
                }

                sr.Close();
                sw.Close();
                MessageBox.Show("文件加密成功，保存路径=" + savePath);
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            //秘钥非空校验
            string encryptKey = txtEncryptKey.Text;
            if (txtEncryptKey.Text == "" || encryptKey == null)
            {
                MessageBox.Show("请输入秘钥");
                return;
            }
            //加密文件，打开资源管理器，选择文件
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件";
            dialog.Filter = "所有文件(*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = dialog.FileName;
                string path = Path.GetDirectoryName(fileName);//目录
                string extension = Path.GetExtension(fileName);//扩展名
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);// 没有扩展名的文件名
                // 读取文件为字节流
                FileStream sr = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                // 解密后路径
                string savePath = path + "//" + fileNameWithoutExtension + "_decrypt" + extension;
                FileStream sw = new FileStream(savePath, FileMode.Create, FileAccess.Write);
                if (sr.Length > 50 * 1024 * 1024)//如果文件大于50M，采取分块加密，按50MB读写
                {
                    byte[] mybyte = new byte[52428800];//每50MB加密一次                  
                    int numBytesRead = 52428800;//每次加密的流大小
                    long leftBytes = sr.Length;//剩余需要加密的流大小
                    long readBytes = 0;//已经读取的流大小
                                       //每50MB加密后会变成50MB+16B
                    byte[] encrpy = new byte[52428816];
                    while (true)
                    {
                        if (leftBytes > numBytesRead)
                        {
                            sr.Read(mybyte, 0, mybyte.Length);
                            encrpy = AES_ECB_EnorDecrypt.AESDecrypt(mybyte, encryptKey);
                            sw.Write(encrpy, 0, encrpy.Length);
                            leftBytes -= numBytesRead;
                            readBytes += numBytesRead;
                        }
                        else//重新设定读取流大小，避免最后多余空值
                        {
                            byte[] newByte = new byte[leftBytes];
                            sr.Read(newByte, 0, newByte.Length);
                            byte[] newWriteByte;
                            newWriteByte = AES_ECB_EnorDecrypt.AESDecrypt(newByte, encryptKey);
                            sw.Write(newWriteByte, 0, newWriteByte.Length);
                            readBytes += leftBytes;
                            break;
                        }
                    }
                }
                else
                {
                    byte[] mybyte = new byte[sr.Length];
                    sr.Read(mybyte, 0, (int)sr.Length);
                    mybyte = AES_ECB_EnorDecrypt.AESDecrypt(mybyte, encryptKey);
                    sw.Write(mybyte, 0, mybyte.Length);
                }

                sr.Close();
                sw.Close();
                MessageBox.Show("文件解密成功，保存路径=" + savePath);
            }
        }
    }
}
