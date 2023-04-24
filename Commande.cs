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
        public Commande(Clients c){
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
                        System.Console.WriteLine(" || ");
                        System.Console.ForegroundColor = ConsoleColor.White;
                        
                        
                    }
                    System.Console.WriteLine();
                }
                connection.Close();
                System.Console.Write("Quelle commande voulez vous ?");
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine(" \t /!/ CASE SENSITIVE");
                System.Console.WriteLine(" \t /!/ CASE SENSITIVE");
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
                    int prix=0;
                    if(choix=="Gros Merci")prix=45;
                    else if (choix=="L amoureux")prix=65;
                    else if (choix=="L exotique")prix=40;
                    else if (choix=="Maman")prix=80;
                    else if (choix=="Vive la mariée")prix=109;
                    else prix = -1;

                    string command = "INSERT INTO `fleur`.`boncommande`(`adresseLivraison`,`messageAcc`,`dateLivraison`,`CodeC`,`EtatCommande`,`CommandeStandard`,`NomStandard`,`prix`)VALUES('"+this.addresse_livraison+"','"+this.message_accompagnant+"','"+this.dateLivraison.ToString("yyyy'-'MM'-'dd")+"','"+this.CodeC+"','"+this.EtatCommande+"',"+this.standard+",'"+choix+"',"+prix+");";
                        
                       connection.Open();
                        MySqlCommand cmd1 = connection.CreateCommand();
                        cmd1.CommandText=command;
                        cmd1.ExecuteNonQuery();
                        connection.Close();
                        System.Console.ForegroundColor = ConsoleColor.White;
                        System.Console.WriteLine("\nLa commande a bien été passée ! Merci pour votre achat !\n");
                        System.Console.WriteLine("\nAppuyez sur une touche pour continuer...");
                }
    
                

            
        }



        public void AjoutBouquetMois(){
            MySqlConnection connection2 = new MySqlConnection(this.connectionString);
            connection2.Open();

            string cm = "UPDATE Clients SET nbBouquetMois = nbBouquetMois+1 WHERE courriel='"+this.courriel+"' and mdp ='"+this.mdp+"';";
                        MySqlCommand cmd2 = connection2.CreateCommand();
                        cmd2.CommandText=cm;
                        cmd2.ExecuteNonQuery();
                        connection2.Close();



        }
        
        
        public void CommandePersonalisee(string magasin){

            System.Console.WriteLine("Voulez-vous faire une commande personalisée par item, ou une description générale? [item/texte]\n");
            System.Console.ForegroundColor = ConsoleColor.Blue;
            if (Console.ReadLine()=="item"){
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.WriteLine($"Voici les stocks disponibles pour le magasin {magasin}");
                System.Console.WriteLine("      Gebera  Ginger Glaieul Marguerite Rose_rouge");
                MySqlConnection connection = new MySqlConnection(connectionString);


                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();

                int[] stockfleur = new int[5];
                cmd.CommandText="SELECT * FROM stock where IdMagasin= '"+magasin+"';";
                cmd.ExecuteNonQuery();
                MySqlDataReader reader = cmd.ExecuteReader();
                string[] valueString = new string[reader.FieldCount];
                while(reader.Read()){
                    
                    for(int i=0;i<reader.FieldCount;i++){                                   
                        System.Console.Write(reader.GetValue(i).ToString());
                        System.Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.Write(" || ");
                        System.Console.ForegroundColor = ConsoleColor.White;
                        if(i!=0){
                            stockfleur[i-1]=Convert.ToInt32(reader.GetValue(i));
                        }
                        
                    }
                    
                }
                connection.Close();
                int[]mois = {5,6,7,8,9,10,11};
            Commande:
                System.Console.WriteLine("\nCombien de Gerberas voulez vous ?\n");
                System.Console.ForegroundColor = ConsoleColor.Blue;
                int gerbera = Convert.ToInt32(Console.ReadLine());
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.WriteLine("\nCombien de Gingers voulez vous ?\n" );
                System.Console.ForegroundColor = ConsoleColor.Blue;
                int ginger = Convert.ToInt32(Console.ReadLine());
                System.Console.ForegroundColor = ConsoleColor.White;
                int glaieul=0;
                if(!Array.Exists(mois, x=>x==dateCreation.Month)){
                    System.Console.ForegroundColor = ConsoleColor.White;
                    System.Console.WriteLine("\nVous ne pouvez pas commander de Glaieuls, car ce n'est pas la saison\n");
                     glaieul = 0;
                }
                else{
                    System.Console.ForegroundColor = ConsoleColor.White;
                    System.Console.WriteLine("\nCombien de Glaieuls voulez vous?\n" );
                    System.Console.ForegroundColor = ConsoleColor.Blue;
                    glaieul =Convert.ToInt32(Console.ReadLine());
                    System.Console.ForegroundColor = ConsoleColor.White;
                }
                
                System.Console.WriteLine("\nCombien de Margerites voulez vous?\n" );
                System.Console.ForegroundColor = ConsoleColor.Blue;
                int margerite = Convert.ToInt32(Console.ReadLine());
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.WriteLine("\nCombien de Roses voulez vous?\n" );
                System.Console.ForegroundColor = ConsoleColor.Blue;
                int rose = Convert.ToInt32(Console.ReadLine());
                System.Console.ForegroundColor = ConsoleColor.White;
                bool stockCorrect=true;

                if(stockfleur[0]-gerbera<0||stockfleur[1]-ginger<0||stockfleur[2]-glaieul<0||stockfleur[3]-margerite<0||stockfleur[4]-rose<0){
                    System.Console.WriteLine("Nous n'avons pas les stocks, veuillez prendre moins de fleurs");
                    goto Commande;
                }
                float prix =(float)(gerbera*5+ginger*4+glaieul*1+margerite*2.25+rose*2.5);
                System.Console.WriteLine("Super! Votre total s'élève à " + prix);

                this.standard = false;

                MySqlConnection connectionperso = new MySqlConnection(connectionString);
                string personalise = "gerbera : " +gerbera+ "ginger : "+ginger+" glaieul : "+glaieul+ " margerite :"+margerite+ " Rose rouge : "+rose ;
                this.EtatCommande="CC";
                connectionperso.Open();
                MySqlCommand cmd3 = connectionperso.CreateCommand();
                cmd3.CommandText="INSERT INTO `fleur`.`boncommande`(`adresseLivraison`,`messageAcc`,`dateLivraison`,`CodeC`,`EtatCommande`,`CommandeStandard`,`Personalisé`,`Prix`)VALUES('"+this.addresse_livraison+"','"+this.message_accompagnant+"','"+this.dateLivraison.ToString("yyyy'-'MM'-'dd")+"','"+this.CodeC+"','"+this.EtatCommande+"',"+this.standard+",'"+personalise+"',"+prix+");";;
                
                
                cmd3.CommandText="UPDATE stock SET gerbera = gerbera -"+gerbera+", ginger = ginger-"+ginger+", glaieul = glaieul-"+glaieul+",marguerite=marguerite-"+margerite+",Rose_rouge=Rose_rouge-"+rose+" where IdMagasin ='"+magasin+"';";
                cmd3.ExecuteNonQuery();
                connectionperso.Close();
               
        }
        else{
                System.Console.ForegroundColor = ConsoleColor.White;
                MySqlConnection connectionperso = new MySqlConnection(connectionString);
                this.standard = false;
                System.Console.WriteLine("Décriver le bouquet que vous voulez :\n");
                System.Console.ForegroundColor = ConsoleColor.Blue;
                string personalise = Console.ReadLine();
                System.Console.ForegroundColor = ConsoleColor.White;
                
                System.Console.WriteLine("Donner le prix que vous voulez payer :\n");
                System.Console.ForegroundColor = ConsoleColor.Blue;
                float prix = float.Parse(Console.ReadLine());
                System.Console.ForegroundColor = ConsoleColor.White;

                this.EtatCommande="CPAV";
                connectionperso.Open();
                MySqlCommand cmd3 = connectionperso.CreateCommand();
                cmd3.CommandText="INSERT INTO `fleur`.`boncommande`(`adresseLivraison`,`messageAcc`,`dateLivraison`,`CodeC`,`EtatCommande`,`CommandeStandard`,`Personalisé`,`prix`)VALUES('"+this.addresse_livraison+"','"+this.message_accompagnant+"','"+this.dateLivraison.ToString("yyyy'-'MM'-'dd")+"','"+this.CodeC+"','"+this.EtatCommande+"',"+this.standard+",'"+personalise+"',"+prix+"');";
                cmd3.ExecuteNonQuery();
                connectionperso.Close();

        }


    
    
        }
    }
}