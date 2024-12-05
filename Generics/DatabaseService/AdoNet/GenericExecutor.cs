using Generics.Common.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace Generics.Services.DatabaseService.AdoNet
{
    public class PaginationModel
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string OrderBy { get; set; }
        public int PageNo { get => PageNumber; set { PageNumber = value; } }

        public PaginationModel(int pageNumber = 1, int pageSize = 12, string orderby = " (SELECT NULL) ")
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            OrderBy = orderby;
        }

        public static PaginationModel Page(int pageNo = 1, int pageSize = 12, string orderby = " (SELECT NULL) ")
        {
            return new PaginationModel(pageNo, pageSize, orderby);
        }
    }
    public class TemplateClass<T>
    {
        public T Result { get; set; }
    }
    public static class GenericExecutor
    {
        const string DefaultSchema = "dbo";

        public static List<T> SelectAll<T>(this string tableName, PaginationModel pagination = null, DatabaseType databaseType = DatabaseType.DefaultDatabase, string schema = DefaultSchema) where T : new()
        {
            var query = GenericQueries.SelectAll(tableName, schema);
            query += updatePagination(pagination);
            return QueryExecutor.List<T>(query, query, databaseType) ?? new List<T>();
        }

        //public static List<string> SelectAll(this string tableName, string columnName, DatabaseType databaseType = DatabaseType.DefaultDatabase, string schema = DefaultSchema) where T : new()
        //{
        //    var query = GenericQueries.SelectAll(tableName, columnName, schema);
        //    return (QueryExecutor.List<TemplateClass<string>>(query, query, databaseType) ?? new List<TemplateClass<string>>())
        //        .Select(r => r.Result).ToList();
        //}
        public static List<T> SelectAll<T>(this string tableName, string columnName, PaginationModel pagination = null, DatabaseType databaseType = DatabaseType.DefaultDatabase, string schema = DefaultSchema)
        {
            var query = GenericQueries.SelectAll(tableName, columnName, schema);
            query += updatePagination(pagination);
            return (QueryExecutor.List<TemplateClass<T>>(query, query, databaseType) ?? new List<TemplateClass<T>>())
                .Select(r => r.Result).ToList();
        }

        public static List<T> SelectList<T>(this string tableName, string where, PaginationModel pagination = null, DatabaseType databaseType = DatabaseType.DefaultDatabase, string schema = DefaultSchema) where T : new()
        {
            var query = GenericQueries.SelectAll(tableName, schema);
            if (!string.IsNullOrWhiteSpace(where))
                query += $" WHERE {where}";

            query += updatePagination(pagination);
            return QueryExecutor.List<T>(query, query, databaseType);
        }

        public static List<T> SelectList<T>(this string tableName, string columnName, string where, PaginationModel pagination = null, DatabaseType databaseType = DatabaseType.DefaultDatabase, string schema = DefaultSchema)
        {
            var query = GenericQueries.SelectAll(tableName, columnName, schema);
            if (!string.IsNullOrWhiteSpace(where))
                query += $" WHERE {where}";

            query += updatePagination(pagination);
            return (QueryExecutor.List<TemplateClass<T>>(query, query, databaseType) ?? new List<TemplateClass<T>>())
                .Select(r => r.Result).ToList();
        }
        public static T Select<T>(this string tableName, string where, PaginationModel pagination = null, DatabaseType databaseType = DatabaseType.DefaultDatabase, string schema = DefaultSchema) where T : new()
        {
            var query = GenericQueries.Select(tableName, schema);
            if (!string.IsNullOrWhiteSpace(where))
                query += $" WHERE {where}";

            query += updatePagination(pagination);
            return QueryExecutor.FirstOrDefault<T>(query, query, databaseType);
        }
        public static T Select<T>(this string tableName, string columnName, string where, PaginationModel pagination = null, DatabaseType databaseType = DatabaseType.DefaultDatabase, string schema = DefaultSchema)
        {
            var query = GenericQueries.Select(tableName, columnName, schema);
            if (!string.IsNullOrWhiteSpace(where))
                query += $" WHERE {where}";

            query += updatePagination(pagination);
            return QueryExecutor.FirstOrDefault<TemplateClass<T>>(query, query, databaseType).Result ?? default;
        }
        public static T Select<T>(this string tableName, long id, PaginationModel pagination = null, DatabaseType databaseType = DatabaseType.DefaultDatabase, string schema = DefaultSchema) where T : new()
        {
            var query = GenericQueries.Select(tableName, schema);
            var columnName = typeof(T).GetAttributeColumnName(typeof(DbGenerated));
            if (string.IsNullOrEmpty(columnName))
                query += $" WHERE Id = {id}";
            else
                query += $" WHERE [{columnName}] = {id}";

            query += updatePagination(pagination);
            return QueryExecutor.FirstOrDefault<T>(query, query, databaseType);
        }
        public static List<T> SelectIn<T>(this string tableName, List<long> id, PaginationModel pagination = null, DatabaseType databaseType = DatabaseType.DefaultDatabase, string schema = DefaultSchema) where T : new()
        {
            var query = GenericQueries.SelectAll(tableName, schema);
            var columnName = typeof(T).GetAttributeColumnName(typeof(DbGenerated));
            if (string.IsNullOrEmpty(columnName))
                columnName = "Id";
            if (id == null || id.Count == 0) return new List<T>();
            if (id.Count == 1)
                query += $" WHERE Id = {id.FirstOrDefault()}";
            else
            {
                var idStr = id.Select(i => i + "").Aggregate((c, n) => c + "," + n);
                query += $" WHERE [{columnName}] IN ({idStr})";
            }

            query += updatePagination(pagination);
            return QueryExecutor.List<T>(query, query, databaseType);
        }
        public static List<T> SelectIn<T>(this string tableName, List<string> id, PaginationModel pagination = null, DatabaseType databaseType = DatabaseType.DefaultDatabase, string schema = DefaultSchema) where T : new()
        {

            var columnName = typeof(T).GetAttributeColumnName(typeof(DbGenerated));
            if (columnName.IsEmpty())
                columnName = "Id";

            return SelectIn<T>(tableName, columnName, id, pagination, databaseType, schema);
        }
        public static List<T> SelectIn<T>(this string tableName, string columnName, List<string> id, PaginationModel pagination = null, DatabaseType databaseType = DatabaseType.DefaultDatabase, string schema = DefaultSchema) where T : new()
        {
            var query = GenericQueries.SelectAll(tableName, schema);

            if (id == null || id.Count == 0) return new List<T>();
            if (columnName.IsEmpty())
                query += $" WHERE Id = '{id.FirstOrDefault()}'";
            else
            {
                var idStr = id.Select(i => $"'{i}'").Aggregate((c, n) => c + "," + n);
                query += $" WHERE [{columnName}] IN ({idStr})";
            }
            query += updatePagination(pagination);

            return QueryExecutor.List<T>(query, query, databaseType);
        }
        public static List<T> SelectIn<T>(this string tableName, string where, PaginationModel pagination = null, DatabaseType databaseType = DatabaseType.DefaultDatabase, string schema = DefaultSchema) where T : new()
        {
            var query = GenericQueries.Select(tableName, schema);
            var columnName = typeof(T).GetAttributeColumnName(typeof(DbGenerated));
            if (string.IsNullOrEmpty(columnName))
                columnName = "Id";
            query += $"WHERE  [{columnName}] IN ({where})";
            query += updatePagination(pagination);
            return QueryExecutor.List<T>(query, query, databaseType);
        }

        #region Insert
        public static long Insert<T>(this T model, string tableName, DatabaseType databaseType = DatabaseType.DefaultDatabase, string schema = DefaultSchema)
            => Insert<T, long>(model, tableName, databaseType, schema);

        public static R Insert<T, R>(this T model, string tableName, DatabaseType databaseType = DatabaseType.DefaultDatabase, string schema = DefaultSchema)
        {
            if (model == null || string.IsNullOrWhiteSpace(tableName)) return default;
            var query = GenericQueries.Insert(model, tableName, new List<string>(), false, schema);
            //var data=QueryExecutor.FirstOrDefault<TemplateClass<string>>(query, query, databaseType);
            return QueryExecutor.FirstOrDefault<TemplateClass<R>>(query, query, databaseType).Result ?? default;
        }
        public static string InsertString<T>(this T model, string tableName, DatabaseType databaseType = DatabaseType.DefaultDatabase, string schema = DefaultSchema)
        {
            if (model == null || string.IsNullOrWhiteSpace(tableName)) return null;
            var query = GenericQueries.Insert(model, tableName, new List<string>(), false, schema);
            return QueryExecutor.FirstOrDefault<TemplateClass<string>>(query, query, databaseType)?.Result ?? null;
        }
        public static List<long> Insert<T>(this List<T> list, string tableName, DatabaseType databaseType = DatabaseType.DefaultDatabase, string schema = DefaultSchema)
            => Insert<T, long>(list, tableName, databaseType, schema);
        public static List<R> Insert<T, R>(this List<T> list, string tableName, DatabaseType databaseType = DatabaseType.DefaultDatabase, string schema = DefaultSchema)
        {
            if (list == null || list.Count <= 0 || string.IsNullOrWhiteSpace(tableName)) return new List<R>();
            if (list.Count <= 1000)
            {
                var query = GenericQueries.Insert(list, tableName, new List<string>(), false, schema);
                return (QueryExecutor.List<TemplateClass<R>>(query, query, databaseType) ?? new List<TemplateClass<R>>())
                    .Select(r => r.Result).ToList();
            }
            else
            {
                var results = new List<R>();
                var lists = list.ListDivider(1000);
                foreach (var l in lists)
                {
                    var query = GenericQueries.Insert(l, tableName, new List<string>(), false, schema);
                    var result = (QueryExecutor.List<TemplateClass<R>>(query, query, databaseType) ?? new List<TemplateClass<R>>())
                        .Select(r => r.Result).ToList();
                    results.AddRange(result);
                }
                return results;
            }
        }
        public static long InsertWithExtraIgnore<T>(this T model, string tableName, List<string> ignoreColumns, DatabaseType databaseType = DatabaseType.DefaultDatabase, string schema = DefaultSchema)
        {
            if (model == null || string.IsNullOrWhiteSpace(tableName)) return 0;
            var query = GenericQueries.Insert(model, tableName, ignoreColumns, false, schema);
            return QueryExecutor.FirstOrDefault<TemplateClass<long>>(query, query, databaseType)?.Result ?? 0;
        }

        public static List<long> InsertIfNotExist<T>(this List<T> list, string tableName, DatabaseType databaseType = DatabaseType.DefaultDatabase, string schema = DefaultSchema)
        {
            if (list == null || list.Count <= 0 || string.IsNullOrWhiteSpace(tableName)) return new List<long>();
            var query = GenericQueries.Insert(list, tableName, new List<string>(), true, schema);
            return (QueryExecutor.List<TemplateClass<long>>(query, query, databaseType) ?? new List<TemplateClass<long>>())
                .Select(r => r.Result).ToList();
        }
        #endregion


        #region Update
        public static long Update<T>(this T model, string tableName, long id, DatabaseType databaseType = DatabaseType.DefaultDatabase, string schema = DefaultSchema)
        {
            if (model == null || string.IsNullOrWhiteSpace(tableName)) return 0;
            var query = GenericQueries.Update(model, tableName, id, new List<string>(), schema);
            return QueryExecutor.FirstOrDefault<TemplateClass<long>>(query, query, databaseType)?.Result ?? 0;
        }
        public static long Update<T>(this T model, string tableName, string id, DatabaseType databaseType = DatabaseType.DefaultDatabase, string schema = DefaultSchema)
        {
            if (model == null || string.IsNullOrWhiteSpace(tableName)) return 0;
            var query = GenericQueries.Update(model, tableName, id, new List<string>(), schema);
            return QueryExecutor.FirstOrDefault<TemplateClass<long>>(query, query, databaseType)?.Result ?? 0;
        }
        public static long UpdateWithExtraIgnore<T>(this T model, string tableName, long id, List<string> ignoreColumns, DatabaseType databaseType = DatabaseType.DefaultDatabase, string schema = DefaultSchema)
        {
            if (model == null || string.IsNullOrWhiteSpace(tableName)) return 0;
            var query = GenericQueries.Update(model, tableName, id, ignoreColumns, schema);
            return QueryExecutor.FirstOrDefault<TemplateClass<long>>(query, query, databaseType)?.Result ?? 0;
        }
        public static string UpdateWithExtraIgnore<T>(this T model, string tableName, string id, List<string> ignoreColumns, DatabaseType databaseType = DatabaseType.DefaultDatabase, string schema = DefaultSchema)
        {
            if (model == null || string.IsNullOrWhiteSpace(tableName)) return null;
            var query = GenericQueries.Update(model, tableName, id, ignoreColumns, schema);
            return QueryExecutor.FirstOrDefault<TemplateClass<string>>(query, query, databaseType)?.Result;
        }

        public static long UpdateWhere<T>(this T model, string tableName, string where, DatabaseType databaseType = DatabaseType.DefaultDatabase, string schema = DefaultSchema)
            => UpdateWhere<T, long>(model, tableName, where, databaseType, schema);

        public static R UpdateWhere<T, R>(this T model, string tableName, string where, DatabaseType databaseType = DatabaseType.DefaultDatabase, string schema = DefaultSchema)
        {
            if (model == null || string.IsNullOrWhiteSpace(tableName)) return default;
            var query = GenericQueries.UpdateWhere(model, tableName, where, null, null, schema);
            var result = QueryExecutor.FirstOrDefault<TemplateClass<R>>(query, query, databaseType);
            if (result == null)
                return default;
            return result.Result ?? default;
        }

        public static bool UpdateListWhere<T>(this List<T> list, string tableName, string where, List<string> whereColumns, DatabaseType databaseType = DatabaseType.DefaultDatabase, string schema = DefaultSchema)
        {
            if (list == null || string.IsNullOrWhiteSpace(tableName)) return false;
            if (list.Count <= 0) return true;

            var query = "";
            foreach (var model in list)
            {
                query += GenericQueries.UpdateWhere(model, tableName, where, new List<string>(), whereColumns, schema);
            }
            return QueryExecutor.ExecuteDml(query, query, databaseType);
        }
        #endregion


        #region Delete
        // Delete Query Executions
        public static bool Delete(this string tableName, long id, DatabaseType databaseType = DatabaseType.DefaultDatabase, string schema = DefaultSchema)
        {
            var query = GenericQueries.Delete(tableName, id, schema);
            return QueryExecutor.ExecuteDml(query, query, databaseType);
        }
        public static bool Delete(this string tableName, string id, DatabaseType databaseType = DatabaseType.DefaultDatabase, string schema = DefaultSchema)
        {
            var query = GenericQueries.Delete(tableName, id, schema);
            return QueryExecutor.ExecuteDml(query, query, databaseType);
        }
        public static bool DeleteWhere(this string tableName, string where, DatabaseType databaseType = DatabaseType.DefaultDatabase, string schema = DefaultSchema)
        {
            var query = GenericQueries.DeleteWhere(tableName, where, schema);
            return QueryExecutor.ExecuteDml(query, query, databaseType);
        }
        #endregion
        public static string updatePagination(PaginationModel pagination)
        {
            if (pagination == null)
                return "";
            if (pagination.PageNo == 0)
            {
                pagination.PageNo = 1;
            }
            var skip = pagination.PageSize * (pagination.PageNo - 1);
            return $" ORDER BY {pagination.OrderBy} OFFSET {skip} ROWS FETCH NEXT {pagination.PageSize} ROWS ONLY";
        }
    }
}
