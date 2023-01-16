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

        public void SmestanjeUBafer(Podatak podatak)
        {
            buffer.LoadBalancerbuffer.Add(podatak);
        }
    }
}
