using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class PodatakPotrosnja
    {
        public int IdMerenja { get; set; }
        public int IdBrojila { get; set; }
        public int Potrosnja { get; set; }
        public int Mesec { get; set; }

        public PodatakPotrosnja()
        {

        }
        public PodatakPotrosnja(int idMerenja, int idBrojila, int potrosnja, int mesec)
        {
            if (idMerenja < 0 || idBrojila < 0 || potrosnja < 0 || (mesec > 12 && mesec < 0))
            {
                throw new ArgumentException("Podaci ne smeju biti negativni");
            }
            else
            {
                IdMerenja = idMerenja;
                IdBrojila = idBrojila;
                Potrosnja = potrosnja;
                Mesec = mesec;
            }
            
        }
    }
}
