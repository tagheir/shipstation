using Generics.Db;
using Generics.Services.DatabaseService.AdoNet;
using System;
using System.Collections.Generic;

namespace LayerDao
{


    public static class SiteMetaDAO
    {
        public static SiteMetaDto GetKey(string Key)
        {
            var query = $"SELECT * FROM dbo.SiteMetas WHERE [KEY] = '{Key}' ";
            return QueryExecutor.FirstOrDefault<SiteMetaDto>(query);
        }
        public static List<SiteMetaDto> GetSiteMetas()
        {
            var query = "SELECT * FROM dbo.SITEMETAS";
            return QueryExecutor.List<SiteMetaDto>(query);
        }
        public static List<string> GetEmailsByKey(string key)
        {
            var emvs = GetKey(key); 
            if(emvs == null)
                return null;
            var ss = emvs.VALUE.Split(",");
            var ab = new List<string>();
            ab.AddRange(ss);
            return ab; 
        }
        public static bool InsertIfNotFound(SiteMetaDto siteMetaDto)
        {
            var query = $"IF (NOT EXISTS(SELECT * FROM SiteMetas WHERE [KEY] = '{siteMetaDto.KEY}' ) )";
            query += " BEGIN \n" +
                $" INSERT INTO SiteMetas([KEY], VALUE, LastUpdated)" +
                $" VALUES('{siteMetaDto.KEY}', '{siteMetaDto.VALUE}', '{DateTime.UtcNow}' ) \n END" +
                $"\n ELSE" +
            $"\n BEGIN" +
                $"\n UPDATE SiteMetas" +
                $" SET VALUE = '{siteMetaDto.VALUE}', LastUpdated = '{DateTime.UtcNow}' " +
                $" WHERE [KEY] = '{siteMetaDto.KEY}' " +
                $"END ";
            return QueryExecutor.ExecuteDml(query);

        }
    }
}
