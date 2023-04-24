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


        public void Stats(){


            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = connection.CreateCommand();
            connection.Open();
            cmd.CommandText="SELECT * FROM clients where nbBouquetMois=(select max(nbBouquetMois) from clients) ;"; //Meilleur client
            cmd.ExecuteNonQuery();
            MySqlDataReader reader = cmd.ExecuteReader();
            string[] valueString = new string[reader.FieldCount];

            while(reader.Read()){
                    for(int i=0;i<reader.FieldCount;i++){                                   
                        System.Console.Write(reader.GetValue(i).ToString());
                    }
            }
            connection.Close();



            connection.Open();
            cmd.CommandText="SELECT NomStandard, COUNT(NomStandard) AS TotalBought FROM BonCommande WHERE CommandeStandard = TRUE GROUP BY NomStandard ORDER BY TotalBought DESC LIMIT 1;";
            cmd.ExecuteNonQuery();
            reader = cmd.ExecuteReader();
            valueString = new string[reader.FieldCount];
           
           System.Console.WriteLine("----------------");  
            while(reader.Read()){
                    for(int i=0;i<reader.FieldCount;i++){                                   
                        System.Console.Write(reader.GetValue(i).ToString());  //Quel est le bouquet standard qui a eu le plus de succès ? 

                    }
            }
            connection.Close();
        
        }
    }



}