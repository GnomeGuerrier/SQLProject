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
        public string courriel;
        public string mdp;
        
    string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleur;UID=root;PASSWORD=root";
        public Commande(clients c){
            this.mdp=c.mdp;
            this.courriel = c.courriel;
            System.Console.WriteLine("Veuillez fournir une addresse de facturation");
            this.CodeC = c.courriel;
            System.Console.WriteLine("Voulez vous que votre commande soit livrée à votre addresse de facturation? [Y/N]\n");
            System.Console.ForegroundColor = ConsoleColor.Blue;
            string msg = Console.ReadLine();
            System.Console.ForegroundColor = ConsoleColor.White;
            if(msg=="Y"){
                this.addresse_livraison = c.adresse_facturation;
            }
            else{
                System.Console.WriteLine("\nDonnez votre addresse de livraison :\n");
                System.Console.ForegroundColor = ConsoleColor.Blue;
                this.addresse_livraison=Console.ReadLine();
                System.Console.ForegroundColor = ConsoleColor.White;
            }
            System.Console.WriteLine("\nVeuillez donner le message accompagnant votre bouquet :\n");
            System.Console.ForegroundColor = ConsoleColor.Blue;
            this.message_accompagnant = Console.ReadLine();
            System.Console.ForegroundColor = ConsoleColor.White;

            System.Console.WriteLine("\nVeuillez donner une date de livraison au format JJ/MM/AAAA\n");
            System.Console.ForegroundColor = ConsoleColor.Blue;
            this.dateLivraison=DateTime.Parse(Console.ReadLine());
            System.Console.ForegroundColor = ConsoleColor.White;
            TimeSpan diff = dateLivraison-DateTime.Now.Date;

            if(diff.TotalDays<3){
                    this.EtatCommande = "VINV";
            }

        }
        public void CommandeStandard(){
            this.standard = true;
            MySqlConnection connection = new MySqlConnection(connectionString);


                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();


                cmd.CommandText="SELECT * FROM commande_standard";

                MySqlDataReader reader = cmd.ExecuteReader();
                string[] valueString = new string[reader.FieldCount];
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.WriteLine("Voici les commandes disponibles : ");
                while(reader.Read()){
                    
                    for(int i=0;i<reader.FieldCount;i++){                                   //Nom bouquets
                        System.Console.Write(reader.GetValue(i).ToString());
                        System.Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("\t||\t");
                        System.Console.ForegroundColor = ConsoleColor.White;
                        
                        
                    }
                    System.Console.WriteLine();
                }
                connection.Close();
                System.Console.Write("Quelle commande voulez vous ?");
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine(" \t /!\ CASE SENSITIVE")
                System.Console.ForegroundColor = ConsoleColor.White;
                string choix = "";
                string[] listNom = {"Gros Merci","L amoureux","L exotique","Maman","Vive la mariée"};
                Debut:
                choix = Console.ReadLine();
                if(!Array.Exists(listNom, x =>x==choix)){
                    System.Console.WriteLine("Vous n'avez pas choisi de bouquet valide");
                    goto Debut;
                }
                else{
                    string command = "INSERT INTO `fleur`.`boncommande`(`adresseLivraison`,`messageAcc`,`dateLivraison`,`CodeC`,`EtatCommande`,`CommandeStandard`,`NomStandard`)VALUES('"+this.addresse_livraison+"','"+this.message_accompagnant+"','"+this.dateLivraison.ToString("yyyy'-'MM'-'dd")+"','"+this.CodeC+"','"+this.EtatCommande+"',"+this.standard+",'"+choix+"');";
                        
                        connection.Open();
                        MySqlCommand cmd1 = connection.CreateCommand();
                        cmd1.CommandText=command;
                        cmd1.ExecuteNonQuery();
                        connection.Close();
                }
    
                

            
        }



        public void AjoutBouquetMois(){
            MySqlConnection connection2 = new MySqlConnection(this.connectionString);
            connection2.Open();

            string cm = "UPDATE clients SET nbBouquetMois = nbBouquetMois+1 WHERE courriel='"+this.courriel+"' and mdp ='"+this.mdp+"';";
                        MySqlCommand cmd2 = connection2.CreateCommand();
                        cmd2.CommandText=cm;
                        cmd2.ExecuteNonQuery();
                        connection2.Close();



        }
        public void CommandePersonalise(string magasin){




    
    
    }
    }
}