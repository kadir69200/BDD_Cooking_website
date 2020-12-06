using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace PROJET_BDD_KADIR
{
    class Plat
    {
        // Attributs

        private int idPlat;
        private string nom;
        private string recette;
        private string description;
        private int prixCdr;
        private int prix;
        private int compteur;
        private DateTime dateCreation;


        #region CONSTRUCTEURS
        // Constructeur

        public Plat(string nom,string recette,string description, int prix,BDD cooking) 
        {
            
            var rand = new Random();
            int idRandom = 0;
            do
            {
                idRandom = rand.Next(0, 9999999);
                this.idPlat = idRandom;
            }
            while (cooking.IdExiste(idRandom, "Plat"));
            this.description = description;
            this.recette = recette;
            this.nom = nom;
            this.prix = prix;
           
        }

        public Plat(MySqlDataReader reader)
        {
            this.idPlat = reader.GetInt32(0);
            this.nom = reader.GetString(1);
            this.recette = reader.GetString(2);
            this.description = reader.GetString(3);
            this.prix = reader.GetInt32(4);
            this.prixCdr = reader.GetInt32(5);
            this.dateCreation = Convert.ToDateTime(reader.GetString(6));   
            
        }

        public Plat() { }
        #endregion

        #region PROPRIETES
        //Propriétés

        public int IdPlat
        {
            get { return idPlat; }
        }

        public string Description
        {
            get { return description; }
        }

        public string Recette
        {
            get { return recette; }
        }

        public string Nom
        {
            get { return this.nom; }
        }

       public int Compteur
        {
            get { return compteur; }
            set { compteur = value; }
        }

        public int Prix
        {
            get { return prix; }
            set { prix = value; }
        }
        public int PrixCdr
        {
            get { return prixCdr; }
            set { prixCdr = value; }
        }
        #endregion

        #region AFFICHAGE
        public int OccurencePlat(IEnumerable<Plat> listeplats,Plat plat)
        {
            int compteur = 0;
            foreach(Plat p in listeplats)
            {
                if (p == plat)
                {
                    compteur += 1;
                }
            }
            return compteur;
        }
        public void AffichageCdr()
        {
            Console.WriteLine(this.nom + " " + " recette : " + this.recette + " description : " + this.description + " créé le "+dateCreation );
            Console.WriteLine("Ce plat coute " + this.prix);
            Console.WriteLine("A chaque achat de ce plat vous serez payé(e) " + this.prixCdr);
            Console.WriteLine("");
        }
        public void AffichageClient()
        {
            Console.WriteLine( Convert.ToString(this.idPlat)+" :  " +this.nom + "     " + " recette : " + this.recette + "       description : " + this.description);
            Console.WriteLine("PRIX TTC : " + this.prix);

        }

        public override string ToString()
        {
            return this.nom;
        }
        #endregion
    }
}
