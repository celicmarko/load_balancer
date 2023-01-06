using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using Common;

namespace LoadBalancer
{
    public class LoadBalancerComp
    {
        public Buffer buffer { get; set; }

        public LoadBalancerComp()
        {
            buffer = new Buffer();
        }
    }
}
