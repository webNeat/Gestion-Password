using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntitiesLayer
{
    class Item : Entity
    {
        public string Title
        {
            get;
            set;
        }
        public string Login
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
        public string Url
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }

        public Item(string title, string login, string password, string url, string description)
        {
            Title = title;
            Login = login;
            Password = password;
            Url = url;
            Description = description;
 
        }

    }
}
