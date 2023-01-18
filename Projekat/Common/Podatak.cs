using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Podatak
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Ulica { get; set; }
        public int Broj { get; set; }
        public string Grad { get; set; }
        public int Post { get; set; }

        public Podatak()
        {
        }

        public Podatak(int id, string ime, string prezime, string ulica, int broj, string grad, int post)
        {
            if (id < 0 || ime == "" || prezime == "" || ulica == "" || broj < 0 || grad == "" || post < 0)
            {
                throw new ArgumentException("Polje ne sme biti prazno");
            }
            else
            {
                Id = id;
                Ime = ime;
                Prezime = prezime;
                Ulica = ulica;
                Broj = broj;
                Grad = grad;
                Post = post;
            }
        }

        public static string GetFormattedHeader()
        {
            return string.Format("{0,-6} {1,-35} {2,-20} {3,-35} {4,-30} {5, -35} {6, -30}",
                "ID", "IME", "PREZIME", "ULICA", "BROJ", "GRAD", "POST");
        }

        public override string ToString()
        {
            return string.Format("{0,-6} {1,-35} {2,-20} {3,-35} {4,-30} {5, -35} {6, -30}",
                Id, Ime, Prezime, Ulica, Broj, Grad, Post);
        }
    }
}
