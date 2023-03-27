﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptTool
{
    internal class AES_EnorDecrypt
    {
        /// <summary>
        /// AES加密，任意文件
        /// </summary>
        /// <param name="Data">被加密的明文</param>
        /// <param name="Key">密钥</param>
        /// <param name="Vector">密钥向量</param>
        /// <returns>密文</returns>
        public static byte[] AESEncrypt(byte[] Data, string Key, string Vector)
        {
            byte[] bKey = new byte[32];//采用32位密码加密
            Array.Copy(Encoding.UTF8.GetBytes(Key.PadRight(bKey.Length)), bKey, bKey.Length);//如果用户输入的密码不足32位，自动填充空格至32位
            byte[] bVector = new byte[16];//密钥向量，为16位
            Array.Copy(Encoding.UTF8.GetBytes(Vector.PadRight(bVector.Length)), bVector, bVector.Length);//如果用户定义的密钥向量不足16位，自动填充空格至16位
            byte[] Cryptograph = null;//加密后的密文
            Rijndael Aes = Rijndael.Create();
            try
            {
                using (MemoryStream Memory = new MemoryStream())
                {
                    //把内存流对象包装成加密流对象
                    using (CryptoStream Encryptor = new CryptoStream(Memory, Aes.CreateEncryptor(bKey, bVector), CryptoStreamMode.Write))
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
        /// <param name="Vector">密钥向量</param>
        /// <returns>明文</returns>
        public static byte[] AESDecrypt(byte[] Data, string Key, string Vector)
        {
            byte[] bKey = new byte[32];
            Array.Copy(Encoding.UTF8.GetBytes(Key.PadRight(bKey.Length)), bKey, bKey.Length);
            byte[] bVector = new byte[16];
            Array.Copy(Encoding.UTF8.GetBytes(Vector.PadRight(bVector.Length)), bVector, bVector.Length);
            byte[] original = null;//解密后的明文
            Rijndael Aes = Rijndael.Create();
            try
            {
                using (MemoryStream Memory = new MemoryStream(Data))
                {
                    //把内存流对象包装成加密对象
                    using (CryptoStream Decryptor = new CryptoStream(Memory, Aes.CreateDecryptor(bKey, bVector), CryptoStreamMode.Read))
                    {
                        //明文存储区
                        using (MemoryStream originalMemory = new MemoryStream())
                        {
                            byte[] Buffer = new byte[1024];
                            int readBytes = 0;
                            while ((readBytes = Decryptor.Read(Buffer, 0, Buffer.Length)) > 0)
                            {
                                originalMemory.Write(Buffer, 0, readBytes);
                            }
                            original = originalMemory.ToArray();
                        }
                    }
                }
            }
            catch
            {
                original = null;
            }
            return original;
        }
    }
}
