using System;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlClient;
using System.Xml;


namespace PROJET_BDD_KADIR
{
    
    class Program
    {
        #region CONNEXION 
        static void Connexion(BDD cooking)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                                ------------------------------------------------------");
            Console.WriteLine("                                -----------------------COOKING------------------------");
            Console.WriteLine("                                ------------------------------------------------------");
            
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\n\n\nBienvenue dans le menu principal\n");
            Console.WriteLine(" 0 : Etes vous client ? Si oui identifiez vous ! ");
            Console.WriteLine(" 1 : Creer mon compte ");


            Console.WriteLine(" 2 : ESPACE ADMIN ");
            Console.WriteLine("\n 3 : Quittez le menu principal ");

            Console.Write("\n Veuillez saisir votre choix : ");
            int saisie0 = Int32.Parse(Console.ReadLine());
            switch (saisie0)
            {
                case 0:
                    Console.WriteLine("\n pseudo :  ");
                    string username = Console.ReadLine();
                    Console.WriteLine("\n mot de passe : ");
                    string mdp = Console.ReadLine();
                    Console.WriteLine("\nEtes vous un de nos nombreux créateur de recettes ? (oui/non) ");
                    string natureClient = Console.ReadLine();
                    if (natureClient == "oui")
                    {
                        //ALLEZ PIOCHER DANS LA TABLE CDR SI L'USERNAME ET LE MDP EXISTE ET CORRESPONDE A UN SEUL TUPLE 
                        while (cooking.Connexion("cdr", username, mdp) == false)                                               
                        {
                            //TESTER SI PSEUDO EXISTE POUR VOIR SI LE PROBLEME VIENT DU PSEUDO
                            if (cooking.PseudoExiste("cdr", username)==false)
                            {
                                Console.WriteLine("\nVOTRE PSEUDO N'EXISTE PAS, VEUILLEZ LE SAISIR A NOUVEAU !\n ");
                                Console.WriteLine("0 : saisir à nouveau mon pseudo");
                                Console.WriteLine("1 : revenir à la page connexion ");
                                string choix = Console.ReadLine();
                                if (choix == "1")
                                {
                                    Connexion(cooking);
                                }
                                Console.WriteLine("Pseudo : ");
                                username = Console.ReadLine();
                                

                                 
                            }
                            //LE PROBLEME VIENT ALORS DU MOT DE PASSE
                            else
                            {
                                // ON LAISSE LE CHOIX DE REVENIR AU MENU PRINCIPAL OU NON
                                Console.WriteLine("\nVOTRE MOT DE PASSE EST FAUX, VEUILLEZ LE SAISIR A NOUVEAU !\n");
                                Console.WriteLine("0 : saisir à nouveau mon mot de passe");
                                Console.WriteLine("1 : revenir à la page connexion ");
                                string choix = Console.ReadLine();
                                if (choix == "1")
                                {
                                    Connexion(cooking);
                                }
                                Console.WriteLine("\nMot de passe :");
                                mdp = Console.ReadLine();
                            }
                            
                        }
                        // ON CREE LE CDR ASSOCIE A LA CONNEXION
                        Cdr cdr2 = cooking.GenererCdr(username, mdp);
                        // ON CREE AUSSI LE CLIENT ASSOCIE AU CDR
                        Client client1 = cooking.GenererClient(username, mdp);
                        Console.WriteLine("\n LA CONNEXION A ETE ETABLIE AVEC SUCCES ! \n");
                        // ON RENVOIE AU MENU PRINCIPAL
                        MenuPrincipal(cooking, "cdr",client1,cdr2);
                    }
                    else
                    {
                        // NOUS SOMMMES DONC DANS UNE CONNEXION CLIENT
                        while (cooking.Connexion("client", username, mdp) == false) 
                        {
                            //TESTER SI LE PSEUDO EXISTE 
                            if (cooking.PseudoExiste("client", username)==false)
                            {
                                Console.WriteLine("\nVOTRE PSEUDO N'EXISTE PAS, VEUILLEZ LE SAISIR A NOUVEAU ! \n");
                                Console.WriteLine("0 : saisir à nouveau mon pseudo");
                                Console.WriteLine("1 : revenir à la page connexion ");
                                string choix = Console.ReadLine();
                                if (choix == "1")
                                {
                                    Connexion(cooking);
                                }
                                Console.WriteLine("\nPseudo :");

                                username = Console.ReadLine();

                                
                            }
                            else
                            {
                               // LE PROBLEME VIENT ALORS DU MOT DE PASSE 
                                Console.WriteLine("\nVOTRE MOT DE PASSE EST FAUX, VEUILLEZ LE SAISIR A NOUVEAU !\n");
                                Console.WriteLine("0 : saisir à nouveau mon mot de passe");
                                Console.WriteLine("1 : revenir à la page connexion ");
                                string choix = Console.ReadLine();
                                if (choix == "1")
                                {
                                    Connexion(cooking);
                                }
                                Console.WriteLine("\nMot de passe :");
                                mdp = Console.ReadLine();
                            }
                            
                        }
                        // ON GENERE LE CLIENT ASSOCIE A LA CONNEXION
                        Client client2 = cooking.GenererClient(username, mdp);
                        // ON INSTANCIE UN CDR NULL
                        Cdr cdr1 = new Cdr();
                        Console.WriteLine("\n LA CONNEXION A ETE ETABLIE AVEC SUCCES ! \n");
                        // ON RENVOIE AU MENU PRINCIPAL
                        MenuPrincipal(cooking, "client",client2,cdr1);




                    }


                    
                   

                    


                    
                    break;

                case 1:
                    //INSCIRPTION DU CLIENT
                    Client client = cooking.Inscription(cooking);
                    //ON INSTANCIE UN CDR NULL
                    Cdr cdr = new Cdr();
                    MenuPrincipal(cooking, "client", client, cdr);
                    break;
                case 2:
                    // CONNEXION ADMIN
                    Console.WriteLine("\n pseudo :  ");
                    string usernameAdmin = Console.ReadLine();
                    Console.WriteLine("\n mot de passe : ");
                    string mdpAdmin = Console.ReadLine();
                    //ON VA VOIR DANS LA TABLE CLIENT PARMIS LES ADMIN (BOOL ADMIN==TRUE)
                    while (cooking.ConnexionAdmin(usernameAdmin, mdpAdmin)==false)
                    {
                        //CONNEXION ECHOUER AVEC MESSAGE D'ERREUR CAR LE BOOL PEUT ETRE FAUX 
                        Console.WriteLine("\nATTENTION SI VOUS N'ETES PAS ADMIN ABANDONNEZ !\n");
                        if (cooking.PseudoExiste("client", usernameAdmin)==false)
                        {
                            Console.WriteLine("\nVOTRE PSEUDO N'EXISTE PAS, VEUILLEZ LE SAISIR A NOUVEAU ! \n");
                            Console.WriteLine("0 : saisir à nouveau mon pseudo");
                            Console.WriteLine("1 : revenir à la page connexion ");
                            string choix = Console.ReadLine();
                            if (choix == "1")
                            {
                                Connexion(cooking);
                            }
                            Console.WriteLine("\nPseudo :");

                            username = Console.ReadLine();

                            
                        }
                        else
                        {
                            //LE PROBLEME VIENT DU MDP
                            Console.WriteLine("\nVOTRE MOT DE PASSE EST FAUX, VEUILLEZ LE SAISIR A NOUVEAU !\n");
                            Console.WriteLine("0 : saisir à nouveau mon mot de passe");
                            Console.WriteLine("1 : revenir à la page connexion ");
                            string choix = Console.ReadLine();
                            if (choix == "1")
                            {
                                Connexion(cooking);
                            }
                            Console.WriteLine("\nMot de passe :");
                            mdp = Console.ReadLine();
                        }
                        
                    }
                    Console.WriteLine("\n LA CONNEXION A ETE ETABLIE AVEC SUCCES ! \n");
                    Client clientAdmin = cooking.GenererClient(usernameAdmin, mdpAdmin);
                    Cdr cdrAdmin = new Cdr();
                    MenuPrincipal(cooking, "admin",clientAdmin,cdrAdmin);
                    break;
                case 3:
                    Console.WriteLine("\nA BIENTOT !\n");
                    break;
                  
            }
        }
    
