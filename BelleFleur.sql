-- Active: 1680565722101@@127.0.0.1@3306@fleur
DROP DATABASE IF EXISTS FLEUR;
CREATE DATABASE IF NOT EXISTS FLEUR;
USE FLEUR;

set sql_safe_updates = 0 ;
SET GLOBAL LOCAL_INFILE = 'ON';

Drop TABLE IF EXISTS Clients;
CREATE TABLE IF NOT EXISTS Clients(
nom VARCHAR(40),
prenom VARCHAR(40),
num_tel INTEGER,
courriel VARCHAR(70),
mdp VARCHAR(30),
adresse_facturation VARCHAR(100),
carte_credit INTEGER,
nbBouquetMois INTEGER DEFAULT 0,
PRIMARY KEY(courriel,mdp)
);



DELIMITER //
CREATE PROCEDURE IF NOT EXISTS ajout_clients(
    IN nom VARCHAR(40),
    IN prenom VARCHAR(40),
    IN num_tel INTEGER,
    IN courriel VARCHAR(70),
    IN mdp VARCHAR(30),
    IN adresse_facturation VARCHAR(100),
    IN carte_credit INTEGER,
    OUT message VARCHAR(255)
)
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM Clients
        WHERE Clients.courriel = courriel
    ) THEN
        INSERT INTO Clients (
            nom,
            prenom,
            num_tel,
            courriel,
            mdp,
            adresse_facturation,
            carte_credit
        )
        VALUES (
            nom,
            prenom,
            num_tel,
            courriel,
            mdp,
            adresse_facturation,
            carte_credit
        );
        SET message = NULL;
    ELSE
        SET message = 'Le client existe déjà dans la base de données';
    END IF;
END //
DELIMITER ;


DROP TABLE IF EXISTS MAGASIN;
CREATE TABLE IF NOT EXISTS MAGASIN(
    nom VARCHAR(40) PRIMARY KEY,
    CA DECIMAL(5,2) DEFAULT 0
);


 DROP TABLE IF EXISTS STOCK;
 CREATE TABLE IF NOT EXISTS STOCK(
    IdMagasin VARCHAR (40),
    Gerbera INTEGER,
    Ginger INTEGER,
    Glaieul INTEGER,
    Marguerite INTEGER,
    Rose_rouge INTEGER,
    PRIMARY KEY (IdMagasin),
    FOREIGN KEY (IdMagasin) REFERENCES MAGASIN(nom)
 );



Drop TABLE IF EXISTS boncommande;
CREATE TABLE IF NOT EXISTS BonCommande(
    adresseLivraison VARCHAR(100),
    messageAcc VARCHAR(200),
    dateLivraison DATETIME,
    dateCreation DATETIME DEFAULT CURRENT_TIMESTAMP,
    CodeC VARCHAR(70),
    EtatCommande VARCHAR(70),
    CommandeStandard BOOLEAN,
    NomStandard VARCHAR(100),
    Personalisé VARCHAR(500), 
    prix DECIMAL(5,2),
    magasin VARCHAR(40),
    PRIMARY KEY(codeC,dateCreation),
    FOREIGN KEY(CodeC) REFERENCES Clients(courriel),
    FOREIGN KEY(magasin) REFERENCES MAGASIN(nom),
    FOREIGN KEY(NomStandard) REFERENCES commande_standard(nom)

);
DROP TABLE IF EXISTS commande_standard;
CREATE TABLE IF NOT EXISTS commande_standard(
    nom VARCHAR(40),
    Compo_Fleur VARCHAR(300),
    prix INTEGER,
    categorie VARCHAR(40),
    PRIMARY KEY(nom)
    
);

DROP TABLE IF EXISTS administrateurs;
CREATE TABLE IF NOT EXISTS administrateurs(
    IdAdmin INTEGER,
    pseudo VARCHAR(20),
    mot_de_passe VARCHAR(20),
    PRIMARY KEY(IdAdmin)
);
DROP TABLE IF EXISTS ACCESSOIRES;
CREATE TABLE IF NOT EXISTS ACCESSOIRES(
Accessoire VARCHAR(400) PRIMARY KEY,
Prix DECIMAL(5,2)
);

