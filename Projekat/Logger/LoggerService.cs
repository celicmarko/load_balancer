using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class LoggerService : ISlanjePoruke
    {
        public static string primljenaPoruka = "";

        public void SlanjeMerenja(string merenje)
        {
            primljenaPoruka = merenje;    
        }
    }
}
