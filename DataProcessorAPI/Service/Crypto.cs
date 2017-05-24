using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DataProcessorAPI.Service
{
    public class Crypto
    {
        public static byte[] decrypt(byte[] encrypted, string key)
        {
            ICryptoTransform cryptoTransform = Crypto.getAESECBCipher(key).CreateDecryptor();
            CryptoStream cryptoStream = new CryptoStream(new MemoryStream(encrypted), cryptoTransform, CryptoStreamMode.Read);
            byte[] numArray = new byte[(int)encrypted.Length];
            int num = 0;
            long num1 = (long)0;
            while (true)
            {
                int num2 = cryptoStream.ReadByte();
                num = num2;
                if (num2 <= 0)
                {
                    break;
                }
                long num3 = num1;
                num1 = num3 + (long)1;
                numArray[checked(num3)] = (byte)num;
            }
            byte[] numArray1 = new byte[checked(num1)];
            Array.Copy(numArray, numArray1, num1);
            return numArray1;
        }

        public static byte[] encrypt(string input, string key)
        {
            Console.WriteLine(string.Concat("Encrypting: ", input));
            ICryptoTransform cryptoTransform = Crypto.getAESECBCipher(key).CreateEncryptor();
            MemoryStream memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(input));
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read);
            List<byte> nums = new List<byte>();
            while (true)
            {
                int num = cryptoStream.ReadByte();
                int num1 = num;
                if (num == -1)
                {
                    break;
                }
                nums.Add((byte)num1);
            }
            byte[] numArray = new byte[nums.Count];
            Array.Copy(nums.ToArray(), numArray, nums.Count);
            return numArray;
        }

        public static AesCryptoServiceProvider getAESECBCipher(string key)
        {
            AesCryptoServiceProvider aesCryptoServiceProvider = new AesCryptoServiceProvider()
            {
                KeySize = 128,
                BlockSize = 128,
                IV = new byte[] { 1, 2, 3, 4, 5, 6, 6, 5, 4, 3, 2, 1, 7, 7, 7, 7 },
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                Key = Crypto.StringToByteArray(key)
            };
            Console.WriteLine("Key is good!");
            return aesCryptoServiceProvider;
        }

        public static byte[] StringToByteArray(string hex)
        {
            int length = hex.Length;
            byte[] num = new byte[length / 2];
            for (int i = 0; i < length; i = i + 2)
            {
                num[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return num;
        }
    }
}
