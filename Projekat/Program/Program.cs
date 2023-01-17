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
                Console.WriteLine("1 - Unos merenja");
                Console.WriteLine("0 - Kraj");

                int unos = int.Parse(Console.ReadLine());

                switch (unos)
                {
                    case 1: unosMerenja(); break;
                    default: break;
                }


                void unosMerenja()
                {
                    Console.WriteLine("Unesite ID brojila: ");
                    int idBrojila = int.Parse(Console.ReadLine());
                    if (baza.PostojiUBazi(idBrojila) != true)
                    {
                        Console.WriteLine("Uneti ID brojila se ne nalazi u bazi");
                        return;
                    }
                    Console.WriteLine("ID brojila pronadjen u bazi");
                    Console.WriteLine("Unesi ID merenja: ");
                    int idMerenja = int.Parse(Console.ReadLine());
                    if(baza.PostojiUBaziMerenja(idMerenja) == true)
                    {
                        Console.WriteLine("Uneti ID merenja vec postoji u bazi");
                        return;
                    }
                    Console.WriteLine("Unesi potrosnju brojila u kW: ");
                    int potrosnja = int.Parse(Console.ReadLine());
                    Console.WriteLine("Unesi mesec merenja: ");
                    int mesec = int.Parse(Console.ReadLine());

                    podatakPotrosnja.IdMerenja = idMerenja;
                    podatakPotrosnja.IdBrojila = idBrojila;
                    podatakPotrosnja.Potrosnja = potrosnja;
                    podatakPotrosnja.Mesec = mesec;



                }
            }

        }
    }
}
