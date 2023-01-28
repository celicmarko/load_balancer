using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Threading;
using System.Threading.Tasks;
using Worker_Cmp;

namespace LoadBalancer
{
    public class LoadBalancerComp : ILoadBalancer
    {
        Korisnik korisnik = new Korisnik();

        const int MAX_DATA_PER_SENT = 10;
        public Buffer buffer { get; set; }
        public List<Worker> workers = new List<Worker>();

        public int brojac { get; set; }
        ActiveWorkers active = new ActiveWorkers();
        bool Prvi = true;

        public LoadBalancerComp()
        {
            buffer = new Buffer();
            brojac = 0;

            // provera da li je barem jedan Worker pokrenut
            if (active.WorkersCount <= 0)
            {
                Console.WriteLine("Nema aktivnih Workera za rad!");
                return;
            }

            Prvi = false;
        }

        public void SmestanjeUBafer(PodatakPotrosnja podatak)
        {
            // prvi prvom dodavanju potrebno je aktivirati background task
            if (Prvi)
            {
                // workeri
                for (int i = 0; i < active.WorkersCount; i++)
                {
                    // pristup workeru
                    string strConn = "tcp://localhost:" + (6500 + i).ToString() + "/Worker";
                    Worker worker = RemotingServices.Connect(typeof(Worker), strConn) as Worker;
                    workers.Add(worker);
                }

                PeriodicCheck();

                Prvi = false;
            }

            buffer.LoadBalancerbuffer.Add(podatak);

            //brojanje podataka u bufferu
            //Console.WriteLine(buffer.LoadBalancerbuffer.Count());
        }

        public void proveraBrojaUBufferu()
        {
            int broj = buffer.LoadBalancerbuffer.Count();
            Console.WriteLine($"TRENUTNO SE NALAZI {broj} merenja u buffer-u");
        }

        public async Task PeriodicCheck()
        {
            CancellationToken ct = new CancellationToken();
            TimeSpan vreme = new TimeSpan(0, 0, 2); // provera na 2 sekunde da li je bafer sa 10 podataka

            for (; !ct.IsCancellationRequested;)
            {
                await WorkersPreCheck(vreme, ct);
            }
        }

        public async Task WorkersPreCheck(TimeSpan interval, CancellationToken cancellationToken)
        {
            // provera da li treba slati podatke ka bazi podataka
            SendDataToWorkers();

            // wait for next iteration
            await Task.Delay(interval, cancellationToken);
        }

        public bool SendDataToWorkers()
        {
            if (buffer.LoadBalancerbuffer.Count >= MAX_DATA_PER_SENT) // ima vise od 10 podataka
            {
                korisnik.SlanjeMerenja($"----------------------------------------------------");
                korisnik.SlanjeMerenja($"Load Balancer: Podaci se salju Worker-ima");
                Console.WriteLine("[Load Balancer]: Slanje podataka Workerima..");

                // slanje na worker po worker
                for (int i = 0; i < MAX_DATA_PER_SENT; i++)
                {
                    int slobodanWorker = GetFreeWorker();

                    // slanje itom worker-u
                    Console.WriteLine("[Load Balancer]: Slanje podataka na Worker {0}!", slobodanWorker + 1);
                    var pod = buffer.LoadBalancerbuffer[0];
                    workers[slobodanWorker].SendToDatabseCrud(pod);
                    buffer.RemoveFirst();
                }

                Console.WriteLine("\n[Load Balancer] Prenos podataka zavrsen");

                // ispis menija nakon ste su se podaci upisali

                Console.WriteLine("============ LOADBALANCER ============");
                Console.WriteLine("Odaberite jednu od opcija: ");
                Console.WriteLine("1 - Unos merenja");
                Console.WriteLine("2 - Ispis svih elektricnih brojila");
                Console.WriteLine("3 - Ispis merenja po brojilu");
                Console.WriteLine("4 - Ispis potrosnje po mesecima za odredjeni grad");
                Console.WriteLine("5 - Ispis potrosnje po mesecima za odredjeno brojilo");
                Console.WriteLine("0 - Kraj\n");

                return true;
            }

            return false;
        }

        private int GetFreeWorker()
        {
            return new Random().Next(0, active.WorkersCount - 1);
        }
    }
}