        #endregion

        #region MENU PRINCIPAL 
        static void MenuPrincipal(BDD cooking, string nature,Client client,Cdr cdr)
        {

            //SELON LE STRING NATURE ON A 3 MENUS DIFFERENT ----> MENU CLIENT / MENU CDR / MENU ADMIN

            //MENU CLIENT
            if (nature == "client")
            {
                Console.WriteLine("\n\n----------MENU PRINCIPAL-----------");
                Console.WriteLine("BIENVENUE "+client.Username +" HEUREUX DE VOUS RETROUVER !");

                Console.WriteLine("\n 0 : DECONNEXION ");
                Console.WriteLine("1 : COMMANDER  ");
                Console.WriteLine("2 : PROPOSER UNE RECETTE ET AINSI DEVENIR CREATEUR DE RECETTE !");
                Console.WriteLine("3 : MON COMPTE");
                

                Console.Write("\n Veuillez saisir votre choix : ");
                int saisie = Int32.Parse(Console.ReadLine());
                switch (saisie)
                {
                    case 0:
                        Connexion(cooking);
                        break;
                    case 1:
                        Commander(cooking,nature,client,cdr);
                        break;
                    case 2:
                        CreationRecette(cooking, nature, client, cdr);
                        break;
                    case 3:
                        GestionCompte(cooking,nature,client,cdr);
                        break;
                }
            }
            //MENU CDR
            if (nature == "cdr")
            {
                Console.WriteLine("----------MENU PRINCIPAL-----------");
                Console.WriteLine("BIENVENUE " + cdr.Username + " HEUREUX DE VOUS RETROUVER !");

                Console.WriteLine("\n 0 : DECONNEXION ");
                Console.WriteLine("1 : COMMANDER  ");
                Console.WriteLine("2 : PROPOSER UNE RECETTE ET BENEFICIER D'AVANTAGES EXCLUSIFS ");
                Console.WriteLine("3 : MON COMPTE");
                Console.WriteLine("4 : MES RECETTES ");


                Console.Write("\n Veuillez saisir votre choix : ");
                int saisie = Int32.Parse(Console.ReadLine());
                switch (saisie)
                {
                    case 0:
                        Connexion(cooking);
                        break;
                    case 1:
                        Commander(cooking, nature, client, cdr);
                        break;
                    case 2:
                        CreationRecetteCdr(cooking, nature, client, cdr);
                        break;
                    case 3:
                        GestionCompte(cooking, nature, client, cdr);
                        break;
                    case 4:
                        List<Plat> recettes = cooking.PlatsCreesCdr(cdr);
                        foreach (Plat p in recettes)
                        {
                            Console.WriteLine("");
                            p.AffichageCdr();
                        }
                        break;
                }
            }
            // MENU ADMIN
            if (nature == "admin")
            {
                Console.WriteLine("----------MENU PRINCIPAL-----------");
                Console.WriteLine("BIENVENUE " + client.Username + " HEUREUX DE VOUS RETROUVER !");

                Console.WriteLine("\n 0 : DECONNEXION ");
                Console.WriteLine("1 : TABLEAU DE LA BORD DE LA SEMAINE");
                Console.WriteLine("2 : LES PRODUITS ");
                Console.WriteLine("3 : MON COMPTE");
                Console.WriteLine("4 : GESTIONS DES CLIENTS DE COOKING ");


                Console.Write("\n Veuillez saisir votre choix : ");
                int saisie = Int32.Parse(Console.ReadLine());
                switch (saisie)
                {
                    case 0:

                        Connexion(cooking);
                        break;
                    case 1:
                        TableauDeBord(cooking,nature,client,cdr);
                        break;
                    case 2:
                        XML(cooking);
                        MenuPrincipal(cooking, nature, client, cdr);
                        break;
                    case 3:
                        GestionCompte(cooking,nature,client,cdr);
                        break;
                    case 4:
                        GestionClientAdmin(cooking, nature, client, cdr);
                        break;
                }
            }


           
            


            

        }
        #endregion

