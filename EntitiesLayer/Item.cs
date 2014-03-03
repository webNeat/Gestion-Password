using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace EntitiesLayer
{
    [Serializable]
    public class Item : Entity
    {
        [XmlAttribute]
        public string Title
        {
            get;
            set;
        }
        
        [XmlAttribute]
        public string Login
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
        public string Url
        {
            get;
            set;
        }

        [XmlAttribute]
        public string Description
        {
            get;
            set;
        }
        public Item(){}
        public Item(string title, string login, string password, string url, string description)
        {
            Title = title;
            Login = login;
            Password = password;
            Url = url;
            Description = description;
 
        }

        public override string ToString()
        {
            return "Titre Item <" + Title + ">\t" + " Login <" + Login + ">  Paswd <" + Password + "> DateC <" + Created + "> DateM <" + Modified + "> Url <" + Url + ">  Description <" + Description + ">\n";
        }
    }
}
