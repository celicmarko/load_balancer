using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class AktivniWorkeri
    {
        private static int brojAktivnihWorkera = 0;

        public int WorkersCount { get => brojAktivnihWorkera; set => brojAktivnihWorkera = value; }
    }
}
