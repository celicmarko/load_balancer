using Common;
using DatabaseCRUD;
using System;
using System.Data;

namespace Worker_Cmp
{
    public class Worker : MarshalByRefObject
    {
        private DatabaseCRUDComp crud = new DatabaseCRUDComp();

        private Korisnik korisnik = new Korisnik();
        public Worker() { }

        public bool SendToDatabseCrud(PodatakPotrosnja podatak)
        {
            Console.WriteLine("Uspesno primljen podataka od Load Balancer-a");
            try
            {
                using (IDbConnection konekcija = Connection.GetConnection())
                {
                    konekcija.Open();
                    // ako podatak ne postoji u bazi onda ce biti upisan u istu

                    if (crud.PostojiLiIstiRedUBaziPodataka(konekcija, podatak.IdMerenja, podatak.IdBrojila, podatak.Potrosnja, podatak.Mesec) == false)
                    {
                        int res = crud.UpisUBazuBrPotrosnja(podatak, konekcija);

                        if (res != 0)
                        {
                            korisnik.SlanjeMerenja($"----------------------------------------------------");
                            korisnik.SlanjeMerenja($"WORKER: Uspesno upisano u bazu podataka");
                            Console.WriteLine("[Worker]: Upisano u bazu podataka!");
                            return true;
                        }
                        else
                            korisnik.SlanjeMerenja($"----------------------------------------------------");
                            korisnik.SlanjeMerenja($"WORKER: Nije upisano u bazu podataka");
                            Console.WriteLine("[Worker]: Nije upisano!");
                    }
                    else
                    {
                        korisnik.SlanjeMerenja($"----------------------------------------------------");
                        korisnik.SlanjeMerenja($"WORKER: Red vec postoji u bazi podataka");
                        Console.WriteLine("[Worker]: Ne upisuje se red koji postoji u bazi podataka!");
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                korisnik.SlanjeMerenja($"----------------------------------------------------");
                korisnik.SlanjeMerenja($"WORKER: Doslo je do greske");
                Console.WriteLine(ex.Message);

                return false;
            }
        }
    }
}
