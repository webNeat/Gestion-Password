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
            private set;
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
    }
}
