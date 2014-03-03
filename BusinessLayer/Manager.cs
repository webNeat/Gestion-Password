using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileLayer;
using EntitiesLayer;
namespace BusinessLayer
{
    public class Manager : IManager
    {
        private static Manager manager = null;
        private IDataBAse dao;
        public static Manager GetInstance(IDataBAse dao)
        {
            if (manager == null)
                return manager = new Manager(dao);
            else
                return manager;
        }
        private Manager(IDataBAse dao) 
        {
            this.dao = dao;
        }

        public void save(UserTree user)
        {
            dao.save(user);
        }

        public UserTree load(string userName)
        {
         return dao.load(userName);
        }
    }
}