        #region MENU CLIENT

        // GESTION DE SON COMPTE CLIENT/CDR
        static void GestionCompte(BDD cooking,string nature, Client client,Cdr cdr)
        {
            if (nature == "client")
            {
                Console.WriteLine("\n-------MON COMPTE-------\n");
                Console.WriteLine("0 : Revenir au Menu Principal ");
                Console.WriteLine("1 : Affichage/Modifications de mes informations ");
                Console.WriteLine("2 : Mes commandes ");
                Console.WriteLine("3 : Recharger mes cooks ");
                Console.Write("\n Veuillez saisir votre choix : ");
                int saisie = Int32.Parse(Console.ReadLine());
                switch (saisie)
                {
                    case 0:

                        MenuPrincipal(cooking, nature, client, cdr);
                        break;
                    case 1:
                        // RECAPITULATIF INFORMATIONS
                        Console.WriteLine("\n MES INFORMATIONS : \n");
                        client.Affichage();
                        // CHOIX DES MODIFFICATIONS 
                        Console.WriteLine("\n\n0 : Revenir à la gestion de mon compte ");
                        Console.WriteLine("1 : Modifier mon pseudo");
                        Console.WriteLine("2 : Modifier mon mot de passe");
                        Console.WriteLine("3 : Modifer mon numero de telephone");
                        
                        Console.WriteLine("\n Veuillez saisir votre choix :");
                        int saisie1 = Int32.Parse(Console.ReadLine());
                        switch (saisie1)
                        {
                            case 0:
                                GestionCompte(cooking, nature, client, cdr);
                                break;
                            case 1:
                                Console.WriteLine("Entrez votre nouveau pseudo : ");
                                string username = Console.ReadLine();
                                client.Username = cooking.ChangezUsernameBDD(nature, client.Username,username);
                                Console.WriteLine("\nLE CHANGEMENT A ETE REALISE AVEC SUCCES !\n");
                                GestionCompte(cooking, nature, client, cdr);
                                break;
                            case 2:
                                Console.WriteLine("Entrez votre ancien mot de passe : ");
                                string ancienMdp = Console.ReadLine();
                                while (cooking.Connexion("client", client.Username, ancienMdp) == false)
                                {
                                    Console.WriteLine("\n Le mot de passe entré n'est pas le bon veuillez reessayer :");
                                    ancienMdp = Console.ReadLine();
                                }
                                Console.WriteLine("Entrez votre nouveau mot de passe :");
                                string mdp = Console.ReadLine();
                                Console.WriteLine("Confirmez votre mot de passe : ");
                                string mdp2 = Console.ReadLine();
                                while (mdp != mdp2)
                                {
                                    Console.WriteLine("\n les mots de passes entrés ne correspondent pas \n ");
                                    Console.WriteLine("Entrez votre nouveau mot de passe :");
                                    mdp = Console.ReadLine();
                                    Console.WriteLine("Confirmez votre mot de passe : ");
                                    mdp2 = Console.ReadLine();
                                }
                                client.Password = cooking.ChangezMDPBDD(nature, client.Username,ancienMdp,mdp);
                                Console.WriteLine("\nLE CHANGEMENT A ETE REALISE AVEC SUCCES !\n");
                                GestionCompte(cooking, nature, client, cdr);

                                break;
                            case 3:
                                Console.WriteLine("Entrez votre nouveau numéro de téléphone: ");
                                string numTel = Console.ReadLine();
                                client.NumeroTel = cooking.ChangezNumTelBDD(nature, client.NumeroTel,numTel);
                                
                                Console.WriteLine("\nLE CHANGEMENT A ETE REALISE AVEC SUCCES !\n");
                                GestionCompte(cooking, nature, client, cdr);
                                break;


                        }
                        break;
                    case 2:
                        Console.WriteLine("MES COMMANDES \n");
                        List<Commande> commandes = cooking.CommandeDuClient(client.IdClient);
                        foreach (Commande c in commandes)
                        {
                            Console.WriteLine(c.ToString());
                        }

                        Console.WriteLine("\n Appuyez sur une touche pour revenir à la gestion de mon compte ");
                        string l = Console.ReadLine();
                        GestionCompte(cooking, nature, client, cdr);
                        break;
                    case 3:
                        string code = "";
                        do
                        {
                            Console.WriteLine("veuillez entrer votre code bancaire ");
                            code = Console.ReadLine();
                        }
                        while (cooking.CodeExiste(code, nature)==false);
                        Console.WriteLine("\nVeuillez saisir le montant en cooks que vous souahitez ajouter\n");
                        int cooks = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Cela vous fera " + Convert.ToString(0.7 * cooks) + " € ");
                        Console.WriteLine("Confirmez vous la transaction ?\n 1:OUI\n 2:NON");
                        int choix = Convert.ToInt32(Console.ReadLine());
                        switch (choix)
                        {
                            case 1:
                                cooking.RechargerCooks(Convert.ToString(client.IdClient), cooks, nature);
                                Console.WriteLine("\nVOTRE SOLDE A ETE RECHARGE !\n");
                                GestionCompte(cooking, nature, client, cdr);
                                break;
                            case 2:
                                GestionCompte(cooking, nature, client, cdr);
                                break;
                        }

                        
                        

                        break;
                }
                
            }
            else
            {
                Console.WriteLine("\n-------MON COMPTE-------\n");
                Console.WriteLine("0 : Revenir au Menu Principal ");
                Console.WriteLine("1 : Affichage/Modifications de mes informations ");
                Console.WriteLine("2 : Mes commandes ");
                Console.WriteLine("3 : Mes recettes  ");
                Console.WriteLine("4 : Recharger mes cooks ");
                Console.Write("\n Veuillez saisir votre choix : ");
                int saisie = Int32.Parse(Console.ReadLine());
                switch (saisie)
                {
                    case 0:

                        MenuPrincipal(cooking, nature, client, cdr);
                        break;
                    case 1:
                        // RECAPITULATIF INFORMATIONS
                        Console.WriteLine("\n MES INFORMATIONS : \n");
                        cdr.Affichage();
                        // CHOIX DES MODIFFICATIONS 
                        Console.WriteLine("0 : Revenir à la gestion de mon compte ");
                        Console.WriteLine("1 : Modifier mon pseudo");
                        Console.WriteLine("2 : Modifier mon mot de passe");
                        Console.WriteLine("3 : Modifer mon numero de telephone");

                        Console.WriteLine("\n Veuillez saisir votre choix :");
                        int saisie1 = Int32.Parse(Console.ReadLine());
                        switch (saisie1)
                        {
                            case 0:
                                GestionCompte(cooking, nature, client, cdr);
                                break;
                            case 1:
                                Console.WriteLine("Entrez votre nouveau pseudo : ");
                                string username = Console.ReadLine();
                                client.Username = cooking.ChangezUsernameBDD("client", client.Username, username);
                                cdr.Username = cooking.ChangezUsernameBDD("cdr", client.Username, username);
                                Console.WriteLine("\nLE CHANGEMENT A ETE REALISE AVEC SUCCES !\n");
                                GestionCompte(cooking, nature, client, cdr);
                                break;
                            case 2:
                                Console.WriteLine("Entrez votre ancien mot de passe : ");
                                string ancienMdp = Console.ReadLine();
                                while (cooking.Connexion("client", client.Username, ancienMdp) == false)
                                {
                                    Console.WriteLine("\n Le mot de passe entré n'est pas le bon veuillez reessayer :");
                                    ancienMdp = Console.ReadLine();
                                }
                                Console.WriteLine("Entrez votre nouveau mot de passe :");
                                string mdp = Console.ReadLine();
                                Console.WriteLine("Confirmez votre mot de passe : ");
                                string mdp2 = Console.ReadLine();
                                while (mdp != mdp2)
                                {
                                    Console.WriteLine("\n les mots de passes entrés ne correspondent pas \n ");
                                    Console.WriteLine("Entrez votre nouveau mot de passe :");
                                    mdp = Console.ReadLine();
                                    Console.WriteLine("Confirmez votre mot de passe : ");
                                    mdp2 = Console.ReadLine();
                                }
                                client.Password = cooking.ChangezMDPBDD("client", client.Username, ancienMdp, mdp);
                                cdr.Password = cooking.ChangezMDPBDD("cdr", client.Username, ancienMdp, mdp);
                                Console.WriteLine("\nLE CHANGEMENT A ETE REALISE AVEC SUCCES !\n");
                                GestionCompte(cooking, nature, client, cdr);

                                break;
                            case 3:
                                Console.WriteLine("Entrez votre nouveau numéro de téléphone: ");
                                string numTel = Console.ReadLine();
                                client.NumeroTel = cooking.ChangezNumTelBDD("client", client.NumeroTel, numTel);
                                cdr.NumeroTel = cooking.ChangezNumTelBDD("cdr", client.NumeroTel, numTel);
                                Console.WriteLine("\nLE CHANGEMENT A ETE REALISE AVEC SUCCES !\n");
                                GestionCompte(cooking, nature, client, cdr);
                                break;


                        }
                        break;
                    case 2: //MES COMMANDES 
                        break;
                    case 3:
                        List < Plat >plat = cooking.PlatsCreesCdr(cdr);
                        cdr.ListeRecettes = plat;
                        Console.WriteLine("\nMES RECETTES \n");
                        foreach(Plat p in plat)
                        {
                            p.AffichageCdr();
                        }
                        Console.WriteLine("\n Appuyez sur une touche pour revenir à la gestion de mon compte ");
                        string l = Console.ReadLine();
                        GestionCompte(cooking, nature, client, cdr);
                        break;
                    case 4:
                        string code = "";
                        do
                        {
                            Console.WriteLine("veuillez entrer votre code bancaire ");
                            code = Console.ReadLine();
                        }
                        while (cooking.CodeExiste(code, nature)==false);
                        Console.WriteLine("\nVeuillez saisir le montant en cooks que vous souahitez ajouter\n");
                        int cooks = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Cela vous fera " + Convert.ToString(0.7 * cooks) + " € ");
                        Console.WriteLine("Confirmez vous la transaction ?\n 1:OUI\n 2:NON");
                        int choix = Convert.ToInt32(Console.ReadLine());
                        switch (choix)
                        {
                            case 1:
                                cooking.RechargerCooks(Convert.ToString(client.IdClient), cooks, "cdr");
                                Console.WriteLine("\nVOTRE SOLDE A ETE RECHARGE !\n");
                                GestionCompte(cooking, nature, client, cdr);
                                break;
                            case 2:
                                GestionCompte(cooking, nature, client, cdr);
                                break;
                        }

                        break;
                }
            }
        }

       
        //COMMANDER CLIENT
        static void Commander(BDD cooking, string nature,Client client,Cdr cdr)
        {
            if (nature == "client")
            {
                List<Plat> plats = client.SelectionPlats(cooking);
                Console.WriteLine("Adresse de livraison : ");
                string adresse = Console.ReadLine();
                DateTime date = DateTime.Now;
                Commande commande = new Commande(0, adresse, date, cooking, plats);
                commande.Prix = commande.GetPrix();
                Console.WriteLine("\nPRIX TTC DE VOTRE COMMANDE : " + Convert.ToString(commande.GetPrix()) + " cooks\n");
                Console.WriteLine("\nVous avez actuellement " + Convert.ToString(client.Cooks) + " cooks dans votre compte");
                if (cooking.PayementPossible(commande, nature, client, cdr))
                {
                    
                    Console.WriteLine("\nLe payement est possible !\n");
                }
                else
                {
                    Console.WriteLine("Vous n'avez pas assez de cooks dans votre compte, veuillez annuler votre commande.");

                }
                Console.WriteLine("\n0 : CONFIRMER MA COMMANDE ");
                Console.WriteLine("1 : ANNULEZ MA COMMANDE ");
                Console.WriteLine("\nVeuillez saisir votre choix :");
                int choix = Convert.ToInt32(Console.ReadLine());
                switch (choix)
                {
                    case 0:
                        cooking.Payement(commande, nature, client, cdr);
                        Console.WriteLine("\nVOICI UN RECAPITULATIF DE VOTRE COMMANDE : ");
                        commande.InfoCommande();             // NE MARCHE PAS 
                        cooking.CommandeToBdd(commande, nature, client, cdr);
                        foreach(Plat p in plats)
                        {
                            cooking.ActualisationStock(Convert.ToString(p.IdPlat), 1);
                        }
                        //ACTUALISATION PRIX RECETTE
                        cooking.ActualisationPrixRecette(commande);
                        //REMUNERATION RECETTE
                        cooking.RemunerationCdr(commande);
                        //ACTUALISATION DATE UTILISATION 
                        cooking.ActualisationDate(commande);
                        Console.WriteLine("\nVOTRE COMMANDE A ETE PRISE EN COMPTE PAR NOS CUISINIERS, L'AVANCEMENT DE LA COMMANDE VOUS SERA COMMUNIQUE PAR SMS ! ");
                        MenuPrincipal(cooking, nature, client, cdr);
                        break;
                    case 1:
                        Console.WriteLine("\nVOTRE COMMANDE A ETE ANNULE AVEC SUCCES !");  
                        MenuPrincipal(cooking, nature, client, cdr);
                        break;

                }

            }
            else
            {
                List<Plat> plats = cdr.SelectionPlats(cooking);
                Console.WriteLine("Adresse de livraison : ");
                string adresse = Console.ReadLine();
                DateTime date = DateTime.Now;
                Commande commande = new Commande(0, adresse, date, cooking, plats);
                commande.Prix = commande.GetPrix();
                Console.WriteLine("\nPRIX TTC DE VOTRE COMMANDE : " + Convert.ToString(commande.GetPrix()) + " cooks\n");
                Console.WriteLine("\nVous avez actuellement " + Convert.ToString(cdr.Cooks) + " cooks dans votre compte");
                if (cooking.PayementPossible(commande, nature, client, cdr))
                {
                   
                    Console.WriteLine("\nLe payement est possible !\n");
                }
                else
                {
                    Console.WriteLine("Vous n'avez pas assez de cooks dans votre compte, veuillez annuler votre commande.");
                }
                Console.WriteLine("\n0 : CONFIRMER MA COMMANDE ");
                Console.WriteLine("1 : ANNULEZ MA COMMANDE ");
                Console.WriteLine("\nVeuillez saisir votre choix :");
                int choix = Convert.ToInt32(Console.ReadLine());
                switch (choix)
                {
                    case 0:
                        //PAYEMENT EN TANT QUE CDR
                        cooking.Payement(commande, nature, client, cdr);
                        //PAYEMENT EN TANT QUE CLIENT

                        Console.WriteLine("\nVOICI UN RECAPITULATIF DE VOTRE COMMANDE : ");
                        commande.InfoCommande();             // NE MARCHE PAS 
                        cooking.CommandeToBdd(commande, nature, client, cdr);
                        foreach (Plat p in plats)
                        {
                            cooking.ActualisationStock(Convert.ToString(p.IdPlat), 1);
                        }
                        // ACTUALISATION PRIX RECETTE
                        cooking.ActualisationPrixRecette(commande);
                        //REMUNERATION DU CDR
                        cooking.RemunerationCdr(commande);
                        //ACTUALISATION DATE
                        cooking.ActualisationDate(commande);
                        Console.WriteLine("\nVOTRE COMMANDE A ETE PRISE EN COMPTE PAR NOS CUISINIERS, L'AVANCEMENT DE LA COMMANDE VOUS SERA COMMUNIQUE PAR SMS ! ");
                        MenuPrincipal(cooking, nature, client, cdr);
                        break;
                    case 1:
                        Console.WriteLine("\nVOTRE COMMANDE A ETE ANNULE AVEC SUCCES !");  //METTRE UN STRING DANS UN FICHIER XML POUR RAISON D'ANNULATION
                        MenuPrincipal(cooking, nature, client, cdr);
                        break;

                }

            }


        }

