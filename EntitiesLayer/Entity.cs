using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer
{
    class Entity
    {
        public DateTime Created
        {
            get;
            private set;
        }
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
