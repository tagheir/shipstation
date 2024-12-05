using Generics.Db;
using Generics.Services.DatabaseService.AdoNet;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayerDao
{
    public class EmailRecorderDAO
    {
        public const string TableName = "EmailSender";
        public static long AddEmailRecord(EmailRecorderDto emailRecorderDto)
        {
            var rec =  GetEmailRecorder(emailRecorderDto.OrderId, emailRecorderDto.EmailPurpose);
            if (rec != null)
                return rec.Id;

           return GenericExecutor.Insert<EmailRecorderDto>(emailRecorderDto, TableName);
        }
        public static EmailRecorderDto GetEmailRecorder(string orderId,string EmailPurpose)
        {
            var query = $"SELECT * FROM {TableName} WHERE OrderId = '{orderId}' " +
                $"AND" +
                $" EmailPurpose = '{EmailPurpose}'";
            return QueryExecutor.FirstOrDefault<EmailRecorderDto>(query);
        }
        
    }
}
