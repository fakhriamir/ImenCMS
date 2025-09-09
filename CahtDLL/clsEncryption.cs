namespace ChatDLL
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    internal class clsEncryption
    {
        private byte[] Key;
        private const string strKEY = "81wUWvdGb";
        private byte[] Vector;

        public clsEncryption()
        {
            this.GenerateKey("81wUWvdGb");
        }

        public string Decrypt(string encryptedText)
        {
            if (this.Key == null)
            {
                throw new InvalidOperationException("Password must be provided or set.");
            }
            byte[] buffer = Convert.FromBase64String(encryptedText);
            ICryptoTransform transform = new RijndaelManaged().CreateDecryptor(this.Key, this.Vector);
            MemoryStream stream = new MemoryStream(buffer);
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
            byte[] buffer2 = new byte[buffer.Length];
            int count = stream2.Read(buffer2, 0, buffer2.Length);
            stream.Close();
            stream2.Close();
            return new ASCIIEncoding().GetString(buffer2, 0, count);
        }

        public string Decrypt(string encryptedText, string password)
        {
            this.GenerateKey(password);
            return this.Decrypt(encryptedText);
        }

        public string Encrypt(string plainText)
        {
            if (this.Key == null)
            {
                throw new InvalidOperationException("Password must be provided or set.");
            }
            byte[] bytes = new ASCIIEncoding().GetBytes(plainText);
            ICryptoTransform transform = new RijndaelManaged().CreateEncryptor(this.Key, this.Vector);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            stream2.Close();
            stream.Close();
            return Convert.ToBase64String(stream.ToArray());
        }

        public string Encrypt(string plainText, string password)
        {
            this.GenerateKey(password);
            return this.Encrypt(plainText);
        }

        private void GenerateKey(string password)
        {
            byte[] sourceArray = new SHA384Managed().ComputeHash(new ASCIIEncoding().GetBytes(password));
            this.Key = new byte[0x20];
            this.Vector = new byte[0x10];
            Array.Copy(sourceArray, 0, this.Key, 0, 0x20);
            Array.Copy(sourceArray, 0x20, this.Vector, 0, 0x10);
        }

        public string Password
        {
            set
            {
                this.GenerateKey(value);
            }
        }
    }
}

