using Common;
using LoadBalancer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Writer
{
    public class WriterComp
    {
        public LoadBalancerComp loadBalancer { get; set; }
        public Podatak podatak { get; set; }

        public WriterComp()
        {
            loadBalancer = new LoadBalancerComp();
            podatak = new Podatak();
        }

        public bool SlanjePoruke()
        {
            Podatak podatak = new Podatak();

            return false;
        }

    }
}
