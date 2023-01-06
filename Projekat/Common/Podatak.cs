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

        public Podatak(int id, double vrednost)
        {
            Id = id;
            Vrednost = vrednost;
        }
    }
}
