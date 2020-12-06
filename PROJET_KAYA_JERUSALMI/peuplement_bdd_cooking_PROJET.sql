
-- PEUPLEMENT CLIENT

insert into client values (9900,'KAYA','Kadir','kadir.kaya','0601993277','turkiye68',0,'true',"0000000");
insert into client values (9800,'JERUSALMI','Kevin','kevin.jerusalmi','0605887654','keke99',0,'true',"000000");
insert into client values (9700,'DANIEL','Jean','daniel.jean','0689765434','chouchou',0,'false',"00000");
insert into client values (9600,'FERHAOUI','Yani','yani.ferhaoui','0789654323','hackeur95',0,'false',"00000");
insert into client values (9500,'HELALI','Nader','nader.helali','0690989988','nader44',1,'false',"0000");
insert into client values (1244,'NCIR','Walid','walid.ncir','0789876545','walid93',0,'false',"0000");
insert into client values (8767,'BOCUSE','Paul','paul.bocuse','0987899087',"PAUPAUL",1000,"false","123456686543");
insert into client values (9876,'NUSRET','Nusret','nusret',"094939292",'turkiye',1000,'false',"12398711230");
insert into client values (9976,'Dubois','jean paul','JP','004489767898',"JP99",1000,"false","123455677553");



-- PEUPLEMENT CREATEUR DE RECETTE

insert into cdr values (9876,8767,'BOCUSE','Paul','paul.bocuse','paul69latrick','0987899087',1000,"123456686543");
insert into cdr values (6789,9876,'NUSRET','Nusret','nusret','turkiye','004489767898',1000,"12398711230");
insert into cdr values (6989,9976,'Dubois','jean paul','JP','JP99','004489767898',1000,"12398711230");



-- PEUPLEMENT COMMANDE
insert into commande values(99,300, 'adresse','25/04/2020');
insert into commande values(88,200,'adresse','27/04/2020');
insert into commande values(35,340,'adresse','28/04/2020');
insert into commande values(61,100,'adresse','29/04/2020');
 

-- PEUPLEMENT PLAT 
insert into plat values (5543,'Pates au beurre','pate et beurre','Des pates et du beurre',10,0,'01/02/2020',0);
insert into plat values (7821,'Pate au saumon','pate et saumon frais','pate,saumon',40,0,'08/08/2020',0);
insert into plat values (6712,'Hamburger','cuire le steack, puis mettre dans le pain','Pain, steack, salade, onions',30,0,'04/12/2008',0);
insert into plat values (989,'soupe','vas voir sur internet','pleine de belle choses',100,0,'08/12/97',3);
insert into plat values (1234,'entrecote dorée','vas voir sur internet','une entrecote et de la poudre doree comestible',200,0,'12/03/2018',1);
-- PEUPLEMENT PRODUIT