        //CREATION RECETTE CLIENT
        static void CreationRecette(BDD cooking, string nature, Client client,Cdr cdr)
        {
            List<Produit> ingredients = new List<Produit>();
            List<double> quantite = new List<double>();
            List<Produit> toutlesproduits = cooking.GetProduits();

            Console.WriteLine("\n--------CREATION RECETTE--------\n");

            Console.WriteLine("Nom de votre plat : ");
            string nomRecette = Console.ReadLine();
            Console.WriteLine("Veuillez saisir votre recette avec une description simple : (texte limité à 256 caractères) ");
            string recette = Console.ReadLine();    // ARRETEZ A 256 CARACTERE METHODE ?
            Console.WriteLine("Décrivez le plat en 1 phrase :");
            string description = Console.ReadLine();
            Console.WriteLine("Proposez nous un prix à titre indicatif :");
            int prix = Convert.ToInt32(Console.ReadLine());
            Plat plat = new Plat(nomRecette, recette, description, prix, cooking);

            int choix = 0;
            while (choix != 3)
            {
                Console.WriteLine("1 : Rechercher un ingredient \n2 : Parcourir la liste des ingrédients \n3 : Valider les ingredients\n");
                Console.WriteLine(" Veuillez saisir votre choix ");

                choix = Convert.ToInt32(Console.ReadLine());
                switch (choix)
                {
                    case 1:
                        int saisie = 0;
                        while (saisie == 0)
                        {
                            Console.WriteLine("Tapez le nom du produit que vous recherchez: \n");
                            string selection1 = Console.ReadLine();
                            List<Produit> RechercheProduit = cooking.RechercherProduit(selection1);
                            foreach (Produit p in RechercheProduit)
                            {
                                p.AffichageCreateur();
                                Console.WriteLine("__________________");
                            }
                            Console.WriteLine("0 : Recommencer ma recherche  \n1 : Saisir l'ID du produit que je souhaite ajouter à ma recette ");
                            saisie = Convert.ToInt32(Console.ReadLine());
                        }
                        Console.WriteLine("ID :");
                        string id = Console.ReadLine();
                        Console.WriteLine("Quantité : ");
                        double quantiteSouhaite = Double.Parse(Console.ReadLine());

                        foreach (Produit p in toutlesproduits)
                        {
                            if (Convert.ToString(p.IdProduit) == id)
                            {
                                ingredients.Add(p);
                            }
                        }
                        quantite.Add(quantiteSouhaite);

                        Console.WriteLine("\nVOTRE PLAT A ETE AJOUTE AVEC SUCCES !\n");
                        break;

                    case 2:
                        Console.WriteLine(" Voici les produits disponibles");


                        foreach (Produit p in toutlesproduits)
                        {
                            p.AffichageCreateur();

                        }
                        Console.WriteLine("Veuillez saisir l'ID du produit choisi ainsi que sa quantité ");
                        Console.WriteLine("ID : ");
                        string id1 = Console.ReadLine();
                        Console.WriteLine("Quantité : ");
                        double quantiteSouhaite1 = Double.Parse(Console.ReadLine());
                        foreach (Produit p in toutlesproduits)
                        {
                            if (Convert.ToString(p.IdProduit) == id1)
                            {
                                ingredients.Add(p);
                            }
                        }
                        quantite.Add(quantiteSouhaite1);
                        break;
                    case 3:
                        break;
                }
                
            }

            // CLIENT DEVIENT CDR

            
            if (nature == "client")
            {
                cdr = new Cdr(client.IdClient, client.Nom, client.Prenom, client.Username, client.NumeroTel, client.Password, client.Cooks, client.CodeBancaire, cooking);

                cooking.AddCdrToBDD(cdr,client);
                nature = "cdr";
                
                cooking.CreationRecette(plat, ingredients, quantite, cdr);
                Console.WriteLine("\n\n\n\n\n\nVEUILLEZ VOUS RECONNECTER EN TANT QUE CREATEUR DE RECETTE\n\n\n\n\n\n ");
                Console.WriteLine("\nBRAVO VOUS ETES MAINTENANT UN DE NOS CREATEURS DE RECETTES !\nVOUS SEREZ REMUNERE DE 2 COOKS A CHAQUE COMMANDE DE VOTRE RECETTE !\nSI CELLE CI DEPASSE LES 50 COMMANDES ALORS VOTRE REMUNERATION PASSERA A 4\n");
                Connexion(cooking);
            }
            else
            {
                //CREATION RECETTE BDD
                cooking.CreationRecette(plat, ingredients, quantite, cdr);
                MenuPrincipal(cooking, nature, client, cdr);
            }
           


            




            

        }
        #endregion

