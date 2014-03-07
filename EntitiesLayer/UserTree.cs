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

      

        public override string ToString()
        {
            return "Nom Utilisateur  : " + UserName + " Paswword : " + Settings.Password + " DateC <" + Created + "> DateM <" + Modified + ">\n" + Racine.ToString();
        }

    }
}
