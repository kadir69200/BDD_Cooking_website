using System;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlClient;


namespace PROJET_BDD_KADIR
{

    class BDD 
    {

        string bdd;
        string password;
        MySqlConnection connection;


        public BDD(string bdd,string password)
        {
            this.bdd = bdd;
            this.password = password;
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE="+bdd+";UID=root;PASSWORD="+password+";";
            MySqlConnection connection = new MySqlConnection(connectionString);
            this.connection = connection;

        }

        #region CONNEXION 

        //RENVOIE BOOL SI LE MDP CORRESPOND AU PSEUDO
        public bool Connexion(string objet,string username,string password)
        {
            this.connection.Open();
            
            bool connexVerif = false;
            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "SELECT * FROM "+objet+" WHERE username='"+username+"' and password='"+password+"';";

            MySqlDataReader reader = command.ExecuteReader();

            List<int> clients = new List<int>();

            while (reader.Read())
            {

                clients.Add(reader.GetInt32(0));

            }
            if (clients.Count() == 1)
            {
                connexVerif = true;
            }
            this.connection.Close();
            return connexVerif;
        }


        //RENVOIE BOOL SI LE PSEUDO EXISTE
        public bool PseudoExiste(string objet, string username)
        {
            this.connection.Open();

            bool pseudoExiste = false;
            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "SELECT * FROM " + objet + " WHERE username='" + username + "';";

            MySqlDataReader reader = command.ExecuteReader();

            List<int> clients = new List<int>();

            while (reader.Read())
            {

                clients.Add(reader.GetInt32(0));

            }
            if (clients.Count() == 1)
            {
                pseudoExiste = true;
            }
            this.connection.Close();
            return pseudoExiste;
        }

        //INSCIPTION 
        public Client Inscription(BDD cooking)
        {
            
            Console.WriteLine("Nom : ");
            string nom = Console.ReadLine();
            Console.WriteLine("Prenom : ");
            string prenom = Console.ReadLine();
            Console.WriteLine("Username : ");

            string username = Console.ReadLine();
            while (PseudoExiste("client", username))
            {
                Console.WriteLine("CE PSEUDO EXISTE DEJA ! VEUILLEZ EN SAISIR UN NOUVEAU");
                username = Console.ReadLine();
            }
            Console.WriteLine("Password : ");
            string password = Console.ReadLine();
            Console.WriteLine("Confirmation password : ");
            string password2 = Console.ReadLine();
            Console.WriteLine("Numéro de téléphone : ");
            string numeroTel = Console.ReadLine();
            Console.WriteLine("Code bancaire : ");
            string codeBancaire = Console.ReadLine();
            
            var rand = new Random();
            Client client = new Client(nom, prenom, username, numeroTel,password,codeBancaire,cooking);

            this.connection.Open();
            MySqlCommand command = this.connection.CreateCommand();
            
            command.CommandText = "insert into client values('"+Convert.ToString(client.IdClient)+"','"+nom+"','"+prenom+"','" + username + "','" + password + "','" + numeroTel + "',0, 'true',"+codeBancaire+");";
            command.ExecuteNonQuery();
            this.connection.Close();
            Console.WriteLine("\n VOUS AVEZ ETE INSCRIS AVEC SUCCES ! \n");
            Console.WriteLine("\n --------BIENVENUE PARMI NOUS !-----------\n");
            return client;
        }
        // RENVOIE BOOL SI ADMIN OU NON 
        public bool ConnexionAdmin(string username, string mdp)
        {
            this.connection.Open();

            bool isAdmin = false;
            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "SELECT * FROM client WHERE username='" + username + "' and password='" + mdp + "' and admin='true' ;";

            MySqlDataReader reader = command.ExecuteReader();

            List<int> clients = new List<int>();
            
            while (reader.Read())
            {

                clients.Add(reader.GetInt32(0));
                
            }
            if (clients.Count() == 1)
            {
                isAdmin = true;
            }
            this.connection.Close();
            return isAdmin;
        }

        #endregion

        #region FONCTIONS REQUETE 

        public bool IdExiste(int id, string table)
        {
            this.connection.Open();
            bool existence = false;
            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "SELECT id"+table+" FROM "+table+";";
            List<int> ID = new List<int> { };
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

                ID.Add(reader.GetInt32(0));

            }
            if (ID.Contains(id))
            {
                existence = true;
            }

            this.connection.Close();
            return existence;


        }
        public bool CodeExiste(string code, string nature )
        {
            this.connection.Open();
            bool existence = false;
            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "SELECT CodeBancaire from " + nature + ";"; ;
            List<string> ID = new List<string> { };
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

                ID.Add(reader.GetString(0));

            }
            if (ID.Contains(code))
            {
                existence = true;
            }

