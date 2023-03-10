using Common;
using DatabaseCRUD;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
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

            DatabaseAnalitics bazaAnalitics = new DatabaseAnalitics();

            // pokretanje workera
            try
            {
                Console.Write("Koliko zelite aktivnih Worker-a: ");
                int WorkersCount = int.Parse(Console.ReadLine());

                if (WorkersCount < 0)
                {
                    throw new ArgumentException();
                }

                for (int i = 0; i < WorkersCount; i++)
                {
                    // svaki worker ima svoj port
                    // kreiranje novog procesa

                    string dir = Environment.CurrentDirectory;
                    dir = Directory.GetParent(dir).Parent.Parent.FullName;

                    Process pro = new Process();

                    pro.StartInfo.FileName = dir + "\\bin\\Debug\\Worker.exe";
                    pro.StartInfo.Arguments = (6500 + i).ToString();
                    pro.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;

                    pro.Start();
                }

                // cuvanje broja aktivnih workera
                ActiveWorkers active = new ActiveWorkers();
                active.WorkersCount = WorkersCount;
            }
            catch
            {
                Console.WriteLine("GRESKA: Broj workera mora biti pozitivan ceo broj!");
                Console.ReadLine();
                return;
            }

            while (true)
            {
                Console.WriteLine("============ LOADBALANCER ============");
                Console.WriteLine("Odaberite jednu od opcija: ");
                Console.WriteLine("1 - Unos merenja");
                Console.WriteLine("2 - Ispis svih elektricnih brojila");
                Console.WriteLine("3 - Ispis merenja po brojilu");
                Console.WriteLine("4 - Ispis potrosnje po mesecima za odredjeni grad");
                Console.WriteLine("5 - Ispis potrosnje po mesecima za odredjeno brojilo");
                Console.WriteLine("0 - Kraj\n");

                int unos = int.Parse(Console.ReadLine());

                switch (unos)
                {
                    case 1: unosMerenja(); break;

                    case 2: ispisSvihBrojila(); break;

                    case 3: ispisSvihMerenja(); break;

                    case 4: ispiSvihMerenjaZaGrad(); break;

                    case 5: ispisSvihMerenjaZaOdredjenoBrojilo(); break;

                    case 0: return;

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
                    if (baza.PostojiUBaziMerenja(idMerenja) == true)
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

                    // kreiranje objekta za slanje
                    PodatakPotrosnja novi = new PodatakPotrosnja(idMerenja, idBrojila, potrosnja, mesec);

                    try
                    {
                        writer.SlanjePoruke(novi);

                    }
                    catch (Exception e)
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

                    if (baza.pronadjiSvaMerenja(idBrojila).Count() == 0)
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

                void ispiSvihMerenjaZaGrad()
                {
                    korisnik.SlanjeMerenja($"----------------------------------------------------");
                    korisnik.SlanjeMerenja($"OBAVESTENJE: Korisnik je zatrazio ispis potrosnje za odredjeni grad");
                    Console.WriteLine("Upisite naziv grada za kog zelite da pogledate potrosnju");
                    string nazivGrada = Console.ReadLine();
                    nazivGrada = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nazivGrada);

                    bazaAnalitics.pronadjiSvaMerenjaZaGrad(nazivGrada);
                }

                void ispisSvihMerenjaZaOdredjenoBrojilo()
                {
                    korisnik.SlanjeMerenja($"----------------------------------------------------");
                    korisnik.SlanjeMerenja($"OBAVESTENJE: Korisnik je zatrazio ispis potrosnje po mesecima za odredjeno brojilo");
                    Console.WriteLine("Upisite IDBrojila za kog zelite da pogledate potrosnju po mesecima");
                    int idbro = int.Parse(Console.ReadLine());

                    bazaAnalitics.pronadjiPotrosnjeZaBrojilo(idbro);
                }
            }

        }
    }
}
