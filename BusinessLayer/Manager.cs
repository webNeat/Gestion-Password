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
        private IDatabase database;
        private UserTree userData;
        private List<Dossier> path;

        public event ManagerEventHandler Changed;
        private void OnChanged()
        {
            if (Changed != null)
                Changed();
        }
        public event ManagerEventHandler Saved;
        private void OnSaved()
        {
            if (Saved != null)
                Saved();
        }

        public static Manager GetInstance()
        {
            if (manager == null)
                return manager = new Manager();
            else
                return manager;
        }
        private Manager(){
            path = null;
            database = DatabaseFactory.Create(DatabaseType.EncryptedBinary);
        }

        public void Save()
        {
            database.save(userData);
            OnSaved();
        }

        public void Load(string userName)
        {
            userData = database.load(userName);
            path = new List<Dossier>();
            path.Add(userData.Racine);
        }

        public bool Exists(string userName)
        {
            return database.hasFile(userName);
        }

        public void CreateUser(string userName, string pass, int numChars, int numSpecChars)
        {
            UserTree ut = new UserTree(userName, pass, numChars, numSpecChars);
            Dossier root = ut.Racine;
            root.Dossiers.Add(new Dossier("Applications","","Mots de passes des applications"));
            root.Dossiers.Add(new Dossier("Internet", "", "Mots de passes des sites internet"));
            root.Dossiers.Add(new Dossier("Machines", "", "Mots de passes des différentes machines"));
            userData = ut;
            Save();
        }

        public Dossier GetFolderByTitle(string name)
        {
            if (path != null)
            {
                foreach (Dossier d in path.Last().Dossiers)
                {
                    if (d.Title == name)
                    {
                        return d;
                    }
                }
            }
            return null;
        }

        public Item GetItemByTitle(string name)
        {
            if (path != null)
            {
                foreach (Item i in path.Last().Items)
                {
                    if (i.Title == name)
                    {
                        return i;
                    }
                }
            }
            return null;
        }


        public Dossier GetCurrentFolder()
        {
            if (path == null)
                return null;
            return path.Last();
        }

        public Dossier MoveToFolder(string name)
        {
            Dossier target = GetFolderByTitle(name);
            if (target != null)
                path.Add(target);
            return target;
        }

        public Dossier GetParentFolder()
        {
            if (path == null)
                return null;
            if (path.Count > 1)
                path.Remove(path.Last());
            return path.Last();
        }

        public Dossier AddFolder(string title, string icon, string descr)
        {
            Dossier nouveau = new Dossier(title, icon, descr);
            GetCurrentFolder().Dossiers.Add(nouveau);
            OnChanged();
            return nouveau;
        }

        public bool EditFolder(string name, string title, string icon, string descr)
        {
            Dossier d = GetFolderByTitle(name);
            if (d == null)
                return false;
            d.Title = title;
            d.Icone = icon;
            d.Description = descr;
            OnChanged();
            return true;
        }

        public bool DeleteFolder(string name)
        {
            Dossier d = GetFolderByTitle(name);
            if (d == null)
                return false;
            GetCurrentFolder().Dossiers.Remove(d);
            OnChanged();
            return true;
        }

        public Item AddItem(string title, string login, string pass, string url, string descr)
        {
            Item nouveau = new Item(title, login, pass, url, descr);
            GetCurrentFolder().Items.Add(nouveau);
            OnChanged();
            return nouveau;
        }

        public bool EditItem(string name, string title, string login, string pass, string url, string descr)
        {
            Item i = GetItemByTitle(name);
            if (i == null)
                return false;
            i.Title = title;
            i.Login = login;
            i.Password = pass;
            i.Url = url;
            i.Description = descr;
            OnChanged();
            return true;
        }

        public bool DeleteItem(string name)
        {
            Item i = GetItemByTitle(name);
            if (i == null)
                return false;
            GetCurrentFolder().Items.Remove(i);
            OnChanged();
            return true;
        }

        public UserSettings GetUserSettings()
        {
            return userData.Settings;
        }

        public void EditUserSettings(string pass, int numChars, int numSpecChars)
        {
            userData.Settings.Password = pass;
            userData.Settings.NumberOFChars = numChars;
            userData.Settings.NumerOfSpeciaux = numSpecChars;
            OnChanged();
        }
    }
}