        #region MENU CDR

        //CREATION RECETTE POUR UN CDR
        static void CreationRecetteCdr(BDD cooking, string nature, Client client, Cdr cdr)
        {
            List<Produit> ingredients = new List<Produit>();
            List<double> quantite = new List<double>();
            List<Produit> toutlesproduits = cooking.GetProduits();

            Console.WriteLine("\n--------CREATION RECETTE--------\n");

            Console.WriteLine("Nom de votre plat : ");
            string nomRecette = Console.ReadLine();
            Console.WriteLine("Veuillez saisir votre recette avec une description simple : (texte limité à 256 caractères) ");
            string recette = Console.ReadLine();    
            Console.WriteLine("Décrivez le plat en 1 phrase :");
            string description = Console.ReadLine();
            Console.WriteLine("Proposez nous un prix à titre indicatif :");
            int prix = Convert.ToInt32(Console.ReadLine());
            Plat plat = new Plat(nomRecette, recette, description, prix, cooking);

            int choix = 0;
            while (choix != 3)
            {
                Console.WriteLine("1 : Rechercher un ingredient \n2 : Parcourir la liste des ingrédients \n3 : Valider les ingredients\n");
                Console.WriteLine(" Veuillez saisir votre choix : ");
                choix = Convert.ToInt32(Console.ReadLine());
                switch (choix)
                {
                    case 1:
                        int saisie = 0;
                        while (saisie == 0)
                        {
                            Console.WriteLine("Tapez le nom du produit que vous recherchez: \n");
                            string selection1 = Console.ReadLine();
                            List<Produit> RechercheProduit = cooking.RechercherProduit(selection1);
                            foreach (Produit p in RechercheProduit)
                            {
                                p.AffichageCreateur();
                                Console.WriteLine("__________________");
                            }
                            Console.WriteLine("0 : Recommencer ma recherche  \n1 : Saisir l'ID du produit que je souhaite ajouter à ma recette ");
                            saisie = Convert.ToInt32(Console.ReadLine());
                        }
                        Console.WriteLine("ID :");
                        string id = Console.ReadLine();
                        Console.WriteLine("Quantité : ");
                        double quantiteSouhaite = Double.Parse(Console.ReadLine());

                        foreach (Produit p in toutlesproduits)
                        {
                            if (Convert.ToString(p.IdProduit) == id)
                            {
                                ingredients.Add(p);
                            }
                        }
                        quantite.Add(quantiteSouhaite);

                        Console.WriteLine("\nVOTRE PLAT A ETE AJOUTE AVEC SUCCES !\n");
                        break;

                    case 2:
                        Console.WriteLine(" Voici les produits disponibles");


                        foreach (Produit p in toutlesproduits)
                        {
                            p.AffichageCreateur();

                        }
                        Console.WriteLine("Veuillez saisir l'ID du produit choisi ainsi que sa quantité ");
                        Console.WriteLine("ID : ");
                        string id1 = Console.ReadLine();
                        Console.WriteLine("Quantité : ");
                        double quantiteSouhaite1 = Double.Parse(Console.ReadLine());
                        foreach (Produit p in toutlesproduits)
                        {
                            if (Convert.ToString(p.IdProduit) == id1)
                            {
                                ingredients.Add(p);
                            }
                        }
                        quantite.Add(quantiteSouhaite1);
                        break;
                    case 3:
                        break;
                }

            }

            


            //CREATION RECETTE BDD
            cooking.CreationRecette(plat, ingredients, quantite, cdr);


            Console.WriteLine("\n\nVOUS SEREZ REMUNERE DE 2 COOKS A CHAQUE COMMANDE DE VOTRE RECETTE !\nSI CELLE CI DEPASSE LES 50 COMMANDES ALORS VOTRE REMUNERATION PASSERA A 4\n");
            MenuPrincipal(cooking, nature, client, cdr);

        }

