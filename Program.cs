using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Runtime.Serialization;

namespace TESTCONSOLE
{
    class Program{
        static void Main(string[] args){
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleur;UID=root;PASSWORD=root";


            //Clients c = new Clients();
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
            Console.WriteLine("---- Menu Principal ----\n\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("[1] Inscription\n[2] Connexion\n[3] Connexion Admin\n\nEntrez le numéro de votre choix:\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            string choix = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            switch (choix)
            {
                case "1":
                    PageInscription();
                    break;
                case "2":
                    PageConnexion();
                    break;
                case "3":
                    PageAdmin();
                    break;
                default:
                    MainMenu();
                    break;
            }
        }

        static void PageConnexion()
        {
            Debut:
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
            Console.ForegroundColor = ConsoleColor.White;


            Clients c = new Clients(courriel, mdp);
            if (c.exists == false)
            {
                Console.WriteLine("Informations erronées :\n[1] Réessayer\n[2] Menu Principal");
                Console.ForegroundColor = ConsoleColor.Blue;
                string choix = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                switch (choix)
                {
                    case "1":
                        goto Debut;
                        break;
                    case "2":
                        MainMenu();
                        break;
                    default:
                        goto Debut;
                        break;
                }
            }
            PageClient(c);

        }


        #region Inscription

        static void PageInscription()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("---- Inscription ----");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Bienveue chez Belle Fleur, merci de nous faire confiance !\nInscrivez-vous en répondant aux questions suivantes :");
            Console.WriteLine("\nQuel est votre nom ?\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            string nom = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nQuel est votre prénom ?\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            string prenom = Console.ReadLine();
            Console.ForegroundColor= ConsoleColor.White;
            Console.WriteLine("\nQuel est votre numéro de téléphone ?\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            int tel = Convert.ToInt32(Console.ReadLine());
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nQuel est votre courriel ?\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            string courriel = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nQuelle est votre adresse ?\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            string adresse = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nQuel est votre numéro de carte de crédit ?\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            int carte_credit = Convert.ToInt32(Console.ReadLine());
            Console.ForegroundColor = ConsoleColor.White;
            string mdp = DefinitionMDP();
            Console.WriteLine("\nMot de passe défini : " + mdp);

            Clients c = new Clients(nom, prenom, tel, courriel, mdp, adresse, carte_credit);
            PageClient(c);


        }

        public static string DefinitionMDP()
        {
            Console.WriteLine("\nDéfinissez votre mot-de-passe :\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            string mdp1 = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\nVérifiez votre mot-de-passe :\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            string mdp2 = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;

            if(mdp1 == mdp2)
            {
                return mdp1;
            }
            else
            {
                Console.WriteLine("Mots de passes différents ! Veuillez ré-essayer !\n\nAppuyez sur une touche");
                Console.ReadKey();
                mdp1 = DefinitionMDP();
            }
            return mdp1;
        }

        #endregion

        private static void PageClient(Clients c)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("---- Page Client ----");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Vous êtes connecté en tant que " + c.prenom + " " + c.nom);
            Console.WriteLine("[1] Passer commande\n[2] Se déconnecter\n\nEntrez le numéro de votre choix:\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            string choix = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            switch (choix)
            {
                case "1":
                    Commande cm = new Commande(c);
                    cm.CommandeStandard();
                    break;
                case "2":
                    MainMenu();
                    break;
                default:
                    PageClient(c);
                    break;
            }
        }


        #region Administration

        private static void PageAdmin()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("---- Page Admin ----\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("[1] Connectez vous comme administrateur\n[2] Inscription comme administrateur\n\nEntrez le numéro de votre choix:\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            string choix = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            switch (choix)
            {
                case "1":
                    ConnexionAdmin();
                    break;
                case "2":
                    InscriptionAdmin();
                    break;
                default:
                    PageAdmin();
                    break;
            }
        }

        private static void InscriptionAdmin()
        {
            

            Admin A = new Admin();
            Console.WriteLine("Votre ID Administrateur est : " + A.idAdmin);
            Console.WriteLine("Retenez votre ID, il vous sera util pour vous connecter !");
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
            InterfaceAdmin(A);

        }

        static void ConnexionAdmin()
        {
            Debut:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("---- Connexion Admin ----\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\nEntrez votre ID Administrateur :\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            string id = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\nEntrez votre mot-de-passe :\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            string mdp = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;


            Admin A = new Admin(id, mdp);
            if (A.exists == false)
            {
                Console.WriteLine("Informations erronées :\n[1] Réessayer\n[2] Menu Principal");
                Console.ForegroundColor = ConsoleColor.Blue;
                string choix = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                switch (choix)
                {
                    case "1":
                        goto Debut;
                        break;
                    case "2":
                        MainMenu();
                        break;
                    default:
                        goto Debut;
                        break;
                }
            }
            InterfaceAdmin(A);

        }

        private static void InterfaceAdmin(Admin A)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("---- Interface Admin ----\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Vous êtes connecté en tant que " + A.pseudo + " | ID = " + A.idAdmin);
            Console.WriteLine("[1] Accéder aux statistiques\n[2] Changer l'état d'une commande\n[3] Se déconnecter\n\nEntrez le numéro de votre choix:\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            string choix = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            switch (choix)
            {
                case "1":
                    InterfaceAdmin(A);
                    break;
                case "2":
                    InterfaceAdmin(A);
                    break;
                case "3":
                    MainMenu();
                    break;
                default:
                    InterfaceAdmin(A);
                    break;
            }
        }

        #endregion
    }
}
