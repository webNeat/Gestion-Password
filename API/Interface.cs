using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicLayer
{
    public delegate void InterfaceEventHandler();
    /** API Interface utilisant la console.
     * Les commandes possible sont:
     *  > Afficher
     *      afficher les dossiers/clés du dossier courant
     *  > Ouvrir nom_dossier
     *      se déplacer vers un sous dossier
     *  > Retour
     *      se déplacer vers le dossier parent


     * > Choisir titreClé
     *      séléctionner une clé pour pouvoir: 
     *          Ctrl+W : copier l'identifiant
     *          Ctrl+X : copier le mot de passe 
     *          Ctrl+O : ouvrir l'url dans le navigateur
     *          ESC : quiter se mode
     * > Aide
     *      affiche la liste des commandes
     * > Quiter
     *      quiter l'application
     * */
    class Interface
    {
        // Les événements
        public static event InterfaceEventHandler Launched;
        private static void OnLaunched()
        {
            if (Launched != null)
                Launched();
        }
        public static event InterfaceEventHandler Closed;
        private static void OnClosed()
        {
            if (Closed != null)
                Closed();
        }

        public static Dictionary<string, string> MakeMap(IList<string> list)
        {
            Dictionary<string, string> map = new Dictionary<string, string>();
            List<string> item;
            foreach (string element in list)
            {
                item = new List<string>(element.Split(':'));
                if (item.Count < 2)
                    item.Add("");
                map[item[0]] = item[1];
            }
            return map;
        }

        [STAThreadAttribute]
        public static void Main(string[] args)
        {
            Controller.Start();
            OnLaunched();
            if (args.Length > 1)
            {
                string title = args[1];
                Controller.FindItem(title);

            }
            else
            {
                bool exit = false;
                string cmd;
                List<string> arguments;

                while (!exit)
                {
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("{0} > ", Controller.FolderName());
                        cmd = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.White;
                        arguments = new List<string>(cmd.Trim().Split(' '));
                        //Dictionary<string, string> values = null;
                        switch (arguments[0].ToLower())
                        {
                            case "afficher":
                                Controller.Afficher();
                                break;
                            case "ouvrir":
                                if (arguments.Count < 2)
                                    throw new Exception("Syntax: > Ouvrir nom_dossier");
                                Controller.Ouvrir(arguments[1]);
                                break;
                            case "retour":
                                Controller.Retour();
                                break;
                            case "choisir":
                                if (arguments.Count < 2)
                                    throw new Exception("Syntax: > Choisir titre");
                                Controller.Choisir(arguments[1]);
                                break;
                            case "trouver":
                                if (arguments.Count < 2)
                                    throw new Exception("Syntax: > Choisir titre");
                                Controller.FindItem(arguments[1]);
                                break;

                            case "aide":
                                Console.WriteLine("Les commandes possible sont:\n> Afficher\n     afficher les dossiers/clés du dossier courant\n> Ouvrir nom_dossier\n     se déplacer vers un sous dossier\n> Retour\n     se déplacer vers le dossier parent\n> Aide\n     affiche la liste des commandes\n> Quiter\n     quiter l'application\n");
                                break;
                            case "quitter":
                                exit = true;
                                break;
                            default:
                                throw new Exception("Commande Inconnu ! Tapez 'aide' pour voir les commandes possibles ");
                        }

                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(e.Message);
                    }

                }
                OnClosed();
            }
        }
    }
}