-- dans l'optique que cooking est une start-up les stocks minimum sont égales à 0 car on se dit que l'on propose les plats jusqu'à que ce soit plus possible d'en préparer
insert into produit values (67, "mouton","viande/poisson/oeuf","kilo",0,500,100,"Fournisseur1","1234","09/05/2020");
insert into produit values (34,"Persil","légume","kilo",0,100,30,"Fournisseur1","1234","09/05/2020");
insert into produit values (7,"sauce tomate","autre","litre",0,300,10,"Fournisseur1","1234","09/05/2020");
insert into produit values (26,"pates","féculent","kilo",0,100,70,"Fournisseur1","1234","09/05/2020");
insert into produit values (45,"beurre","","kilo",0,50,14,"Fournisseur1","1234","09/05/2020");
insert into produit values (46,"tomate","fruit","kilo",0,500,356,"Fournisseur1","1234","09/05/2020");
insert into produit values (42,"comcombre","légume","kilo",0,500,316,"Fournisseur1","1234","09/05/2020");
insert into produit values (48,"onion","légume","kilo",0,720,278,"Fournisseur1","1234","09/05/2020");
insert into produit values (43,"poivron","légume","kilo",0,200,87,"Fournisseur1","1234","09/05/2020");
insert into produit values (54,"ail","légume","kilo",0,20,619,"Fournisseur1","1234","09/05/2020");
insert into produit values (234,"haricots","légume","kilo",0,50,23,"Fournisseur1","1234","09/05/2020");
insert into produit values (2354,"champignons","légume","kilo",0,200,136,"Fournisseur1","1234","09/05/2020");
insert into produit values (546,"riz","féculent","kilo",0,200,42,"Fournisseur1","1234","09/05/2020");
insert into produit values (23,"blé","féculent","kilo",0,20,38,"Fournisseur1","1234","09/05/2020");
insert into produit values (754,"oeuf","viande/poisson/oeuf","kilo",0,200,320,"Fournisseur1","1234","09/05/2020");
insert into produit values (544,"entrecote","viande/poisson/oeuf","kilo",0,100,20,"Fournisseur1","1234","09/05/2020");
insert into produit values (74,"oreilles de porc","viande/poisson/oeuf","kilo",0,5,3,"Fournisseur1","1234","09/05/2020");
insert into produit values (57,"pomme de terre","féculent","kilo",0,200,87,"Fournisseur1","1234","09/05/2020");
insert into produit values (125556,"orange","fruit","kilo",65,200,20,"Fournisseur1","1234","09/05/2020");
insert into produit values (1423,"banane","fruit","kilo",0,200,13,"Fournisseur1","1234","09/05/2020");
insert into produit values (13560,"sauce soja","autre","litre",0,200,20,"Fournisseur1","1234","09/05/2020");
insert into produit values (12326,"levure","autre","unite",6,10,5,"Fournisseur1","1234","09/05/2020");
insert into produit values (515,"chocolat","confiserie","kilo",0,10,5,"Fournisseur1","1234","09/05/2020");
insert into produit values (11116,"poivre","autre","kilo",0,20,20,"Fournisseur1","1234","09/05/2020");
insert into produit values (22344,"cumin","épices","kilo",10,10,1,"Fournisseur1","1234","09/05/2020");
insert into produit values (445669,"herbes de provences","épices","kilo",0,10,1,"Fournisseur1","1234","09/05/2020");
insert into produit values (298654,"cumin","épices","kilo",0,10,1,"Fournisseur1","1234","09/05/2020");
insert into produit values (47654345,"salade","légume","kilo",0,40,0,"Fournisseur1","1234","09/05/2020");
insert into produit values (213344,"cumin","épices","kilo",0,10,1,"Fournisseur1","1234","09/05/2020");
insert into produit values (409875,"citron","épices","kilo",0,10,0,"Fournisseur1","1234","09/05/2020");
insert into produit values (244554,"beurre","autre","kilo",10,10,6,"Fournisseur1","1234","09/05/2020");




-- PEUPLEMENT COMPOSE_DE
insert into composé_de values (5543,22344,0.5);
insert into composé_de values (5543,244554,0.05);
insert into composé_de values (6712,26,0.001);
insert into composé_de values (6712,47654345,0.1);
insert into composé_de values (989,47654345,0.001);
insert into composé_de values (1234,11116,0.250);
insert into composé_de values (7821,26,0.5);
-- finir le reste avec produit et recette

-- PEUPLEMENT PASSE
insert into passe values(9700,99);
insert into passe values(9700,88);
insert into passe values(9600,35);

-- PEUPLEMENT contient
 insert into contient values(99,989,1);
 insert into contient values(99,1234,1);
 insert into contient values(88,989,2);
 insert into contient values(35,989,1);
 insert into contient values(35,1234,1);
 insert into contient values(61,5543,1);
 
 insert into contient values(35,6712,1);
-- PEUPELEMENT INVENTE PAR 
insert into inventé_par values(989,9876);
insert into inventé_par values(1234,6789);
insert into inventé_par values(6712,6789);
insert into inventé_par values(5543,6989);
-- PEUPLEMENT FOURNISSEUR 
insert into fournisseur values(888,"METRO","0989786543");
insert into fournisseur values(2,"O'FRAIS","0989765467");
-- PEUPLEMENT FOURNIT PAR 
insert into fournit_par values (67,2);
insert into fournit_par values (67,888);
insert into fournit_par values (34,2);
insert into fournit_par values (7,888);
insert into fournit_par values (43,888);