INSERT INTO `fleur`.`commande_standard` (`nom`,`Compo_Fleur`,`prix`,`categorie`)VALUES ('Gros Merci','Arrangement floral avec marguerites et verdure',45,'toute occasion');
INSERT INTO `fleur`.`commande_standard` (`nom`,`Compo_Fleur`,`prix`,`categorie`)VALUES('L amoureux','Arrangement floral avec roses blanches et roses rouges',65,'Saint Valentin');
INSERT INTO `fleur`.`commande_standard` (`nom`,`Compo_Fleur`,`prix`,`categorie`)VALUES('L exotique','Arrangement floral avec ginger,oiseaux du paradis, roses et genet',40,'Toute Occasion');
INSERT INTO `fleur`.`commande_standard` (`nom`,`Compo_Fleur`,`prix`,`categorie`)VALUES('Maman','Arrangement floral avec gerbera,roses blanches, lys et alstroméria',80,'Fête des mères');
INSERT INTO `fleur`.`commande_standard` (`nom`,`Compo_Fleur`,`prix`,`categorie`)VALUES('Vive la mariée','Arrangement floral avec lys et orchidées',109,'Mariage');

INSERT INTO magasin(nom) VALUES ('Paris');
INSERT INTO magasin(nom) VALUES ('Marseille');
INSERT INTO magasin(nom) VALUES ('Lyon');
INSERT INTO magasin(nom) VALUES ('Annecy');
INSERT INTO magasin(nom) VALUES ('Lille');

INSERT INTO `fleur`.`stock` (`IdMagasin`,`Gerbera`,`Ginger`,`Glaieul`,`Marguerite`,`Rose_rouge`)VALUES ('Paris',210,190,205,145,120);
INSERT INTO `fleur`.`stock` (`IdMagasin`,`Gerbera`,`Ginger`,`Glaieul`,`Marguerite`,`Rose_rouge`)VALUES ('Marseille',140,130,250,100,80);
INSERT INTO `fleur`.`stock` (`IdMagasin`,`Gerbera`,`Ginger`,`Glaieul`,`Marguerite`,`Rose_rouge`)VALUES ('Lyon',230,150,170,170,130);
INSERT INTO `fleur`.`stock` (`IdMagasin`,`Gerbera`,`Ginger`,`Glaieul`,`Marguerite`,`Rose_rouge`)VALUES ('Annecy',80,70,50,120,70);
INSERT INTO `fleur`.`stock` (`IdMagasin`,`Gerbera`,`Ginger`,`Glaieul`,`Marguerite`,`Rose_rouge`)VALUES ('Lille',140,170,210,130,100);




INSERT INTO `fleur`.`ACCESSOIRES` (`ACCESSOIRE`,`prix`) VALUES ('Vase',5);
INSERT INTO `fleur`.`ACCESSOIRES` (`ACCESSOIRE`,`prix`) VALUES ('Boite pour fleur',10);
INSERT INTO `fleur`.`ACCESSOIRES` (`ACCESSOIRE`,`prix`) VALUES ('Boite de chocolat',10);
INSERT INTO `fleur`.`ACCESSOIRES` (`ACCESSOIRE`,`prix`) VALUES ('decoraction papier maché',13);

SELECT * FROM Clients where courriel = '155555555555555555' and mdp= '1';

SELECT * FROM commande_standard;
CALL ajout_clients('test', 'test', 10, 'test@test.com', 'mdp', '34 rue', 101010);

INSERT INTO `fleur`.`boncommande`(`adresseLivraison`,`messageAcc`,`dateLivraison`,`CodeC`,`EtatCommande`,`CommandeStandard`,`NomStandard`)VALUES('ff','rr', '2004-01-01','1','ee',true,'GrosMerci');

INSERT INTO `fleur`.`boncommande`(`adresseLivraison`,`messageAcc`,`dateLivraison`,`CodeC`,`EtatCommande`,`CommandeStandard`,`NomStandard`)VALUES('1','deddededede','2000-01-01','1','',False,'Gros Merci');
INSERT INTO `fleur`.`boncommande`(`adresseLivraison`,`messageAcc`,`dateLivraison`,`CodeC`,`EtatCommande`,`CommandeStandard`,`NomStandard`)VALUES('1','frthyjukijvecz','2000-01-01','1','',False,'Gros Merci');

INSERT INTO `fleur`.`stock` (`IdMagasin`,`Gerbera`,`Ginger`,`Glaieul`,`Marguerite`,`Rose_rouge`)VALUES ('Paris',210,190,205,145,120);
INSERT INTO `fleur`.`stock` (`IdMagasin`,`Gerbera`,`Ginger`,`Glaieul`,`Marguerite`,`Rose_rouge`)VALUES ('Marseille',140,130,250,100,80);
INSERT INTO `fleur`.`stock` (`IdMagasin`,`Gerbera`,`Ginger`,`Glaieul`,`Marguerite`,`Rose_rouge`)VALUES ('Lyon',230,150,170,170,130);
INSERT INTO `fleur`.`stock` (`IdMagasin`,`Gerbera`,`Ginger`,`Glaieul`,`Marguerite`,`Rose_rouge`)VALUES ('Annecy',80,70,50,120,70);
INSERT INTO `fleur`.`stock` (`IdMagasin`,`Gerbera`,`Ginger`,`Glaieul`,`Marguerite`,`Rose_rouge`)VALUES ('Lille',140,170,210,130,100);

