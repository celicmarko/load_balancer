using Common;
using NUnit.Framework;
using System;
using Writer;

namespace WriterTests
{
    [TestFixture]
    public class Testovi
    {
        [Test]
        [TestCase()]
        public void OkConst()
        {
            WriterComp wc = new WriterComp();

            Assert.NotNull(wc);
        }

        [Test]
        [TestCase()]
        public void IspisConst()
        {
            WriterComp wc = new WriterComp();

            wc.IspisiBrojila();
        }

        [Test]
        [TestCase(1, 1, 1, 1)]
        [TestCase(1, 12, 13, 14)]
        [TestCase(1, 11, 11, 12)]
        public void TestSlanjeMerenje(int id, int bid, int p, int m)
        {
            WriterComp wc = new WriterComp();

            try
            {
                wc.SlanjePoruke(new PodatakPotrosnja(id, bid, p, m));
            }
            catch (Exception) { }
        }
    }
}
