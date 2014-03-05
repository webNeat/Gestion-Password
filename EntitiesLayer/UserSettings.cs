using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EntitiesLayer
{
    [Serializable]
    public class UserSettings
    {
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

        public UserSettings() { }
    }
}
