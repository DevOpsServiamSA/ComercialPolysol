using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Polysol.Comercial.DataAccess
{
    public abstract class Repository
    {
        #region Enterprise

        protected Database ExactusBD = new DatabaseProviderFactory().Create("ExactusConn");

        protected Database PolyIntranetBD = new DatabaseProviderFactory().Create("PolyIntranetBDConn");

        protected Database CorpSeguridadBD = new DatabaseProviderFactory().Create("CorpSeguridadBDConn");

        #endregion Enterprise
    }
}
