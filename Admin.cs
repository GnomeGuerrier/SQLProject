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


    }



}