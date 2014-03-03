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
        public List<Dossier> Dossiers
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

        public Dossier() { }
        public Dossier(string title, string icon, string description)
        {
            Title = title;
            ICone = icon;
            Description = description;
            Dossiers = new List<Dossier>();
            Items = new List<Item>();
        }
        public Dossier GetLastDossier() 
        {
            return Dossiers[Dossiers.Count - 1];
        }
        public void AddItemToDossier(Item item)
        {
            Items.Add(item);
        }
        public void AddSousDosToDossier(Dossier sousdossier)
        {
            Dossiers.Add(sousdossier);
        }

        public override string ToString()
        {
            string chaine = "Titre Dossier <" + Title + "> DateC <"+ Created + "> DateM <"+ Modified + "> ICON <" + ICone + ">  Description <" + Description + ">\n\t";
            foreach(Item item in Items )
            {
                chaine += item.ToString();
            }
            chaine += "\t";
            foreach (Dossier dossier in Dossiers)
            {
                
                chaine += dossier.ToString();
            }
            return chaine;
        }

    }
}
