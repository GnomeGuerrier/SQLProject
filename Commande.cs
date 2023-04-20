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
        public bool standard;
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
        public void CommandeStandard(){
            MySqlConnection connection = new MySqlConnection(connectionString);
            System.Console.WriteLine("Voulez vous un bouquet standart ou personalisé [S/P]");
            if(Console.ReadLine()=="S"){
                standard = false;
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();


                cmd.CommandText="SELECT * FROM commande_standard";

                MySqlDataReader reader = cmd.ExecuteReader();
                string[] valueString = new string[reader.FieldCount];
                System.Console.WriteLine("Voici les commandes disponibles");
                while(reader.Read()){
                    for(int i=0;i<reader.FieldCount;i++){
                        System.Console.Write(reader.GetValue(i).ToString()+" || ");
                        System.Console.Write(reader.GetValue(i+1).ToString()+" || ");
                        System.Console.Write(reader.GetValue(i+2).ToString()+" || ");
                        System.Console.Write(reader.GetValue(i+3).ToString());
                        System.Console.WriteLine();
                    }
                }
                connection.Close();
                System.Console.WriteLine("Quelle commande voulez vous? Veuiller écrire le nom exacte");
                string choix ="";
                Debut:
                    choix = Console.ReadLine();
                    
                    switch(choix){
                        case("Gros Merci"):
                        string command = "INSERT INTO `fleur`.`boncommande`(`adresseLivraison`,`messageAcc`,`dateLivraison`,`CodeC`,`EtatCommande`,`CommandeStandard`,`NomStandard`)VALUES('"+this.addresse_livraison+"','"+this.message_accompagnant+"',"+this.dateLivraison+",'"+this.CodeC+"','"+this.EtatCommande+"',"+this.standard+",'"+choix+");";
                        cmd.CommandText = command;
                        connection.Close();
                        
                        
                        break;

                        default:
                        System.Console.WriteLine("Vous n'avez pas choisi de bouquet valide");
                        goto Debut;
                        
                    }
                        

                
                

            }
        }
    }
}