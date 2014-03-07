using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public delegate void ManagerEventHandler();

    public interface IManager
    {
        event ManagerEventHandler Changed;
        event ManagerEventHandler Saved;

        void Save();
        void Load(string userName);
        bool Exists(string userName);
        void CreateUser(string userName, string pass, int numChars, int numSpecChars);
        Dossier GetFolderByTitle(string name);
        Item GetItemByTitle(string name);
        Item FindItem(string name);
        Dossier GetCurrentFolder();
        Dossier MoveToFolder(string name);
        Dossier GetParentFolder();
        Dossier AddFolder(string title, string icon, string descr);
        bool EditFolder(string name, string title, string icon, string descr);
        bool DeleteFolder(string name);
        Item AddItem(string title, string login, string pass, string url, string descr);
        bool EditItem(string name, string title, string login, string pass, string url, string descr);
        bool DeleteItem(string name);
        UserSettings GetUserSettings();
        void EditUserSettings(string pass, int numChars, int numSpecChars);
    }
}
