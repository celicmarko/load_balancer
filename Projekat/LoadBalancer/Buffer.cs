using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer
{
    public class Buffer
    {
        public List<Podatak> LoadBalancerbuffer {get; set; }

        public Buffer() 
        { 
            LoadBalancerbuffer = new List<Podatak>();
        }
    }
}
