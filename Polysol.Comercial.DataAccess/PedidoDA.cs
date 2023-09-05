using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Polysol.Comercial.DataAccess
{
    public class PedidoDA : Repository
    {
        GenericDA _repoGeneric = new GenericDA();

        #region Almacen
        public DataSet ListarPedidosAlmacenPaginado(string p_Pedido, int p_StartRowIndex, int p_MaximumRows)
        {
            return _repoGeneric.ListarDatosDS("COM.PA_PED_PEDIDO_ALMACEN_PAGINADO_LISTAR", p_Pedido, p_StartRowIndex, p_MaximumRows);
        }
        public DataTable ListarPedidosAlmacen(string p_Pedido)
        {
            return _repoGeneric.ListarDatos("COM.PA_PED_PEDIDO_ALMACEN_LISTAR", p_Pedido);
        }
        public DataSet ListarPedidosAlmacenDetalle(string p_Pedido, string p_Programado)
        {
            return _repoGeneric.ListarDatosDS("COM.PA_PED_PEDIDO_ALMACEN_DETALLE_LISTAR", p_Pedido, p_Programado);
        }
        public DataSet InsertarEtiquetasAlmacen(string p_Pedido, string p_Programado, string p_Etiqueta, string p_Usuario)
        {
            return _repoGeneric.ListarDatosDS("COM.PA_PED_PEDIDO_ALMACEN_ETIQUETA_INSERTAR", p_Pedido, p_Programado, p_Etiqueta, p_Usuario);
        }
        public DataSet EliminarEtiquetasAlmacen(string p_Pedido, string p_Programado, string p_Etiqueta, string p_Usuario)
        {
            return _repoGeneric.ListarDatosDS("COM.PA_PED_PEDIDO_ALMACEN_ETIQUETA_ELIMINAR", p_Pedido, p_Programado, p_Etiqueta, p_Usuario);
        }
        public DataSet LimpiarLogCarga(string p_Pedido, string p_Programado)
        {
            return _repoGeneric.ListarDatosDS("COM.PA_PED_PEDIDO_ALMACEN_LOG_CARGA_ELIMINAR", p_Pedido, p_Programado);
        }
        public DataSet LimpiarCarga(string p_Pedido, string p_Programado)
        {
            return _repoGeneric.ListarDatosDS("COM.PA_PED_PEDIDO_ALMACEN_CARGA_ELIMINAR", p_Pedido, p_Programado);
        }
        public void EnviarCarga(string p_Pedido, string p_Programado, string p_Observacion, string p_Usuario)
        {
            _repoGeneric.Ejecutar("COM.PA_PED_PEDIDO_ALMACEN_CARGA_ENVIAR", p_Pedido, p_Programado, p_Observacion, p_Usuario);
        }
        #endregion

        #region Vendedor
        public DataTable ListarPedidosVendedor(string p_NoProgramado, string p_ValorFiltro, string p_Usuario)
        {
            return _repoGeneric.ListarDatos("COM.PA_PED_PEDIDO_VENDEDOR_PENDIENTES_LISTAR", p_NoProgramado, p_ValorFiltro, p_Usuario);
        }
        public DataTable ListarPedidosVendedorProgramacion(string p_Pedido)
        {
            return _repoGeneric.ListarDatos("COM.PA_PED_PEDIDO_VENDEDOR_PENDIENTES_DETALLE_PROGRAMACION_LISTAR", p_Pedido);
        }
        public string PartirPedidosVendedorProgramacion(DataTable p_dt, string p_Pedido, int p_Enc_Pedido_Linea, int p_Pedido_Programacion, decimal p_Cantidad, string p_Usuario)
        {
            using (DbCommand cmd = PolyIntranetBD.GetStoredProcCommand("COM.PA_PED_PEDIDO_VENDEDOR_PROGRAMACION_PARTIR"))
            {
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@PED_PROGRAMACION";
                parameter.SqlDbType = SqlDbType.Structured;
                parameter.Value = p_dt;
                cmd.Parameters.Add(parameter);

                PolyIntranetBD.AddInParameter(cmd, "@P_PEDIDO", DbType.String, p_Pedido);
                PolyIntranetBD.AddInParameter(cmd, "@P_ENC_PEDIDO_LINEA", DbType.Int32, p_Enc_Pedido_Linea);
                PolyIntranetBD.AddInParameter(cmd, "@P_PEDIDO_PROGRAMACION", DbType.Int32, p_Pedido_Programacion);
                PolyIntranetBD.AddInParameter(cmd, "@P_CANTIDAD", DbType.Decimal, p_Cantidad);
                PolyIntranetBD.AddInParameter(cmd, "@P_USUARIO", DbType.String, p_Usuario);
                PolyIntranetBD.ExecuteNonQuery(cmd);

                return "";
            }
        }
        public string ModificarColorProgramacion(string p_Pedido, int p_Enc_Pedido_Linea, int p_Pedido_Programacion, string p_Color, string p_Usuario)
        {
            using (DbCommand cmd = PolyIntranetBD.GetStoredProcCommand("COM.PA_PED_PEDIDO_VENDEDOR_COLOR_ACTUALIZAR"))
            {
                PolyIntranetBD.AddInParameter(cmd, "@P_PEDIDO", DbType.String, p_Pedido);
                PolyIntranetBD.AddInParameter(cmd, "@P_ENC_PEDIDO_LINEA", DbType.Int32, p_Enc_Pedido_Linea);
                PolyIntranetBD.AddInParameter(cmd, "@P_PEDIDO_PROGRAMACION", DbType.Int32, p_Pedido_Programacion);
                PolyIntranetBD.AddInParameter(cmd, "@P_COLOR", DbType.String, p_Color);
                PolyIntranetBD.AddInParameter(cmd, "@P_USUARIO", DbType.String, p_Usuario);
                PolyIntranetBD.ExecuteNonQuery(cmd);
                
                return "";
            }
        }
        public string GrabarCambiosProgramacion(DataTable p_dt, string p_Pedido, string p_Observacion, string p_Usuario)
        {
            using (DbCommand cmd = PolyIntranetBD.GetStoredProcCommand("COM.PA_PED_PEDIDO_VENDEDOR_PROGRAMACION_GRABAR"))
            {
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@PED_PROGRAMACION";
                parameter.SqlDbType = SqlDbType.Structured;
                parameter.Value = p_dt;
                cmd.Parameters.Add(parameter);

                PolyIntranetBD.AddInParameter(cmd, "@P_PEDIDO", DbType.String, p_Pedido);
                PolyIntranetBD.AddInParameter(cmd, "@P_OBSERVACION", DbType.String, p_Observacion);
                PolyIntranetBD.AddInParameter(cmd, "@P_USUARIO", DbType.String, p_Usuario);
                PolyIntranetBD.ExecuteNonQuery(cmd);

                return "";
            }
            //return _repoGeneric.ListarDatos("COM.PA_PED_PEDIDO_VENDEDOR_PROGRAMACION_GRABAR", p_Pedido, p_Usuario, p_dt);
        }
        public string ProgramacionMasiva(DataTable p_dt, string p_Usuario)
        {
            using (DbCommand cmd = PolyIntranetBD.GetStoredProcCommand("COM.PA_PED_PEDIDO_VENDEDOR_PROGRAMACION_MASIVA"))
            {
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@P_PED_PEDIDOS";
                parameter.SqlDbType = SqlDbType.Structured;
                parameter.Value = p_dt;
                cmd.Parameters.Add(parameter);
                                
                PolyIntranetBD.AddInParameter(cmd, "@P_USUARIO", DbType.String, p_Usuario);
                PolyIntranetBD.ExecuteNonQuery(cmd);

                return "";
            }
            //return _repoGeneric.ListarDatos("COM.PA_PED_PEDIDO_VENDEDOR_PROGRAMACION_GRABAR", p_Pedido, p_Usuario, p_dt);
        }
        public string Programar(string p_Pedido, string p_Observacion, string p_Estado, string p_Usuario)
        {
            using (DbCommand cmd = PolyIntranetBD.GetStoredProcCommand("COM.PA_PED_PEDIDO_VENDEDOR_PROGRAMAR"))
            {
                PolyIntranetBD.AddInParameter(cmd, "@P_PEDIDO", DbType.String, p_Pedido);
                PolyIntranetBD.AddInParameter(cmd, "@P_OBSERVACION", DbType.String, p_Observacion);
                PolyIntranetBD.AddInParameter(cmd, "@P_ESTADO", DbType.String, p_Estado);
                PolyIntranetBD.AddInParameter(cmd, "@P_USUARIO", DbType.String, p_Usuario);
                PolyIntranetBD.ExecuteNonQuery(cmd);

                return "";
            }
        }
        public DataTable ListarColoresExactus()
        {
            return _repoGeneric.ListarDatos("COM.PA_PED_PEDIDO_COLORES_EXACTUS_LISTAR");
        }
        #endregion

        #region Ventas
        public DataTable ListarPedidosVentas(string p_TipoFiltro, string p_ValorFiltro, string p_Usuario)
        {
            return _repoGeneric.ListarDatos("COM.PA_PED_PEDIDO_VENTAS_PENDIENTES_LISTAR", p_TipoFiltro, p_ValorFiltro, p_Usuario);
        }
        public DataTable ListarPedidosVentasDetalle(string p_Pedido)
        {
            return _repoGeneric.ListarDatos("COM.PA_PED_PEDIDO_VENTAS_ETIQUETAS_DETALLE_LISTAR", p_Pedido);
        }
        public string CargarEtiquetasExactus(DataTable p_dt, string p_Pedido, string p_Usuario)
        {
            using (DbCommand cmd = PolyIntranetBD.GetStoredProcCommand("COM.PA_PED_PEDIDO_VENTAS_ETIQUETAS_GRABAR"))
            {
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@P_PED_PROGRAMACION_ETIQUETAS_ARTICULO";
                parameter.SqlDbType = SqlDbType.Structured;
                parameter.Value = p_dt;
                cmd.Parameters.Add(parameter);

                PolyIntranetBD.AddInParameter(cmd, "@P_PEDIDO", DbType.String, p_Pedido);                
                PolyIntranetBD.AddInParameter(cmd, "@P_USUARIO", DbType.String, p_Usuario);
                PolyIntranetBD.ExecuteNonQuery(cmd);

                return "";
            }            
        }
        public string RechazarEtiquetas(DataTable p_dt, string p_Pedido, string p_Usuario)
        {
            using (DbCommand cmd = PolyIntranetBD.GetStoredProcCommand("COM.PA_PED_PEDIDO_VENTAS_ETIQUETAS_RECHAZAR"))
            {
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@P_PED_PROGRAMACION_ETIQUETAS_ARTICULO";
                parameter.SqlDbType = SqlDbType.Structured;
                parameter.Value = p_dt;
                cmd.Parameters.Add(parameter);

                PolyIntranetBD.AddInParameter(cmd, "@P_PEDIDO", DbType.String, p_Pedido);
                PolyIntranetBD.AddInParameter(cmd, "@P_USUARIO", DbType.String, p_Usuario);
                PolyIntranetBD.ExecuteNonQuery(cmd);

                return "";
            }
        }
        #endregion
    }
}
