using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer
{
    class UserTree :  Entity
    {
        public string UserName
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
        public int NumberOFChars
        {
            get;
            set;
        }
        public int NumerOfSpeciaux
        {
            get;
            set;
        }
        public Dossier Racine
        {
            get;
            set;
        }
        public UserTree(string userName)
        {
            UserName = userName;
            Password = generatePassword(5, 5);
            Racine = new Dossier("Racine","c:/", "Dossier Racine");

        }
        public string generatePassword(int nbChar, int nbSpec)
        {
            //j'ai défini une chaine qui contient tous les lettres Maj et Min
            string chars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            //une autre chaine qui contient nombre et les charactères spéciaux
            string speciaux = "0123456789!@$?_-";
            // c'est la chaines qui va contenir le pswd
            char [] chaine = new char[nbChar + nbSpec];
            Random rand = new Random();
            
            for (int i = 0; i < nbSpec ; i++ )
            {
                chaine[i] = speciaux[rand.Next(0, speciaux.Length)];

            }
            for (int i = 0; i < nbChar; i++)
            {
                chaine[i] = chars[rand.Next(0, chars.Length)];
            }

            return new String(chaine);
        }

    }
}