SELECT * FROM stock where IdMagasin='paris';
SELECT *
FROM clients
WHERE nbBouquetMois = (
    SELECT MAX(nbBouquetMois)
    FROM clients
);
Update stock SET gerbera = 10 where `IdMagasin` = 'Paris';
SELECT SUM(Price) AS TotalCA
FROM (
    SELECT BonCommande.Prix AS Price
    FROM BonCommande
    JOIN commande_standard ON BonCommande.NomStandard = commande_standard.nom
    WHERE CommandeStandard = TRUE
    UNION ALL
    SELECT BonCommande.prix AS Price
    FROM BonCommande
    WHERE CommandeStandard = FALSE
) AS AllBouquets;




CALL ajout_clients('Alice', 'Smith', 12345, 'alice.smith@example.com', 'alice123', '123 Main St', 1111);
CALL ajout_clients('Bob', 'Johnson', 23456, 'bob.johnson@example.com', 'bob123', '456 Oak St', 2222);
CALL ajout_clients('Carol', 'Williams', 34567, 'carol.williams@example.com', 'carol123', '789 Elm St', 3333);
CALL ajout_clients('David', 'Brown', 45678, 'david.brown@example.com', 'david123', '1012 Pine St', 4444);
CALL ajout_clients('Eva', 'Jones', 56789, 'eva.jones@example.com', 'eva123', '1314 Maple St', 5555);
CALL ajout_clients('Frank', 'Garcia', 67890, 'frank.garcia@example.com', 'frank123', '1618 Cedar St', 6666);
CALL ajout_clients('Grace', 'Miller', 7890, 'grace.miller@example.com', 'grace123', '2022 Birch St', 7777);
CALL ajout_clients('Hank', 'Davis', 8901, 'hank.davis@example.com', 'hank123', '2426 Chestnut St', 8888);
CALL ajout_clients('Iris', 'Rodriguez', 9012, 'iris.rodriguez@example.com', 'iris123', '2830 Willow St', 9999);
CALL ajout_clients('Jack', 'Martinez', 1234, 'jack.martinez@example.com', 'jack123', '3034 Spruce St', 1111);


INSERT INTO `fleur`.`boncommande`(`adresseLivraison`,`messageAcc`,`dateLivraison`,`CodeC`,`EtatCommande`,`CommandeStandard`,`NomStandard`,`prix`,`magasin`)
VALUES('123 Main St','Happy Birthday!','2023-05-01','alice.smith@example.com','Processing',1,'Bouquet 1',35.99,'Magasin 1');

INSERT INTO `fleur`.`boncommande`(`adresseLivraison`,`messageAcc`,`dateLivraison`,`CodeC`,`EtatCommande`,`CommandeStandard`,`NomStandard`,`prix`,`magasin`)
VALUES('456 Oak St','Congratulations!','2023-05-10','bob.johnson@example.com','Processing',1,'Bouquet 2',45.99,'Magasin 2');

INSERT INTO `fleur`.`boncommande`(`adresseLivraison`,`messageAcc`,`dateLivraison`,`CodeC`,`EtatCommande`,`CommandeStandard`,`NomStandard`,`prix`,`magasin`)
VALUES('789 Elm St','Happy Anniversary!','2023-05-15','carol.williams@example.com','Processing',1,'Bouquet 3',55.99,'Magasin 3');

INSERT INTO `fleur`.`boncommande`(`adresseLivraison`,`messageAcc`,`dateLivraison`,`CodeC`,`EtatCommande`,`CommandeStandard`,`Personalisé`,`prix`,`magasin`)
VALUES('246 Pine St','Get Well Soon!','2023-05-20','david.brown@example.com','Processing',0,'Roses, Lilies, and Orchids',50.99,'Magasin 4');

INSERT INTO `fleur`.`boncommande`(`adresseLivraison`,`messageAcc`,`dateLivraison`,`CodeC`,`EtatCommande`,`CommandeStandard`,`Personalisé`,`prix`,`magasin`)
VALUES('135 Maple St','Thank You!','2023-05-25','frank.garcia@example.com','Processing',0,'Sunflowers, Daisies, and Tulips',40.99,'Magasin 5');

INSERT INTO `fleur`.`boncommande`(`adresseLivraison`,`messageAcc`,`dateLivraison`,`CodeC`,`EtatCommande`,`CommandeStandard`,`Personalisé`,`prix`,`magasin`)
VALUES('864 Birch St','Thinking of You!','2023-05-30','grace.miller@example.com','Processing',0,'Carnations, Chrysanthemums, and Larkspur',45.99,'Magasin 6');

-- Add more INSERT statements for other clients, following the same pattern.


