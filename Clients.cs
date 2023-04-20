using MySql.Data.MySqlClient;
using System.Data;

namespace TESTCONSOLE
{
    public class clients{
        public string nom;
        public string prenom;
        public int num;
        public string courriel;
        public string mdp;
        public string adresse_facturation;
        public int carte_credit;
        public clients(){
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleur;UID=root;PASSWORD=root";
             #region add client
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {


        
        connection.Open();

        using (MySqlCommand cmd = new MySqlCommand("ajout_clients", connection))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            System.Console.WriteLine("Ecris ton nom");
            nom = Console.ReadLine();
            cmd.Parameters.AddWithValue("@nom", nom);
            System.Console.WriteLine("prenom");
            prenom = Console.ReadLine();
            cmd.Parameters.AddWithValue("@prenom", prenom);
            System.Console.WriteLine("num tel");
            num = Convert.ToInt32(Console.ReadLine());
            cmd.Parameters.AddWithValue("@num_tel", num);
            System.Console.WriteLine("courriel");
            courriel =Console.ReadLine();
            cmd.Parameters.AddWithValue("@courriel", courriel);
            System.Console.WriteLine("mdp");
            mdp=Console.ReadLine();
            cmd.Parameters.AddWithValue("@mdp", mdp);
            System.Console.WriteLine("addresse facturation");
            adresse_facturation=Console.ReadLine();
            cmd.Parameters.AddWithValue("@adresse_facturation", adresse_facturation);
            System.Console.WriteLine("carte credit");
            carte_credit=Convert.ToInt32(Console.ReadLine());
            cmd.Parameters.AddWithValue("@carte_credit", carte_credit);

           MySqlParameter messageParam = new MySqlParameter("@message", MySqlDbType.VarChar, 255);
            messageParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(messageParam);
                    cmd.ExecuteNonQuery();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    string messages = "avant requete";
            while(reader.Read()){
                 messages= (string)reader[0]; 
            }
            
            System.Console.WriteLine(messages);
            if (messages== null)
            {
                Console.WriteLine("Client added successfully."+ messages);
            }
            else
            {
                System.Console.WriteLine("error");
            }
            connection.Close();
        }
    }
    #endregion
        }





    }



}