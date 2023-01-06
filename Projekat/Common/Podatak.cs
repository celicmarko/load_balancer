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
        public double Vrednost { get; set; }

        public Podatak()
        {
        }

        public Podatak(int id = 0, double vrednost = 0)
        {
            if (id > 0)
            {
                Id = id;
            }
            else
            {
                throw new ArgumentException("Id mora biti veci od 0");
            }

            Vrednost = vrednost;
        }

    }
}
