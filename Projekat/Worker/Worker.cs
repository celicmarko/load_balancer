using Common;
using DatabaseCRUD;
using System;
using System.Data;

namespace Worker_Cmp
{
    public class Worker : MarshalByRefObject
    {
        private DatabaseCRUDComp crud = new DatabaseCRUDComp();
        public Worker() { }

        public bool SendToDatabseCrud(PodatakPotrosnja podatak)
        {
            Console.WriteLine("Primljen podatak od Load Balanser-a!");
            try
            {
                using (IDbConnection konekcija = Connection.GetConnection())
                {
               
                    if (crud.PostojiLiIstiRedUBaziPodataka(konekcija, podatak.IdMerenja, podatak.IdBrojila, podatak.Potrosnja, podatak.Mesec) == false)
                    {
                        int res = crud.UpisUBazuBrPotrosnja(podatak, konekcija);

                        if (res != 0)
                        {
                            Console.WriteLine("[Upisano u bazu podataka!");
                            return true;
                        }
                        else
                            Console.WriteLine("Nije upisano!");
                    }
                    else
                    {
                        Console.WriteLine("Ne upisuje se red koji postoji u bazi podataka!");
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }
    }
}
