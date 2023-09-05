using Polysol.Comercial.Entities;
using Polysol.Comercial.Logics;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Polysol.Comercial.WebApp.Views
{
    public partial class LecturaEtiqueta : PaginaBase
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    InicializarControles();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(eTipoAlerta.DANGER, ex.Message);
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }
        protected void grvPedidos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "SELECCIONAR")
                {
                    string[] _datos = e.CommandArgument.ToString().Split(';');
                    string pedido = _datos[0].ToString();
                    string programacion = _datos[1].ToString();

                    CargarPedido(pedido, programacion);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(eTipoAlerta.DANGER, ex.Message);
            }
        }
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            if (txtPEDIDO.Visible)
            {
                txtPEDIDO.Text = "";
                lblPed.Visible = false;
                txtPEDIDO.Visible = false;
            }
            else
            {
                txtPEDIDO.Text = "";
                lblPed.Visible = true;
                txtPEDIDO.Visible = true;
                txtPEDIDO.Focus();
            }
        }
        protected void btnEnviarCarga_Click(object sender, EventArgs e)
        {
            try
            {
                txtObservacion.Text = "";
                EjecutarScript("$('#modalEnviar').modal('show')");
                txtObservacion.Focus();
            }
            catch (Exception ex)
            {
                MostrarMensaje(eTipoAlerta.DANGER, ex.Message);
            }
        }
        protected void btnAnadirEtq_Click(object sender, EventArgs e)
        {
            if (btnAnadirEtq.CssClass == "btn btn-light btn-sm")
            {
                btnAnadirEtq.CssClass = "btn btn-success btn-sm";
                btnQuitarEtq.CssClass = "btn btn-light btn-sm";
                txtETIQUETAL.Visible = true;
                btnBUSCARETIQUETA.Visible = true;
                txtETIQUETAL.Enabled = true;
                txtETIQUETAL.Focus();
            }
            else
            {
                btnAnadirEtq.CssClass = "btn btn-light btn-sm";
                btnQuitarEtq.CssClass = "btn btn-light btn-sm";
                txtETIQUETAL.Visible = false;
                btnBUSCARETIQUETA.Visible = false;
                txtETIQUETAL.Text = "";
                txtETIQUETAL.Enabled = false;
            }
        }
        protected void btnQuitarEtq_Click(object sender, EventArgs e)
        {
            if (btnQuitarEtq.CssClass == "btn btn-light btn-sm")
            {
                btnQuitarEtq.CssClass = "btn btn-danger btn-sm";
                btnAnadirEtq.CssClass = "btn btn-light btn-sm";
                txtETIQUETAL.Visible = true;
                btnBUSCARETIQUETA.Visible = true;
                txtETIQUETAL.Enabled = true;
                txtETIQUETAL.Focus();
            }
            else
            {
                btnQuitarEtq.CssClass = "btn btn-light btn-sm";
                btnAnadirEtq.CssClass = "btn btn-light btn-sm";
                txtETIQUETAL.Visible = false;
                btnBUSCARETIQUETA.Visible = false;
                txtETIQUETAL.Text = "";
                txtETIQUETAL.Enabled = false;
            }
        }
        protected void btnBUSCARETIQUETA_Click(object sender, EventArgs e)
        {
            LeerEtiqueta();
        }
        protected void btnErroresEtq_Click(object sender, EventArgs e)
        {
            if (grvLogErrores.Rows.Count == 0)
                btnLimpiar.Visible = false;
            else
                btnLimpiar.Visible = true;

            EjecutarScript("$('#ModalLogErrores').modal('show')");
        }
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dsLogErrores = new PedidoBL().LimpiarLogCarga(__hfPedido.Value.ToString(), __hfProgramado.Value.ToString());

                if (dsLogErrores != null)
                {
                    CargarProgramacion(dsLogErrores.Tables[0], dsLogErrores.Tables[1], dsLogErrores.Tables[2], dsLogErrores.Tables[3].Rows[0]["ESTADO_DESCRIPCION"].ToString(), dsLogErrores.Tables[4].Rows[0]["OBSERVACION_VENDEDOR"].ToString());
                    MostrarMensaje(eTipoAlerta.SUCCESS, "La operación se realizó exitosamente");
                    EjecutarScript("$('#ModalLogErrores').modal('hide')");
                }
                else
                {
                    MostrarMensaje(eTipoAlerta.DANGER, "No se pudo cargar la información, es posible que ya no se encuentre disponible.");
                    EjecutarScript("$('#ModalLogErrores').modal('hide')");
                    EjecutarScript("$('#ModalDetalleProgramacion').modal('hide')");
                    CargarGrilla();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(eTipoAlerta.DANGER, ex.Message);
            }
        }
        protected void btnCancelarCarga_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dsResultado = new PedidoBL().LimpiarCarga(
                  __hfPedido.Value.ToString(),
                  __hfProgramado.Value.ToString());

                CargarProgramacion(dsResultado.Tables[0], dsResultado.Tables[1], dsResultado.Tables[2], dsResultado.Tables[3].Rows[0]["ESTADO_DESCRIPCION"].ToString(), dsResultado.Tables[4].Rows[0]["OBSERVACION_VENDEDOR"].ToString());

                btnAnadirEtq.CssClass = "btn btn-light btn-sm";
                btnQuitarEtq.CssClass = "btn btn-light btn-sm";

                txtETIQUETAL.Visible = false;
                btnBUSCARETIQUETA.Visible = false;

                txtETIQUETAL.Text = "";
                txtETIQUETAL.Enabled = false;

                MostrarMensaje(eTipoAlerta.SUCCESS, "La operación se realizó exitosamente");
            }
            catch (Exception ex)
            {
                MostrarMensaje(eTipoAlerta.DANGER, ex.Message);
            }
        }
        protected void grvPedidos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = (DataRowView)e.Row.DataItem;

                    string fecha_programada;
                    fecha_programada = dr.Row["PROGRAMADO"].ToString();

                    if (Convert.ToDateTime(fecha_programada) <= Convert.ToDateTime(DateTime.Today.ToShortDateString()))
                    {
                        e.Row.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(eTipoAlerta.DANGER, ex.Message);
            }
        }
        protected void grvDetalleProgramacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = (DataRowView)e.Row.DataItem;

                    decimal cantidad_pedida;
                    decimal cantidad_cargada;
                    decimal cantidad_restante;

                    cantidad_pedida = Convert.ToDecimal(dr.Row["CANTIDAD"].ToString());
                    cantidad_cargada = Convert.ToDecimal(dr.Row["CARGA"].ToString());
                    cantidad_restante = cantidad_pedida - cantidad_cargada;

                    if (cantidad_pedida == cantidad_cargada)
                    {
                        e.Row.ForeColor = Color.Green;
                    }
                    else if (cantidad_cargada > cantidad_pedida)
                    {
                        e.Row.ForeColor = Color.Green;
                        e.Row.Cells[5].ForeColor = Color.Red;
                        e.Row.Cells[6].ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(eTipoAlerta.DANGER, ex.Message);
            }
        }
        #endregion

        #region Metodos
        private void CargarGrilla()
        {
            var _pedidos = PedidoBL.Instancia.ListarPedidosAlmacen(txtPEDIDO.Text.ToString());
            grvPedidos.DataSource = _pedidos;
            grvPedidos.DataBind();

            if (grvPedidos.Rows.Count == 1)
            {
                string pedido = _pedidos.Rows[0]["PEDIDO"].ToString();
                string programacion = _pedidos.Rows[0]["PROGRAMADO"].ToString();
                CargarPedido(pedido, programacion);
            }
        }
        private void InicializarControles()
        {
            txtPEDIDO.Visible = false;
            lblPed.Visible = false;
            CargarGrilla();
        }
        private void CargarPedido(string pedido, string programacion)
        {
            lblDetalle.Text = "Pedido: " + pedido + "    /    " + " Prog: " + programacion;

            __hfPedido.Value = pedido;
            __hfProgramado.Value = programacion;

            DataSet dSDetalleProgramacion = new PedidoBL().ListarPedidosAlmacenDetalle(pedido, programacion);
            if (dSDetalleProgramacion != null)
            {
                CargarProgramacion(dSDetalleProgramacion.Tables[0], dSDetalleProgramacion.Tables[1], dSDetalleProgramacion.Tables[2], dSDetalleProgramacion.Tables[3].Rows[0]["ESTADO_DESCRIPCION"].ToString(), dSDetalleProgramacion.Tables[4].Rows[0]["OBSERVACION_VENDEDOR"].ToString());
            }
            else
            {
                MostrarMensaje(eTipoAlerta.DANGER, "No se pudo cargar la información, es posible que ya no se encuentre disponible.");
                CargarGrilla();
                return;
            }

            txtETIQUETAL.Visible = false;
            btnBUSCARETIQUETA.Visible = false;

            txtETIQUETAL.Text = "";
            txtETIQUETAL.Enabled = false;

            btnAnadirEtq.CssClass = "btn btn-light btn-sm";
            btnQuitarEtq.CssClass = "btn btn-light btn-sm";

            EjecutarScript("$('#ModalDetalleProgramacion').modal('show')");
        }
        private void LeerEtiqueta()
        {
            try
            {
                if (btnAnadirEtq.CssClass == "btn btn-success btn-sm" ||
               btnQuitarEtq.CssClass == "btn btn-danger btn-sm")
                {
                    if (txtETIQUETAL.Text.Trim() == "")
                    {
                        MostrarMensaje(eTipoAlerta.DANGER, "No se ha ingresado la etiqueta");
                        txtETIQUETAL.Focus();
                        return;
                    }

                    DataSet dsEtiquetas;

                    if (btnAnadirEtq.CssClass == "btn btn-success btn-sm")
                    {
                        dsEtiquetas = new PedidoBL().
                        InsertarEtiquetasAlmacen(
                        __hfPedido.Value.ToString(),
                        __hfProgramado.Value.ToString(),
                        txtETIQUETAL.Text.Trim(),
                        CredencialActiva.UsuarioAlias);
                    }
                    else
                    {
                        dsEtiquetas = new PedidoBL().
                        EliminarEtiquetasAlmacen(
                        __hfPedido.Value.ToString(),
                        __hfProgramado.Value.ToString(),
                        txtETIQUETAL.Text.Trim(),
                        CredencialActiva.UsuarioAlias);
                    }

                    if (dsEtiquetas != null)
                    {
                        CargarProgramacion(dsEtiquetas.Tables[1], dsEtiquetas.Tables[2], dsEtiquetas.Tables[3], dsEtiquetas.Tables[4].Rows[0]["ESTADO_DESCRIPCION"].ToString(), dsEtiquetas.Tables[5].Rows[0]["OBSERVACION_VENDEDOR"].ToString());

                        if (dsEtiquetas.Tables[0].Rows[0]["CARGADO"].ToString() == "N")
                        {
                            MostrarMensaje(eTipoAlerta.DANGER, dsEtiquetas.Tables[0].Rows[0]["RESULTADO"].ToString());
                        }

                        txtETIQUETAL.Text = "";
                        txtETIQUETAL.Focus();
                    }
                    else
                    {
                        MostrarMensaje(eTipoAlerta.DANGER, "No se pudo cargar la información, es posible que ya no se encuentre disponible.");
                        EjecutarScript("$('#ModalDetalleProgramacion').modal('hide')");
                        CargarGrilla();
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(eTipoAlerta.DANGER, ex.Message);
            }
        }
        private void CargarProgramacion(DataTable dtEtiquetas, DataTable dtProgramacion, DataTable dtLogErrores, string estado_descripcion, string observacion_vendedor)
        {
            lblEstadoPedido.Text = estado_descripcion;
            lblObservacionVendedor.Text = observacion_vendedor;

            grvDetalleEtiquetas.DataSource = dtEtiquetas;
            grvDetalleEtiquetas.DataBind();

            grvDetalleProgramacion.DataSource = dtProgramacion;
            grvDetalleProgramacion.DataBind();

            grvLogErrores.DataSource = dtLogErrores;
            grvLogErrores.DataBind();

            lblNroEtiquetasCargadas.Text = dtEtiquetas.Rows.Count.ToString();

            if (dtLogErrores.Rows.Count > 0)
            {
                btnErroresEtq.Visible = true;
                btnErroresEtq.Text = dtLogErrores.Rows.Count.ToString() + " ERRORES";
            }
            else
            {
                btnErroresEtq.Visible = false;
                btnErroresEtq.Text = "";
            }

            if (dtEtiquetas.Rows.Count > 0)
            {
                btnEnviarCarga.Visible = true;
                btnCancelarCarga.Visible = true;
            }
            else
            {
                btnEnviarCarga.Visible = false;
                btnCancelarCarga.Visible = false;
            }

            if (dtProgramacion.Rows.Count == 0)
            {
                MostrarMensaje(eTipoAlerta.DANGER, "No se pudo cargar la información, es posible que ya no se encuentre disponible.");
                EjecutarScript("$('#ModalDetalleProgramacion').modal('hide')");
                CargarGrilla();
            }
        }
        #endregion

        protected void btnEnviarEtiquetas_Click(object sender, EventArgs e)
        {
            new PedidoBL().EnviarCarga(__hfPedido.Value.ToString(), __hfProgramado.Value.ToString(), txtObservacion.Text.Trim(), CredencialActiva.UsuarioAlias);
            MostrarMensaje(eTipoAlerta.SUCCESS, "La operación se realizó exitosamente");
            EjecutarScript("$('#modalEnviar').modal('hide')");
            EjecutarScript("$('#ModalDetalleProgramacion').modal('hide')");
            CargarGrilla();
        }
    }
}