using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace EntitiesLayer
{
    [Serializable]
    public class Dossier : Entity
    {
        [XmlAttribute]
        public string Title
        {
            get;
            set;
        }
        [XmlAttribute]
        public string ICone
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
        [XmlAttribute]
        public List<Dossier> Dosssiers
        {
            get;
            set;
        }
        [XmlAttribute]
        public List<Item> Items
        {
            get;
            set;
        }

        public Dossier(string title, string icon, string description)
        {
            Title = title;
            ICone = icon;
            Description = description;
            Dosssiers = new List<Dossier>();
            Items = new List<Item>();
        }

    }
}
