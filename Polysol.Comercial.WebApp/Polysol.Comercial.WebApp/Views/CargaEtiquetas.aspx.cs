using Polysol.Comercial.Entities;
using Polysol.Comercial.Logics;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace Polysol.Comercial.WebApp.Views
{
    public partial class CargaEtiquetas : PaginaBase
    {
        #region Metodos
        private void CargarDetalle(string pedido)
        {
            grvDetalleProgramacion.DataSource = new PedidoBL().ListarPedidosVentasDetalle(pedido);
            grvDetalleProgramacion.DataBind();
        }
        private void Inicializar()
        {
            DataTable dtFiltros = new DataTable();
            dtFiltros.Columns.Add("CODIGO", typeof(string));
            dtFiltros.Columns.Add("DESCRIPCION", typeof(string));
            dtFiltros.Rows.Add("PE", "PEDIDO");
            dtFiltros.Rows.Add("CL", "CLIENTE");
            CargarCombo(cbo_TipoFiltro, dtFiltros, "CODIGO", "DESCRIPCION", true, true);
            CargarGrilla();
        }
        private void CargarGrilla()
        {
            DataTable dtPedidos = new PedidoBL().ListarPedidosVentas(
                (cbo_TipoFiltro.Visible ? cbo_TipoFiltro.SelectedValue.ToString() : ""),
                (txt_ValorFiltro.Visible ? txt_ValorFiltro.Text.Trim() : ""),
                CredencialActiva.UsuarioAlias);
            grvPedidos.DataSource = dtPedidos;
            grvPedidos.DataBind();
        }
        private void CalcularEtiquetasSeleccionadas()
        {
            DataTable dt = ObtenerEtiquetasSeleccionadas();

            if (dt.Rows.Count > 0)
            {
                DataView view = new DataView(dt);
                string[] columnas = { "ARTICULO", "LOTE" };
                DataTable distinctValues = view.ToTable(true, columnas);
                lblNroLineasSeleccionadas.Text = distinctValues.Rows.Count.ToString();

                lblMontoSeleccionado.Text = Convert.ToDecimal(dt.Compute("SUM(SUBTOTAL)", "ENC_PEDIDO_LINEA > 0")).ToString("N2");
                lblCantidadSeleccionada.Text = Convert.ToDecimal(dt.Compute("SUM(CANTIDAD)", "ENC_PEDIDO_LINEA > 0")).ToString("N2");
            }
            else
            {
                lblNroLineasSeleccionadas.Text = "0";
                lblMontoSeleccionado.Text = "0.00";
                lblCantidadSeleccionada.Text = "0.00";
            }
        }
        private DataTable ObtenerEtiquetasSeleccionadas()
        {
            DataTable _dt = new DataTable();
            _dt.Columns.Add("PEDIDO", typeof(string));
            _dt.Columns.Add("ENC_PEDIDO_LINEA", typeof(int));
            _dt.Columns.Add("PEDIDO_PROGRAMACION", typeof(int));
            _dt.Columns.Add("ID_ETIQUETA", typeof(int));
            _dt.Columns.Add("CANTIDAD", typeof(decimal));
            _dt.Columns.Add("SUBTOTAL", typeof(decimal));
            _dt.Columns.Add("ARTICULO", typeof(string));
            _dt.Columns.Add("LOTE", typeof(string));

            if (grvDetalleProgramacion.Rows.Count > 0)
            {
                foreach (GridViewRow fila in grvDetalleProgramacion.Rows)
                {
                    HiddenField __PEDIDO = (HiddenField)fila.FindControl("__PEDIDO");
                    HiddenField __ENC_PEDIDO_LINEA = (HiddenField)fila.FindControl("__ENC_PEDIDO_LINEA");
                    HiddenField __PEDIDO_PROGRAMACION = (HiddenField)fila.FindControl("__PEDIDO_PROGRAMACION");
                    HiddenField __ID_ETIQUETA = (HiddenField)fila.FindControl("__ID_ETIQUETA");
                    HiddenField __CANTIDAD = (HiddenField)fila.FindControl("__CANTIDAD");
                    HiddenField __SUBTOTAL = (HiddenField)fila.FindControl("__SUBTOTAL");
                    HiddenField __ARTICULO = (HiddenField)fila.FindControl("__ARTICULO");
                    HiddenField __LOTE = (HiddenField)fila.FindControl("__LOTE");

                    CheckBox chk = (CheckBox)fila.FindControl("CHK");

                    if (chk.Checked)
                    {
                        DataRow _dr = _dt.NewRow();
                        _dr["PEDIDO"] = __PEDIDO.Value.ToString();
                        _dr["ENC_PEDIDO_LINEA"] = Convert.ToInt32(__ENC_PEDIDO_LINEA.Value);
                        _dr["PEDIDO_PROGRAMACION"] = Convert.ToInt32(__PEDIDO_PROGRAMACION.Value);
                        _dr["ID_ETIQUETA"] = Convert.ToInt32(__ID_ETIQUETA.Value);
                        _dr["CANTIDAD"] = Convert.ToDecimal(__CANTIDAD.Value);
                        _dr["SUBTOTAL"] = Convert.ToDecimal(__SUBTOTAL.Value);
                        _dr["ARTICULO"] = __ARTICULO.Value.ToString();
                        _dr["LOTE"] = __LOTE.Value.ToString();

                        _dt.Rows.Add(_dr);
                    }
                }
                return _dt;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Eventos
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                CargarGrilla();
            }
            catch (Exception ex)
            {
                string msj = ex.Message;
                grvPedidos.DataSource = null;
                grvPedidos.DataBind();

                //MostrarMensaje(ex.Message);
            }
        }
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            if (cbo_TipoFiltro.Visible)
            {
                cbo_TipoFiltro.Visible = false;
                txt_ValorFiltro.Visible = false;
                //btnFiltrar.Text = "Menos filtros";
            }
            else
            {
                cbo_TipoFiltro.Visible = true;
                txt_ValorFiltro.Visible = true;
                //btnFiltrar.Text = "Más filtros";
            }
        }
        protected void btnCargar_Click(object sender, EventArgs e)
        {
            if (grvDetalleProgramacion.Rows.Count > 0)
            {
                try
                {
                    DataTable _dt = ObtenerEtiquetasSeleccionadas();
                    if (_dt != null)
                    {
                        if (_dt.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(lblNroLineasSeleccionadas.Text) <= 12)
                            {
                                string sResultado = new PedidoBL().CargarEtiquetasExactus(_dt, _dt.Rows[0]["PEDIDO"].ToString(), CredencialActiva.UsuarioAlias);
                                CargarGrilla();
                                EjecutarScript("$('#ModalDetalleEtiquetas').modal('hide')");
                            }
                            else
                            {
                                MostrarMensaje(eTipoAlerta.DANGER, "No puede cargar más de 12 líneas simultáneamente. Por favor verifique. Ya????");
                            }
                        }
                        else
                        {
                            MostrarMensaje(eTipoAlerta.DANGER, "No ha seleccionado etiquetas");
                        }
                    }
                    else
                    {
                        MostrarMensaje(eTipoAlerta.DANGER, "No ha seleccionado etiquetas");
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje(eTipoAlerta.DANGER, ex.Message);
                }
            }
            else
            {
                MostrarMensaje(eTipoAlerta.DANGER, "No se encontraron etiquetas");
            }
        }
        protected void CHKALL_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = new CheckBox();
            chk = (CheckBox)grvDetalleProgramacion.HeaderRow.FindControl("CHKALL");
            bool valor = chk.Checked;

            foreach (GridViewRow dtgItem in grvDetalleProgramacion.Rows)
            {
                CheckBox chk_item = ((CheckBox)grvDetalleProgramacion.Rows[dtgItem.RowIndex].FindControl("CHK"));
                chk_item.Checked = valor;
            }
            CalcularEtiquetasSeleccionadas();
        }
        protected void CHK_CheckedChanged(object sender, EventArgs e)
        {
            CalcularEtiquetasSeleccionadas();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Inicializar();
        }
        protected void grvPedidos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string pedido = e.CommandArgument.ToString();

                switch (e.CommandName)
                {
                    case "DETALLE":

                        CargarDetalle(pedido);
                        lblDetalle.Text = "SELECCIÓN DE ETIQUETAS - PEDIDO: " + pedido;
                        lblNroLineasSeleccionadas.Text = "0";
                        lblCantidadSeleccionada.Text = "0.00";
                        lblMontoSeleccionado.Text = "0.00";
                        EjecutarScript("$('#ModalDetalleEtiquetas').modal('show');");
                        break;
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(eTipoAlerta.DANGER, ex.Message);
            }
        }
        protected void btnRechazar_Click(object sender, EventArgs e)
        {
            if (grvDetalleProgramacion.Rows.Count > 0)
            {
                try
                {
                    DataTable _dt = ObtenerEtiquetasSeleccionadas();
                    if (_dt != null)
                    {
                        if (_dt.Rows.Count > 0)
                        {
                            string sResultado = new PedidoBL().RechazarEtiquetas(_dt, _dt.Rows[0]["PEDIDO"].ToString(), CredencialActiva.UsuarioAlias);
                            CargarDetalle(_dt.Rows[0]["PEDIDO"].ToString());
                        }
                        else
                        {
                            MostrarMensaje(eTipoAlerta.DANGER, "No ha seleccionado etiquetas");
                        }
                    }
                    else
                    {
                        MostrarMensaje(eTipoAlerta.DANGER, "No ha seleccionado etiquetas");
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje(eTipoAlerta.DANGER, ex.Message);
                }
            }
            else
            {
                MostrarMensaje(eTipoAlerta.DANGER, "No se encontraron etiquetas");
            }
        }
        #endregion
    }
}