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
        string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleur;UID=root;PASSWORD=root";
        bool exists=false;
        public clients(){
            
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
                   string messages=null;
            while(reader.Read()){
                 messages= (string)reader[0]; 
            }
            
            System.Console.WriteLine(messages);
            if (messages== null||messages=="")
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

    public clients(string nomDonne, string prenomDonne, int num_tel, string courrielDonne, string mdpDonne,string addresseDonne, int carteDonne){
            
             #region add client
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {


        
        connection.Open();

        using (MySqlCommand cmd = new MySqlCommand("ajout_clients", connection))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            this.nom=nomDonne;
            cmd.Parameters.AddWithValue("@nom", nomDonne);
            this.prenom = prenomDonne;
            cmd.Parameters.AddWithValue("@prenom", prenomDonne);
            this.num = num_tel;
            cmd.Parameters.AddWithValue("@num_tel", num_tel);
            this.courriel = courrielDonne;
            cmd.Parameters.AddWithValue("@courriel", courrielDonne);
            this.adresse_facturation = addresseDonne;
            this.mdp = mdpDonne;
            cmd.Parameters.AddWithValue("@mdp", mdpDonne);
            
            cmd.Parameters.AddWithValue("@adresse_facturation", addresseDonne);
            this.carte_credit=carteDonne;
            cmd.Parameters.AddWithValue("@carte_credit", carteDonne);

           MySqlParameter messageParam = new MySqlParameter("@message", MySqlDbType.VarChar, 255);
            messageParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(messageParam);
                    cmd.ExecuteNonQuery();
                    MySqlDataReader reader = cmd.ExecuteReader();
                   string messages=null;
            while(reader.Read()){
                 messages= (string)reader[0]; 
            }
            
            System.Console.WriteLine(messages);
            if (messages== null||messages=="")
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

        public clients(string courrielAtester, string mdpAtester){
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                try{
                    cmd.CommandText = "SELECT * FROM clients where courriel='"+courrielAtester+"' and mdp='"+mdpAtester+"';";
                    MySqlDataReader reader = cmd.ExecuteReader();
                    
                    while(reader.Read()){
                        if(reader.GetValue(0).ToString()!=null &&reader.GetValue(0).ToString()!=""){
                            this.exists = true;
                            
                            this.nom=reader.GetValue(0).ToString();
                            this.prenom=reader.GetValue(1).ToString();
                            this.num=Convert.ToInt32(reader.GetValue(2));
                            this.courriel = reader.GetValue(3).ToString();
                            this.mdp = reader.GetValue(4).ToString();
                            this.adresse_facturation = reader.GetValue(5).ToString();
                            this.num=Convert.ToInt32(reader.GetValue(6));
                            System.Console.WriteLine("Connecté!");
                        }
                        else{
                            System.Console.WriteLine("Vous n'existez pas, veuillez renter un bon courriel et mdp")
                            ;
                        }
                    }
                    

                connection.Close();
                }catch(Exception e){
                    System.Console.WriteLine(e);
                }
                
        }



    }



}