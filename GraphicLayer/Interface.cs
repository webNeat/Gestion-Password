using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicLayer
{
    public delegate void InterfaceEventHandler();
    /** User Interface utilisant la console.
     * Les commandes possible sont:
     *  > Afficher
     *      afficher les dossiers/clés du dossier courant
     *  > Ouvrir nom_dossier
     *      se déplacer vers un sous dossier
     *  > Retour
     *      se déplacer vers le dossier parent
<<<<<<< HEAD
     *  > AjouterDossier titre [icone:icone_url descr:description]
     *      ajouter un dossier dans le dossier courant et le contenu entre crochets est optionel
     *  > ModifierDossier titre_du_dossier [titre:nouveau_titre icone:nouvelle_icone descr:nouvelle_descr] 
=======
     *  > AjouterDossier titre [icone=icone_url descr=description]
     *      ajouter un dossier dans le dossier courant
     *  > ModifierDossier titre_du_dossier [titre=nouveau_titre icone=nouvelle_icone descr=nouvelle_descr] 
>>>>>>> Last
     *      modifier un dossier dans le dossier courant
     *  > SupprimerDossier titre
     *      supprimer un dossier dans le dossier courant
     *  
     * > AjouterCle titre login=identifiant [pass=mot_de_passe url=site_url descr=description]
     *      ajouter une clé dans le dossier courant
     * > ModifierCle titre_de_clé [login=nouveau_identifiant pass=nouveau_mot_de_passe titre=nouveau_titre url=nouvelle_url descr=nouvelle_descr] 
     *      modifier une clé dans le dossier courant
     * > SupprimerCle titre
     *      supprimer une clé dans le dossier courant
<<<<<<< HEAD
     * > Choisir titreClé
     *      séléctionner une clé pour pouvoir: 
     *          Ctrl+W : copier l'identifiant
     *          Ctrl+X : copier le mot de passe 
     *          Ctrl+O : ouvrir l'url dans le navigateur
     *          ESC : quiter se mode
=======
     * > Choisir titre
     *      séléctionner une clé pour pouvoir 
     *          Ctrl+W = copier l'identifiant
     *          Ctrl+X = copier le mot de passe 
     *          Ctrl+O = ouvrir l'url dans le navigateur
     *          ESC = quiter se mode
>>>>>>> Last
     *          
     * > Config
     *      afficher les preferences de l'utilisateur
     * > ModifierConfig pass-taille=nouvelle_taille spec-carac=nombre_des_caractères_spéciaux ...
     *      modifier les preferences de l'utilisateur
     *      
     * > Sauvegarder 
     *      sauvegarder les modifications
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
                item = new List<string>(element.Split('='));
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
            bool exit = false;
            string cmd;
            List<string> arguments;
            while (!exit)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("{0} > ",Controller.FolderName());
                    cmd = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.White; 
                    arguments = new List<string>(cmd.Trim().Split(' '));
                    Dictionary<string, string> values;
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
                        case "ajouterdossier":
                            if (arguments.Count < 2)
                                throw new Exception("Syntax: > AjouterDossier titre [icone:icone_url descr:description]");
                            values = MakeMap(arguments.GetRange(2, arguments.Count - 2));
                            values["titre"] = arguments[1];
                            Controller.AjouterDossier(values);
                            break;
                        case "modifierdossier":
                            if (arguments.Count < 2)
                                throw new Exception("Syntax: > ModifierDossier titre_du_dossier [titre:nouveau_titre icone:nouvelle_icone descr:nouvelle_descr]");
                            values = MakeMap(arguments.GetRange(2, arguments.Count - 2));
                            values["name"] = arguments[1];
                            Controller.ModifierDossier(values);
                            break;
                        case "supprimerdossier":
                            if (arguments.Count < 2)
                                throw new Exception("Syntax: > SupprimerDossier titre");
                            Controller.SupprimerDossier(arguments[1]);
                            break;
                        case "ajoutercle":
                            if (arguments.Count < 3)
                                throw new Exception("Syntax: > AjouterCle titre login:identifiant [pass:mot_de_passe url:site_url descr:description]");
                            values = MakeMap(arguments.GetRange(2, arguments.Count - 2));
                            values["titre"] = arguments[1];
                            Controller.AjouterCle(values);
                            break;
                        case "modifiercle":
                            if (arguments.Count < 2)
                                throw new Exception("Syntax: > ModifierCle titre_de_clé [login:nouveau_identifiant pass:nouveau_mot_de_passe titre:nouveau_titre url:nouvelle_url descr:nouvelle_descr]");
                            values = MakeMap(arguments.GetRange(2, arguments.Count - 2));
                            values["name"] = arguments[1];
                            Controller.ModifierCle(values);
                            break;
                        case "supprimercle":
                            if (arguments.Count < 2)
                                throw new Exception("Syntax: > SupprimerCle titre");
                            Controller.SupprimerCle(arguments[1]);
                            break;
                        case "voircle":
                            if (arguments.Count < 2)
                                throw new Exception("Syntax: > VoirCle titre");
                            Controller.VoirCle(arguments[1]);
                            break;
                        case "choisir":
                            if (arguments.Count < 2)
                                throw new Exception("Syntax: > Choisir titre");
                            Controller.Choisir(arguments[1]);
                            break;
                        case "config" :
                            Controller.Config();
                            break;
                        case "modifierconfig" :
                            if (arguments.Count < 2)
                                throw new Exception("Rien à modifier !");
                            values = MakeMap(arguments.GetRange(1, arguments.Count - 1));
                            Controller.ModifierConfig(values);
                            break;
                        case "sauvegarder" :
                            Controller.Sauvegarder();
                            break;
                        case "aide":
                            Console.WriteLine("Les commandes possible sont:\n> Afficher\n     afficher les dossiers/clés du dossier courant\n> Ouvrir nom_dossier\n     se déplacer vers un sous dossier\n> Retour\n     se déplacer vers le dossier parent\n> AjouterDossier titre [icone:icone_url descr:description]\n     ajouter un dossier dans le dossier courant\n> ModifierDossier titre_du_dossier [titre:nouveau_titre icone:nouvelle_icone descr:nouvelle_descr] \n     modifier un dossier dans le dossier courant\n> SupprimerDossier titre\n     supprimer un dossier dans le dossier courant\n \n> AjouterCle titre login:identifiant [pass:mot_de_passe url:site_url descr:description]\n     ajouter une clé dans le dossier courant\n> ModifierCle titre_de_clé [login:nouveau_identifiant pass:nouveau_mot_de_passe titre:nouveau_titre url:nouvelle_url descr:nouvelle_descr] \n     modifier une clé dans le dossier courant\n> SupprimerCle titre\n     supprimer une clé dans le dossier courant\n> Choisir titre\n     séléctionner une clé pour pouvoir: \n         Ctrl+W : copier l'identifiant\n         Ctrl+X : copier le mot de passe \n         Ctrl+O : ouvrir l'url dans le navigateur\n         ESC : quiter se mode\n         \n> Config\n     afficher les preferences de l'utilisateur\n> ModifierConfig pass-taille:nouvelle_taille spec-carac:nombre_des_caractères_spéciaux ...\n     modifier les preferences de l'utilisateur\n     \n> Sauvegarder \n     sauvegarder les modifications\n> Aide\n     affiche la liste des commandes\n> Quiter\n     quiter l'application\n");
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
