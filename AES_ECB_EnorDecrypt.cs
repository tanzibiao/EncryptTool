using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptTool
{
    internal class AES_ECB_EnorDecrypt
    {
        /// <summary>
        /// AES加密，任意文件
        /// </summary>
        /// <param name="Data">被加密的明文</param>
        /// <param name="Key">密钥</param>
        /// <returns>密文</returns>
        public static byte[] AESEncrypt(byte[] Data, string Key)
        {
            byte[] bKey = new byte[Key.Length];//采用32位密码加密
            Array.Copy(Encoding.UTF8.GetBytes(Key.PadRight(bKey.Length)), bKey, bKey.Length);//如果用户输入的密码不足32位，自动填充空格至32位
            byte[] Cryptograph = null;//加密后的密文
            RijndaelManaged Aes = new RijndaelManaged();
            Aes.Mode = CipherMode.ECB;
            Aes.Padding = PaddingMode.PKCS7;
            Aes.KeySize = 128;
            Aes.Key = bKey;
            try
            {
                using (MemoryStream Memory = new MemoryStream())
                {
                    //把内存流对象包装成加密流对象
                    using (CryptoStream Encryptor = new CryptoStream(Memory, Aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        Encryptor.Write(Data, 0, Data.Length);
                        Encryptor.FlushFinalBlock();
                        Cryptograph = Memory.ToArray();
                    }
                }
            }
            catch
            {
                Cryptograph = null;
            }
            return Cryptograph;
        }

        /// <summary>
        /// AES解密，任意文件
        /// </summary>
        /// <param name="Data">被解密的密文</param>
        /// <param name="Key">密钥</param>
        /// <returns>明文</returns>
        public static byte[] AESDecrypt(byte[] Data, string Key)
        {
            byte[] bKey = new byte[Key.Length];
            Array.Copy(Encoding.UTF8.GetBytes(Key.PadRight(bKey.Length)), bKey, bKey.Length);
            byte[] original = null; // 解密后的明文
            RijndaelManaged Aes = new RijndaelManaged();
            Aes.Mode = CipherMode.ECB;
            Aes.Padding = PaddingMode.None;
            Aes.KeySize = 128;
            Aes.Key = bKey;
            try
            {
                using (MemoryStream Memory = new MemoryStream(Data))
                {
                    // 把内存流对象包装成加密对象
                    using (CryptoStream Decryptor = new CryptoStream(Memory, Aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        // 明文存储区
                        using (MemoryStream originalMemory = new MemoryStream())
                        {
                            int readByte;
                            while ((readByte = Decryptor.ReadByte()) != -1)
                            {
                                originalMemory.WriteByte((byte)readByte);
                            }
                            original = originalMemory.ToArray();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("异常");
                Console.WriteLine(ex);
                original = null;
            }
            return original;
        }


    }
}
