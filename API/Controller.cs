using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EntitiesLayer;
using BusinessLayer;
using System.Diagnostics;

namespace GraphicLayer
{
    // TODO: Add Threads ... !
    // Regex Checkings !
    // App Settings
    class Controller
    {
        private static IManager manager;

        private static void Init()
        {
            string user = Environment.UserName;
            if (manager.Exists(user))
            {
                manager.Load(user);
               
            }
           
        }

        public static bool Exists()
        {
            string user = Environment.UserName;
            return manager.Exists(user);
        }
        public static void Start()
        {
            manager = Manager.GetInstance();
            Interface.Launched += Init;
        }

        public static string FolderName()
        {
            Dossier current = manager.GetCurrentFolder();
            if (current == null)
                return "";
            return current.Title;
        }

        public static void Afficher()
        {
            Dossier current = manager.GetCurrentFolder();
            if (current.Dossiers.Count < 1 && current.Items.Count < 1)
                Console.WriteLine("Ce dossier est vide");
            else
            {
                Console.WriteLine("Type  \t\t Titre \t\t Description");
                foreach (Dossier d in current.Dossiers)
                    Console.WriteLine("Dossier \t {0} \t {1}", d.Title, d.Description);
                foreach (Item i in current.Items)
                    Console.WriteLine("Cle \t\t {0} \t {1}", i.Title, i.Description);
            }
        }

        public static void Ouvrir(string folderName)
        {
            Dossier target = manager.MoveToFolder(folderName);
            if (target == null)
                throw new Exception("Pas de dossier avec le titre " + folderName);
        }

        public static void Retour()
        {
            manager.GetParentFolder();
        }

        public static void Choisir(string name){
            Item i = manager.GetItemByTitle(name);
            ConsoleKeyInfo cki;
            Console.TreatControlCAsInput = true;
            do
            {
                cki = Console.ReadKey();
                if ((cki.Modifiers & ConsoleModifiers.Control) != 0 && cki.Key.ToString() == "C")
                    Clipboard.SetText(i.Password);
                if ((cki.Modifiers & ConsoleModifiers.Control) != 0 && cki.Key.ToString() == "B")
                    Clipboard.SetText(i.Login);
                if ((cki.Modifiers & ConsoleModifiers.Control) != 0 && cki.Key.ToString() == "W")
                    Process.Start(i.Url);
            } while (cki.Key != ConsoleKey.Escape);

            Console.TreatControlCAsInput = false;
            Console.WriteLine("");
        }
        public static void FindItem(string name)
        {
            Item item = manager.FindItem(name);
            if(item != null)
                Console.WriteLine("Login : {0}\tPassword : {1}",  item.Login,  item.Password);
        }
    }
}
