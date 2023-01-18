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
        public PodatakPotrosnja podatakPotrosnja { get; set; }

        public Podatak podatak { get; set; }

        public WriterComp()
        {
            loadBalancer = new LoadBalancerComp();
            podatakPotrosnja = new PodatakPotrosnja();
            podatak = new Podatak();
        }

        public void SlanjePoruke(PodatakPotrosnja podatak)
        {
            loadBalancer.SmestanjeUBafer(podatak);

            Korisnik korisnik = new Korisnik();
            korisnik.SlanjeMerenja($"----------------------------------------------------");
            korisnik.SlanjeMerenja($"USPESNO: Uneta merenja su uspesno poslata LoadBalancer-u i smestena su u buffer.");
        }

        public void IspisiBrojila()
        {
            podatak.ToString();
        }

    }
}
