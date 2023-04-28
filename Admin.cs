using MySql.Data.MySqlClient;
using System.Data;

namespace TESTCONSOLE
{
    public class Admin
    {
        public int idAdmin;
        public string pseudo;
        public string mdp;

        string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleur;UID=root;PASSWORD=root";
        public bool exists = false;

        
        #region add admin
        public Admin()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("---- Inscription Administrateur ----");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Vous allez ajouter un nouvel administrateur.\n");
            Console.WriteLine("\nChoisissez un pseudo\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            string pseudoInput = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            string mdpInput = Program.DefinitionMDP();
            Console.WriteLine("\nMot de passe défini : " + mdp);
            this.pseudo = pseudoInput;
            this.mdp = mdpInput;
            NouvelAdmin();
        }
        #endregion


        #region Connect admin
        public Admin(string idATester, string mdpAtester)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            try
            {
                cmd.CommandText = "SELECT * FROM administrateurs where IdAdmin='" + idATester + "' and mot_de_passe='" + mdpAtester + "';";
                cmd.ExecuteNonQuery();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader.GetValue(0).ToString() != null && reader.GetValue(0).ToString() != "")
                    {
                        this.exists = true;
                        this.idAdmin = Convert.ToInt32(reader.GetValue(0));
                        this.pseudo = reader.GetValue(1).ToString();
                        this.mdp = reader.GetValue(2).ToString();
                    }
                    else
                    {
                        System.Console.WriteLine("Informations erronées, veuillez vérifier vos informations.");
                    }
                }


