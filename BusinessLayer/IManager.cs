using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    interface IManager
    {
        void save(UserTree user);
        UserTree load(string userName);
     
    }
}