            this.connection.Close();
            return existence;


        }


        #endregion

        #region CLIENT

        //RECHARGEMENT DE COOKS
        public void RechargerCooks(string id,int cooks,string nature)
        {
            if (nature == "client")
            {
                this.connection.Open();
                MySqlCommand command = this.connection.CreateCommand();
                command.CommandText = "update client set Cooks=Cooks+" + cooks + " where idClient='" + id + "';";
                command.ExecuteNonQuery();
                this.connection.Close();
            }
            else
            {
                this.connection.Open();
                MySqlCommand command = this.connection.CreateCommand();
                command.CommandText = "update client set Cooks=Cooks+" + Convert.ToString(cooks) + " where idClient='" + id + "';";
                command.ExecuteNonQuery();
                this.connection.Close();

                this.connection.Open();
                MySqlCommand command1 = this.connection.CreateCommand();
                command1.CommandText = "update cdr set Cooks=Cooks+" + Convert.ToString(cooks) + " where idClient='" + id + "';";
                command1.ExecuteNonQuery();
                this.connection.Close();
            }
            
        }

        // RENVOIE LE NOMBRE DE CLIENT ENREGISTRER 

        public int NbClients()
        {
            this.connection.Open();

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "select count(*) from client;";
            int row = command.ExecuteNonQuery();
            MySqlDataReader reader = command.ExecuteReader();
            int val = 0;

            while (reader.Read())
            {
                val = reader.GetInt32(0);
            }
            this.connection.Close();
            return val;
        }

        // AJOUTER CLIENT SUR C# EN TANT QUE OBJET CLIENT
        public List<Client> UpdateClientsFromBDD(List<Client> liste) // Raffraichie la liste des clients de c# à partir de celle de BDD
        {
            this.connection.Open();
            MySqlCommand command = this.connection.CreateCommand();      
            command.CommandText = "select * from client;";
            command.ExecuteNonQuery();
            MySqlDataReader reader = command.ExecuteReader();
            bool test = false;
            while(reader.Read())
            {
                test = false;
                //Tester si l'id existe déja
                foreach(Client clt in liste)                    // SUPPRIMER LES CLIENTS QUI NE SONT PAS DANS LA BDD ????
                {
                    if(clt.IdClient == reader.GetInt32(0))
                    {
                        test = true; 
                    }
                }
                if (test == false)//si l'id du reader ne correspond a celui d'aucun client existant en c#
                {
                    liste.Add(new Client(reader));
                }

            }
            this.connection.Close();
            return liste;
        }

        // AJOUTER UN CLIENT SPECIFIQUE EN TANT QUE OBJET A PART D'UN USERNAME ET D'UN PASSWORD
        public Client GenererClient(string username,string password)
        {
            this.connection.Open();
            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "select * from client where username='"+username+"' and password='"+password+"';";
            MySqlDataReader reader = command.ExecuteReader();
            Client client = new Client();
            while (reader.Read())
            {
                client = new Client(reader);
            }
            this.connection.Close();
            return client;

        }

        // AJOUTER CLIENT A PARTIR DE C# SUR LA BASE DE DONNEE
        public void AddClientToBDD(Client client)
        {
            this.connection.Open();

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "insert into client values ('" + Convert.ToString(client.IdClient) + "'," + "'" + client.Nom + "','" + client.Prenom + "','" + client.Username + "','" + client.NumeroTel + "','" + client.Password + "','" + Convert.ToString(client.Cooks) + "','" + Convert.ToString(client.Admin) + "');"; // Vérification Id js comment ca ce génère du coup + LES VALEURS DE TYPE INT
            int row = command.ExecuteNonQuery();
            
           
            this.connection.Close();
        }

        // SUPPRIMER CLIENT A PARTIR DE C# SUR LA BASE DE DONNEE
        public void DeleteClientFromBDD(Client client)
        {
            this.connection.Open();

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "delete from client where IdClient = '" + client.IdClient + "';";
            command.ExecuteNonQuery();


            this.connection.Close();
        }
        public void DeleteClientFromBDD(string nom, string prenom)
        {
            this.connection.Open();

            //PRISE DE l'ID CLIENT
            string id = "";
            MySqlCommand command2 = this.connection.CreateCommand();
            command2.CommandText = "select IdClient from Cdr where Nom = '" + nom + "' and Prenom ='" + prenom + "';";
            MySqlDataReader reader = command2.ExecuteReader();
            while (reader.Read())
            {
                id = Convert.ToString(reader.GetInt32(0));
            }
            this.connection.Close();

            this.connection.Open();
            //SUPRESSION PASSE
            MySqlCommand command1 = this.connection.CreateCommand();
            command1.CommandText = "delete from passe where IdClient='"+id+"';";
            command1.ExecuteNonQuery();

            //SUPRESSION CLIENT 
            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "delete from client where Nom = '" + nom + "' and Prenom ='" + prenom + "';";
            command.ExecuteNonQuery();

            this.connection.Close();
        }

        // AFFICHAGE CLIENT PAR NOM ORDRE ALPHABETIQUE + PRENOM SI NECESSAIRE
        public List<Client> AfficherClients()
        {

            this.connection.Open();

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "SELECT * FROM CLIENT ORDER BY nom, prenom";
            int row = command.ExecuteNonQuery();
            Console.WriteLine(row);
            MySqlDataReader reader = command.ExecuteReader();

            List<Client> clients = new List<Client>();
            Client client;

            while (reader.Read())
            {
                client = new Client(reader);
                clients.Add(client);
                Console.WriteLine(client);

            }
            this.connection.Close();
            return clients;
        }

        //RENVOIE TOUT LES CLIENTS
        public List<Client> GetClients()
        {

            this.connection.Open();

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "SELECT * FROM CLIENT ORDER BY nom, prenom";
            int row = command.ExecuteNonQuery();
            Console.WriteLine(row);
            MySqlDataReader reader = command.ExecuteReader();

            List<Client> clients = new List<Client>();

            while (reader.Read())
            {

                clients.Add(new Client(reader));

            }
            this.connection.Close();
            return clients;
        }

        // CHANGEMENT DE MOT DE PASSE 
        public string ChangezMDPBDD(string nature ,string username,string ancienMdp,string mdp)
        {
           
           
            this.connection.Open();

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "update "+nature+" set password='"+mdp+"' where password='"+ancienMdp+"';";
            int row = command.ExecuteNonQuery();
            this.connection.Close();
            return mdp;
        }
        // CHANGEMENT USERNAME 
        public string ChangezUsernameBDD(string nature,string ancienUsername,string username)
        {
           
           
            this.connection.Open();

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "update " + nature + " set username='" + username + "' where username='" + ancienUsername + "';";
            command.ExecuteNonQuery();
            this.connection.Close();
            return username;

        }

        //CHANGEMENT NUMERO DE TELEPHONE
        public string ChangezNumTelBDD(string nature, string ancienNumTel,string numTel)
        {
            
            
            this.connection.Open();

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "update " + nature + " set numeroTel='" + numTel + "' where numeroTel='" + ancienNumTel + "';";
            int row = command.ExecuteNonQuery();
            this.connection.Close();
            return numTel;

        }

       


        #endregion

        #region CREATEUR DE RECETTE (CDR)

        // AFFICHAGE CDR ET INFOS CORRESPONDANTES
        public List<Cdr> GetCdr()
        {

            this.connection.Open();

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "SELECT * FROM CDR ORDER BY nom, prenom";
            int row = command.ExecuteNonQuery();
            Console.WriteLine(row);
            MySqlDataReader reader = command.ExecuteReader();

            List<Cdr> cdrs = new List<Cdr>();

            while (reader.Read())
            {

                cdrs.Add(new Cdr(reader));

            }
            this.connection.Close();
            return cdrs;
        }

        public void AfficherCdr()
        {

            this.connection.Open();

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "SELECT * FROM CDR ORDER BY nom, prenom";
            int row = command.ExecuteNonQuery();
            Console.WriteLine(row);
            MySqlDataReader reader = command.ExecuteReader();

            List<Cdr> cdrs = new List<Cdr>();

            while (reader.Read())
            {

                cdrs.Add(new Cdr(reader));

            }

            this.connection.Close();

            foreach (Cdr cdr in cdrs)
            {
                Console.WriteLine(cdr.ToString() + "  ," + NbRecettesCommandees(cdr) + " recette(s) commandée(s) \n");
            }
            
            
        }

        // RENVOIE LE NOMBRE DE FOIS OU ON A COMMANDER SES RECETTES QUELQUE SOIT LES RECETTES
        public int NbRecettesCommandees(Cdr cdr)
        {

            this.connection.Open();
            List<Cdr> listeCdr = new List<Cdr>();
            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "select sum(cont.quantite) from cdr c join inventé_par inv on c.IdCdr = inv.IdCdr join plat p on inv.IdPlat=p.IdPlat join contient cont on p.IdPlat=cont.IdPlat where c.IdCdr=" + cdr.IdCdr + ";" ;
            int row = command.ExecuteNonQuery();
            
            MySqlDataReader reader = command.ExecuteReader();
            int nb = 0;

            while (reader.Read())
            {
                
                
                    nb = reader.GetInt32(0);
                
                

            }
            this.connection.Close();
            return nb;
        }

        // AJOUTER UN CDR SPECIFIQUE EN TANT QUE OBJET A PART D'UN USERNAME ET D'UN PASSWORD
        public Cdr GenererCdr(string username, string password)
        {
            this.connection.Open();
            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "select * from cdr where username='" + username + "' and password='" + password + "';";
            MySqlDataReader reader = command.ExecuteReader();
            Cdr cdr = new Cdr();
            while (reader.Read())
            {
                cdr = new Cdr(reader);
            }
            this.connection.Close();
            return cdr;
            
        }

        // AJOUTER CDR A PARTIR DE C# SUR LA BASE DE DONNEE
        public void AddCdrToBDD(Cdr cdr,Client client)
        {
            this.connection.Open();

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "insert into cdr values ('" + Convert.ToString(cdr.IdCdr) + "','" + Convert.ToString(cdr.IdClient) + "','" + Convert.ToString(client.Nom) + "','" + client.Prenom + "','" + client.Username + "','" + client.Password  + "','" + client.NumeroTel + "','" + Convert.ToString(cdr.Cooks) + "','"+cdr.CodeBancaire+"');";
            command.ExecuteNonQuery();

            this.connection.Close();
        }

        // SUPPRIMER CDR A PARTIR DE C# SUR LA BASE DE DONNEE
        public void DeleteCdrFromBDD(string nom, string prenom, string id)
        {
            this.connection.Open();

            //SUPRESSION INVENTE_PAR
            MySqlCommand command1 = this.connection.CreateCommand();
            command1.CommandText = "delete from inventé_par where IdCdr='" + id + "';";
            command1.ExecuteNonQuery();

            //SUPRESSION CDR
            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "delete from cdr where Nom = '" + nom + "' and Prenom ='" + prenom + "';";
            int row = command.ExecuteNonQuery();


            this.connection.Close();
        }

        public void DeleteCdrFromBDD(Cdr cdr)
        {
            this.connection.Open();

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "delete from cdr where IdCdr = '" + cdr.IdCdr + "';";
            int row = command.ExecuteNonQuery();


            this.connection.Close();
        }

        
        // RECETTES QUE LE CDR A CREE
        public List<Plat> PlatsCreesCdr(Cdr cdr)
        {
            this.connection.Open();

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "select p.* from plat p,inventé_par o where o.idPlat = p.IdPlat and o.idCdr ='"+Convert.ToString(cdr.IdCdr)+"';";
            MySqlDataReader reader = command.ExecuteReader();

            List<Plat> plats = new List<Plat>();

            while (reader.Read())
            {

                plats.Add(new Plat(reader));

            }
            this.connection.Close();
            return plats;
        }
        public List<Plat> PlatsCreesCdr(string id)
        {
            this.connection.Open();

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "select p.* from plat p,inventé_par o where o.idPlat = p.IdPlat and o.idCdr ='" + id + "';";
            MySqlDataReader reader = command.ExecuteReader();

            List<Plat> plats = new List<Plat>();

            while (reader.Read())
            {

                plats.Add(new Plat(reader));

            }
            this.connection.Close();
            return plats;
        }

        public Plat GoldenRecette()
        {
            Plat plat = new Plat();

            this.connection.Open();

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "select p.* from plat p join contient cont on p.IdPlat = cont.IdPlat group by p.IdPlat having sum(cont.quantite)>= All(select sum(cont1.quantite) from contient cont1 join plat p1 on p1.IdPlat=cont1.IdPlat group by p1.IdPlat);";
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                plat = new Plat(reader);
            }

            this.connection.Close();

            Console.Write("La recette la plus appréciée de nos clients est la suivante: " + plat.Nom);
            Console.WriteLine("\n\n\n");

            return plat;
        }

        public Cdr GoldenCdr()
        {


            this.connection.Open();

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "select c.* from cdr c join inventé_par inv on c.IdCdr = inv.IdCdr join plat p on inv.IdPlat=p.IdPlat join contient cont on p.IdPlat=cont.IdPlat having sum(cont.quantite) >= All(select sum(cont1.quantite) from contient cont1 join plat p1 on p1.IdPlat=cont1.IdPlat group by p1.IdPlat);";
            MySqlDataReader reader = command.ExecuteReader();
            Cdr cdr = new Cdr();

            while (reader.Read())
            {
                cdr = new Cdr(reader);
            }

            this.connection.Close();

            Console.WriteLine("Et l'oscar du Cdr d'Or est attribué à ..." + cdr.ToString());
            Console.WriteLine("\n");

            return cdr;
        }
        // RENVOIE LE TOP 5 DES RECETTES LES PLUS COMMANDEES
        public List<Plat> Top5Recettes()
        {
            List<Plat> liste = new List<Plat>();
            int i = 0;

            this.connection.Open();

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "select p.* from plat p join contient cont on p.IdPlat = cont.IdPlat group by p.IdPlat order by sum(cont.quantite) DESC;";
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                if (i >= 4)
                {
                    break;
                }

                liste.Add(new Plat(reader));
            }



            Console.WriteLine("Voici le top 5 des recettes les plus commandées :");
            foreach (Plat plt in liste)
            {
                Console.WriteLine("-" + plt.Nom);
            }

            this.connection.Close();

            return liste;
        }

        // REMUNERATION SELON LES COMMANDES 
        public void RemunerationCdr(Commande commande)
        {
            IEnumerable<Plat> PlatsDistincts = commande.ListePlats.Distinct();
            foreach (Plat p in PlatsDistincts)
            {
                int compteur = p.OccurencePlat(commande.ListePlats, p);
                //ACTUALISATION COMPTEUR
                this.connection.Open();
                MySqlCommand commande0 = this.connection.CreateCommand();
                commande0.CommandText = "update Plat set compteur=compteur+'" + compteur + "' where IdPlat='" + Convert.ToString(p.IdPlat) + "';";
                commande0.ExecuteNonQuery();
                this.connection.Close();


                if ((p.Compteur + compteur >= 50) && (p.Compteur < 50))
                {
                    p.PrixCdr = 4;
                    string id = Convert.ToString(CreateurPlat(p.IdPlat));
                    this.connection.Open();
                    MySqlCommand comand = this.connection.CreateCommand();
                    comand.CommandText = "update cdr set Cooks=Cooks+('" + Convert.ToString(4 * compteur) + "') where IdCdr='" + id + "';";
                    comand.ExecuteNonQuery();
                    this.connection.Close();
                }
                else
                {
                    p.PrixCdr = 2;
                    string id2 = Convert.ToString(CreateurPlat(p.IdPlat));
                    this.connection.Open();
                    MySqlCommand comand = this.connection.CreateCommand();
                    comand.CommandText = "update cdr set Cooks=Cooks+('" + Convert.ToString(2 * compteur) + "') where IdCdr='" + id2 + "';";
                    comand.ExecuteNonQuery();
                    this.connection.Close();
                }
            }
        }

        #endregion

        #region COMMANDE

        //RENVOIE TOUTE LES COMMANDES 
        public List<Commande> GetCommandes()
        {

            this.connection.Open();

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "SELECT * FROM COMMANDE ORDER BY IdCommande";

            MySqlDataReader reader = command.ExecuteReader();

            List<Commande> commandes = new List<Commande>();

            while (reader.Read())
            {

                commandes.Add(new Commande(reader));

            }
            this.connection.Close();
            return commandes;
        }

        //RECHERCHE COMMANDE 
        public List<Commande> RechercherCommande(string recherche)
        {

            this.connection.Open();

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "SELECT * FROM Commande where IdCommande like " + "'%" + recherche + "%';";
            //Console.WriteLine(command.CommandText);

            MySqlDataReader reader = command.ExecuteReader();

            List<Commande> commandes = new List<Commande>();

            while (reader.Read())
            {

                commandes.Add(new Commande(reader));

            }
            this.connection.Close();
            return commandes;
        }

        
        // MET A JOUR LA LISTE DE PLAT ASSOCIER A CHAQUE COMMANDE
        public List<Plat> GenererListePlat( int idCommande)
        {
            this.connection.Open();

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "select p.* from plat p,commande c,contient o where o.IdPlat=p.IdPlat and o.IdCommande=c.IdCommande and IdCommande='" + Convert.ToInt32(idCommande) + "';";
            MySqlDataReader reader = command.ExecuteReader();

            List<Plat> plats = new List<Plat>();

            while (reader.Read())
            {

                plats.Add(new Plat(reader));

            }
            this.connection.Close();
            return plats;
        }


        // AFFICHAGE DE TOUTE LES COMMANDES REALISE 
        public List<Commande> CommandesRealisees()
        {

            this.connection.Open();

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "SELECT * FROM Commande ORDER BY IdCommande";

            MySqlDataReader reader = command.ExecuteReader();

            List<Commande> commandes = new List<Commande>();

            while (reader.Read())
            {

                commandes.Add(new Commande(reader));

            }
            this.connection.Close();
            return commandes;
        }

        // RENVOIE TOUTE LES COMMANDE REALISE DE CE CLIENT
        public List<Commande> CommandeDuClient(int idClient)
        {
            this.connection.Open();

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "select c.* from commande c,passe p where p.IdCommande=c.IdCommande and p.IdClient='" + Convert.ToString(idClient) + "';";
            MySqlDataReader reader = command.ExecuteReader();

            List<Commande> commande = new List<Commande>();
            while (reader.Read())
            {

                commande.Add(new Commande(reader));

            }
            this.connection.Close();
            return commande;

        }

        // ENTRE LA COMMANDE DANS LA BDD
        public void CommandeToBdd(Commande commande,string nature,Client client,Cdr cdr)
        {
            this.connection.Open();

            // ENTREE DE LA COMMAND EN BDD
            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "insert into commande values ('" + Convert.ToString(commande.IdCommande) + "','" + Convert.ToString(commande.Prix) + "','" + commande.Adresse + "','" + commande.Date.ToString() + "');";
            command.ExecuteNonQuery();

            // MISE EN RELATION DU CLIENT ET DE LA COMMANDE 
            MySqlCommand command2 = this.connection.CreateCommand();
            command2.CommandText = "insert into passe values ('" + Convert.ToString(client.IdClient) + "','" + Convert.ToString(commande.IdCommande) + "');";
            command2.ExecuteNonQuery();


            // MISE EN RELATION PLAT ET COMMANDE 
            //MySqlCommand command3 = this.connection.CreateCommand();
            foreach (Plat p in commande.ListePlats.Distinct())
            {
                MySqlCommand command3 = this.connection.CreateCommand();
                command3.CommandText = "insert into contient values ('" + Convert.ToString(commande.IdCommande) + "','" + Convert.ToString(p.IdPlat) + "','" + Convert.ToString(commande.CompteurPlat(p)) + "');";
                command3.ExecuteNonQuery();
            }
            this.connection.Close();
        }

        //ACTUALISE LA DATE DE DERNIERE UTILISATION LORS D'UNE COMMANDE 
        public void ActualisationDate(Commande commande)
        {
            string date = Convert.ToString(DateTime.Now);
            foreach (Plat p in commande.ListePlats)
            {
                List<Produit> produits = new List<Produit>();
                this.connection.Open();

                MySqlCommand command = this.connection.CreateCommand();
                command.CommandText = "select * from produit p, composé_de c where c.IdProduit=p.IdProduit and C.IdPlat='" + Convert.ToString(p.IdPlat) + "';";
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    produits.Add(new Produit(reader));
                }
                this.connection.Close();

                foreach(Produit w in produits)
                {
                    this.connection.Open();
                    MySqlCommand command1 = this.connection.CreateCommand();
                    command1.CommandText = "update produit set DerniereUtilisation='" + date + "' where IdProduit='" + Convert.ToString(w.IdProduit) + "';";
                    command1.ExecuteNonQuery();
                    this.connection.Close();
                }

            }
        }
        #endregion

        #region PLAT

        //RENVOIE LE NOMBRE DE PLAT DANS LA BDD
        public int NbPlats()
        {
            this.connection.Open();

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "select count(*) from plat;";
            int row = command.ExecuteNonQuery();
            MySqlDataReader reader = command.ExecuteReader();
            int val = 0;

            while (reader.Read())
            {
                val = reader.GetInt32(0);
            }
            this.connection.Close();
            return val;
        }

        //RENVOIE TOUT LES PLATS 
        public List<Plat> GetPlats()
        {

            this.connection.Open();

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "SELECT * FROM Plat ORDER BY IdPlat";

            MySqlDataReader reader = command.ExecuteReader();

            List<Plat> plats = new List<Plat>();

            while (reader.Read())
            {

                plats.Add(new Plat(reader));

            }
            this.connection.Close();
            return plats;
        }

        //RECHERCHE PLATS 
        public List<Plat> RechercherPlat(string recherche)
        {

            this.connection.Open();

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "SELECT * FROM Plat where Nom like " + "'%" + recherche + "%';";
            //Console.WriteLine(command.CommandText);

            MySqlDataReader reader = command.ExecuteReader();

            List<Plat> plats = new List<Plat>();

            while (reader.Read())
            {

                plats.Add(new Plat(reader));

            }
            this.connection.Close();
            return plats;
        }

        //RENVOIE SI LE PLAT EST CUISINABLE SELON LES PRODUITS DISPO
        public bool Cuisinable(string ID,int quantité)
        {
            bool cuisinable = true;
            this.connection.Open();
            
            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "select p.IdProduit,p.StockActuel,c.quantite from produit p, composé_de c where c.IdPlat = '" +ID + "' and c.IdProduit = p.IdProduit;";

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                if (reader.GetFloat(1) - (reader.GetFloat(2) * quantité)<0)
                {
                    cuisinable = false;
                }

            }
            this.connection.Close();
            return cuisinable;
            

            

            
        }

        //ACTUALISE LES STOCKS SELON LE PLAT COMMANDE
        public void ActualisationStock(string ID,int quantité)
        {
            this.connection.Open();
            List<double> lesStocks = new List<double>() { };
            List<string> lesIds = new List<string>() { };
            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "select p.IdProduit,p.StockActuel,c.quantite from produit p, composé_de c where c.IdPlat = '" + ID + "' and c.IdProduit = p.IdProduit;";

            MySqlDataReader reader = command.ExecuteReader();
            double stock = 0;
            
            while (reader.Read())
            {
                stock = reader.GetDouble(1) - (reader.GetDouble(2) * quantité);
                lesStocks.Add(Math.Round(stock,3));
                lesIds.Add(Convert.ToString(reader.GetInt32(0)));

            }
            this.connection.Close();
            this.connection.Open();
            for (int i=0; i < lesIds.Count; i++)
            {
                MySqlCommand commande1 = this.connection.CreateCommand();

                commande1.CommandText = "update produit set StockActuel='" + lesStocks[i] + "' where IdProduit='" + lesIds[i] + "';";

                commande1.ExecuteNonQuery();
            }
            
            this.connection.Close();
        }

        //SUPPRIME UN PLAT DE LA BDD
        public void DeletePlatFromBDD(string id)
        {
            this.connection.Open();

            //SUPRESSION DU PLAT DANS COMPOSE_DE 
            MySqlCommand command0 = this.connection.CreateCommand();
            command0.CommandText = "delete from composé_de where IdPlat='" + id + "';";
            command0.ExecuteNonQuery();

            //SUPRESSION DU PLAT DANS CONTIENT
            MySqlCommand command1 = this.connection.CreateCommand();
            command1.CommandText = "delete from contient where IdPlat='" + id + "';";
            command1.ExecuteNonQuery();

            //SUPRESSION INVENTE PAR 
            MySqlCommand command3 = this.connection.CreateCommand();
            command3.CommandText = "delete from inventé_par where IdPlat='" + id + "';";
            command3.ExecuteNonQuery();


            //SUPRESSION DU PLAT 
            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "delete from plat where IdPlat='" + id + "';";
            command.ExecuteNonQuery();


            this.connection.Close();
        }

        //RENVOIE UN BOOL SI LE PAYEMENT EST POSSIBLE 
        public bool PayementPossible(Commande commande,string nature,Client client,Cdr cdr)
        {
            bool possible = false;
            int prix = commande.GetPrix();
            if (nature == "client")
            {
                if (client.Cooks >= prix)
                {
                    possible = true;
                }
            }
            else
            {
                if(cdr.Cooks >= prix)
                {
                    possible = true;
                }
            }
            return possible;
        }

        //PAYE UNE COMMANDE SELON SA LISTE DE PLAT
        public void Payement(Commande commande,string nature,Client client,Cdr cdr)
        {
            int prix = commande.GetPrix();
            if (nature == "client")
            {
                this.connection.Open();
                MySqlCommand comand = this.connection.CreateCommand();

                comand.CommandText = "update client set Cooks='" + Convert.ToString(client.Cooks - prix) + "' where IdClient='" + Convert.ToString(client.IdClient) + "' ;";
                comand.ExecuteNonQuery();
                client.Cooks = client.Cooks - prix;
                this.connection.Close();
            }
            else
            {
                this.connection.Open();
                MySqlCommand comand = this.connection.CreateCommand();

                comand.CommandText = "update client set Cooks='" + Convert.ToString(client.Cooks - prix) + "' where IdClient='" + Convert.ToString(client.IdClient) + "' ;";
                comand.ExecuteNonQuery();
                client.Cooks = client.Cooks - prix;
                this.connection.Close();

                this.connection.Open();
                MySqlCommand comand1 = this.connection.CreateCommand();

                comand1.CommandText = "update cdr set Cooks='" + Convert.ToString(cdr.Cooks - prix) + "' where IdCdr='" + Convert.ToString(cdr.IdCdr) + "' ;";
               
                comand1.ExecuteNonQuery();
                cdr.Cooks = cdr.Cooks - prix;
                this.connection.Close();
            }
           
        }

        //ACTUALISE LE PRIX DES RECETTES
        public void ActualisationPrixRecette(Commande commande)
        {
            IEnumerable<Plat> PlatsDistincts = commande.ListePlats.Distinct();
            foreach (Plat p in PlatsDistincts)
            {
                if (( p.Compteur+p.OccurencePlat(commande.ListePlats,p)>=10) && (p.Compteur < 10))
                {
                    p.Prix += 2;
                    this.connection.Open();
                    MySqlCommand comand = this.connection.CreateCommand();
                    comand.CommandText = "update plat set prix=prix+2 where IdPlat='" + Convert.ToString(p.IdPlat) + "';";
                    comand.ExecuteNonQuery();
                    this.connection.Close();
                }
                if ((p.Compteur + p.OccurencePlat(commande.ListePlats, p) >= 50) && (p.Compteur < 50))
                {
                    p.Prix += 5;
                    this.connection.Open();
                    MySqlCommand comand = this.connection.CreateCommand();
                    comand.CommandText = "update plat set prix=prix+2 where IdPlat='" + Convert.ToString(p.IdPlat) + "';";
                    comand.ExecuteNonQuery();
                    this.connection.Close();
                }
            }
        }

        //RENVOIE LE IdCdr du CREATEUR DE CE PLAT
       public int CreateurPlat(int IdPlat)
        {
            this.connection.Open();
            int IdCLient = 0;
            MySqlCommand comand = this.connection.CreateCommand();
            comand.CommandText = "select IdCdr from inventé_par where IdPlat= '" + Convert.ToString(IdPlat) + "';";
            MySqlDataReader reader = comand.ExecuteReader();
            while (reader.Read())
            {
                IdCLient = reader.GetInt32(0);
            }
            this.connection.Close();
            return IdCLient;
        }

        //CREATION DE RECETTE DANS LA BDD
        public void CreationRecette(Plat plat,List<Produit> listeProduit,List<double> quantite,Cdr cdr)
        {
            //ENTREE DU PLAT EN  BDD 
            this.connection.Open();
            DateTime date = DateTime.Now;
            MySqlCommand comand = this.connection.CreateCommand();
            comand.CommandText = "insert into plat values ( '"+Convert.ToString(plat.IdPlat)+"','"+Convert.ToString(plat.Nom)+"','"+plat.Recette+"','"+plat.Description+"','"+Convert.ToString(plat.Prix)+"','2','"+date.ToString()+"','0');";
            comand.ExecuteNonQuery();


            //MISE EN RELATION CREATEUR ET PLAT

            MySqlCommand command2 = this.connection.CreateCommand();
            command2.CommandText = "insert into inventé_par values ('" + Convert.ToString(plat.IdPlat) + "','" + Convert.ToString(cdr.IdCdr) + "');";
            command2.ExecuteNonQuery();


            //PEUPLEMENT COMPOSE DE 
            int compteur = 0;
            foreach (Produit p in listeProduit)
            {
                MySqlCommand comand3 = this.connection.CreateCommand();
                comand3.CommandText = "insert into composé_de values ('" + Convert.ToString(plat.IdPlat) + "','" + Convert.ToString(p.IdProduit) + "','" + Convert.ToString(quantite[compteur]) + "');";
                comand3.ExecuteNonQuery();
                compteur += 1;
            }
            this.connection.Close();
        }

        //RENVOIE LES RECETTES COMPOSE DU PRODUIT DEMANDER
        public void RecetteDuProduit(string idProduit)
        {
            this.connection.Open();
            List<Plat> plats = new List<Plat>();
            MySqlCommand comand = this.connection.CreateCommand();
            comand.CommandText = "select * from plat p, composé_de c where c.IdPLat=p.IdPlat and C.IdProduit='" + idProduit + "';";
            MySqlDataReader reader = comand.ExecuteReader();
            while (reader.Read())
            {
                plats.Add(new Plat(reader));
            }
            this.connection.Close();

            foreach(Plat p in plats)
            {
                p.AffichageClient();
            }

        }

        #endregion

        #region PRODUIT 

        //MISE A JOUR DES STOCKS
        public void MiseAJourStoks(Produit produit)
        {
            List<Produit> listeProduits = GetProduits();
            foreach(Produit prod in listeProduits)
            {
                if (DateTime.Compare(DateTime.Now, prod.DerniereUtilisation.AddDays(30))>0)
                {
                    prod.StockMax = prod.StockMax / 2;
                    prod.StockMin = prod.StockMin / 2;
                }
            }
        }

        //REAPPROVISIONNEMENT DES PRODUITS
        public void Reapprovisionnement()
        {
            List<Produit> produits = this.GetProduits();
            foreach(Produit prod in produits)
            {
                if(DateTime.Now.Day == 1)
                {
                    prod.Reapprovisionnement();
                }
            }
        }
        
        //RECHERCHE DES PRODUITS SELON LA RECHERCHE
        public List<Produit> RechercherProduit(string recherche)
        {

            this.connection.Open();

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "SELECT * FROM Produit where Nom like " + "'%" + recherche + "%';";
            //Console.WriteLine(command.CommandText);

            MySqlDataReader reader = command.ExecuteReader();

            List<Produit> produits = new List<Produit>();

            while (reader.Read())
            {

                produits.Add(new Produit(reader));

            }
            this.connection.Close();
            return produits;
        }

        //RENVOIE LES PRODUITS 
        public List<Produit> GetProduits()
        {

            this.connection.Open();

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "SELECT * FROM Produit ORDER BY Categorie,IdProduit";

            MySqlDataReader reader = command.ExecuteReader();

            List<Produit> produits = new List<Produit>();

            while (reader.Read())
            {

                produits.Add(new Produit(reader));

            }
            this.connection.Close();
            return produits;
        }

        //RENVOIE SI UN PRODUIT EST DISPONIBLE OU NON
        public bool Disponible(int ID, double quantité)
        {
            bool disponible = false;
            this.connection.Open();

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "select StockActuel,StockMin from produit where IdProduit='"+Convert.ToString(ID)+"';";

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                if (reader.GetInt32(0) - quantité >= reader.GetInt32(1))
                {
                    disponible = true;
                }
            }
            this.connection.Close();

            return disponible;
        }

        //RENVOIE LES PRODUITS AVEC UN STOCK INSUFISANT 
        public void ProduitsPetitStock()
        {

            this.connection.Open();

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "SELECT * FROM produit where stockActuel <= 2*stockMin;";

            MySqlDataReader reader = command.ExecuteReader();

            List<Produit> produits = new List<Produit>();

            while (reader.Read())
            {

                produits.Add(new Produit(reader));
            

            }
            this.connection.Close();

            foreach (Produit prod in produits)
            {
                Console.WriteLine(prod.ToString());
            }
            
        }

        //RENVOIE LES PRODUITS AVEC UN STOCKS NUL
        public List<Produit> ProduitsStockNul()
        {
            this.connection.Open();

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "SELECT * FROM produit where StockActuel=0 order by NomFournisseur,Nom;";

            MySqlDataReader reader = command.ExecuteReader();

            List<Produit> produits = new List<Produit>();

            while (reader.Read())
            {

                produits.Add(new Produit(reader));


            }

            
            this.connection.Close();
            return produits;

        }




        #endregion

        #region FOURNISSEUR



        #endregion

    }
}
