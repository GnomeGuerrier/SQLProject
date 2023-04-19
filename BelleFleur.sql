-- Active: 1680565722101@@127.0.0.1@3306@fleur
DROP DATABASE IF EXISTS FLEUR;
CREATE DATABASE IF NOT EXISTS FLEUR;
USE FLEUR;

set sql_safe_updates = 0 ;
SET GLOBAL LOCAL_INFILE = 'ON';

Drop TABLE IF EXISTS clients;
CREATE TABLE IF NOT EXISTS clients(
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
        FROM clients
        WHERE clients.courriel = courriel
    ) THEN
        INSERT INTO clients (
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







Drop TABLE IF EXISTS boncommande;
CREATE TABLE IF NOT EXISTS BonCommande(
    adresseLivraison VARCHAR(100),
    messageAcc VARCHAR(200),
    dateLivraison DATETIME,
    dateCreation DATETIME DEFAULT CURRENT_TIMESTAMP,
    CodeC VARCHAR(70),
    EtatCommande VARCHAR(70),
    PRIMARY KEY(codeC,dateCreation),
    FOREIGN KEY(CodeC) REFERENCES clients(courriel)
);
DROP TABLE IF EXISTS commande_standard;
CREATE TABLE IF NOT EXISTS commande_standard(
    nom VARCHAR(40),
    Compo_Fleur VARCHAR(300),
    prix INTEGER,
    categorie VARCHAR(40),
    PRIMARY KEY(nom)
    
);
INSERT INTO `fleur`.`commande_standard` (`nom`,`Compo_Fleur`,`prix`,`categorie`)VALUES ('Gros Merci','Arrangement floral avec marguerites et verdure',45,'toute occasion');
INSERT INTO `fleur`.`commande_standard` (`nom`,`Compo_Fleur`,`prix`,`categorie`)VALUES('L amoureux','Arrangement floral avec roses blanches et roses rouges',65,'Saint Valentin');
INSERT INTO `fleur`.`commande_standard` (`nom`,`Compo_Fleur`,`prix`,`categorie`)VALUES('L exotique','Arrangement floral avec ginger,oiseaux du paradis, roses et genet',40,'Toute Occasion');
INSERT INTO `fleur`.`commande_standard` (`nom`,`Compo_Fleur`,`prix`,`categorie`)VALUES('Maman','Arrangement floral avec gerbera,roses blanches, lys et alstroméria',80,'Fête des mères');
INSERT INTO `fleur`.`commande_standard` (`nom`,`Compo_Fleur`,`prix`,`categorie`)VALUES('Vive la mariée','Arrangement floral avec lys et orchidées',109,'Mariage');






#CALL ajout_clients('test', 'test', 10, 'test@test.com', 'mdp', '34 rue', 101010);