                connection.Close();
            }
            catch (Exception e)
            {
            }

        }

        #endregion

        /// <summary>
        /// Permet la création d'un nouvel admin
        /// </summary>
        public void NouvelAdmin()
        {
            // Récup automatique de l'IdAdmin

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            try
            {
                cmd.CommandText = "SELECT Count(IdAdmin) FROM administrateurs;";
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader.GetValue(0).ToString() != null && reader.GetValue(0).ToString() != "")
                    {
                        this.idAdmin = Convert.ToInt32(reader.GetValue(0));
                        
                    }
                    else
                    {
                        System.Console.WriteLine("Bug obtention nombre d'admins");
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }
            connection.Close();




            // Création de l'admin

            string command = "INSERT INTO `fleur`.`administrateurs`(`IdAdmin`,`pseudo`,`mot_de_passe`)VALUES("+ this.idAdmin + ",'" + this.pseudo+ "','" + this.mdp+ "');";


            connection.Open();
            MySqlCommand cmd1 = connection.CreateCommand();
            cmd1.CommandText = command;
            cmd1.ExecuteNonQuery();
            connection.Close();
            System.Console.ForegroundColor = ConsoleColor.White;

        }

        /// <summary>
        /// Permet le changement de l'état d'une commande
        /// </summary>
        /// <param name="courriel"> le courriel de la commande</param>
        /// <param name="etat">le novel état</param>
        /// <param name="dl">la date e livraison de la commande</param>
        public void ChangementEtatCommande(string courriel, string etat, DateTime dl){

            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = connection.CreateCommand();
            connection.Open();
            cmd.CommandText="UPDATE boncommande SET EtatCommande = '"+etat+"' where CodeC = '"+courriel+"' and dateLivraison ='"+dl.ToString("yyyy'-'MM'-'dd")+"';"; 
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        /// <summary>
        /// Permet d'afficher toutes les commandes d'un utilisateur
        /// </summary>
        /// <param name="courriel">le courriel de l'utilisateur à afficher</param>
        public void AffCommande(string courriel){
             MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = connection.CreateCommand();
            connection.Open();
            cmd.CommandText="SELECT * from boncommande where codeC = '"+courriel+"';";
            cmd.ExecuteNonQuery();
            MySqlDataReader reader = cmd.ExecuteReader();
            string[] valueString = new string[reader.FieldCount];

            while(reader.Read()){
                    for(int i=0;i<reader.FieldCount;i++){                                   
                        System.Console.Write(reader.GetValue(i).ToString());
                        System.Console.Write(" | ");
                    }
                    System.Console.WriteLine();
            }
            connection.Close();
        }
        /// <summary>
        /// Permet de regarder si les stockes sont basses sur un magasin
        /// </summary>
        /// <param name="stockInfo"></param>
        /// <param name="magasin"></param>
        public void stock(int[] stockInfo, string magasin){

            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = connection.CreateCommand();
            connection.Open();
            cmd.CommandText="SELECT * FROM stock where IdMagasin = '"+magasin+"';";
            cmd.ExecuteNonQuery();
            MySqlDataReader reader = cmd.ExecuteReader();
            string[] valueString = new string[reader.FieldCount];
            bool deficit = false;
            while(reader.Read()){
                    for(int i = 1;i<reader.FieldCount;i++){
                        
                        if(stockInfo[i-1]>=Convert.ToInt32(reader.GetValue(i))){
                            switch(i-1){
                                case(0):
                                deficit =true;
                                    System.Console.WriteLine("Vos stocks sont bas sur les Gerbera, elles sont à "+Convert.ToInt32(reader.GetValue(i)));
                                    break;
                                case(1):
                                deficit =true;
                                    System.Console.WriteLine("Vos stocks sont bas sur les Ginger, elles sont à "+Convert.ToInt32(reader.GetValue(i)));
                                    break;
                                case(2):
                                deficit =true;
                                    System.Console.WriteLine("Vos stocks sont bas sur les Glaieul, elles sont à "+Convert.ToInt32(reader.GetValue(i)));
                                    break;
                                case(3):
                                deficit =true;
                                    System.Console.WriteLine("Vos stocks sont bas sur les Marguerite, elles sont à "+Convert.ToInt32(reader.GetValue(i)));
                                    break;
                                case(4):
                                deficit =true;
                                    System.Console.WriteLine("Vos stocks sont bas sur les rose rouges, elles sont à "+Convert.ToInt32(reader.GetValue(i)));
                                    break;
                                default:
                                System.Console.WriteLine("Vos stocks sont bas");
                                break;
                            }
                        }
                    }
            }
            if(!deficit)System.Console.WriteLine("Vos stocks ne sont pas en dessous de l'alerte");
            connection.Close();
        }
        /// <summary>
        /// Permet d'afficher toutes les stats intéressantes que l'admin pourrait vouloir
        /// </summary>
        public void Stats(){

            MySqlConnection connection = new MySqlConnection(connectionString);         // Définition connexion
            MySqlCommand cmd = connection.CreateCommand();                              // Définition de la commande
            connection.Open();                                                          // Ouverture connexion
            Console.WriteLine("Nombre de ventes : \n");                                 // Interface, pour faire joli
            cmd.CommandText = "SELECT COUNT(*) AS TotalBonCommande FROM BonCommande;";  // Set de la commande
            cmd.ExecuteNonQuery();                                                      // Execution commande
            MySqlDataReader reader = cmd.ExecuteReader();                               // Lecture résultat
            string[] valueString = new string[reader.FieldCount];                       // Mise des résultats dans un tableau d'une ligne

            while (reader.Read())                                                       // Tant qu'on lit ...
            {       
                for (int i = 0; i < reader.FieldCount; i++)                                 // Pour chaque case du tableau
                {
                    System.Console.Write(reader.GetValue(i).ToString());                    // On écrit
                    System.Console.Write(" | ");                                            // On sépare par un |
                }
                System.Console.WriteLine();                                             // Retour à la ligne
            }
            connection.Close();                                                         // Fermeture connexion


            connection = new MySqlConnection(connectionString);
            cmd = connection.CreateCommand();
            connection.Open();
            System.Console.WriteLine("----------------");
            Console.WriteLine("CA total : \n");
            cmd.CommandText = "SELECT SUM(Price) AS TotalCA FROM (SELECT BonCommande.Prix AS Price FROM BonCommande JOIN commande_standard ON BonCommande.NomStandard = commande_standard.nom WHERE CommandeStandard = TRUE UNION ALL SELECT BonCommande.prix AS Price FROM BonCommande WHERE CommandeStandard = FALSE) AS AllBouquets;"; //CA TOTAL
            cmd.ExecuteNonQuery();
            reader = cmd.ExecuteReader();
            valueString = new string[reader.FieldCount];

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    System.Console.Write(reader.GetValue(i).ToString());
                    System.Console.Write(" | ");
                }
                System.Console.WriteLine();
            }
            connection.Close();


            connection = new MySqlConnection(connectionString);
            cmd = connection.CreateCommand();
            connection.Open();
            System.Console.WriteLine("----------------");
            Console.WriteLine("Informations du meilleur client : \n");
            cmd.CommandText="SELECT * FROM clients where nbBouquetMois=(select max(nbBouquetMois) from clients) ;"; //Meilleur client
            cmd.ExecuteNonQuery();
            reader = cmd.ExecuteReader();
            valueString = new string[reader.FieldCount];

            while(reader.Read()){
                    for(int i=0;i<reader.FieldCount;i++){                                   
                        System.Console.Write(reader.GetValue(i).ToString());
                        System.Console.Write(" | ");
                    }
                    System.Console.WriteLine();
            }
            connection.Close();



            connection.Open();
            cmd.CommandText="SELECT NomStandard, COUNT(NomStandard) AS TotalBought FROM BonCommande WHERE CommandeStandard = TRUE GROUP BY NomStandard ORDER BY TotalBought DESC LIMIT 1;";
            cmd.ExecuteNonQuery();
            reader = cmd.ExecuteReader();
            valueString = new string[reader.FieldCount];
           
           System.Console.WriteLine("----------------");
            Console.WriteLine("Bouquet standard avec le plus de succès :\n");
            while (reader.Read()){
                    for(int i=0;i<reader.FieldCount;i++){                                   
                        System.Console.Write(reader.GetValue(i).ToString());  //Quel est le bouquet standard qui a eu le plus de succès ? 
                    System.Console.Write(" | ");
                    }
                    System.Console.WriteLine();
            }
            connection.Close();
        


         connection.Open();
            cmd.CommandText="SELECT AVG(boncommande.prix) AS AveragePrice FROM BonCommande JOIN commande_standard ON BonCommande.NomStandard = commande_standard.nom WHERE CommandeStandard = TRUE;";
            cmd.ExecuteNonQuery();
            reader = cmd.ExecuteReader();
            valueString = new string[reader.FieldCount];
           
           System.Console.WriteLine("----------------");
            System.Console.WriteLine("Prix moyen bouquet standard :\n");
            while(reader.Read()){
                    for(int i=0;i<reader.FieldCount;i++){                                   
                        System.Console.Write(reader.GetValue(i).ToString());  //Prix moyen bouquet standard 
                        System.Console.Write(" | ");
                    }
                    System.Console.WriteLine();
            }
            connection.Close();


            connection.Open();
            cmd.CommandText="SELECT * FROM magasin where CA=(select max(CA) from magasin) ;"; //Meilleur magasin
            cmd.ExecuteNonQuery();
            reader = cmd.ExecuteReader();
            valueString = new string[reader.FieldCount];

            System.Console.WriteLine("----------------");
            System.Console.WriteLine("Meilleur magasin :\n");
            while(reader.Read()){
                    for(int i=0;i<reader.FieldCount;i++){                                   
                        System.Console.Write(reader.GetValue(i).ToString());
                        System.Console.Write(" | ");
                    }
                    System.Console.WriteLine();
            }
            connection.Close();


            connection.Open();
            cmd.CommandText = "SELECT SUM(Gerbera) AS TotalGerbera, SUM(Ginger) AS TotalGinger, SUM(Glaieul) AS TotalGlaieul, SUM(Marguerite) AS TotalMarguerite, SUM(Rose_rouge) AS TotalRoseRouge FROM stock;"; //Fleurs par catégorie
            cmd.ExecuteNonQuery();
            reader = cmd.ExecuteReader();
            valueString = new string[reader.FieldCount];

            System.Console.WriteLine("----------------");
            System.Console.WriteLine("Nombre fleurs par catégorie :\nGerbera\tGinger\tGlaieul\tMarg.\tRoses_rouges");
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    System.Console.Write(reader.GetValue(i).ToString());
                    System.Console.Write("\t| ");
                }
                System.Console.WriteLine();
            }
            connection.Close();
        }
        /// <summary>
        /// Affiche les commandes par magasin
        /// </summary>
        public void AffCommandeParMag()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = connection.CreateCommand();
            connection.Open();
            cmd.CommandText = "SELECT * FROM BonCommande ORDER BY magasin";
            cmd.ExecuteNonQuery();
            MySqlDataReader reader = cmd.ExecuteReader();
            string[] valueString = new string[reader.FieldCount];

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    System.Console.Write(reader.GetValue(i).ToString());
                    System.Console.Write(" | ");
                }
                System.Console.WriteLine();
            }
            connection.Close();
        }
        /// <summary>
        /// Permet d'augmenter les stocks d'un magasin
        /// </summary>
        /// <param name="magasin">les stocks à augmenter</param>
        /// <param name="Nstocks">la quantité de stocks à augemnter</param>
        public void AugmenteStocks(string magasin, int[] Nstocks){
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = connection.CreateCommand();
            connection.Open();
            cmd.CommandText = "UPDATE stock SET gerbera = gerbera +"+Nstocks[0]+", ginger = ginger+"+Nstocks[1]+", glaieul = glaieul+"+Nstocks[2]+",marguerite=marguerite+"+Nstocks[3]+",Rose_rouge=Rose_rouge+"+Nstocks[4]+" where IdMagasin ='"+magasin+"';";
            cmd.ExecuteNonQuery();
            System.Console.WriteLine("\nLes stocks ont bien été modifiés!");
        }
    }



}