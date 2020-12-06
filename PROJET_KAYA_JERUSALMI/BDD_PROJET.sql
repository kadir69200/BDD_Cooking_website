use cooking;
DROP TABLE IF EXISTS Client;
DROP TABLE IF EXISTS Cdr;
DROP TABLE IF EXISTS Plat;
DROP TABLE IF EXISTS fournisseur;
DROP TABLE IF EXISTS contient;
DROP TABLE IF EXISTS produit;
DROP TABLE IF EXISTS Commande ;
DROP TABLE IF EXISTS Inventé_par ;
DROP TABLE IF EXISTS Composé_de ;
DROP TABLE IF EXISTS Fournit_par ;
DROP TABLE IF EXISTS Passe ;


CREATE TABLE Client (IdClient INT(20) AUTO_INCREMENT NOT NULL,
Nom VARCHAR(20),
Prenom VARCHAR(20),
username VARCHAR(20),
NumeroTel VARCHAR(20),
password VARCHAR(20),
Cooks INT(20),
Admin varchar(20),
CodeBancaire VARCHAR(20),
PRIMARY KEY (IdClient)) ENGINE=InnoDB;


CREATE TABLE CDR (IdCDR INT(20) AUTO_INCREMENT NOT NULL,
IdClient INT(20),
Nom VARCHAR(20),
Prenom VARCHAR(20),
username VARCHAR(20),
password VARCHAR(20),
NumeroTel VARCHAR(20),
Cooks INT(20),
CodeBancaire VARCHAR(20),

PRIMARY KEY (IdCDR)) ENGINE=InnoDB;


CREATE TABLE Plat (IdPlat INT AUTO_INCREMENT NOT NULL,
Nom varchar(20),
Recette VARCHAR(1000),
description VARCHAR(500),
prix INT(20),
prix_cdr INT(20),
creation_date varchar(50),
compteur INT(20),
PRIMARY KEY (IdPlat)) ENGINE=InnoDB;


CREATE TABLE Produit (IdProduit INT(20) AUTO_INCREMENT NOT NULL,
Nom VARCHAR(50),
Categorie VARCHAR(20),
Unite varchar(20),
StockMin double,
StockMax double,
StockActuel double,
NomFournisseur varchar(20),
RefFournisseur varchar(20),
DerniereUtilisation varchar(20),

PRIMARY KEY (IdProduit)) ENGINE=InnoDB;


CREATE TABLE Fournisseur (Reference INT(20) AUTO_INCREMENT NOT NULL,
Nom VARCHAR(20),
NumeroTel VARCHAR(20),
PRIMARY KEY (Reference)) ENGINE=InnoDB;


CREATE TABLE Commande (IdCommande INT(20) AUTO_INCREMENT NOT NULL,
prix int(5),
adresse VARCHAR(50),
date VARCHAR(50),
PRIMARY KEY (IdCommande)) ENGINE=InnoDB;


CREATE TABLE Contient (IdCommande INT(20) AUTO_INCREMENT NOT NULL,
IdPlat INT NOT NULL,
quantite INT(10),
PRIMARY KEY (IdCommande,
 IdPlat)) ENGINE=InnoDB;


CREATE TABLE Inventé_par (IdPlat INT AUTO_INCREMENT NOT NULL,
IdCDR INT(20) NOT NULL,
PRIMARY KEY (IdPlat,
 IdCDR)) ENGINE=InnoDB;


CREATE TABLE Composé_de (IdPlat INT AUTO_INCREMENT NOT NULL,
IdProduit INT(20) NOT NULL,
quantite float(10),
PRIMARY KEY (IdPlat,
 IdProduit)) ENGINE=InnoDB;


CREATE TABLE Fournit_par (IdProduit INT(20) AUTO_INCREMENT NOT NULL,
Reference INT(20) NOT NULL,
PRIMARY KEY (IdProduit,
 Reference)) ENGINE=InnoDB;


CREATE TABLE Passe (IdClient INT(20) AUTO_INCREMENT NOT NULL,
IdCommande INT(20) NOT NULL,
PRIMARY KEY (IdClient,
 IdCommande)) ENGINE=InnoDB;

ALTER TABLE Contient ADD CONSTRAINT FK_Contient_IdCommande FOREIGN KEY (IdCommande) REFERENCES Commande (IdCommande);

ALTER TABLE Contient ADD CONSTRAINT FK_Contient_IdPlat FOREIGN KEY (IdPlat) REFERENCES Plat (IdPlat);
ALTER TABLE Inventé_par ADD CONSTRAINT FK_Inventé_par_IdPlat FOREIGN KEY (IdPlat) REFERENCES Plat (IdPlat);
ALTER TABLE Inventé_par ADD CONSTRAINT FK_Inventé_par_IdCDR FOREIGN KEY (IdCDR) REFERENCES CDR (IdCDR);
ALTER TABLE Composé_de ADD CONSTRAINT FK_Composé_de_IdPlat FOREIGN KEY (IdPlat) REFERENCES Plat (IdPlat);
ALTER TABLE Composé_de ADD CONSTRAINT FK_Composé_de_IdProduit FOREIGN KEY (IdProduit) REFERENCES Produit (IdProduit);
ALTER TABLE Fournit_par ADD CONSTRAINT FK_Fournit_par_IdProduit FOREIGN KEY (IdProduit) REFERENCES Produit (IdProduit);
ALTER TABLE Fournit_par ADD CONSTRAINT FK_Fournit_par_Reference FOREIGN KEY (Reference) REFERENCES Fournisseur (Reference);
ALTER TABLE Passe ADD CONSTRAINT FK_Passe_IdClient FOREIGN KEY (IdClient) REFERENCES Client (IdClient);
ALTER TABLE Passe ADD CONSTRAINT FK_Passe_IdCommande FOREIGN KEY (IdCommande) REFERENCES Commande (IdCommande);
