using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace PROJET_BDD_KADIR
{
    class Commande
    {
        //Attributs

        private int idCommande;
        private int prix;
        private List<Plat> listePlats;
        private string adresse;
        private DateTime date;





        #region CONSTRUCTEURS

        public Commande() { }
        public Commande(int prix, string adresse, DateTime date,BDD cooking)
        {
            
            var rand = new Random();
            int idRandom = 0;
            do
            {
                idRandom = rand.Next(0, 9999999);
                this.idCommande = idRandom;
            }
            while (cooking.IdExiste(idRandom, "Commande"));

            
            this.adresse = adresse;
            this.date = date;
            this.prix = prix;
        }
        public Commande(int prix, string adresse, DateTime date, BDD cooking,List<Plat> listePlats) : this(prix, adresse, date, cooking)
        {
            this.listePlats = listePlats;
        }
        public Commande(MySqlDataReader reader) 
        {
            BDD cooking = new BDD("cooking", "Kevin_0605");
            this.idCommande = reader.GetInt32(0);
            this.prix = reader.GetInt32(1);
            this.adresse = reader.GetString(2);
            //this.date=reader.GetString(3);
            this.ListePlats = cooking.GenererListePlat(idCommande);
        }

        #endregion

        #region PROPRIETES
        //PROPRIETES
        public int IdCommande
        {
            get { return this.idCommande; }
        }

        public List<Plat> ListePlats
        {
            get { return this.listePlats; }
            set { this.listePlats = value;}
        }

        public int Prix
        {
            get { return prix; }
            set { this.prix = value; }
        }
        public string Adresse
        {
            get { return adresse; }
            set { adresse = value; }
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        // Méthodes
        public int GetPrix ()
        {
            int prix = 0;
            foreach (Plat plat in listePlats)
            {
                prix += plat.Prix;
            }

            return prix;
        }
        #endregion

        #region AFFICHAGE
        public override string ToString()
        {
            string commande = ("Commande :" + idCommande + "\n"); ;
            foreach(Plat plat in listePlats)
            {
                commande += " " + plat.ToString();
            }
            return commande;
        }

        public string InfoCommande()
        {
            string infos = ("Informations de la commandes :\n");
            foreach (Plat plat in listePlats)
            {
                infos += plat.ToString() + " :\n" + plat.Recette;
            }
            return infos;
        }
        #endregion

        //COMPTEUR DE PLAT D'UNE COMMANDE
        public int CompteurPlat(Plat p)
        {
            int compteur = 0;
            foreach ( Plat q in this.listePlats)
            {
                if (p == q)
                {
                    compteur += 1;
                }
            }
            return compteur;
        }
        
        
    }
}
