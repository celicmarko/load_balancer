using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;

namespace DatabaseCRUD
{
    public class DatabaseCRUDComp : IDatabase
    {

        private bool PostojiUBazi(int id, IDbConnection connection)
        {
            string query = "select * from brojilo where id = :id_b";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                ParameterUtil.AddParameter(command, "id_b", DbType.Int32);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "id_b", id);
                return command.ExecuteScalar() != null;
            }
        }
        public bool PostojiUBazi(int id)
        {
            using (IDbConnection connection = Connection.GetConnection())
            {
                connection.Open();
                return PostojiUBazi(id, connection);
            }
        }


        private bool PostojiUBaziMerenja(int id, IDbConnection connection)
        {
            string query = "select * from brpotrosnja where idmerenja = :idmerenja_b";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                ParameterUtil.AddParameter(command, "idmerenja_b", DbType.Int32);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "idmerenja_b", id);
                return command.ExecuteScalar() != null;
            }
        }
        public bool PostojiUBaziMerenja(int id)
        {
            using (IDbConnection connection = Connection.GetConnection())
            {
                connection.Open();
                return PostojiUBaziMerenja(id, connection);
            }
        }

        public int UpisUBazuBrojilo(Podatak podatak, IDbConnection connection)
        {
            string insertSql = "INSERT INTO brojilo (ime, prezime, ulica, broj, grad, post, id)" +
                " VALUES (:ime_b, :prezime_b, :ulica_b, :broj_b, :grad_b, :post_b, :id_b)";
            string updateSql = "update brojilo set ime = :ime_b, prezime = :prezime_b, " +
                "ulica = :ulica_b, broj = :broj_b, grad = :grad_b, post = :post_b where id=:id_b";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = PostojiUBazi(podatak.Id, connection) ? updateSql : insertSql;
                ParameterUtil.AddParameter(command, "ime_b", DbType.String);
                ParameterUtil.AddParameter(command, "prezime_b", DbType.String);
                ParameterUtil.AddParameter(command, "ulica_b", DbType.String);
                ParameterUtil.AddParameter(command, "broj_b", DbType.Int32);
                ParameterUtil.AddParameter(command, "grad_b", DbType.String);
                ParameterUtil.AddParameter(command, "post_b", DbType.Int32);
                ParameterUtil.AddParameter(command, "id_b", DbType.Int32);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "ime_b", podatak.Ime);
                ParameterUtil.SetParameterValue(command, "prezime_b", podatak.Prezime);
                ParameterUtil.SetParameterValue(command, "ulica_b", podatak.Ulica);
                ParameterUtil.SetParameterValue(command, "broj_b", podatak.Broj);
                ParameterUtil.SetParameterValue(command, "grad_b", podatak.Grad);
                ParameterUtil.SetParameterValue(command, "post_b", podatak.Post);
                ParameterUtil.SetParameterValue(command, "id_b", podatak.Id);

                return command.ExecuteNonQuery();
            }
        }

        public int UpisUBazuBrPotrosnja(PodatakPotrosnja podatakPotrosnja, IDbConnection connection)
        {
            string insertSql = "INSERT INTO brpotrosnja (idbr, potrosnja, mesec, idmerenja)" +
                " VALUES (:idbr_b, :potrosnja_b, :mesec_b, :idmerenja_b)";
            string updateSql = "update brpotrosnja set idbr = :idbr_b, potrosnja = :potrosnja_b, " +
                "mesec = :mesec_b where idmerenja = :idmerenja_b";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = PostojiUBazi(podatakPotrosnja.IdMerenja, connection) ? updateSql : insertSql;
                ParameterUtil.AddParameter(command, "idbr_b", DbType.Int32);
                ParameterUtil.AddParameter(command, "potrosnja_b", DbType.Int32);
                ParameterUtil.AddParameter(command, "mesec_b", DbType.Int32);
                ParameterUtil.AddParameter(command, "idmerenja_b", DbType.Int32);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "idbr_b", podatakPotrosnja.IdBrojila);
                ParameterUtil.SetParameterValue(command, "potrosnja_b", podatakPotrosnja.Potrosnja);
                ParameterUtil.SetParameterValue(command, "mesec_b", podatakPotrosnja.Mesec);
                ParameterUtil.SetParameterValue(command, "idmerenja_b", podatakPotrosnja.IdMerenja);

                return command.ExecuteNonQuery();
            }
        }
    }

    
}
