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
    }
}
