using System;
using System.Collections.Generic;
using System.Data;

namespace Polysol.Comercial.DataAccess
{
    public class GenericDA : Repository
    {
        public DataTable ListarDatosSEG(string p_SP, params object[] p_Parametros)
        {
            try
            {
                var comando = CorpSeguridadBD.GetStoredProcCommand(p_SP, p_Parametros);
                DataSet ds = CorpSeguridadBD.ExecuteDataSet(comando);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarDatosXtus(string p_SP, params object[] p_Parametros)
        {
            try
            {
                var comando = ExactusBD.GetStoredProcCommand(p_SP, p_Parametros);
                DataSet ds = ExactusBD.ExecuteDataSet(comando);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarDatos(string p_SP, params object[] p_Parametros)
        {
            try
            {
                var comando = PolyIntranetBD.GetStoredProcCommand(p_SP, p_Parametros);
                DataSet ds = PolyIntranetBD.ExecuteDataSet(comando);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object ObtenerValor(string p_SP, params object[] p_Parametros)
        {
            try
            {
                var comando = PolyIntranetBD.GetStoredProcCommand(p_SP, p_Parametros);
                return PolyIntranetBD.ExecuteScalar(comando);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet ListarDatosDS(string p_SP, params object[] p_Parametros)
        {
            try
            {
                var comando = PolyIntranetBD.GetStoredProcCommand(p_SP, p_Parametros);
                DataSet ds = PolyIntranetBD.ExecuteDataSet(comando);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Ejecutar(string p_SP, params object[] p_Parametros)
        {
            try
            {
                var comando = PolyIntranetBD.GetStoredProcCommand(p_SP, p_Parametros);
                int _resultado = PolyIntranetBD.ExecuteNonQuery(comando);
                if (_resultado <= 0)
                    return 0;
                else
                    return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<T> GetData<T>(string p_SP, params object[] p_Parametros) where T : new()
        {
            var properties = typeof(T).GetProperties();

            using (var comando = PolyIntranetBD.GetStoredProcCommand(p_SP, p_Parametros))
            {
                using (var reader = PolyIntranetBD.ExecuteReader(comando))
                {
                    while (reader.Read())
                    {
                        var element = new T();

                        foreach (var f in properties)
                        {
                            var o = reader[f.Name];
                            if (o.GetType() != typeof(DBNull)) f.SetValue(element, o, null);
                        }
                        yield return element;
                    }
                }
            }
        }
    }
}
