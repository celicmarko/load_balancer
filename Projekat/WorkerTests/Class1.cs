using Common;
using Moq;
using NUnit.Framework;
using Worker_Cmp;

namespace WorkerTests
{
    [TestFixture]
    public class Testovi
    {
        [Test]
        [TestCase()]
        public void OkConst()
        {
            Worker w = new Worker();

            Assert.NotNull(w);
        }

        [Test]
        [TestCase(1, 1, 1, 1)]
        [TestCase(1, 12, 13, 14)]
        [TestCase(1, 11, 11, 12)]
        public void TestSlanjeMerenjeBaza(int id, int bid, int p, int m)
        {
            PodatakPotrosnja podatakPotrosnja = new PodatakPotrosnja(id, bid, p, m);

            Mock<IWorker> mock = new Mock<IWorker>();

            mock.Setup(pm => pm.SendToDatabseCrud(podatakPotrosnja)).Returns(true);
        }

        [Test]
        [TestCase(22, 10, 426, 4)]
        [TestCase(20, 10, 457, 3)]
        [TestCase(25, 10, 400, 10)]
        public void TestSlanjeMerenjeBazaPostojiRed(int id, int bid, int p, int m)
        {
            PodatakPotrosnja podatakPotrosnja = new PodatakPotrosnja(id, bid, p, m);

            Mock<IWorker> mock = new Mock<IWorker>();

            mock.Setup(pm => pm.SendToDatabseCrud(podatakPotrosnja)).Returns(false);
        }
    }
}
