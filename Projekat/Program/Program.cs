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
            Korisnik korisnik = new Korisnik();

            DatabaseCRUDComp baza = new DatabaseCRUDComp();

            Podatak podatak = new Podatak();

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

                    case 2: ispisSvihBrojila(); break;

                    case 3: ispisSvihMerenja(); break;

                    default: break;

                }


                void unosMerenja()
                {
                    korisnik.SlanjeMerenja($"----------------------------------------------------");
                    korisnik.SlanjeMerenja($"OBAVESTENJE: Korisnik je zapoceo unos merenja");
                    Console.WriteLine("\nUnesite ID brojila: ");
                    int idBrojila = int.Parse(Console.ReadLine());
                    if (baza.PostojiUBazi(idBrojila) != true)
                    {
                        Console.WriteLine("GRESKA: Uneti ID brojila se ne nalazi u bazi");
                        korisnik.SlanjeMerenja($"----------------------------------------------------");
                        korisnik.SlanjeMerenja($"GRESKA: Korisnik je uneo ID brojila koji se ne nalazi u bazi");
                        return;
                    }
                    Console.WriteLine("USPESNO: ID brojila pronadjen u bazi");
                    korisnik.SlanjeMerenja($"----------------------------------------------------");
                    korisnik.SlanjeMerenja($"USPESNO: Korisnik je uneo ID brojila koji je pronadjen u bazi");
                    Console.WriteLine("MERENJE: Unesi ID merenja: ");
                    int idMerenja = int.Parse(Console.ReadLine());
                    if(baza.PostojiUBaziMerenja(idMerenja) == true)
                    {
                        Console.WriteLine("GRESKA: Uneti ID merenja vec postoji u bazi");
                        korisnik.SlanjeMerenja($"----------------------------------------------------");
                        korisnik.SlanjeMerenja($"GRESKA: Korisnik je uneo ID merenja koji se vec nalazi u bazi");
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


                    try
                    {
                        writer.SlanjePoruke(podatakPotrosnja);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                        korisnik.SlanjeMerenja($"----------------------------------------------------");
                        korisnik.SlanjeMerenja($"GRESKA: Uneto merenje nije uspesno prosledjeno Writer-u");
                    }
                }

                void ispisSvihBrojila()
                {
                    korisnik.SlanjeMerenja($"----------------------------------------------------");
                    korisnik.SlanjeMerenja($"OBAVESTENJE: Korisnik je zatrazio ispis svih elektricnih brojila");

                    if (baza.pronadjiSvePodatke().Count() == 0)
                    {
                        Console.WriteLine("UPOZORENJE: U bazi se ne nalazi ni jedno brojilo");
                        return;
                    }

                    Console.WriteLine(Podatak.GetFormattedHeader());

                    try
                    {
                        foreach (Podatak temp in baza.pronadjiSvePodatke())
                        {
                            Console.WriteLine(temp);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine("\n\n");
                }

                void ispisSvihMerenja()
                {
                    korisnik.SlanjeMerenja($"----------------------------------------------------");
                    korisnik.SlanjeMerenja($"OBAVESTENJE: Korisnik je zatrazio ispis merenja za odredjeno elektricno brojilo");

                    Console.WriteLine("\nUnesite ID brojila za kog zelite da pogledate merenja: ");
                    int idBrojila = int.Parse(Console.ReadLine());
                    if (baza.PostojiUBazi(idBrojila) != true)
                    {
                        Console.WriteLine("GRESKA: Uneti ID brojila se ne nalazi u bazi");

                        korisnik.SlanjeMerenja($"----------------------------------------------------");
                        korisnik.SlanjeMerenja($"GRESKA: Korisnik je uneo ID brojila koji nije pronadjen u bazi");

                        return;
                    }

                    if(baza.pronadjiSvaMerenja(idBrojila).Count() == 0)
                    {
                        Console.WriteLine("UPOZERENJE: Za ovo brojilo ne postoje merenja\n\n");
                        return;
                    }

                    Console.WriteLine(PodatakPotrosnja.GetFormattedHeader());

                    try
                    {
                        foreach (PodatakPotrosnja tempMerenje in baza.pronadjiSvaMerenja(idBrojila))
                        {
                            Console.WriteLine(tempMerenje);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine("\n\n");
                }
            }

        }
    }
}
