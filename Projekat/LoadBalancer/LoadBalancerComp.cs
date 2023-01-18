using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using Common;

namespace LoadBalancer
{
    public class LoadBalancerComp : ILoadBalancer
    {       
        public Buffer buffer { get; set; }

        public int brojac { get; set; }

        public LoadBalancerComp()
        {
            buffer = new Buffer();
            brojac = 0;
        }

        public void SmestanjeUBafer(PodatakPotrosnja podatak)
        {
            buffer.LoadBalancerbuffer.Add(podatak);

            //Console.WriteLine(buffer.LoadBalancerbuffer.Count());
        }

        public void proveraBrojaUBufferu()
        {
            int broj = buffer.LoadBalancerbuffer.Count();
            Console.WriteLine($"TRENUTNO SE NALAZI {broj} merenja u buffer-u");
        }
    }
}
