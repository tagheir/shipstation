using Generics.Db;
using Generics.Services.DatabaseService.AdoNet;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayerDao
{
    public class SamsAccountDao
    {
        static string Table = "SamsAccount";
        public static long InsertSamsAccountDao(SamsAccontDto samsAccountDto)
        {
            return GenericExecutor.Insert<SamsAccontDto>(samsAccountDto, Table);
        }
        public static long UpdateSamsAccountDao(SamsAccontDto samsAccountDto)
        {
            return GenericExecutor.Update<SamsAccontDto>(samsAccountDto, Table,samsAccountDto.Id);
        }
        public static  SamsAccontDto GetSamsAccontDto(int id)
        {
            return GenericExecutor.Select<SamsAccontDto>(Table, id);
        }
        public static List<SamsAccontDto> GetAllAccountInfo()
        {
            return GenericExecutor.SelectAll<SamsAccontDto>(Table);
        }
    }
}
