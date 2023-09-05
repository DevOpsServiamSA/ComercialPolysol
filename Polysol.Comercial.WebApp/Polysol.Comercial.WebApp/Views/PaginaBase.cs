using Polysol.Comercial.Entities;
using Polysol.Comercial.WebApp.AccesoService;
using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Polysol.Comercial.WebApp.Views
{
    public class PaginaBase : Page
    {
        #region PROPIEDADES
        public string CodApp
        {
            get
            {
                return ConfigurationManager.AppSettings["_CODAPP"].ToString();
            }
        }
        public Credencial CredencialActiva
        {
            get
            {
                return (Credencial)Session["Session.POLYCO.Usuario"];
            }
        }               
        public Rol Rol
        {
            get
            {
                return (Rol)Session["Session.POLYCO.Rol"];
            }
            set
            {
                Session["Session.POLYCO.Rol"] = value;
            }
        }
        #endregion PROPIEDADES

        #region EVENTOS
        protected override void OnLoad(EventArgs e)
        {
            if (Session["Session.POLYCO.Usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            base.OnLoad(e);
        }
        #endregion EVENTOS

        #region METODOS PERSONALIZADOS        
        public void MostrarMensaje(eTipoAlerta p_Tipo, string p_Mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "mensaje", "MostrarMensaje('" + p_Tipo.ToString() + "','" + p_Mensaje.Replace("'", "") + "');", true);
        }
        public void EjecutarScript(string p_Script)
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "keyEjec", p_Script, true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyEjec", p_Script, true);
        }
        public void CargarCombo(DropDownList combo, object lstDatos, string strValor, string strTexto, bool flgTextoDefault = false, bool flgFiltro = false)
        {
            combo.DataSource = lstDatos;
            combo.DataValueField = strValor;
            combo.DataTextField = strTexto;
            combo.DataBind();

            if (flgTextoDefault)
            {
                ListItem lstItem = new ListItem();
                lstItem.Value = "0";

                if (flgFiltro)
                    lstItem.Text = "[Todos]";
                else
                    lstItem.Text = "[Seleccione]";

                combo.Items.Insert(0, lstItem);
            }
        }
        #endregion METODOS PERSONALIZADOS
    }
}