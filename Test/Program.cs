using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer;
using BusinessLayer;
using FileLayer;
namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            
            UserTree tree = new UserTree("Internet", 5, 5);
            tree.Racine.AddItemToDossier(new Item("Facebook", "nabilzaini", tree.generatePassword(), "http//facebook.com", "mon compte face" ));         
            tree.Racine.AddSousDosToDossier(new Dossier("Sport", "ico1", "dossier du sport"));
            tree.Racine.GetLastDossier().AddItemToDossier(new Item("koora", "aminbenhamou", tree.generatePassword(), "http//koora.com", "mon copte koora"));         
            
            Console.WriteLine(tree.ToString());

            Manager manager =  Manager.GetInstance(new BinaryFile());
            manager.save(tree);

        }
    }
}
