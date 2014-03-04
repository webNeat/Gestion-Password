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
        public string Password
        {
            get;
            set;
        }


        [XmlAttribute]
        public int NumberOFChars
        {
            get;
            set;
        }


        [XmlAttribute]
        public int NumerOfSpeciaux
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
            NumberOFChars = 5;
            NumerOfSpeciaux =5;
            
        }
       
        public UserTree(string userName, int nbc, int nbs)
        {
            NumberOFChars = nbc;
            NumerOfSpeciaux = nbs;
            UserName = userName;
            Password = generatePassword();
            Racine = new Dossier("Racine","c:/", "Dossier Racine");

        }
        public string generatePassword()
        {
            //j'ai défini une chaine qui contient tous les lettres Maj et Min
            string chars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            //une autre chaine qui contient nombre et les charactères spéciaux
            string speciaux = "!@$?_-(){}[]|";
            // c'est la chaines qui va contenir le pswd
            char [] chaine = new char[NumberOFChars + NumerOfSpeciaux];
            Random rand = new Random();
            
            for (int i = 0; i < NumerOfSpeciaux ; i++ )
            {
                chaine[i] = speciaux[rand.Next(0, speciaux.Length)];

            }
            for (int i = NumerOfSpeciaux; i < (NumberOFChars + NumerOfSpeciaux); i++)
            {
                chaine[i] = chars[rand.Next(0, chars.Length)];
            }
            // mélanger la chaine avant de retourner
            String mix = new String(chaine.OrderBy(s => (rand.Next(2) % 2) == 0).ToArray());
            return mix;
        }

        public override string ToString()
        {
            return "Nom Utilisateur  : " + UserName + " Paswword : " + Password + " DateC <" + Created + "> DateM <" + Modified + ">\n" + Racine.ToString();
        }

    }
}
