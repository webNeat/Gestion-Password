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
    public class BinaryFile : IDataBAse
    {
        private string key = null;

        public void save(UserTree user)
        {
       
            using(Stream stream = File.Open(getFileName(Environment.UserName), FileMode.Create))
            {
                using (SymmetricAlgorithm algo = SymmetricAlgorithm.Create(user.Password))
                {//algo = null cela du symetric algorithmFactory ??
                    using(CryptoStream encryptedFile = new CryptoStream(stream, algo.CreateEncryptor(), CryptoStreamMode.Write))
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
                using (SymmetricAlgorithm algorithm = SymmetricAlgorithm.Create(key))
                {//SymmetricAlgorithmFactory
                    using (CryptoStream encryptedFile = new CryptoStream(stream, algorithm.CreateDecryptor(), CryptoStreamMode.Read))
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
