using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer;
using System.Xml.Serialization;
using System.IO;
namespace FileLayer
{
    public class XMLFile : IDatabase
    {
        public void save(UserTree user)
        {
            //On crée une instance de XmlSerializer dans lequel on lui spécifie le type de l'objet à sérialiser
            XmlSerializer serilizer = new XmlSerializer(typeof(UserTree));
            //Création d'un Stream Writer qui permet d'écrire dans un fichier. On lui spécifie le chemin.   
            using(StreamWriter stream = new StreamWriter(getFileName(Environment.UserName), false))
            {
                //On sérialise en spécifiant le flux d'écriture et l'objet à sérialiser.
                serilizer.Serialize(stream, user);
                
            }

            
        }

        public UserTree load(string userName)
        {
            //On crée une instance de XmlSerializer dans lequel on lui spécifie le type
            XmlSerializer serializer = new XmlSerializer(typeof(UserTree));
            //Création d'un StreamReader qui permet de lire un fichier. On lui spécifie le chemin.
            using (StreamReader stream = new StreamReader(getFileName(userName)))
            {
                //On obtient une instance de l'objet désérialisé.
                UserTree user = (UserTree)serializer.Deserialize(stream);
                //On ferme le flux en tout temps !!!
                return user;
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
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + userName + ".xml";         
            return path;
        }
    }
}
