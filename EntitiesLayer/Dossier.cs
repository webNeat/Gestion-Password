using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntitiesLayer
{
    class Dossier : Entity
    {
        public string Title
        {
            get;
            set;
        }
        public string ICone
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public List<Dossier> Dosssiers
        {
            get;
            set;
        }
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
