using GestionaleQuadri.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Xml;
using System.Xml.Serialization;

namespace GestionaleQuadri.Controllers
{
    public class DatabaseController : Controller
    {
        private static string connectionString = string.Empty;
        private static Database data = null;

        //private static string connectionString = @"Data Source=localhost;Initial Catalog=gestionale_quadri;Integrated Security=True;TrustServerCertificate=true; Connect Timeout=30;Encrypt=False;";

        private static SqlConnection sdwDBConnection = null;
        public DatabaseController()
        {

        }

        private static void connectDb()
        {
            //Database data = new Database();
            data = new Database();
            XmlSerializer xmlsd = new XmlSerializer(typeof(Database));
            using (TextReader tr = new StreamReader(@"./wwwroot/connection.xml"))
            {
                data = (Database)xmlsd.Deserialize(tr);
            }

            connectionString = $"Data Source={data.Server}; Initial Catalog={data.Db}; Password={data.Password}; TrustServerCertificate=true; User ID={data.Username}";

            sdwDBConnection = new SqlConnection(connectionString.ToString());

            sdwDBConnection.Open();

        }

        public static void fetchDB() //test
        {
            string sql = "SELECT * FROM [gestionale_quadri].[commesse]";

            connectDb();

            SqlCommand cmd = new SqlCommand(sql, sdwDBConnection);

            cmd.CommandTimeout = 3600;

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(reader["nome_commessa"].ToString());
            }

            sdwDBConnection.Close();

        }


        public static List<T> SELECT_GET_LIST<T>(string sql)
        {
            List<T> list = new List<T>();

            //try
            //{
                connectDb();

                //SqlConnection sdwDBConnection = new SqlConnection(connectionString.ToString());

                //sdwDBConnection.Open();

                //sql = "SET DATEFORMAT ymd " + sql;

                SqlCommand cmd = new SqlCommand($"use {data.Db}; {sql}", sdwDBConnection);

                cmd.CommandTimeout = 3600;

                SqlDataReader reader = cmd.ExecuteReader();

                list = GetList<T>((IDataReader)reader);

                sdwDBConnection.Close();
            //}
            //catch (Exception ex)
            //{
            //    //MessageBox.Show(ex.Message);
            //}





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

        public IActionResult Index()
        {
            return View();
        }
    }
}
