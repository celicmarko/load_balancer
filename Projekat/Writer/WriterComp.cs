using Common;
using LoadBalancer;

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
            loadBalancer.proveraBrojaUBufferu();

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
