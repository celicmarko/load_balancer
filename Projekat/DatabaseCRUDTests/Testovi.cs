using Common;
using DatabaseCRUD;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data;

namespace DatabaseCRUDTests
{
    [TestFixture]
    public class Testovi
    {
        // connection
        [Test]
        [TestCase()]
        public void DbConnectionOk()
        {
            Assert.NotNull(Connection.GetConnection());
        }

        [Test]
        [TestCase()]
        public void DisposableTest()
        {
            using (IDbConnection kon = Connection.GetConnection())
            {
                kon.Open();

                kon.Dispose();
            }

            var db = new Connection();
            db.Dispose();
            Assert.NotNull(Connection.GetConnection());
        }

        // databasecrud
        // private bool PostojiUBazi(int id, IDbConnection connection)
        [Test]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        [TestCase(20)]
        public void Postoji(int n)
        {
            DatabaseCRUDComp crud = new DatabaseCRUDComp();
            Assert.IsTrue(crud.PostojiUBazi(n)); // postoji u bazi
        }

        [Test]
        [TestCase(110)]
        [TestCase(111)]
        [TestCase(112)]
        [TestCase(113)]
        [TestCase(114)]
        [TestCase(115)]
        [TestCase(116)]
        public void NePostoji(int n)
        {
            DatabaseCRUDComp crud = new DatabaseCRUDComp();
            Assert.False(crud.PostojiUBazi(n)); // postoji u bazi
        }

        // public bool PostojiUBaziMerenja(int id)
        [Test]
        [TestCase(110)]
        [TestCase(111)]
        [TestCase(112)]
        [TestCase(113)]
        [TestCase(114)]
        [TestCase(115)]
        [TestCase(116)]
        public void NePostojiMerenje(int n)
        {
            DatabaseCRUDComp crud = new DatabaseCRUDComp();
            Assert.False(crud.PostojiUBaziMerenja(n)); // ne postoji u bazi
        }

        [Test]
        [TestCase(22)]
        [TestCase(25)]
        [TestCase(35)]
        [TestCase(36)]
        [TestCase(40)]
        [TestCase(41)]
        [TestCase(45)]
        public void PostojiMerenje(int n)
        {
            DatabaseCRUDComp crud = new DatabaseCRUDComp();
            Assert.True(crud.PostojiUBaziMerenja(n)); // postoji u bazi
        }

        // public int UpisUBazuBrojilo(Podatak podatak, IDbConnection connection)
        [Test]
        [TestCase(1, "ime", "prezime", "ulica", 43, "Novi Sad", 21000)]
        [TestCase(2, "ime2", "prezime", "ulica", 43, "Novi Sad", 21000)]
        [TestCase(3, "ime", "prezime3", "ulica", 43, "Novi Sad", 11000)]
        [TestCase(4, "ime", "prezi4me", "ulica4", 54, "Novi Sad", 15200)]
        [TestCase(5, "ime", "prezime", "ulica", 12, "Novi Sad", 21000)]
        public void MockDbUpis1(int id, string ime, string prezime, string ulica, int broj, string grad, int post)
        {
            Podatak pod = new Podatak(id, ime, prezime, ulica, broj, grad, post);

            Mock<IDatabaseCRUDComp> mock = new Mock<IDatabaseCRUDComp>();

            using (IDbConnection kon = Connection.GetConnection())
            {
                kon.Open();
                mock.Setup(p => p.UpisUBazuBrojilo(pod, kon)).Returns(1);
            }
        }

        // podatak potrosnja upis mock
        [Test]
        [TestCase(1, 1, 1, 1)]
        [TestCase(1, 12, 13, 14)]
        [TestCase(1, 11, 11, 12)]
        public void MockDbUpis2(int idMerenja, int idBrojila, int potrosnja, int mesec)
        {
            PodatakPotrosnja pod = new PodatakPotrosnja(idMerenja, idBrojila, potrosnja, mesec);

            Mock<IDatabaseCRUDComp> mock = new Mock<IDatabaseCRUDComp>();

            using (IDbConnection kon = Connection.GetConnection())
            {
                kon.Open();
                mock.Setup(p => p.UpisUBazuBrPotrosnja(pod, kon)).Returns(1);

            }
        }

        // public List<Podatak> pronadjiSvePodatke()
        [Test]
        [TestCase]
        public void SviPodaci()
        {
            // baza ne bi trebalo da bude prazna
            DatabaseCRUDComp databaseCRUDComp = new DatabaseCRUDComp();

            List<Podatak> podaci = databaseCRUDComp.pronadjiSvePodatke();

            Assert.NotZero(podaci.Count);
        }

        // public List<PodatakPotrosnja> pronadjiSvaMerenja(int id)
        [Test]
        [TestCase(22)]
        [TestCase(25)]
        [TestCase(35)]
        [TestCase(36)]
        [TestCase(40)]
        [TestCase(45)]
        public void SviPodaciMerenje(int id)
        {
            // baza ne bi trebalo da bude prazna
            DatabaseCRUDComp databaseCRUDComp = new DatabaseCRUDComp();

            List<PodatakPotrosnja> podaci = databaseCRUDComp.pronadjiSvaMerenja(id);

            Assert.Zero(podaci.Count);
        }

        // public bool PostojiLiIstiRedUBaziPodataka(IDbConnection konekcija, int idMerenja, int idBrojila, int potrosnja, int mesec)
        [Test]
        [TestCase(1, 1, 1, 1)]
        [TestCase(1, 12, 13, 14)]
        [TestCase(1, 11, 11, 12)]
        public void NotExistBazaRed(int idMerenja, int idBrojila, int potrosnja, int mesec)
        {
            DatabaseCRUDComp databaseCRUDComp = new DatabaseCRUDComp();

            using (IDbConnection kon = Connection.GetConnection())
            {
                kon.Open();

                Assert.False(databaseCRUDComp.PostojiLiIstiRedUBaziPodataka(kon, idMerenja, idBrojila, potrosnja, mesec));
            }
        }

        [Test]
        [TestCase(22, 10, 426, 4)]
        [TestCase(20, 10, 457, 3)]
        [TestCase(25, 10, 400, 10)]
        public void ExistBazaRed(int idMerenja, int idBrojila, int potrosnja, int mesec)
        {
            DatabaseCRUDComp databaseCRUDComp = new DatabaseCRUDComp();

            using (IDbConnection kon = Connection.GetConnection())
            {
                kon.Open();

                Assert.True(databaseCRUDComp.PostojiLiIstiRedUBaziPodataka(kon, idMerenja, idBrojila, potrosnja, mesec));
            }
        }
    }
}
