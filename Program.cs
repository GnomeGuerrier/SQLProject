using MySql.Data.MySqlClient;
using System;
using System.Data;


namespace TESTCONSOLE
{
    class Program{
        static void Main(string[] args){
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleur;UID=root;PASSWORD=root";


            //clients c = new clients();
            //Commande commande = new Commande(c);
            //commande.CommandeStandard();

            WelcomePage();
            Console.ReadKey();
            MainMenu();



            Console.ReadKey();
        }

        static void WelcomePage()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("---- BELLE FLEUR ----");

            Console.ForegroundColor = ConsoleColor.White;  
            Console.WriteLine("     Projet  MDD     \n      ESILV  A2\n\n\n");
            
            Console.WriteLine("Par:\nCOUTAZ Eliott\t|\tTD A\nBONNELL Hugo\t|\tTD A\n\n\n\n\n\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Pour lancer l'application, appuyez sur une touche");
            Console.ForegroundColor = ConsoleColor.White;
        }


        static void MainMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("---- Menu Principal ----\n\n\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("[1] Inscription\n[2] Connexion\n[3] Connexion Admin\n\nEntrez le numéro de votre choix:\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            string choix = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            switch (choix)
            {
                case "1":
                    
                    break;
                case "2":
                    PageConnexion();
                    break;
                case "3":
                    break;
                default:
                    MainMenu();
                    break;
            }
        }

        static void PageConnexion()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("---- Connexion ----");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\nEntrez votre courriel :\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            string courriel = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\nEntrez votre mot-de-passe :\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            string mdp = Console.ReadLine();


            // INSERER ICI LA SUITE DE LA CONNEXION


            Console.WriteLine("\n\n\nTest : " + courriel + "\t" + mdp);
        }


        static void PageInscription()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("---- Inscription ----");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Bienveue chez Belle Fleur, merci de nous faire confiance !\nInscrivez-vous en répondant aux questions suivantes :");
            Console.WriteLine("Quel est votre nom ?\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            string nom = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Quel est votre prénom ?\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            string prenom = Console.ReadLine();
            Console.ForegroundColor= ConsoleColor.White;
            Console.WriteLine("Quel est votre numéro de téléphone ?\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            string tel = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Quel est votre courriel ?\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            string courriel = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Quelle est votre adresse ?\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            string adresse = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Quel est votre numéro de carte de crédit ?\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            string carte_credit = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Enfin, définissez votre mot de passe :\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            string courriel = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

        private string DefinitionMDP()
        {
            string mdp;


            return mdp;
        }
    }
}