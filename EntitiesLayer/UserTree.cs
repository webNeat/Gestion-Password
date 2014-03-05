using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EntitiesLayer
{
    [Serializable]
    public class UserTree :  Entity
    {
        [XmlAttribute]
        public string UserName
        {
            get;
            set;
        }

        [XmlAttribute]
        public UserSettings Settings
        {
            get;
            set;
        }

        [XmlAttribute]
        public Dossier Racine
        {
            get;
            set;
        }


        public UserTree()
        {
            Settings = new UserSettings();
            Settings.NumberOFChars = 5;
            Settings.NumerOfSpeciaux = 5;
            
        }
       
        public UserTree(string userName, string pass, int nbc, int nbs)
        {
            Settings = new UserSettings();
            Settings.NumberOFChars = nbc;
            Settings.NumerOfSpeciaux = nbs;
            UserName = userName;
            // Settings.Password = generatePassword();
            Settings.Password = pass;
            Racine = new Dossier(userName,"", "Dossier Racine de " + userName);
        }

        public string generatePassword()
        {
            //j'ai défini une chaine qui contient tous les lettres Maj et Min
            string chars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            //une autre chaine qui contient nombre et les charactères spéciaux
            string speciaux = "!@$?_-(){}[]|";
            // c'est la chaines qui va contenir le pswd
            char[] chaine = new char[Settings.NumberOFChars + Settings.NumerOfSpeciaux];
            Random rand = new Random();

            for (int i = 0; i < Settings.NumerOfSpeciaux; i++)
            {
                chaine[i] = speciaux[rand.Next(0, speciaux.Length)];

            }
            for (int i = Settings.NumerOfSpeciaux; i < (Settings.NumberOFChars + Settings.NumerOfSpeciaux); i++)
            {
                chaine[i] = chars[rand.Next(0, chars.Length)];
            }
            // mélanger la chaine avant de retourner
            String mix = new String(chaine.OrderBy(s => (rand.Next(2) % 2) == 0).ToArray());
            return mix;
        }

        public override string ToString()
        {
            return "Nom Utilisateur  : " + UserName + " Paswword : " + Settings.Password + " DateC <" + Created + "> DateM <" + Modified + ">\n" + Racine.ToString();
        }

    }
}
