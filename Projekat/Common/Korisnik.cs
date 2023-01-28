using System;
using System.ServiceModel;

namespace Common
{
    public class Korisnik
    {
        public ISlanjePoruke proxy { get; set; }

        public Korisnik()
        {
            string address = "net.tcp://localhost:4000/ISlanjePoruke";
            NetTcpBinding binding = new NetTcpBinding();

            ChannelFactory<ISlanjePoruke> kanal = new ChannelFactory<ISlanjePoruke>(binding, address);

            try
            {
                proxy = null;
                proxy = kanal.CreateChannel();
                if (proxy == null)
                {
                    Console.WriteLine("Kanal zatvoren !");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void SlanjeMerenja(string merenje)
        {
            proxy.SlanjeMerenja(merenje);
        }
    }
}
