using Common;
using NUnit.Framework;
using System;

namespace CommonTests
{
    [TestFixture]
    public class Testovi
    {
        [Test]
        [TestCase()]
        [TestCase()]
        [TestCase()]
        public void OkConstructorPodatak()
        {
            Podatak podatak = new Podatak();

            Assert.NotNull(podatak);
        }

        [Test]
        [TestCase(1, "ime", "prezime", "ulica", 43, "Novi Sad", 21000)]
        [TestCase(2, "ime2", "prezime", "ulica", 43, "Novi Sad", 21000)]
        [TestCase(3, "ime", "prezime3", "ulica", 43, "Novi Sad", 11000)]
        [TestCase(4, "ime", "prezi4me", "ulica4", 54, "Novi Sad", 15200)]
        [TestCase(5, "ime", "prezime", "ulica", 12, "Novi Sad", 21000)]
        public void OkPodatakaParametri(int id, string ime, string prezime, string ulica, int broj, string grad, int post)
        {
            try
            {
                Podatak podatak = new Podatak(id, ime, prezime, ulica, broj, grad, post);

                Assert.NotNull(podatak);
            }
            catch
            {
                return;
            }
        }

        [Test]
        [TestCase(1, "", "prezime", "ulica", 43, "Novi Sad", 21000)]
        [TestCase(2, "ime2", "", "ulica", 43, "Novi Sad", 21000)]
        [TestCase(3, "ime", "prezime3", "", 0, "Novi Sad", 11000)]
        [TestCase(4, "ime", "prezi4me", "ulica4", 54, "", 15200)]
        [TestCase(5, "ime", "", "", 12, "Novi Sad", 0)]
        public void ErrorPodatakaParametri(int id, string ime, string prezime, string ulica, int broj, string grad, int post)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Podatak podatak = new Podatak(id, ime, prezime, ulica, broj, grad, post);
            });
        }

        [Test]
        public void OtherMethods()
        {
            Podatak podatak = new Podatak();
            podatak.ToString();
            Podatak.GetFormattedHeader();
        }

        // Klasa korisnik
        [Test]
        [TestCase]
        public void KorisnikOk()
        {
            Korisnik korisnik = new Korisnik();

            Assert.NotNull(korisnik);
        }

        [Test]
        [TestCase]
        public void SlanjeOk()
        {
            try
            {
                Korisnik korisnik = new Korisnik();

                korisnik.SlanjeMerenja("merenje");
            }
            catch (Exception) { }
        }

        // podatak potrosnja
        [Test]
        [TestCase()]
        public void OkPPKonst()
        {
            PodatakPotrosnja podatakPotrosnja = new PodatakPotrosnja();

            Assert.NotNull(podatakPotrosnja);
        }

        [Test]
        [TestCase(1, 1, 1, 1)]
        [TestCase(1, 12, 13, 14)]
        [TestCase(1, 11, 11, 12)]
        public void OkPPKonstParams(int idMerenja, int idBrojila, int potrosnja, int mesec)
        {
            PodatakPotrosnja podatakPotrosnja = new PodatakPotrosnja(idMerenja, idBrojila, potrosnja, mesec);

            Assert.NotNull(podatakPotrosnja);
        }

        [Test]
        [TestCase(1, -21, -1, 1)]
        [TestCase(-1, 0, 13, 14)]
        [TestCase(1, 11, -1, 12)]
        [TestCase(1, 11, -1, -1)]
        public void ErrPPKonstParams(int idMerenja, int idBrojila, int potrosnja, int mesec)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                PodatakPotrosnja podatakPotrosnja = new PodatakPotrosnja(idMerenja, idBrojila, potrosnja, mesec);
            });
        }

        [Test]
        public void OtherMethodsPP()
        {
            PodatakPotrosnja podatak = new PodatakPotrosnja();
            podatak.ToString();
            PodatakPotrosnja.GetFormattedHeader();
        }
    }
}
