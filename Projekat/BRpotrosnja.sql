CREATE TABLE BRpotrosnja(
    IDmerenja integer NOT NULL,
    IDbr integer NOT NULL,
    Potrosnja integer NOT NULL,
    Mesec integer NOT NULL,
    CONSTRAINT BRpotrosnja_PK PRIMARY KEY (IDmerenja),
    CONSTRAINT BRpotrosnja_FK FOREIGN KEY (Idbr) REFERENCES brojilo(Id)
);