# load_balancer
* Predmetni projekat iz predmeta Elementi razvoja softvera

## Opis projekta
Projekat predstavlja simulaciju rada Load Balancer-a koji služi da ravnomerno rasporedi posao slobodnim radnicima. Korisnici unose podatke o merenjima električnih brojila koji se prosleđuju Load Balancer-u koji te podatke smešta u svoj buffer. Nakon prikupljenih 10 merenja koje su korisnici uneli, podaci se ravnomernu prosleđuju slobodnim radnicima (Worker-ima). Zadatak Worker-a jeste da podatke dobijene od Load Balancer-a obradi odnosno smesti u bazu podataka. 

## Writer
Komponenta ima ulogu da podatke u vidu očitanih vrednosti sa kućnih brojila dobijene od korisnika prosledi Load Balancer-u. Takodje Writer je komponenta koja pokreće radnike (Worker-e).

## Load Balancer
Komponenta je zadužena za ravnomerno raspoređivanje posla koji obavljaju Worker-i. Podaci dobijeni od Writer-a smeštaju se u buffer. Nakon prikupljenih 10 podataka u buffer-u, podaci se ravnomerno prosleđuju slobodnim Worker-ima.

## Worker
Komponenta služi za obradu podataka dobijenih od Load Balancer-a. Posredstvom DatabaseCRUD komponenta, Worker obrađuje podatke u vidu smeštanja istih u bazu podataka.
##DatabaseCRUD
Komponenta je zadužena za svu komunikaciju koja se odvija sa bazom podataka i za izvršavanje CRUD (Create, Read, Update, Delete) operacija. 

## Baze podataka
* brojilo     - informacije o odredjenom brojilu (id, ime, prezime, ulica, broj, grad, postanski_broj)
* brpotrosnja - podaci o potrošnji brojila (id_brojila, idbr*, potrosnja, mesec)

## DatabaseAnalitics
Komponenta služi za izvlačenje statistika u vidu dva tipa izveštaja 
* potrošnja po mesecima za određeni grad
* potrošnja po mesecima za konkretno brojilo 

## AcivityDiagram
![diagram](https://github.com/celicmarko/load_balancer/blob/main/diagram.png?raw=true)

## Autori
* Dušan Borovićanin  PR56/2020
* Marko Ćelić        PR57/2020
* Veronika Ivanić    PR53/2020
* Tatjana Spasojević PR50/2020