        #endregion

        #region MENU ADMIN

        static void GestionClientAdmin(BDD cooking, string nature, Client client, Cdr cdr)
        {
            Console.WriteLine("");
            Console.WriteLine("0 : Supprimer une recette ");
            Console.WriteLine("1 : Supprimer un créateur de recette ainsi que toute ses recettes");
            Console.WriteLine("\n2 : Revenir au menu principal ");
            Console.Write("\n Veuillez saisir votre choix : ");
            int saisie = Int32.Parse(Console.ReadLine());
            switch (saisie)
            {
                case 0:
                    List<Plat> recettes = cooking.GetPlats();
                    Console.WriteLine("\n0 : Rechercher le plat à supprimer ");
                    Console.WriteLine("1 : Tout les plats ");
                    Console.WriteLine("2 : Revenir au menu précédant");
                    Console.Write("\n Veuillez saisir votre choix : ");
                    int saisie1 = Int32.Parse(Console.ReadLine());
                    switch (saisie1)
                    {
                        case 0:
                            int saisie2 = 0;
                            while (saisie2 == 0)
                            {
                                Console.WriteLine("Tapez le nom du plat que vous recherchez: \n");
                                string selection1 = Console.ReadLine();
                                List<Plat> RecherchePLat = cooking.RechercherPlat(selection1);
                                foreach (Plat p in RecherchePLat)
                                {
                                    p.AffichageClient();
                                    Console.WriteLine("__________________");
                                }
                                Console.WriteLine("0 : Recommencer ma recherche  \n1 : Saisir l'ID du plat que je souhaite supprimer \n");
                                saisie2 = Convert.ToInt32(Console.ReadLine());
                            }
                            Console.WriteLine("\nID :");
                            string id = Console.ReadLine();

                            //SUPRESSION DU PLAT DE LA BDD
                            cooking.DeletePlatFromBDD(id);

                            Console.WriteLine("\nCe plat a bien été supprimé.");
                            GestionClientAdmin(cooking, nature, client, cdr);
                            break;
                        case 1:
                            Console.WriteLine("\nVoici les plats :\n ");
                            List<Plat> toutlesplats = cooking.GetPlats();

                            foreach (Plat p in toutlesplats)
                            {
                                p.AffichageClient();

                            }
                            Console.WriteLine("Veuillez saisir l'ID du plat que vous souhaitez supprimer");
                            Console.WriteLine("ID : ");
                            string id2 = Console.ReadLine();
                            //SUPRESSION DU PLAT DE LA BDD
                            cooking.DeletePlatFromBDD(id2);

                            Console.WriteLine("\nCe plat a bien été supprimé.");
                            GestionClientAdmin(cooking, nature, client, cdr);
                            break;
                        case 2:
                            GestionClientAdmin(cooking, nature, client, cdr);
                            break;

                    }
                    break;
                case 1:
                    List<Cdr> cdrS = cooking.GetCdr();
                    foreach (Cdr c in cdrS)
                    {
                        c.AffichageAdmin(cooking, c);
                    }
                    Console.WriteLine("\nVeuillez entrez le,le prénom et l'Id du Cdr que vous souhaitez supprimer\n");
                    Console.WriteLine("Nom :");
                    string nom = Console.ReadLine();
                    Console.WriteLine("Prenom : ");
                    string prenom = Console.ReadLine();
                    Console.WriteLine("Id : ");
                    string id1 = Console.ReadLine();

                    //SUPRESSION DE SES RECETTES
                    List<Plat> recetteCdr = cooking.PlatsCreesCdr(id1);
                    foreach (Plat p in recetteCdr)
                    {
                        cooking.DeletePlatFromBDD(Convert.ToString(p.IdPlat));
                    }

                    //SUPRESSION CDR

                    cooking.DeleteCdrFromBDD(nom, prenom, id1);
                    Console.WriteLine("\nLe Cdr choisi a été supprimé\n");

                    Console.WriteLine("Voulez vous que le Cdr choisi reste tout de même un client ? (oui/non)");
                    string choix = Console.ReadLine();
                    if (choix == "non")
                    {
                        //SUPPRESSION CLIENT 
                        cooking.DeleteClientFromBDD(nom, prenom);
                        Console.WriteLine("Le cdr a été aussi supprimé des clients ");
                    }
                    GestionClientAdmin(cooking, nature, client, cdr);

                    break;
                case 2:
                    MenuPrincipal(cooking, nature, client, cdr);
                    break;

            }

        }

