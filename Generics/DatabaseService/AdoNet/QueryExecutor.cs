using System;
using System.Collections.Generic;
using System.Threading;

namespace Generics.Services.DatabaseService.AdoNet
{
    public static class QueryExecutor
    {
        // ADO.NET
        public static bool ExecuteDml(string query, string function = null, DatabaseType databaseType = DatabaseType.DefaultDatabase, bool retry = true, int retryCount = 1)
        {
            try
            {
                return DatabaseQueryExecuter<object>.Execute(query, databaseType) >= 0;
            }
            catch (Exception ex)
            {
                if (retry && !string.IsNullOrWhiteSpace(ex.ToString()) && ex.ToString().ToLower().Contains("timeout") && retryCount > 0)
                {
                    Thread.Sleep(100);
                    return ExecuteDml(query, function, databaseType, false, retryCount - 1);
                }
                //Constants.LogInfo(query);
                //Constants.LogInfo(ex.ToString());
            }

            return false;
        }
        public static int ExecuteInsert(string query, string function = null, bool retry = true)
        {
            try
            {
                return DatabaseQueryExecuter<object>.ExecuteInsert(query);
            }
            catch (Exception ex)
            {
                if (retry && !string.IsNullOrWhiteSpace(ex.ToString()) && ex.ToString().ToLower().Contains("timeout"))
                    return ExecuteInsert(query, function, false);
                //Constants.LogInfo(query);
                //Constants.LogInfo(ex.ToString());
            }

            return 0;
        }

        public static T FirstOrDefault<T>(string query, string function = null, DatabaseType databaseType = DatabaseType.DefaultDatabase, bool retry = true) where T : new()
        {
            try
            {
                return DatabaseQueryExecuter<T>.Get(query, databaseType);
            }
            catch (Exception ex)
            {
                if (retry && !string.IsNullOrWhiteSpace(ex.ToString()) && ex.ToString().ToLower().Contains("timeout"))
                {
                    Thread.Sleep(100);
                    return FirstOrDefault<T>(query, function, databaseType, false);
                }
                Constants.LogInfo(query);
                Constants.LogInfo(ex.ToString());
            }

            return default;
        }
        public static List<T> List<T>(string query, string function = null, DatabaseType databaseType = DatabaseType.DefaultDatabase, bool retry = true) where T : new()
        {
            try
            {
                return DatabaseQueryExecuter<T>.GetAll(query, databaseType);
            }
            catch (Exception ex)
            {
                if (retry && !string.IsNullOrWhiteSpace(ex.ToString()) && ex.ToString().ToLower().Contains("timeout"))
                {
                    Thread.Sleep(100);
                    return List<T>(query, function, databaseType, false);
                }
                Constants.LogInfo(query);
                Constants.LogInfo(ex.ToString());
            }

            return default;
        }

    }
}
