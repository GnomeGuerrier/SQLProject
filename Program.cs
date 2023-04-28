using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Runtime.Serialization;

namespace TESTCONSOLE
{
    class Program{
        static void Main(string[] args){
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleur;UID=root;PASSWORD=root";

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
            Console.WriteLine("[1] Passer commande\n[2] Passer une commande personnalisée\n[3] Se déconnecter\n\nEntrez le numéro de votre choix:\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            string choix = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
            switch (choix)
            {
                case "1":
                    Commande cm = new Commande(c);
                    DebutCmSt:
                    Console.WriteLine("Depuis quel magasin voulez-vous commander ?\nLes magasins disponibles sont :\nParis\nMarseille\nLyon\nLille\nAnnecy\n");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string magasin = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("");
                    if (magasin != "Paris" && magasin != "Lyon" && magasin != "Lille" && magasin != "Marseille" && magasin != "Annecy")
                    {
                        goto DebutCmSt;
                    }
                    cm.CommandeStandard(magasin);
                    cm.AjoutBouquetMois();
                    Console.ReadKey();
                    PageClient(c);
                    break;
                case "2":

                DebutCmPerso1:
                    Console.WriteLine("Depuis quel magasin voulez-vous commander ?\nLes magasins disponibles sont :\nParis\nMarseille\nLyon\nLille\nAnnecy\n");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string magasinperso = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("");
                    if (magasinperso != "Paris" && magasinperso != "Lyon" && magasinperso != "Lille" && magasinperso != "Marseille" && magasinperso != "Annecy")
                    {
                        goto DebutCmPerso1;
                    }
                   
                  
                        Commande cmd = new Commande(c);
                    cmd.CommandePersonalisee(magasinperso);
                    cmd.AjoutBouquetMois();
                    Console.WriteLine("\nAppuyez sur une touche pour continuer ...");
                    Console.ReadKey();
                    PageClient(c);
                   
                    
                    break;
                case "3":
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
            string courriel = "";

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("---- Interface Admin ----\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Vous êtes connecté en tant que " + A.pseudo + " | ID = " + A.idAdmin);
            Console.WriteLine("[1] Accéder aux statistiques\n[2] Liste commandes\n[3] Modifier état commande\n[4] Vérifier l'état de vos stocks\n[5] Liste commandes par magasin\n[6] Remplir vos stocks\n[7] Se déconnecter\n\nEntrez le numéro de votre choix:\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            string choix = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            switch (choix)
            {
                case "1":
                    A.Stats();
                    Console.WriteLine("\nAppuyez sur une touche pour continuer ...");
                    Console.ReadKey();
                    InterfaceAdmin(A);
                    break;
                case "2":
                    Console.WriteLine("\nDe quel courriel voulez vous inspecter les commandes ?\n");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    courriel = Console.ReadLine();
                    Console.ForegroundColor= ConsoleColor.White;
                    Console.WriteLine("");
                    A.AffCommande(courriel);
                    Console.WriteLine("\nAppuyez sur une touche pour continuer ...");
                    Console.ReadKey();
                    InterfaceAdmin(A);
                    break;
                case "4":
                int[] stock = {20,20,20,20,20};
                DebutCmPerso2:
                Console.WriteLine("\nDe quel magasin voulez-vous inspecter les stocks ?\nLes magasins disponibles sont :\nParis\nMarseille\nLyon\nLille\nAnnecy\n");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string magasinperso = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("");
                    if (magasinperso != "Paris" && magasinperso != "Lyon" && magasinperso != "Lille" && magasinperso != "Marseille" && magasinperso != "Annecy")
                    {
                        goto DebutCmPerso2;
                    }
                    A.stock(stock,magasinperso);
                    Console.WriteLine("\nAppuyez sur une touche pour continuer ...");
                    Console.ReadKey();
                    InterfaceAdmin(A);

                    break;
                case "3":
                    Console.WriteLine("\nDe quel courriel voulez vous changer la commande ?\n");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    courriel = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.WriteLine("\nQuel état souhaitez vous appliquer ?\n");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string etat = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.WriteLine("\nQuelle commande voulez-vous modifier ? Entrez la date de livraison (JJ/MM/AAAA)\n");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    DateTime dl = Convert.ToDateTime(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(dl);

                    A.ChangementEtatCommande(courriel, etat, dl);
                    Console.WriteLine("\nAppuyez sur une touche pour continuer ...");
                    Console.ReadKey();
                    InterfaceAdmin(A);
                    break;
                case "5":
                    A.AffCommandeParMag();
                    Console.WriteLine("\nAppuyez sur une touche pour continuer ...");
                    Console.ReadKey();
                    InterfaceAdmin(A);
                    break;
                case "7":
                    MainMenu();
                    break;
                case "6":
                    DebutCmPerso3:
                Console.WriteLine("\nDans quel magasin voulez vous changer les stocks?");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string magasinperso1 = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("");
                    if (magasinperso1 != "Paris" && magasinperso1 != "Lyon" && magasinperso1 != "Lille" && magasinperso1 != "Marseille" && magasinperso1 != "Annecy")
                    {
                        goto DebutCmPerso3;
                    }
                    int[] nouvellesStocks = new int[5];
                    System.Console.WriteLine("Combien de nouvelles Gerbera a "+magasinperso1);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    nouvellesStocks[0] = Convert.ToInt32(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.White;
                    System.Console.WriteLine("\nCombien de nouvelles Ginger a "+magasinperso1);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    nouvellesStocks[1] = Convert.ToInt32(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.White;
                    System.Console.WriteLine("\nCombien de nouvelles Glaieul a "+magasinperso1);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    nouvellesStocks[2] = Convert.ToInt32(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.White;
                    System.Console.WriteLine("\nCombien de nouvelles Marguerite a "+magasinperso1);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    nouvellesStocks[3] = Convert.ToInt32(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.White;
                    System.Console.WriteLine("\nCombien de nouvelles roses rouges a "+magasinperso1);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    nouvellesStocks[4] = Convert.ToInt32(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.White;
                    A.AugmenteStocks(magasinperso1, nouvellesStocks);
                    System.Console.WriteLine("\nAppuyez sur une touche pour continuer ...");
                    Console.ReadKey();
                    InterfaceAdmin(A);
                    break;
                default:
                    InterfaceAdmin(A);
                    break;
            }
        }

        #endregion
    }
}
