using Common;
using System;
using System.Collections.Generic;

namespace LoadBalancer
{
    [Serializable]
    public class Buffer
    {
        public List<PodatakPotrosnja> LoadBalancerbuffer { get; set; }

        public Buffer()
        {
            LoadBalancerbuffer = new List<PodatakPotrosnja>();
        }

        public void RemoveFirst()
        {
            if (LoadBalancerbuffer.Count > 0)
            {
                LoadBalancerbuffer.RemoveAt(0);
            }
        }
    }
}
