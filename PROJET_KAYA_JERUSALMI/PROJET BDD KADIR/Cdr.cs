using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace PROJET_BDD_KADIR
{
    class Cdr:Client
    {
        // Attributs

        private int idCdr;
        private int idClient;
        private string nom;
        private string prenom;
        private string username;
        private string password;
        private string numTel;
        private int cooks;
        private string codeBancaire;
     
        
        private List<Plat> listeRecettes;

        #region CONSTRUCTEURS
        //Constructeur
        public Cdr() { }

        public Cdr(MySqlDataReader reader)
        {
            this.idCdr = reader.GetInt32(0);
            this.idClient = reader.GetInt32(1);
            this.nom = reader.GetString(2);
            this.prenom = reader.GetString(3);
            this.username = reader.GetString(4);
            this.numTel = reader.GetString(6);
            this.password = reader.GetString(5);
            
            this.cooks = reader.GetInt32(7);
            this.codeBancaire = reader.GetString(8);
        }

        //CONSTRUCTEUR CLIENT PASSE CDR
        public Cdr(int idclient, string nom, string prenom, string username, string numeroTel, string password, int cooks,string codeBancaire, BDD cooking) : base(idclient, nom, prenom, username,numeroTel,password,cooks,codeBancaire)
        {
            //CREATION DE SON ID CDR
            var rand = new Random();
            int idRandom = 0;
            do
            {
                idRandom = rand.Next(0, 9999999);
                this.idCdr = idRandom;
            }
            while (cooking.IdExiste(idRandom, "Cdr"));


        }

        //CONSTRUCUTEUR BASIQUE
        public Cdr(string nom, string prenom, string username, string numeroTel,string password, int cooks, List<Plat> listeRecettes, int nbPoints,string codeBancaire,BDD cooking) : base(nom, prenom, username, numeroTel,password,codeBancaire,cooking)
        {
            //CREATION DE SON ID CDR
            var rand = new Random();
            int idRandom = 0;
            do
            {
                idRandom = rand.Next(0, 9999999);
                this.idCdr = idRandom;
            }
            while (cooking.IdExiste(idRandom, "Cdr"));


            //CREATION DE SON ID CLIENT
            int idRandom2 = 0;
            do
            {
                idRandom2 = rand.Next(0, 9999999);
                this.idClient = idRandom;
            }
            while (cooking.IdExiste(idRandom, "Client"));

            this.listeRecettes = new List<Plat>();
            
        }

        #endregion

        #region PROPRIETES
        //Propriétés
        public int IdCdr
        {
            get { return this.idCdr; }
            set { idCdr = value; }
        }
        public List<Plat> ListeRecettes
        {
            get { return listeRecettes; }
            set { this.listeRecettes = value; }
        }

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
            get { return numTel; }
            set { numTel = value; }
        }

        public int Cooks
        {
            get { return cooks; }
            set { this.cooks = value; }
        }

        public string Username
        {
            get { return this.username; }
            set { username = value; }
        }

        public  string Password
        {
            get { return this.password; }
            set { password = value; }
        }
        #endregion

        #region AFFICHAGE
        public override void Affichage()
        {


            Console.Write("NOM : ");
            Console.Write(this.nom);
            Console.WriteLine("");
            Console.Write("Prenom : ");
            Console.Write(this.prenom);
            Console.WriteLine("");
            Console.Write("username : ");
            Console.Write(this.username);
            Console.WriteLine("");
            Console.Write("password : ");
            Console.Write(this.password);
            Console.WriteLine("");
            Console.Write("Numero de téléphone : ");
            Console.Write(this.numTel);
        }

        public void AffichageAdmin(BDD cooking,Cdr cdr)
        {
            Console.WriteLine(idCdr + " " + nom + " " + prenom);
            int nombre = 0;
            List<Plat> recettes = cooking.PlatsCreesCdr(cdr);
            nombre = recettes.Count;
            Console.WriteLine("Recettes crées : " + Convert.ToString(nombre)+" \n");

        }

        

        public override string ToString()
        {
            return this.Prenom + "  " + this.Nom ;
        }
        #endregion
    }
}
