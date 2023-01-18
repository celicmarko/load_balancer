using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DatabaseCRUD;
using Writer;

namespace Program
{
    class Program
    {
        public static WriterComp writer = new WriterComp();

        static void Main(string[] args)
        {

            DatabaseCRUDComp baza = new DatabaseCRUDComp();

            PodatakPotrosnja podatakPotrosnja = new PodatakPotrosnja();

            while (true)
            {
                Console.WriteLine("============ LOADBALANCER ============");
                Console.WriteLine("Odaberite jednu od opcija: ");
                Console.WriteLine("1 - Unos merenja");
                Console.WriteLine("2 - Ispis svih elektricnih brojila");
                Console.WriteLine("3 - Ispis merenja po brojilu");
                Console.WriteLine("0 - Kraj\n");

                int unos = int.Parse(Console.ReadLine());

                switch (unos)
                {
                    case 1: unosMerenja(); break;
                    default: break;
                }


                void unosMerenja()
                {
                    Console.WriteLine("\nUnesite ID brojila: ");
                    int idBrojila = int.Parse(Console.ReadLine());
                    if (baza.PostojiUBazi(idBrojila) != true)
                    {
                        Console.WriteLine("GRESKA: Uneti ID brojila se ne nalazi u bazi");
                        return;
                    }
                    Console.WriteLine("USPESNO: ID brojila pronadjen u bazi");
                    Console.WriteLine("MERENJE: Unesi ID merenja: ");
                    int idMerenja = int.Parse(Console.ReadLine());
                    if(baza.PostojiUBaziMerenja(idMerenja) == true)
                    {
                        Console.WriteLine("GRESKA: Uneti ID merenja vec postoji u bazi");
                        return;
                    }
                    Console.WriteLine("MERENJE: Unesi potrosnju brojila u kW: ");
                    int potrosnja = int.Parse(Console.ReadLine());
                    Console.WriteLine("MERENJE: Unesi mesec merenja: ");
                    int mesec = int.Parse(Console.ReadLine());

                    podatakPotrosnja.IdMerenja = idMerenja;
                    podatakPotrosnja.IdBrojila = idBrojila;
                    podatakPotrosnja.Potrosnja = potrosnja;
                    podatakPotrosnja.Mesec = mesec;

                    
                    writer.SlanjePoruke(podatakPotrosnja);

                }
            }

        }
    }
}
