using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.IO;

namespace GestionaleQuadri.Models
{
    public class Database
    {

        private static string connectionString = string.Empty;

        public Database()
        {



            //connectionString = "Data Source=DESKTOP-6GISBQC\\SQLEXPRESS;Initial Catalog=cn8_rp;Integrated Security=True";
        }


        public static List<T> SELECT_GET_LIST<T>(string sql, string database = "")
        {
            List<T> list = new List<T>();

            connectionString = File.ReadAllText("connecting.txt");
            //connectionString = @"Data Source=DESKTOP-6GISBQC\SQLEXPRESS;Initial Catalog=gestionale_quadri;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                sql = "SET DATEFORMAT ymd " + sql;

                SqlCommand command = new SqlCommand(sql, connection);

                command.Connection.Open();

                command.CommandTimeout = 600;

                SqlDataReader reader = command.ExecuteReader();

                list = GetList<T>(reader);

                command.Connection.Close();
            }

            return list;

        }

        public static List<T> GetList<T>(IDataReader reader)
        {
            List<T> list = new List<T>();

            while (reader.Read())
            {
                var type = typeof(T);
                T obj = (T)Activator.CreateInstance(type);
                foreach (var prop in type.GetProperties())
                {
                    var protoType = prop.PropertyType;
                    prop.SetValue(obj, Convert.ChangeType(reader[prop.Name].ToString().Trim(), protoType));
                }
                list.Add(obj);
            }

            return list;
        }

    }
}
