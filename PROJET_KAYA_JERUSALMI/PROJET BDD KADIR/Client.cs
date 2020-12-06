using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace PROJET_BDD_KADIR
{
    class Client
    {
        //Attributs

        private int idClient;
        private string nom;
        private string prenom;
        private string username;
        private string numeroTel;
        private string password;
        private int cooks;
        private string codeBancaire;
        private bool admin;

        

        //Constructeurs
        public Client() { }
        public Client(string nom, string prenom, string username, string numeroTel, string password,string codeBancaire,BDD cooking)
        {
            
            var rand = new Random();
            int idRandom = 0;
            do
            {
                idRandom = rand.Next(0, 9999999);
                this.idClient = idRandom;
            }
            while (cooking.IdExiste(idRandom, "Client"));
            this.nom = nom;
            this.prenom = prenom;
            this.username = username;
            this.numeroTel = numeroTel;
            this.password = password;
            this.cooks = 0;
            this.admin = false;
            this.codeBancaire = codeBancaire;
        }
        public Client(string username,string password)
        {
            this.username = username;
            this.password = password;
        }
        public Client(MySqlDataReader reader)
        {
            this.idClient = reader.GetInt32(0);
            this.nom = reader.GetString(1);
            this.prenom = reader.GetString(2);
            this.username = reader.GetString(3);
            this.numeroTel = reader.GetString(4);
            this.password = reader.GetString(5);
            this.cooks = reader.GetInt32(6);
            this.codeBancaire = reader.GetString(8);
            this.admin = false;

        }
        public Client(int idClient,string nom,string prenom,string username,string numTel,string password,int cooks,string codeBancaire)
        {
            this.idClient = idClient;
            this.nom = nom;
            this.prenom = prenom;
            this.username = username;
            this.numeroTel = numTel;
            this.password = password;
            this.cooks = cooks;
            this.codeBancaire = codeBancaire;
            
        }

        // Propriétés

        public int IdClient { get { return idClient; } }

       
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public string Prenom
        {
            get { return prenom; }
            set { prenom = value; }
        }


        public string NumeroTel
        {
            get { return numeroTel; }
            set { numeroTel = value; }
        }

        public int Cooks
        {
            get { return cooks; }
            set { this.cooks = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public bool Admin
        {
            get { return admin; }
        }

        public string CodeBancaire
        {
            get { return codeBancaire; }
        }


        #region AFFICHAGE
        public virtual void Affichage()
        {
            Console.WriteLine("\nNOM : " + this.nom);

        }
        public override string ToString()
        {
            return this.nom + this.prenom + this.idClient;
        }
        #endregion

        //RECHER DE PLAT POUR LA SELECTION
        public List<Plat> RechercheDePlat(BDD bdd,List<Plat> listePlats)
        {
            
            int saisie = 0;
            while (saisie == 0)
            {
                Console.WriteLine("Tapez le nom du plat que vous recherchez: \n");
                string selection1 = Console.ReadLine();
                List<Plat> RecherchePLat = bdd.RechercherPlat(selection1);
                foreach (Plat p in RecherchePLat)
                {
                    p.AffichageClient();
                    Console.WriteLine("__________________");
                }
                Console.WriteLine("0 : Recommencer ma recherche  \n1 : Saisir l'ID du plat que je souhaite commander ");
                saisie = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine("ID :");
            string id =Console.ReadLine();
            Console.WriteLine("Quantité : " );
            int quantite = Convert.ToInt32(Console.ReadLine());
            List<Plat> toutLesPlats = bdd.GetPlats();
            if (bdd.Cuisinable(id, quantite))
            {
                foreach (Plat p in toutLesPlats)
                {
                    if (Convert.ToString(p.IdPlat) == id)
                    {
                        for (int i = 0; i < quantite; i++)
                        {
                            listePlats.Add(p);
                        }
                    }
                }
                
            }
            else
            {
                Console.WriteLine("Ce plat avec les QUANTITES choisis n'est pas disponible, veuillez nous excusez...");
                int quantitePossible = 0;
                for (int i=quantite-1; i==0; i--)
                {
                    if (bdd.Cuisinable(id, i))
                    {
                        quantitePossible = i;
                    }
                }
                if (quantitePossible != 0)
                {
                    Console.WriteLine("\nCependant le plat choisi est cuisinale pour une quantité de " + Convert.ToString(quantitePossible));
                }
               
            }
            
            return listePlats;

        }

        // SELECTION DE PLAT
        public List<Plat> SelectionPlats(BDD bdd)
        {
            int choix = 1;
            List<Plat> plats = new List<Plat>();
            while (choix != 3)
            {
                Console.WriteLine("1 : Rechercher un plat \n2 : Parcourir la liste des plats \n3 : Valider la commande\n");
                Console.WriteLine(" Veuillez saisir votre choix : ");
                choix = Convert.ToInt32(Console.ReadLine());
                switch (choix)
                {
                    case 1:
                        plats = RechercheDePlat(bdd, plats);
                        Console.WriteLine("\nVOTRE PLAT A ETE AJOUTE AVEC SUCCES !\n");
                        break;

                    case 2:
                        Console.WriteLine(" Voici les plats disponibles");
                        List<Plat> toutlesplats = bdd.GetPlats();
                        
                        foreach (Plat p in toutlesplats)
                        {
                            if (bdd.Cuisinable(Convert.ToString(p.IdPlat), 1))
                            {
                                p.AffichageClient();
                            }
                            
                        }
                        Console.WriteLine("Veuillez saisir l'ID du plat choisi ainsi que sa quantité ");
                        Console.WriteLine("ID : ");
                        string id = Console.ReadLine();
                        Console.WriteLine("Quantité : ");
                        int quantite = Convert.ToInt32(Console.ReadLine());

                        if (bdd.Cuisinable(id, quantite))
                        {
                            foreach (Plat p in toutlesplats)
                            {
                                if (Convert.ToString(p.IdPlat) == id)
                                {
                                    for (int i = 0; i < quantite; i++)
                                    {
                                        plats.Add(p);
                                    }
                                }
                            }
                            Console.WriteLine("\nVOTRE PLAT A ETE AJOUTE AVEC SUCCES !\n");

                            //bdd.ActualisationStock(id, quantite);
                        }
                        else
                        {
                            Console.WriteLine("Ce plat avec les QUANTITES choisis n'est pas disponible, veuillez nous excusez...");
                            int quantitePossible = 0;
                            for (int i = quantite - 1; i == 0; i--)
                            {
                                if (bdd.Cuisinable(id, i))
                                {
                                    quantitePossible = i;
                                }
                            }
                            if (quantitePossible != 0)
                            {
                                Console.WriteLine("\nCependant le plat choisi est cuisinale pour une quantité de " + Convert.ToString(quantitePossible));
                            }

                        }

                  
                        
                        break;
                    case 3:
                        break;
                }



            }
            return plats;
        }
       
      
    }
}
