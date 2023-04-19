using MySql.Data.MySqlClient;
using System.Data;

namespace TESTCONSOLE
{
    public class Commande{
        public string addresse_livraison;
        public string message_accompagnant;
        public DateTime dateLivraison;
        public DateTime dateCreation = DateTime.Now;
        public string CodeC;
        public string EtatCommande;
    string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleur;UID=root;PASSWORD=root";
        public Commande(clients c){
            System.Console.WriteLine("Veuillez fournir une addresse de facturation");
            this.CodeC = c.courriel;
            System.Console.WriteLine("Voulez vous que votre commande soit livrée à votre addresse de facturation? [Y/N]");
            
            string msg = Console.ReadLine();
            if(msg=="Y"){
                this.addresse_livraison = c.adresse_facturation;
            }
            else{
                System.Console.WriteLine("Donner votre addresse de livraison");
                this.addresse_livraison=Console.ReadLine();  
            }
            System.Console.WriteLine("Veuillez donner le message accompagnant");
            this.message_accompagnant = Console.ReadLine();
            System.Console.WriteLine("Veuillez donner une date de livraison [moismois/jourjour/annee]");
            this.dateLivraison=DateTime.Parse(Console.ReadLine());
        }
        public void CommandeStandart(){
            MySqlConnection connection = new MySqlConnection(connectionString);
            System.Console.WriteLine("VOulez vous un bouquet standart ou personalisé [S/P]");
            if(Console.ReadLine()=="S"){
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();


                cmd.CommandText="SELECT * FROM commande_standart";

                MySqlDataReader reader = cmd.ExecuteReader();
                string[] valueString = new string[reader.FieldCount];

                while(reader.Read()){
                    
                }
            }
        }
    }
}