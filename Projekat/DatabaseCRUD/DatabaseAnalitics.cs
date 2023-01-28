using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCRUD
{
    public class DatabaseAnalitics : DatabaseCRUDComp
    {
        public void pronadjiSvaMerenjaZaGrad(string nazivGrada)
        {
            string query = "select sum(potrosnja), mesec, grad from brojilo" +
                " left outer join brpotrosnja on id = idbr group by grad, mesec " +
                "having grad = :grad_b order by mesec";

            using (IDbConnection connection = Connection.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "grad_b", DbType.String);
                    command.Prepare();
                    ParameterUtil.SetParameterValue(command, "grad_b", nazivGrada);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine($"\nPOTROSNJA ZA {nazivGrada.ToUpper()}");
                        Console.WriteLine("================================");

                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                Console.WriteLine($"Mesec: {reader.GetInt32(1)}\tPotrosnja: {reader.GetInt32(0)}kWh");
                            }
                        }
                        Console.WriteLine("================================");
                        Console.WriteLine();
                    }
                }
            }
        }

        public void pronadjiPotrosnjeZaBrojilo(int idBrojila)
        {
            string query = "select potrosnja, mesec from brpotrosnja where idbr = :idBrojila_b order by mesec asc";
            using (IDbConnection connection = Connection.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "idBrojila_b", DbType.Int32);
                    command.Prepare();
                    ParameterUtil.SetParameterValue(command, "idBrojila_b", idBrojila);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine($"\nPOTROSNJA ZA BROJILO: {idBrojila}");
                        Console.WriteLine("================================");

                        while (reader.Read())
                        {
                            Console.WriteLine($"Mesec: {reader.GetInt32(1)}\tPotrosnja: {reader.GetInt32(0)}kWh");
                        }
                        Console.WriteLine("================================");
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
