using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Generics.Services.DatabaseService.AdoNet
{
    public class DatabaseQueryExecuter<T> where T : new()
    {
        public static string Conn = "Server=shipstation.database.windows.net;Initial Catalog=shipstation;Persist Security Info=True;User ID=sams8;Password=Hellworld123;MultipleActiveResultSets=True;";
        private static SqlConnection DefaultConnection;
        private static SqlConnection LogsConnection;

        public static T Get(string query, DatabaseType databaseType = DatabaseType.DefaultDatabase, SqlConnection conn = null)
        {
            try
            {
                var command = GetCommand(query, databaseType, conn);
                var reader = command.ExecuteReader();

                var obj = new ObjectMapper<T>().MapReaderToObject(reader);
                reader.Close();

                if (conn != null)
                    conn.Close();

                return obj;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                conn?.Close();
                throw;
            }
        }
        public static List<T> GetAll(string query, DatabaseType databaseType = DatabaseType.DefaultDatabase, SqlConnection conn = null)
        {
            try
            {
                var command = GetCommand(query, databaseType, conn);
                var reader = command.ExecuteReader();

                var obj = new ObjectMapper<T>().MapReaderToObjectList(reader);

                reader.Close();

                if (conn != null)
                    conn.Close();

                return obj;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                conn?.Close();
                throw;
            }

        }

        public static int Execute(string query, DatabaseType databaseType = DatabaseType.DefaultDatabase, SqlConnection conn = null)
        {
            try
            {
                var command = GetCommand(query, databaseType, conn);
                var reader = command.ExecuteNonQuery();

                if (conn != null)
                    conn.Close();

                return reader;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                conn?.Close();
                throw;
            }
        }



        public static int ExecuteInsert(string query, DatabaseType databaseType = DatabaseType.DefaultDatabase, SqlConnection conn = null)
        {
            try
            {
                var command = GetCommand(query, databaseType, conn);
                var reader = Convert.ToInt32(command.ExecuteScalar());

                if (conn != null)
                    conn.Close();
                return reader;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                conn?.Close();
                throw;
            }
        }

        private static string GetConnectionString(DatabaseType databaseType = DatabaseType.DefaultDatabase)
        {
            if (databaseType == DatabaseType.DefaultDatabase)
                return Constants.ConnectionString;
            else if (databaseType == DatabaseType.LogsDatabase)
                return Constants.JobsConnectionString;
            else return null;
        }
        private static bool OpenConnection(DatabaseType databaseType = DatabaseType.DefaultDatabase)
        {
            try
            {
                if (databaseType == DatabaseType.DefaultDatabase)
                {

                    if (DefaultConnection == null || DefaultConnection.State != System.Data.ConnectionState.Open)
                    {
                        DefaultConnection ??= new SqlConnection(GetConnectionString(databaseType));
                        DefaultConnection.Open();
                    }
                }
                else if (databaseType == DatabaseType.LogsDatabase)
                {

                    if (LogsConnection == null || LogsConnection.State != System.Data.ConnectionState.Open)
                    {
                        LogsConnection ??= new SqlConnection(GetConnectionString(databaseType));
                        LogsConnection.Open();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return false;
            }
        }
        private static SqlConnection GetConnection(DatabaseType databaseType = DatabaseType.DefaultDatabase, SqlConnection connection = null)
        {
            if (connection != null) return connection;
            OpenConnection(databaseType);
            try
            {

                if (databaseType == DatabaseType.DefaultDatabase)
                {
                    return DefaultConnection;
                }
                else if (databaseType == DatabaseType.LogsDatabase)
                {
                    return LogsConnection;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            return null;
        }
        private static SqlCommand GetCommand(string query, DatabaseType databaseType = DatabaseType.DefaultDatabase, SqlConnection connection = null)
        {
            var localConnection = GetConnection(databaseType, connection);
            var command = new SqlCommand(query, localConnection) { CommandTimeout = 0 };
            return command;
        }

    }


}
