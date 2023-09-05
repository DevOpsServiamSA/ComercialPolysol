using Polysol.Comercial.Entities;
using Polysol.Comercial.Logics;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;

namespace Polysol.Comercial.WebApp.Views
{
    public partial class PedidosPendientes : PaginaBase
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Inicializar();
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                CargarGrilla();
            }
            catch (Exception ex)
            {
                grvDatos.DataSource = null;
                grvDatos.DataBind();

                MostrarMensaje(eTipoAlerta.DANGER, ex.Message);
            }
        }
        protected void grvDatos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string[] pedido = e.CommandArgument.ToString().Split(',');

                switch (e.CommandName)
                {
                    case "PROGRAMAR":
                        CargarDetalleProgramacion(pedido[0]);
                        lblDetalle.Text = "PROGRAMAR DESPACHO DEL PEDIDO: " + pedido[0];
                        txtObservacionProg.Text = pedido[1];
                        lblCONDICIONPAGO.Text = pedido[2];

                        EjecutarScript("$('#modalDetalleProgramacion').modal('show');");
                        break;

                    case "NOPROGRAMAR":
                        __hfOperacion.Value = "S";
                        __hfPedidoPro.Value = pedido[0];
                        lblObservacion.Text = "NO PROGRAMAR DESPACHO DEL PEDIDO: " + pedido[0];
                        txtObservacion.Text = pedido[1];
                        EjecutarScript("$('#modalObservacion').modal('show');");
                        break;
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(eTipoAlerta.DANGER, ex.Message);
            }
        }
        protected void grvDatos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = (DataRowView)e.Row.DataItem;

                    string estado_programacion;
                    estado_programacion = dr.Row["ESTADO_PROGRAMACION"].ToString();

                    switch (estado_programacion)
                    {
                        case "P":
                            e.Row.ForeColor = Color.Green;
                            break;

                        case "S":
                            e.Row.ForeColor = Color.Red;
                            break;

                        case "A":
                            e.Row.ForeColor = Color.Orange;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(eTipoAlerta.DANGER, ex.Message);
            }
        }
        protected void btnOperacion_Click(object sender, EventArgs e)
        {
            try
            {
                string sresultado = new PedidoBL().Programar(__hfPedidoPro.Value.ToString(), txtObservacion.Text.Trim(), __hfOperacion.Value.ToString(), CredencialActiva.UsuarioAlias);
                CargarGrilla();
                EjecutarScript("$('#modalObservacion').modal('hide')");
            }
            catch (Exception ex)
            {
                MostrarMensaje(eTipoAlerta.DANGER, ex.Message);
            }
        }
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            txt_ValorFiltro.Visible = !txt_ValorFiltro.Visible;
            lblFiltro.Visible = !lblFiltro.Visible;
            txt_ValorFiltro.Focus();
        }
        protected void chk_NOPROGRAMADO_CheckedChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }
        protected void chk_TODOS_CheckedChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }
        protected void chk_PROGRAMADO_CheckedChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }
        protected void grvProgramacionDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string[] pedido = e.CommandArgument.ToString().Split(',');

                switch (e.CommandName)
                {
                    case "PARTIR":
                        GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                        int RowIndex = gvr.RowIndex;
                        string valida = ValidaParticion(RowIndex);

                        if (valida != "")
                        {
                            MostrarMensaje(eTipoAlerta.DANGER, valida);
                        }
                        else
                        {
                            lblCANTIDADACTUAL.Text = "Cantidad actual: " + pedido[6].ToString() + " " + pedido[5].ToString();
                            lblNUEVACANTIDAD.Text = "Nueva cantidad";
                            lblUNIDADPART.Text = " " + pedido[5].ToString();

                            __hfPEDIDO.Value = pedido[0];
                            __hfENC_PEDIDO_LINEA.Value = pedido[1];
                            __hfPEDIDO_PROGRAMACION.Value = pedido[2];
                            __hfUNIDAD.Value = pedido[5].ToString();
                            __hfCANTIDAD.Value = pedido[6].ToString();

                            txtCANTPROG1.Text = "";
                            txtCANTPROG1.Focus();

                            EjecutarScript("$('#modalPartirProgramacion').modal('show');");
                        }
                        break;

                    case "EDITAR_COLOR":

                        __hfCPedido.Value = pedido[0];
                        __hfCEnc_Pedido_Linea.Value = pedido[1];
                        __hfCPedido_Programacion.Value = pedido[2];
                        __hfColor.Value = pedido[3].ToString();

                        ddlColor.DataSource = new PedidoBL().ListarColoresExactus();
                        ddlColor.DataTextField = "DESCRIPCION";
                        ddlColor.DataValueField = "CODIGO";
                        ddlColor.DataBind();

                        //Selecciona el primero por defecto                                        
                        ddlColor.SelectedValue = __hfColor.Value != "" ? __hfColor.Value : "0";
                        EjecutarScript("$('#modalSeleccionarColor').modal('show');");

                        break;
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(eTipoAlerta.DANGER, ex.Message);
            }
        }
        protected void grvProgramacionDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtPROGRAMADO = (TextBox)e.Row.FindControl("txtPROGRAMADO");
                    //DropDownList ddlCOLOR = (DropDownList)e.Row.FindControl("ddlCOLOR");
                    LinkButton btnPartirProgramacion = (LinkButton)e.Row.FindControl("btnPartirProgramacion");

                    DataRowView dr = (DataRowView)e.Row.DataItem;

                    string estado_programacion;
                    estado_programacion = dr.Row["ESTADO"].ToString();

                    switch (estado_programacion)
                    {
                        case "P":
                            txtPROGRAMADO.ReadOnly = false;
                            //ddlCOLOR.Enabled = true;
                            btnPartirProgramacion.Visible = true;
                            break;

                        case "S":
                            txtPROGRAMADO.ReadOnly = false;
                            //ddlCOLOR.Enabled = true;
                            btnPartirProgramacion.Visible = true;
                            break;

                        default:
                            txtPROGRAMADO.ReadOnly = false;
                            //txtPROGRAMADO.ReadOnly = true;
                            //ddlCOLOR.Enabled = true;
                            //btnPartirProgramacion.Visible = false;
                            btnPartirProgramacion.Visible = true;
                            break;
                    }

                    //string COLOR = ((DataTable)grvProgramacionDetalle.DataSource).Rows[e.Row.RowIndex]["COLOR"].ToString();

                    //ddlCOLOR.DataSource = (DataTable)ViewState["COLORES"];
                    //ddlCOLOR.DataTextField = "DESCRIPCION";
                    //ddlCOLOR.DataValueField = "CODIGO";
                    //ddlCOLOR.DataBind();

                    //Selecciona el primero por defecto                                        
                    //ddlCOLOR.SelectedValue = COLOR != "" ? COLOR : "0";

                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(eTipoAlerta.DANGER, ex.Message);
            }
        }
        protected void btnGrabarParticion_Click(object sender, EventArgs e)
        {
            try
            {
                GrabarCambiosProgramacion();
                MostrarMensaje(eTipoAlerta.SUCCESS, "La operación se realizó exitosamente");
                EjecutarScript("$('#modalDetalleProgramacion').modal('hide')");
            }
            catch (Exception ex)
            {
                MostrarMensaje(eTipoAlerta.DANGER, ex.Message);
            }
        }
        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            EjecutarScript("$('#modalDetalleProgramacion').modal('hide')");
        }
        protected void btnPartir_Click(object sender, EventArgs e)
        {
            if (__hfCANTIDAD.Value.ToString() != "" && Convert.ToDecimal(__hfCANTIDAD.Value.ToString()) > 0 && Convert.ToDecimal(__hfCANTIDAD.Value.ToString()) > Convert.ToDecimal(txtCANTPROG1.Text.Trim()))
            {
                try
                {
                    string sresultado = new PedidoBL().PartirPedidosVendedorProgramacion(ObtenerDTProgramacion(), __hfPEDIDO.Value.ToString(), Convert.ToInt32(__hfENC_PEDIDO_LINEA.Value.ToString()), Convert.ToInt32(__hfPEDIDO_PROGRAMACION.Value.ToString()), Convert.ToDecimal(txtCANTPROG1.Text.Trim()), CredencialActiva.UsuarioAlias);
                    GrabarCambiosProgramacion();
                    EjecutarScript("$('#modalPartirProgramacion').modal('hide')");
                }
                catch (Exception ex)
                {
                    MostrarMensaje(eTipoAlerta.DANGER, ex.Message);
                }
            }
            else
            {
                MostrarMensaje(eTipoAlerta.DANGER, "Ingrese una cantidad válida");
                return;
            }
        }
        protected void btnMasivo_Click(object sender, EventArgs e)
        {
            if (grvDatos.Rows.Count > 0)
            {
                try
                {
                    DataTable _dt = ObtenerPedidosSeleccionados();
                    if (_dt != null)
                    {
                        string sResultado = new PedidoBL().ProgramacionMasiva(_dt, CredencialActiva.UsuarioAlias);
                        MostrarMensaje(eTipoAlerta.SUCCESS, "La operación se realizó exitosamente");
                        CargarGrilla();
                    }
                    else
                    {
                        MostrarMensaje(eTipoAlerta.DANGER, "No se encontraron Pedidos");
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje(eTipoAlerta.DANGER, ex.Message);
                }
            }
            else
            {
                MostrarMensaje(eTipoAlerta.DANGER, "No se encontraron Pedidos");
            }
        }
        protected void CHKALL_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = new CheckBox();
            chk = (CheckBox)grvDatos.HeaderRow.FindControl("CHKALL");
            bool valor = chk.Checked;

            foreach (GridViewRow dtgItem in grvDatos.Rows)
            {
                if (grvDatos.Rows[dtgItem.RowIndex].Cells[15].Text.Trim() == "NO PROG.")
                {
                    CheckBox chk_item = ((CheckBox)grvDatos.Rows[dtgItem.RowIndex].FindControl("CHK"));
                    chk_item.Checked = valor;
                }
                else
                {
                    CheckBox chk_item = ((CheckBox)grvDatos.Rows[dtgItem.RowIndex].FindControl("CHK"));
                    chk_item.Checked = false;
                }
            }
        }
        protected void CHK_CheckedChanged(object sender, EventArgs e)
        {
            //Cuando se hace uno por uno, se debe validar que solo permita seleccionar los que se encuentran en estado 'S' (SIN PROGRAMAR)
            //Aquí debe validar que sólo se pueda seleccionar los que se encuentran NO PROGRAMADOS
        }
        protected void btnGrabarColor_Click(object sender, EventArgs e)
        {
            try
            {
                string sresultado = new PedidoBL().ModificarColorProgramacion(__hfCPedido.Value.ToString(), Convert.ToInt32(__hfCEnc_Pedido_Linea.Value.ToString()), Convert.ToInt32(__hfCPedido_Programacion.Value), ddlColor.SelectedValue.ToString(), CredencialActiva.UsuarioAlias);
                CargarDetalleProgramacion(__hfCPedido.Value);
                CargarGrilla();

                EjecutarScript("$('#modalSeleccionarColor').modal('hide')");
                ddlColor.DataSource = null;
            }
            catch (Exception ex)
            {
                MostrarMensaje(eTipoAlerta.DANGER, ex.Message);
            }
        }
        #endregion

        #region Metodos
        private void CargarDetalleProgramacion(string pedido)
        {
            try
            {
                DataTable dtProgramacion = new PedidoBL().ListarPedidosVendedorProgramacion(pedido);
                grvProgramacionDetalle.DataSource = dtProgramacion;
                grvProgramacionDetalle.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje(eTipoAlerta.DANGER, ex.Message);
            }
        }
        private void Inicializar()
        {
            try
            {
                //DataTable dtFiltros = new DataTable();
                //dtFiltros.Columns.Add("CODIGO", typeof(string));
                //dtFiltros.Columns.Add("DESCRIPCION", typeof(string));
                //dtFiltros.Rows.Add("PE", "PEDIDO");
                //dtFiltros.Rows.Add("CL", "CLIENTE");
                //CargarCombo(cbo_TipoFiltro, dtFiltros, "CODIGO", "DESCRIPCION", false, false);
                chk_NOPROGRAMADO.Checked = true;

                //ViewState["COLORES"] = new PedidoBL().ListarColoresExactus();
                CargarGrilla();
            }
            catch (Exception ex)
            {
                MostrarMensaje(eTipoAlerta.DANGER, ex.Message);
            }
        }
        private void CargarGrilla()
        {
            try
            {
                DataTable dtPedidos = new PedidoBL().ListarPedidosVendedor(
                (chk_NOPROGRAMADO.Checked ? "S" : chk_PROGRAMADO.Checked ? "N" : "T"),
                //(cbo_TipoFiltro.Visible ? cbo_TipoFiltro.SelectedValue.ToString() : ""),                
                (txt_ValorFiltro.Visible ? txt_ValorFiltro.Text.Trim() : ""),
                CredencialActiva.UsuarioAlias);
                grvDatos.DataSource = dtPedidos;
                grvDatos.DataBind();

                //lblGlosa.Text = "<b>Total Fact (Sin IGV): </b>";
                lblGlosa.Text = "<b>Total Fact: </b>";

                if (dtPedidos != null && dtPedidos.Rows.Count > 0)
                {
                    if (dtPedidos.Compute("SUM(MONTO)", "MONEDA='S/.'") != null && dtPedidos.Compute("SUM(MONTO)", "MONEDA='S/.'").ToString() != "")
                    {
                        lblTotalMontoSoles.Text = "<b>S/: </b>" + Convert.ToDecimal(dtPedidos.Compute("SUM(MONTO)", "MONEDA='S/.'")).ToString("N2");
                        lblSepara1.Text = " | ";
                    }
                    else
                    {
                        lblTotalMontoSoles.Text = "<b>S/: </b>" + Convert.ToDecimal(0).ToString("N2");
                    }

                    if (dtPedidos.Compute("SUM(MONTO)", "MONEDA='USD'") != null && dtPedidos.Compute("SUM(MONTO)", "MONEDA='USD'").ToString() != "")
                    {
                        lblTotalMontoDolares.Text = "<b>USD: </b>" + Convert.ToDecimal(dtPedidos.Compute("SUM(MONTO)", "MONEDA='USD'")).ToString("N2");
                    }
                    else
                    {
                        lblTotalMontoDolares.Text = "<b>USD: </b>" + Convert.ToDecimal(0).ToString("N2");
                    }

                    lblTotalGruesas.Text = "<b>Und: </b>" + Convert.ToDecimal(dtPedidos.Compute("SUM(CANTIDAD_PENDIENTE)", "CANTIDAD_PENDIENTE>0")).ToString("N2");
                }
                else
                {
                    lblTotalMontoSoles.Text = "<b>S/: </b>" + Convert.ToDecimal(0).ToString("N2");
                    lblSepara1.Text = " | ";
                    lblTotalMontoDolares.Text = "<b>USD: </b>" + Convert.ToDecimal(0).ToString("N2");
                    lblTotalGruesas.Text = "<b>Und: </b>" + Convert.ToDecimal(0).ToString("N2");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(eTipoAlerta.DANGER, ex.Message);
            }
        }
        protected bool CheckDate(string date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private string ValidaParticion(int row)
        {
            DataTable _dt = ObtenerDTProgramacion();
            string resultado = "";

            if (_dt != null)
            {
                if (_dt.Rows[row]["PROGRAMADO"].ToString() == "")
                {
                    resultado = "No puede partir un detalle sin programar. Ingrese una fecha válida y vuelva a intentarlo.";
                }
            }
            else
            {
                resultado = "Error de validación de datos. Por favor revise todas las fechas ingresadas.";
            }

            return resultado;
        }
        private void GrabarCambiosProgramacion()
        {
            if (grvProgramacionDetalle.Rows.Count > 0)
            {
                try
                {
                    DataTable _dt = ObtenerDTProgramacion();
                    if (_dt != null)
                    {
                        string sResultado = new PedidoBL().GrabarCambiosProgramacion(_dt, _dt.Rows[0]["PEDIDO"].ToString(), txtObservacionProg.Text.Trim(), CredencialActiva.UsuarioAlias);
                        CargarDetalleProgramacion(_dt.Rows[0]["PEDIDO"].ToString());
                        CargarGrilla();
                    }
                    else
                    {
                        MostrarMensaje(eTipoAlerta.DANGER, "Error de validación de datos. Por favor revise las fechas ingresadas");
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje(eTipoAlerta.DANGER, ex.Message);
                }
            }
            else
            {
                MostrarMensaje(eTipoAlerta.DANGER, "No se encontraron detalles de la programación");
            }
        }
        private DataTable ObtenerDTProgramacion()
        {
            DataTable _dt = new DataTable();
            _dt.Columns.Add("PEDIDO", typeof(string));
            _dt.Columns.Add("ENC_PEDIDO_LINEA", typeof(int));
            _dt.Columns.Add("PEDIDO_PROGRAMACION", typeof(int));
            _dt.Columns.Add("COLOR", typeof(string));
            _dt.Columns.Add("PROGRAMADO", typeof(string));

            if (grvProgramacionDetalle.Rows.Count > 0)
            {
                foreach (GridViewRow fila in grvProgramacionDetalle.Rows)
                {
                    HiddenField __PEDIDO = (HiddenField)fila.FindControl("__PEDIDO");
                    HiddenField __ENC_PEDIDO_LINEA = (HiddenField)fila.FindControl("__ENC_PEDIDO_LINEA");
                    HiddenField __PEDIDO_PROGRAMACION = (HiddenField)fila.FindControl("__PEDIDO_PROGRAMACION");
                    HiddenField __COLOR = (HiddenField)fila.FindControl("__COLOR");

                    //DropDownList ddlCOLOR = (DropDownList)fila.FindControl("ddlCOLOR");
                    TextBox txtPROGRAMADO = (TextBox)fila.FindControl("txtPROGRAMADO");

                    if (txtPROGRAMADO.Text != "" && !CheckDate(txtPROGRAMADO.Text))
                    {
                        return null;
                    }

                    DataRow _dr = _dt.NewRow();
                    _dr["PEDIDO"] = __PEDIDO.Value.ToString();
                    _dr["ENC_PEDIDO_LINEA"] = Convert.ToInt32(__ENC_PEDIDO_LINEA.Value);
                    _dr["PEDIDO_PROGRAMACION"] = Convert.ToInt32(__PEDIDO_PROGRAMACION.Value);
                    //_dr["COLOR"] = ddlCOLOR.SelectedValue.ToString();
                    _dr["COLOR"] = __COLOR.Value.ToString();
                    _dr["PROGRAMADO"] = txtPROGRAMADO.Text;

                    _dt.Rows.Add(_dr);
                }
                return _dt;
            }
            else
            {
                return null;
            }
        }
        private DataTable ObtenerPedidosSeleccionados()
        {
            DataTable _dt = new DataTable();
            _dt.Columns.Add("PEDIDO", typeof(string));

            if (grvDatos.Rows.Count > 0)
            {
                foreach (GridViewRow fila in grvDatos.Rows)
                {
                    HiddenField __PEDIDO = (HiddenField)fila.FindControl("__PEDIDO");
                    CheckBox chk = (CheckBox)fila.FindControl("CHK");

                    if (chk.Checked)
                    {
                        DataRow _dr = _dt.NewRow();
                        _dr["PEDIDO"] = __PEDIDO.Value.ToString();
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

        protected void btnCancelarCambio_Click(object sender, EventArgs e)
        {
            ddlColor.DataSource = null;
            EjecutarScript("$('#modalSeleccionarColor').modal('hide')");            
        }
    }
}