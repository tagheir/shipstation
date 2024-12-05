using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace databaseproject
{
    public class MyValues
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            try
            {
                var ConnectionString = "Server=shipstation.database.windows.net;Initial Catalog=shipstation;Persist Security Info=True;User ID=sams8;Password=Helloworld123;MultipleActiveResultSets=True;";
                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    var query = "select * from sitemetas";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    SqlDataReader dr = sqlCommand.ExecuteReader();
                    if (dr.HasRows)
                    {
                        var enumerator = dr.GetEnumerator();
                        List<MyValues> list = new List<MyValues>();
                        while (dr.Read()) // no of rows fetched from the database 
                        {
                            Console.WriteLine(dr["KEY"]);
                            Console.WriteLine(dr["Id"]);
                            Console.Write(dr["VALUE"]);

                            list.Add(new MyValues()
                            {
                                Id = int.Parse(dr["Id"].ToString()),
                                Key = dr["KEY"].ToString(),
                                Value = dr["VALUE"].ToString()

                            });
                        }
                    }


                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


        }
    }
}
