using Polysol.Comercial.DataAccess;
using System.Data;

namespace Polysol.Comercial.Logics
{
    public class PedidoBL : Singleton<PedidoBL>
    {
        PedidoDA _repositorio = new PedidoDA();

        #region Almacen
        public DataSet ListarPedidosAlmacenPaginado(string p_Pedido, int p_StartRowIndex, int p_MaximumRows)
        {
            return _repositorio.ListarPedidosAlmacenPaginado(p_Pedido, p_StartRowIndex, p_MaximumRows);
        }
        public DataTable ListarPedidosAlmacen(string p_Pedido)
        {
            return _repositorio.ListarPedidosAlmacen(p_Pedido);
        }
        public DataSet ListarPedidosAlmacenDetalle(string p_Pedido, string p_Programado)
        {
            return _repositorio.ListarPedidosAlmacenDetalle(p_Pedido, p_Programado);
        }
        public DataSet InsertarEtiquetasAlmacen(string p_Pedido, string p_Programado, string p_Etiqueta, string p_Usuario)
        {
            return _repositorio.InsertarEtiquetasAlmacen(p_Pedido, p_Programado, p_Etiqueta, p_Usuario);
        }
        public DataSet EliminarEtiquetasAlmacen(string p_Pedido, string p_Programado, string p_Etiqueta, string p_Usuario)
        {
            return _repositorio.EliminarEtiquetasAlmacen(p_Pedido, p_Programado, p_Etiqueta, p_Usuario);
        }
        public DataSet LimpiarLogCarga(string p_Pedido, string p_Programado)
        {
            return _repositorio.LimpiarLogCarga(p_Pedido, p_Programado);
        }
        public DataSet LimpiarCarga(string p_Pedido, string p_Programado)
        {
            return _repositorio.LimpiarCarga(p_Pedido, p_Programado);
        }
        public void EnviarCarga(string p_Pedido, string p_Programado, string p_Observacion, string p_Usuario)
        {
            _repositorio.EnviarCarga(p_Pedido, p_Programado, p_Observacion, p_Usuario);
        }
        #endregion

        #region Vendedor
        public DataTable ListarPedidosVendedor(string p_NoProgramado, string p_ValorFiltro, string p_Usuario)
        {
            return _repositorio.ListarPedidosVendedor(p_NoProgramado, p_ValorFiltro, p_Usuario);
        }
        public DataTable ListarPedidosVendedorProgramacion(string p_Pedido)
        {
            return _repositorio.ListarPedidosVendedorProgramacion(p_Pedido);
        }
        public string PartirPedidosVendedorProgramacion(DataTable p_dt, string p_Pedido, int p_Enc_Pedido_Linea, int p_Pedido_Programacion, decimal p_Cantidad, string p_Usuario)
        {
            return _repositorio.PartirPedidosVendedorProgramacion(p_dt, p_Pedido, p_Enc_Pedido_Linea, p_Pedido_Programacion, p_Cantidad, p_Usuario);
        }
        public string ModificarColorProgramacion(string p_Pedido, int p_Enc_Pedido_Linea, int p_Pedido_Programacion, string p_Color, string p_Usuario)
        {
            return _repositorio.ModificarColorProgramacion(p_Pedido, p_Enc_Pedido_Linea, p_Pedido_Programacion, p_Color, p_Usuario);
        }
        public string GrabarCambiosProgramacion(DataTable p_dt, string p_Pedido, string p_Observacion, string p_Usuario)
        {
            return _repositorio.GrabarCambiosProgramacion(p_dt, p_Pedido, p_Observacion, p_Usuario);
        }
        public string ProgramacionMasiva(DataTable p_dt, string p_Usuario)
        {
            return _repositorio.ProgramacionMasiva(p_dt, p_Usuario);
        }
        public string Programar(string p_Pedido, string p_Observacion, string p_Estado, string p_Usuario)
        {
            return _repositorio.Programar(p_Pedido, p_Observacion, p_Estado, p_Usuario);
        }
        public DataTable ListarColoresExactus()
        {
            return _repositorio.ListarColoresExactus();
        }
        #endregion

        #region Ventas
        public DataTable ListarPedidosVentas(string p_TipoFiltro, string p_ValorFiltro, string p_Usuario)
        {
            return _repositorio.ListarPedidosVentas(p_TipoFiltro, p_ValorFiltro, p_Usuario);
        }
        public DataTable ListarPedidosVentasDetalle(string p_Pedido)
        {
            return _repositorio.ListarPedidosVentasDetalle(p_Pedido);
        }
        public string CargarEtiquetasExactus(DataTable p_dt, string p_Pedido, string p_Usuario)
        {
            return _repositorio.CargarEtiquetasExactus(p_dt, p_Pedido, p_Usuario);
        }
        public string RechazarEtiquetas(DataTable p_dt, string p_Pedido, string p_Usuario)
        {
            return _repositorio.RechazarEtiquetas(p_dt, p_Pedido, p_Usuario);
        }
        #endregion
    }
}
