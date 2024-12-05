using Generics.Db;
using System.Collections.Generic;

namespace LayerBao
{
    public static class SiteMetaBAO
    {
        public static SiteMetaDto GetKey(string Key)
        {
            return LayerDao.SiteMetaDAO.GetKey(Key);
        }
        public static List<SiteMetaDto> GetSiteMetas()
        {
            return LayerDao.SiteMetaDAO.GetSiteMetas();
        }
        public static bool InsertIfNotFound(SiteMetaDto siteMetaDto)
        {
            return LayerDao.SiteMetaDAO.InsertIfNotFound(siteMetaDto);
        }
    }
}
