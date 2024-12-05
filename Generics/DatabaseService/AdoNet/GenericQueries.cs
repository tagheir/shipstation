using Generics.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Generics.Services.DatabaseService.AdoNet
{
    static class GenericQueries
    {
        public static string Delete(string tableName, long id, string schema) =>
            $"DELETE FROM [{schema}].[{tableName}] WHERE Id = {id}";
        public static string Delete(string tableName, string id, string schema) =>
           $"DELETE FROM [{schema}].[{tableName}] WHERE Id = '{id}'";
        public static string DeleteWhere(string tableName, string where, string schema) =>
           $"DELETE FROM [{schema}].[{tableName}] WHERE {where}";
        public static string SelectAll(string tableName, string schema) =>
            $"SELECT * FROM [{schema}].[{tableName}] (nolock)";
        public static string SelectAll(string tableName, string columnName, string schema) =>
            $"SELECT {columnName} Result FROM [{schema}].[{tableName}] (nolock)";
        public static string Select(string tableName, string schema) =>
            $"SELECT TOP 1 * FROM [{schema}].[{tableName}] (nolock)";
        public static string Select(string tableName, string columnName, string schema) =>
            $"SELECT TOP 1 {columnName} Result  FROM [{schema}].[{tableName}] (nolock)";
        public static string Insert<T>(T model, string tableName, List<string> ignoreColumns, bool insertIfNotExists, string schema)
        {
            var columns = "";
            var columnValues = "";
            var haveIdColumn = false;

            var idColumnName = typeof(T).GetAttributeColumnName(typeof(DbGenerated));
            if (string.IsNullOrEmpty(idColumnName))
                idColumnName = "Id";

            foreach (PropertyInfo prop in model.GetType().GetProperties())
            {
                var columnName = prop.CustomAttributes?
                    .FirstOrDefault(c => c.AttributeType == typeof(ColumnName))?
                    .ConstructorArguments?.ToList()?.FirstOrDefault()
                    .Value?.ToString() ?? prop.Name;
                if (columnName == idColumnName) haveIdColumn = true;
                if (ignoreColumns != null && ignoreColumns.Count > 0)
                {
                    if (ignoreColumns.FirstOrDefault(c => c != null && c.ToLower() == columnName.ToLower()) != null)
                        continue;
                }
                var value = prop.GetValue(model, null)?.ToString();

                var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                if (!type.IsSimple())
                    continue;

                var ignoreByAttribute = prop.CustomAttributes.FirstOrDefault(c => c.AttributeType == typeof(Ignore)) != null;
                if (ignoreByAttribute)
                {
                    if (columnName == idColumnName) haveIdColumn = false;
                    continue;
                }
                var ignoreByDbGeneratedAttribute = prop.CustomAttributes.FirstOrDefault(c => c.AttributeType == typeof(DbGenerated)) != null;
                if (ignoreByDbGeneratedAttribute) continue;

                var ignoreByAttributeIfValueNull = prop.CustomAttributes
                    .FirstOrDefault(c => c.AttributeType == typeof(IgnoreIfNull)) != null;
                if (ignoreByAttributeIfValueNull && value == null) continue;

                columns += $"[{columnName}], ";
                if (value == null)
                    columnValues += $"NULL, ";
                else if (type == typeof(string))
                    columnValues += $"'{value.Replace("'", "''")}', ";
                else if (type == typeof(DateTime))
                    columnValues += $"'{value}', ";
                else if (type == typeof(bool))
                    columnValues += $"{value.ToBoolInt()}, ";
                else if (type == typeof(double) || type == typeof(float))
                    columnValues += $"{value.ToDouble()}, ";
                else
                    columnValues += $"{value.ToLong()}, ";

            }
            if (columns.EndsWith(", "))
                columns = columns.Substring(0, columns.Length - 2);
            if (columnValues.EndsWith(", "))
                columnValues = columnValues.Substring(0, columnValues.Length - 2);
            var query = $"INSERT INTO [{schema}].[{tableName}] ({columns}) " +
                (haveIdColumn ? $"OUTPUT INSERTED.{idColumnName} AS Result " : "") +
                $"VALUES ({columnValues})";
            return query;
        }


        public static string Insert<T>(List<T> list, string tableName, List<string> ignoreColumns, bool insertIfNotExists, string schema)
        {
            if (list == null || list.Count <= 0) return null;
            var columns = "";
            var allRowsColumnValues = new List<string>();
            var haveIdColumn = false;
            var isColumnNameListDone = false;
            var tempTableName = $"Temp{tableName}";

            var notExistsConditionList = "";

            var idColumnName = typeof(T).GetAttributeColumnName(typeof(DbGenerated));
            if (string.IsNullOrEmpty(idColumnName))
                idColumnName = "Id";

            foreach (var model in list)
            {
                var columnValues = "";
                foreach (PropertyInfo prop in model.GetType().GetProperties())
                {
                    var columnName = prop.Name;
                    if (columnName == idColumnName) haveIdColumn = true;
                    if (ignoreColumns != null && ignoreColumns.Count > 0)
                    {
                        if (ignoreColumns.FirstOrDefault(c => c != null && c.ToLower() == columnName.ToLower()) != null)
                            continue;
                    }
                    var value = prop.GetValue(model, null)?.ToString();

                    var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    if (!type.IsSimple())
                        continue;

                    var ignoreByAttribute = prop.CustomAttributes.FirstOrDefault(c => c.AttributeType == typeof(Ignore)) != null;
                    if (ignoreByAttribute)
                    {
                        if (columnName == idColumnName) haveIdColumn = false;
                        continue;
                    }
                    var ignoreByDbGeneratedAttribute = prop.CustomAttributes.FirstOrDefault(c => c.AttributeType == typeof(DbGenerated)) != null;
                    if (ignoreByDbGeneratedAttribute) continue;

                    var ignoreByAttributeIfValueNull = prop.CustomAttributes
                        .FirstOrDefault(c => c.AttributeType == typeof(IgnoreIfNull)) != null;
                    if (ignoreByAttributeIfValueNull && value == null) continue;



                    if (!isColumnNameListDone)
                    {
                        columns += $"[{columnName}], ";
                        notExistsConditionList += $"[{tableName}].[{columnName}] = [{tempTableName}].[{columnName}] AND ";
                    }



                    if (value == null)
                        columnValues += $"NULL, ";
                    else if (type == typeof(string))
                        columnValues += $"'{value.Replace("'", "''")}', ";
                    else if (type == typeof(DateTime))
                        columnValues += $"'{value}', ";
                    else if (type == typeof(bool))
                        columnValues += $"{value.ToBoolInt()}, ";
                    else if (type == typeof(double) || type == typeof(float))
                        columnValues += $"{value.ToDouble()}, ";
                    else
                        columnValues += $"{value.ToLong()}, ";

                }
                isColumnNameListDone = true;
                if (columnValues.EndsWith(", "))
                    columnValues = columnValues[0..^2];
                allRowsColumnValues.Add($"({columnValues})");
            }

            if (columns.EndsWith(", "))
                columns = columns[0..^2];

            if (notExistsConditionList.EndsWith(" AND "))
                notExistsConditionList = notExistsConditionList[0..^5];

            string allRowsValues;
            if (allRowsColumnValues.Count == 1)
                allRowsValues = allRowsColumnValues.FirstOrDefault();
            else
                allRowsValues = allRowsColumnValues.Aggregate((c, n) => $"{c},{n}");

            var query = $"INSERT INTO [{schema}].[{tableName}] ({columns}) " +
                (haveIdColumn ? $"OUTPUT INSERTED.{idColumnName} AS Result " : "") +
                (insertIfNotExists ? "SELECT DISTINCT * FROM ( " : "") +
                $"VALUES {allRowsValues}" +
                (insertIfNotExists ? $") AS {tempTableName}({columns}) " : "") +
                (insertIfNotExists ? $"WHERE NOT EXISTS ( SELECT 1 FROM [{schema}].[{tableName}] WHERE {notExistsConditionList} )" : "");
            return query;
        }

        public static string Update<T>(T model, string tableName, long id, List<string> ignoreColumns, string schema)
        {
            var columnsAndValues = "";
            var haveIdColumn = false;

            var idColumnName = typeof(T).GetAttributeColumnName(typeof(DbGenerated));
            if (string.IsNullOrEmpty(idColumnName))
                idColumnName = "Id";

            foreach (PropertyInfo prop in model.GetType().GetProperties())
            {
                var columnName = prop.Name;

                if (columnName == idColumnName) haveIdColumn = true;

                if (ignoreColumns != null && ignoreColumns.Count > 0)
                {
                    if (ignoreColumns.FirstOrDefault(c => c != null && c.ToLower() == columnName.ToLower()) != null)
                        continue;
                }
                var value = prop.GetValue(model, null)?.ToString();

                var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                if (!type.IsSimple())
                    continue;

                var ignoreByAttribute = prop.CustomAttributes
                    .FirstOrDefault(c => c.AttributeType == typeof(IgnoreOnUpdate) || c.AttributeType == typeof(DbGenerated) || c.AttributeType == typeof(Ignore)) != null;
                if (ignoreByAttribute) continue;

                var ignoreByAttributeIfValueNull = prop.CustomAttributes
                    .FirstOrDefault(c => c.AttributeType == typeof(IgnoreIfNull)) != null;
                if (ignoreByAttributeIfValueNull && value == null) continue;

                var set = $"[{columnName}] = ";
                if (value == null)
                    set += $"NULL, ";
                else if (type == typeof(string))
                    set += $"'{value.Replace("'", "''")}', ";
                else if (type == typeof(DateTime))
                    set += $"'{value}', ";
                else if (type == typeof(bool))
                    set += $"{value.ToBoolInt()}, ";
                else if (type == typeof(double) || type == typeof(float))
                    set += $"{value.ToDouble()}, ";
                else
                    set += $"{value.ToLong()}, ";
                columnsAndValues += set;

            }
            if (columnsAndValues.EndsWith(", "))
                columnsAndValues = columnsAndValues.Substring(0, columnsAndValues.Length - 2);
            var query = $"UPDATE [{schema}].[{tableName}] SET {columnsAndValues} " +
                (haveIdColumn ? $"OUTPUT INSERTED.{idColumnName} AS Result " : "") +
                $"WHERE [{idColumnName}] = {id}";
            return haveIdColumn ? query : null;
        }
        public static string Update<T>(T model, string tableName, string id, List<string> ignoreColumns, string schema)
        {
            var columnsAndValues = "";
            var haveIdColumn = false;

            var idColumnName = typeof(T).GetAttributeColumnName(typeof(DbGenerated));
            if (string.IsNullOrEmpty(idColumnName))
                idColumnName = "Id";

            foreach (PropertyInfo prop in model.GetType().GetProperties())
            {
                var columnName = prop.Name;

                if (columnName == idColumnName) haveIdColumn = true;

                if (ignoreColumns != null && ignoreColumns.Count > 0)
                {
                    if (ignoreColumns.FirstOrDefault(c => c != null && c.ToLower() == columnName.ToLower()) != null)
                        continue;
                }
                var value = prop.GetValue(model, null)?.ToString();

                var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                if (!type.IsSimple())
                    continue;

                var ignoreByAttribute = prop.CustomAttributes
                    .FirstOrDefault(c => c.AttributeType == typeof(IgnoreOnUpdate) || c.AttributeType == typeof(DbGenerated) || c.AttributeType == typeof(Ignore)) != null;
                if (ignoreByAttribute) continue;

                var ignoreByAttributeIfValueNull = prop.CustomAttributes
                    .FirstOrDefault(c => c.AttributeType == typeof(IgnoreIfNull)) != null;
                if (ignoreByAttributeIfValueNull && value == null) continue;

                var set = $"[{columnName}] = ";
                if (value == null)
                    set += $"NULL, ";
                else if (type == typeof(string))
                    set += $"'{value.Replace("'", "''")}', ";
                else if (type == typeof(DateTime))
                    set += $"'{value}', ";
                else if (type == typeof(bool))
                    set += $"{value.ToBoolInt()}, ";
                else if (type == typeof(double) || type == typeof(float))
                    set += $"{value.ToDouble()}, ";
                else
                    set += $"{value.ToLong()}, ";
                columnsAndValues += set;

            }
            if (columnsAndValues.EndsWith(", "))
                columnsAndValues = columnsAndValues[0..^2];
            var query = $"UPDATE [{schema}].[{tableName}] SET {columnsAndValues} " +
                (haveIdColumn ? $"OUTPUT INSERTED.{idColumnName} AS Result " : "") +
                $"WHERE [{idColumnName}] = '{id}'";
            return haveIdColumn ? query : null;
        }

        public static string UpdateWhere<T>(T model, string tableName, string where, List<string> ignoreColumns, List<string> whereColumns, string schema)
        {
            ignoreColumns ??= new List<string>();
            whereColumns ??= new List<string>();

            if (whereColumns.Count == 0 && string.IsNullOrWhiteSpace(where)) return "";

            var columnsAndValues = "";
            var haveIdColumn = false;

            var whereCluase = "";

            var idColumnName = typeof(T).GetAttributeColumnName(typeof(DbGenerated));
            if (string.IsNullOrEmpty(idColumnName))
                idColumnName = "Id";

            foreach (PropertyInfo prop in model.GetType().GetProperties())
            {
                var columnName = prop.Name;

                if (columnName == idColumnName) haveIdColumn = true;

                if (ignoreColumns != null && ignoreColumns.Count > 0)
                {
                    if (ignoreColumns.FirstOrDefault(c => c != null && c.ToLower() == columnName.ToLower()) != null)
                        continue;
                }
                var value = prop.GetValue(model, null)?.ToString();

                var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                if (!type.IsSimple())
                    continue;

                var ignoreByAttribute = prop.CustomAttributes
                    .FirstOrDefault(c => c.AttributeType == typeof(IgnoreOnUpdate) || c.AttributeType == typeof(DbGenerated) || c.AttributeType == typeof(Ignore)) != null;
                if (ignoreByAttribute) continue;

                var ignoreByAttributeIfValueNull = prop.CustomAttributes
                    .FirstOrDefault(c => c.AttributeType == typeof(IgnoreIfNull)) != null;
                if (ignoreByAttributeIfValueNull && value == null) continue;

                var set = $"[{columnName}] = ";
                if (value == null)
                    set += $"NULL";
                else if (type == typeof(string))
                    set += $"'{value.Replace("'", "''")}'";
                else if (type == typeof(DateTime))
                    set += $"'{value}'";
                else if (type == typeof(bool))
                    set += $"{value.ToBoolInt()}";
                else if (type == typeof(double) || type == typeof(float))
                    set += $"{value.ToDouble()}";
                else
                    set += $"{value.ToLong()}";
                if (whereColumns.Contains(columnName))
                {
                    if (value != null)
                        whereCluase += set + " AND ";
                }
                else
                    columnsAndValues += set + ", ";

            }
            if (columnsAndValues.EndsWith(", "))
                columnsAndValues = columnsAndValues[0..^2];
            if (whereCluase.EndsWith("AND "))
                whereCluase = whereCluase[0..^4];
            var query = $"UPDATE [{schema}].[{tableName}] SET {columnsAndValues} " +
                (haveIdColumn ? $"OUTPUT INSERTED.{idColumnName} AS Result " : "") +
                $"WHERE ";
            if (string.IsNullOrWhiteSpace(whereCluase) && string.IsNullOrWhiteSpace(where)) return "";
            if (!string.IsNullOrWhiteSpace(whereCluase) && string.IsNullOrWhiteSpace(where)) return $"{query} {whereCluase};";
            if (string.IsNullOrWhiteSpace(whereCluase) && !string.IsNullOrWhiteSpace(where)) return $"{query} {where};";
            if (!string.IsNullOrWhiteSpace(whereCluase) && !string.IsNullOrWhiteSpace(where)) return $"{query} {where} AND {whereCluase};";
            return query;
        }
    }
}
