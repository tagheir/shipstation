using System;

namespace Generics.Services.DatabaseService.AdoNet
{
    public class Constants
    {
        public static Func<string, string, bool> Logger;
        public static string TicketId;
        public static void LogInfo(string info)
        {
            if (Logger == null && TicketId.IsEmpty()) return;
            Logger(TicketId, info);
        }
        public static string ConnectionString { get; set; }
        public static string JobsConnectionString { get; set; }
    }

    public enum DatabaseType
    {
        DefaultDatabase,
        LogsDatabase
    }
}
