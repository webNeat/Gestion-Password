using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EntitiesLayer
{
    [Serializable]
    public class Entity
    {

        [XmlAttribute]
        public DateTime Created
        {
            get;
            set;
        }
        
        [XmlAttribute]
        public DateTime Modified
        {
            get; set;
        }

        public Entity()
        {
            Created = DateTime.UtcNow;
            Modified = DateTime.UtcNow;
        }
        public override string ToString()
        {
            return "Date Création <" + Created + "> Date Modification<" + Modified + ">";
        }
    }
}