       //PRODUITS A COMMANDER EN XML
        static void XML(BDD cooking)
        {

            // ON METS DANS UNE LISTE TOUT PRODUITS AVEC LE STOCK NUL
            List<Produit> produits = cooking.ProduitsStockNul();

            //ON IMPLEMENTE LES PARAMETRE DE NOTRE FICHIER XML AVEC UN XMLWRITER
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("    ");
            settings.CloseOutput = true;
            settings.OmitXmlDeclaration = true;
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            
            //EDITION DU FICHIER "produits.xml" ---> DEBUG
            using (XmlWriter writer = XmlWriter.Create("produits.xml",settings))
            {
                //LA COMMANDE A FAIRE
                writer.WriteStartElement("Commande");
                foreach (Produit p in produits)    // POUR CHAQUE PRODUIT ON ENTRE SES INFORMATIONS
                {
                    writer.WriteStartElement("Produit");
                    writer.WriteElementString("Nom", p.Nom);
                    writer.WriteElementString("Fournisseur",p.NomFournisseur) ;
                    writer.WriteElementString("Quantité",Convert.ToString(p.StockMax));
                    writer.WriteElementString("Unité", p.Unite);

                    writer.WriteEndElement();
                    writer.Flush();

                }
                writer.WriteEndElement();
                writer.Flush();


            }

           
            Console.WriteLine("\nFICHIER XML CREE\n");
        }

