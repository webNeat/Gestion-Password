using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
namespace FileLayer
{
    public class BinaryFile : IDatabase
    {

        private byte[] Key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16};
        private byte[] IV = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };

        public void save(UserTree user)
        {
       
            using(Stream stream = File.Open(getFileName(Environment.UserName), FileMode.Create))
            {
                using (RijndaelManaged algo = new RijndaelManaged())
                {
                    using(CryptoStream encryptedFile = new CryptoStream(stream, algo.CreateEncryptor(Key,IV), CryptoStreamMode.Write))
                    {
                        BinaryFormatter formater = new BinaryFormatter();
                        formater.Serialize(encryptedFile, user);
                       
                    }

                }
            }
          

        }

        public UserTree load(string userName)
        {
            if (userName == null)
                throw new ArgumentNullException(userName);
            if (!hasFile(userName))
                throw new ArgumentOutOfRangeException(userName);
            using (FileStream stream = File.Open(getFileName(userName), FileMode.Open))
            {
                using (RijndaelManaged algorithm = new RijndaelManaged())
                {
                    using (CryptoStream encryptedFile = new CryptoStream(stream, algorithm.CreateDecryptor(Key, IV), CryptoStreamMode.Read))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        return (UserTree)formatter.Deserialize(encryptedFile);
                    }
                }
            }
           
        }

        public bool hasFile(string userName)
        {
            if (File.Exists(getFileName(userName)))
                return true;
            return false;
        }

        public string getFileName(string userName)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + userName + ".batbin";
            return path;
        }
    }
}
