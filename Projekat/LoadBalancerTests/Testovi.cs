using Common;
using LoadBalancer;
using NUnit.Framework;
using System;
using Buffer = LoadBalancer.Buffer;

namespace LoadBalancerTests
{
    [TestFixture]
    public class Testovi
    {
        // public void RemoveFirst() - buffer
        [Test]
        [TestCase()]
        public void UkloniJedan()
        {
            Buffer buffer = new Buffer();

            buffer.RemoveFirst();
        }

        // load balancer testovi
        // public void proveraBrojaUBufferu()
        [Test]
        [TestCase()]
        public void ProveriBroj0()
        {
            LoadBalancerComp loadBalancerComp = new LoadBalancerComp();

            loadBalancerComp.proveraBrojaUBufferu();
            Assert.AreEqual(loadBalancerComp.buffer.LoadBalancerbuffer.Count, 0);
        }

        // public void SmestanjeUBafer(PodatakPotrosnja podatak)
        [Test]
        [TestCase(1, 1, 1, 1)]
        [TestCase(1, 12, 13, 14)]
        [TestCase(1, 11, 11, 12)]
        public void SmestiBaffer(int id, int bid, int p, int m)
        {
            LoadBalancerComp lb = new LoadBalancerComp();

            lb.SmestanjeUBafer(new PodatakPotrosnja(id, bid, p, m));

            Assert.AreEqual(1, lb.buffer.LoadBalancerbuffer.Count);
        }


        // public bool SendDataToWorkers()
        [Test]
        [TestCase()]
        public void TestWorkers()
        {
            LoadBalancerComp lb = new LoadBalancerComp();

            Assert.AreEqual(false, lb.SendDataToWorkers());
        }

        [Test]
        [TestCase(1, 11, 11, 12)]
        public void TestWorkersBroj(int id, int bid, int p, int m)
        {
            LoadBalancerComp lb = new LoadBalancerComp();

            try
            {
                for (int i = 0; i < 10; i++)
                    lb.SmestanjeUBafer(new PodatakPotrosnja(id, bid, p, m));

                Assert.AreEqual(true, lb.SendDataToWorkers());
            }

            catch (Exception) { }
        }
    }
}
