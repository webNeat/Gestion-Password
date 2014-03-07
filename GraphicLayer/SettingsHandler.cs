using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using FileLayer;

namespace GraphicLayer
{
    class SettingsHandler : ConfigurationSection
    {
        [ConfigurationProperty("databaseType")]
        public string DatabaseType
        {
            get { return (string)this["databaseType"]; }
            set { this["databaseType"] = value; }
        }

        [ConfigurationProperty("requirePass")]
        public string RequirePass
        {
            get { return (string)this["requirePass"]; }
            set { this["requirePass"] = value; }
        }
    }
}
