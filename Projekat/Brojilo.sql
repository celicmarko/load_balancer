CREATE TABLE brojilo(
	 Id integer NOT NULL,
     Ime varchar(20) NOT NULL,
	 Prezime varchar(25) NOT NULL,
     Ulica varchar(40) NOT NULL,
     Broj integer NOT NULL,
     Grad varchar(25) NOT NULL,
     Post integer NOT NULL, 
     CONSTRAINT brojilo_PK PRIMARY KEY (Id) 
);