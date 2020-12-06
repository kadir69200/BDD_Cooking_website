using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace PROJET_BDD_KADIR
{
    class Produit
    {
        // Attributs

        private int idProduit;
        private string nom;
        private string categorie;
        private string unite;
        private int stockMin;
        private int stockMax;
        private int stockActuel;
        private string nomFounisseur;
        private string refFournisseur;
        private DateTime deniereUtilisation;


        #region CONSTRUCTEURS 
        // Constructeur

        public Produit(string nom, string categorie, string unite, int stockMin, int stockMax, int stockActuel, string nomFounisseur, string refFournisseur,BDD cooking)
        {
            
            var rand = new Random();
            int idRandom = 0;
            do
            {
                idRandom = rand.Next(0, 9999999);
                this.idProduit = idRandom;
            }
            while (cooking.IdExiste(idRandom, "Produit"));
            
            this.nom = nom;
            this.categorie = categorie;
            this.unite = unite;
            this.stockMin = stockMin;
            this.stockMax = stockMax;
            this.stockActuel = stockActuel;
            this.nomFounisseur = nomFounisseur;
            this.refFournisseur = refFournisseur;
            this.deniereUtilisation = DateTime.Now;

        }

        public Produit(MySqlDataReader reader)
        {
            this.idProduit = reader.GetInt32(0);
            this.nom = reader.GetString(1);
            this.categorie = reader.GetString(2);
            this.unite = reader.GetString(3);
            this.stockMin = reader.GetInt32(4);
            this.stockMax = reader.GetInt32(5);
            this.stockActuel = reader.GetInt32(6);
            this.nomFounisseur = reader.GetString(7);
            this.refFournisseur = reader.GetString(8);
            this.deniereUtilisation = Convert.ToDateTime(reader.GetString(9));
        }
        #endregion

        #region PROPRIETES
        // Propriétés

        public int IdProduit
        {
            get { return this.idProduit; }
        }

        public string Nom
        {
            get { return this.nom; }
        }
        public string Categorie
        {
            get { return this.categorie; }
        }
        public string Unite
        {
            get { return this.unite; }
        }
        public int StockMin
        {
            get { return this.stockMin; }
            set { this.stockMin = value; }
        }
        public int StockMax
        {
            get { return this.stockMax; }
            set { this.stockMax = value; }
        }
        public int StockActuel
        {
            get { return this.stockActuel; }
            set { this.stockActuel = value; }
        }
        public string NomFournisseur
        {
            get { return this.nomFounisseur; }
        }
        public string RefFournisseur
        {
            get { return this.refFournisseur; }
        }

        public DateTime DerniereUtilisation
        {
            get { return this.deniereUtilisation; }
            set { this.deniereUtilisation = value; }
        }
        #endregion

        #region AFFICHAGE
        // Méthodes

        public override string ToString()
        {
            return this.idProduit + "   " + this.Nom + "     " + this.Categorie + "    " + this.Unite + "       " + this.stockMin + "      " + this.stockMax + "       " + this.stockActuel + "          " + this.nomFounisseur + "         " + this.refFournisseur + " " ;
        }

        public void Reapprovisionnement()
        {
            this.StockActuel = this.StockMax;
        }
        public void AffichageCreateur()
        {
            Console.WriteLine(Convert.ToString(IdProduit) + " : " + nom);
            
        }
        #endregion
    }
}