        //TABLEAU DE BORD
        static public void TableauDeBord(BDD bdd,string nature,Client client, Cdr cdr)
        {
            Cdr GoldenCdr = bdd.GoldenCdr();
            Plat plat = bdd.GoldenRecette();
            List<Plat> Top5 = bdd.Top5Recettes();
            MenuPrincipal(bdd, nature, client, cdr);
        }

        //FONCTION DEMO POUR L'EXAMINATEUR
        static public void Demo(BDD bdd)
        {
            Console.WriteLine("La base de données contient actuellement "+ bdd.NbClients()+ " clients");
            Console.WriteLine("\nVeuillez appuyez sur entrer pour continuer\n ");
            string transition = Console.ReadLine();
            Console.WriteLine("Voici la liste des Cdr de la base de données :");
            bdd.AfficherCdr();
            Console.WriteLine("\nVeuillez appuyez sur entrer pour continuer\n ");
            string transition1 = Console.ReadLine();
            Console.WriteLine("La base de données contient actuellement " + bdd.NbPlats() + "  recettes");
            Console.WriteLine("\nVeuillez appuyez sur entrer pour continuer\n ");
            string transition3 = Console.ReadLine();
            Console.WriteLine("Liste des produits ayant une quantité en stock <= 2 * leur quantité minimale :\n");
            Console.WriteLine("   id  |  nom  | categorie|unite| Stock Min|Stock Max|Stock Actue|Nom du Fournisseur|Reference fournisseur|");
            bdd.ProduitsPetitStock();
            Console.WriteLine("Veuillez saisir l'Id du produit choisi, ainsi la liste des recettes qu'il compose");
            
            int choix = 0;
            do
            {
                string id = Console.ReadLine();
                bdd.RecetteDuProduit(id);
                Console.WriteLine("\n0 : Saisir un autre Id\n1 : Continuer\n");
                choix = Convert.ToInt32(Console.ReadLine());
            }
            while (choix == 0) ;





        }
        #endregion

        static void Main(string[] args)
        {
            //NOM DE CONSOLE -> COOKING
            Console.Title = "COOKING";


            // SI PROBLEME DANS LES DATES ET QUANTITE ! 
            // SELON SA VERSION DE VISUAL US/FR POUR LES PROBLEMES D'ENTREE DES NOMBRES A VIRGULES ET DU FORMAT DES DATETIME
            //CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");


            //CONNEXION A MYSQL

            Console.WriteLine("Bonjour cher utilisateur, veuillez saisir votre motde passe MySql");
            string mdp = Console.ReadLine();

            //ON INSTANCIE LA BDD
            BDD cooking = new BDD("cooking",mdp);
           
            //FONCTION POUR L'EXAMINATEUR 
            //Demo(cooking);

            //LANCEMENT DU PROCESSUS DE CONNEXION AU SITE
            Connexion(cooking);

            Console.ReadKey();
        }
    }
}
