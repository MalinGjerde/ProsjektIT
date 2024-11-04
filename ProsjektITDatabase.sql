-- SCHEMA: ProsjektIT

-- DROP SCHEMA IF EXISTS "ProsjektIT" ;

CREATE SCHEMA IF NOT EXISTS "ProsjektIT"
    AUTHORIZATION h599034;

	CREATE TABLE Bruker (
    Etternamn VARCHAR(100) NOT NULL,
    Fornamn VARCHAR(100) NOT NULL,
    Epost VARCHAR(255) NOT NULL UNIQUE,
    KortID CHAR(4) NOT NULL CHECK (KortID ~ '^[0-9]{4}$')
);

	CREATE TABLE Kortleser (
	Nummer CHAR(4) NOT NULL CHECK (KortID ~ '^[0-9]{4}$'),
	Plassering INT(4) NOT NULL
	);

	CREATE TABLE Innpass (

	);

	CREATE TABLE ALARM (

	);
	