using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Generics.Services.DatabaseService.AdoNet
{
    public class ObjectMapper<T> where T : new()
    {
        public virtual List<T> MapReaderToObjectList(SqlDataReader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            var list = new List<T>();
            var columns = new List<string>();
            while (reader.Read())
            {
                if (columns == null || columns.Count == 0) columns = GetReaderColumns(reader);
                var item = ItemMapper(reader, columns);
                list.Add(item);
            }
            return list;
        }

        private static T ItemMapper(IDataRecord reader, List<string> columns)
        {
            if (columns == null || columns.Count == 0) return default(T);
            var item = new T();
            var t = item.GetType();
            foreach (var property in t.GetProperties())
            {
                try
                {
                    var type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                    var readerValue = string.Empty;

                    var columnName = property.Name;
                    var attribute = property.GetCustomAttributes(typeof(Column), true);
                    if (attribute.Length > 0)
                    {
                        var attributeValues = (Column)attribute[0];
                        columnName = attributeValues.Name;
                    }

                    if (string.IsNullOrWhiteSpace(columnName) || !columns.Contains(columnName))
                        continue;

                    if (reader[columnName] != DBNull.Value)
                    {
                        readerValue = reader[columnName].ToString();
                    }

                    if (type?.Name == Enums.DataType.Boolean.ToString())
                    {
                        property.SetValue(item, readerValue.ToBoolean(), null);
                    }
                    else if (!string.IsNullOrEmpty(readerValue))
                    {
                        property.SetValue(item, readerValue.To(type), null);
                    }
                    else if (type.Name == Enums.DataType.String.ToString())
                    {
                        property.SetValue(item, readerValue.To(type), null);
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex);
                    // ignored
                }
            }
            return item;
        }

        public T MapReaderToObject(SqlDataReader reader)
        {
            return !reader.Read()
                ? default(T)
                : ItemMapper(reader, GetReaderColumns(reader));
        }
        public List<string> GetReaderColumns(IDataRecord reader)
        {
            var columns = new List<string>();
            for (var i = 0; i < reader.FieldCount; i++) columns.Add(reader.GetName(i));
            return columns;
        }

    }
}
