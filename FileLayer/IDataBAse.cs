using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer;
namespace FileLayer
{
    public interface IDataBAse
    {
        void save(UserTree user);
        UserTree load(string userName);
        bool hasFile(string userName);
    }
}
