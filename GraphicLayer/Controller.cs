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
        private static bool saved;

        private static void Init()
        {
            string user = Environment.UserName;
            if (manager.Exists(user))
            {
                manager.Load(user);
                Console.WriteLine("Bonjour {0} Vos données ont été chargées", user);
            }
            else
            {
                Console.WriteLine("Bienvenue dans votre application de gestion des mots de passes, veuillez insérer votre mot de passe pour céer un nouveau compte :");
                ConsoleColor temp = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Black;
                string pass = Console.ReadLine();
                Console.ForegroundColor = temp;
                Console.WriteLine("Veuillez attendre SVP");
                manager.CreateUser(user, pass, 12, 5);
                manager.Load(user);
                Console.WriteLine("Bonjour {0} Vos données ont été chargées", user);
            }
            saved = true;
        }
        private static void Quit()
        {
            if (saved == false)
            {
                Console.WriteLine("Vous n'avez pas Sauvegarder les dernières modifications ! Voullez-vous les sauvegarder avant de quiter ? (oui, non) ");
                if (Console.ReadLine().ToLower() == "oui")
                {
                    Sauvegarder();
                }
            }
        }

        public static void Start()
        {
            manager = Manager.GetInstance();
            Interface.Launched += Init;
            Interface.Closed += Quit;
            manager.Changed += () => { saved = false; };
            manager.Saved += () => { saved = true; };
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

        public static void AjouterDossier(Dictionary<string, string> values)
        {
            string icon = "";
            if (values.ContainsKey("icone"))
                icon = values["icone"];
            string descr = "";
            if (values.ContainsKey("descr"))
                descr = values["descr"];
            Dossier d = manager.AddFolder(values["titre"], icon, descr);
            Console.WriteLine("Les dossier {0} a été ajouté avec succés", d.Title);
        }
        public static void ModifierDossier(Dictionary<string, string> values)
        {
            Dossier d = manager.GetFolderByTitle(values["name"]);
            if (d == null)
                throw new Exception("Dossier introuvable");

            string icon = d.Icone;
            if (values.ContainsKey("icone"))
                icon = values["icone"];
            string descr = d.Description;
            if (values.ContainsKey("descr"))
                descr = values["descr"];
            string title = d.Title;
            if (values.ContainsKey("titre"))
                title = values["titre"];
            manager.EditFolder(values["name"], title, icon, descr);
            Console.WriteLine("Le dossier a été modifié");
        }
        public static void SupprimerDossier(string name){
            if(manager.DeleteFolder(name))
                Console.WriteLine("Le dossier a été supprimé");
            else
                throw new Exception("Dossier introuvable");
        }
        public static void AjouterCle(Dictionary<string, string> values)
        {
            string login = "";
            if (values.ContainsKey("login"))
                login = values["login"];
            string pass = "";
            if (values.ContainsKey("pass"))
                pass = values["pass"];
            string url = "";
            if (values.ContainsKey("url"))
                url = values["url"];
            string descr = "";
            if (values.ContainsKey("descr"))
                descr = values["descr"];

            // TODO: Check the url with Regex
            // TODO: generate the pass if not given 
            
            Item i = manager.AddItem(values["titre"], login, pass, url, descr);
            Console.WriteLine("La clé {0} a été ajouté avec succés", i.Title);
        }
        public static void ModifierCle(Dictionary<string, string> values)
        {
            Item i = manager.GetItemByTitle(values["name"]);
            if (i == null)
                throw new Exception("Clé introuvable");

            string title = i.Title;
            if (values.ContainsKey("titre"))
                title = values["titre"];
            string descr = i.Description;
            if (values.ContainsKey("descr"))
                descr = values["descr"];
            string login = i.Login;
            if (values.ContainsKey("login"))
                login = values["login"];
            string url = i.Url;
            if (values.ContainsKey("url"))
                url = values["url"];
            string pass = i.Password;
            if (values.ContainsKey("pass"))
                pass = values["pass"];

            // TODO: Check the url with Regex
            // TODO: generate the pass if not given 

            manager.EditItem(values["name"], title, login, pass, url, descr);
            Console.WriteLine("La clé a été modifié");
        }
        public static void SupprimerCle(string name){
            if (manager.DeleteItem(name))
                Console.WriteLine("La clé a été supprimé");
            else
                throw new Exception("Clé introuvable");
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
        public static void Config(){
            UserSettings us = manager.GetUserSettings();
            Console.WriteLine("Nombre des caractères par mot de passe : {0}", us.NumberOFChars);
            Console.WriteLine("Nombre des caractères spéciaux par mot de passe : {0}", us.NumerOfSpeciaux);
        }

        public static void ModifierConfig(Dictionary<string, string> values)
        {
            UserSettings us = manager.GetUserSettings();
            int nc = us.NumberOFChars;
            if (values.ContainsKey("pass-taille"))
                nc = Convert.ToInt32(values["pass-taille"].Trim());
            int ncs = us.NumerOfSpeciaux;
            if (values.ContainsKey("spec-carac"))
                ncs = Convert.ToInt32(values["spec-carac"].Trim());
            string pass = us.Password;
            if (values.ContainsKey("pass"))
                pass = values["pass"];

            manager.EditUserSettings(pass, nc, ncs);
            Console.WriteLine("Vos paramètres ont été modifiés avec succés");
        }

        public static void Sauvegarder(){
            int count = 3;
            do
            {
                Console.Write("Entrez votre mot de passe pour sauvgarder les changements : ");
                ConsoleColor temp = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Black;
                string pass = Console.ReadLine();
                Console.ForegroundColor = temp;
                if (pass == manager.GetUserSettings().Password)
                {
                    manager.Save();
                    Console.WriteLine("Vos données ont été sauvegardées");
                    return;
                }
                else
                {
                    count --;
                    Console.WriteLine("Mot de passe incorrect ! Vous avez droit à {0} autres tentatives !", count);
                }
            } while (count > 0);
        }
    }
}
