using Polysol.Comercial.Entities;
using Polysol.Comercial.WebApp.AccesoService;
using System;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace Polysol.Comercial.WebApp
{
    public partial class Login : Page
    {
        #region VARIABLES / PROPIEDADES

        ISoftEmpAcceso _acceso = new SoftEmpAccesoClient();

        #endregion VARIABLES / PROPIEDADES

        #region EVENTOS

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

        private void Ingresar()
        {
            try
            {
                if (ValidaCampos())
                {
                    string sCodApp = __appSE.Value;
                    string sUsuario = txtUsuario.Text;
                    string sClave = txtContrasenha.Text;
                    bool usaERP = true;

                    Credencial _credencial = _acceso.ValidaAcceso(sUsuario, sClave, usaERP);

                    if (_credencial != null)
                    {
                        Session["Session.POLYCO.Usuario"] = _credencial;

                        Rol _rol = _acceso.ObtenerRol(sCodApp, _credencial.UsuarioAlias);

                        if (_rol == null)
                        {
                            MostrarMensaje(eTipoAlerta.WARNING, string.Format(Resources.Mensajes.msjNoConfig, sUsuario));
                            return;
                        }
                        
                        FormsAuthenticationTicket tkt;
                        string cookiestr;

                        tkt = new FormsAuthenticationTicket(1, sUsuario, DateTime.Now, DateTime.Now.AddMinutes(3600), true, sUsuario);
                        cookiestr = FormsAuthentication.Encrypt(tkt);
                        HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
                        ck.Expires = tkt.Expiration;
                        ck.Path = FormsAuthentication.FormsCookiePath;
                        Response.Cookies.Add(ck);

                        if (_rol.RolId == 33)
                        {
                            Response.Redirect("Views/LecturaEtiqueta.aspx", true);
                        }
                        else if (_rol.RolId == 34)
                        {
                            Response.Redirect("Views/PedidosPendientes.aspx", true);
                        }
                        else if (_rol.RolId == 35)
                        {
                            Response.Redirect("Views/CargaEtiquetas.aspx", true);
                        }
                        else if (_rol.RolId == 36)
                        {
                            Response.Redirect("Views/PanelPrincipal.aspx", true);
                        }
                    }
                    else
                    {
                        MostrarMensaje(eTipoAlerta.DANGER, Resources.Mensajes.msjErrorLogin);
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(eTipoAlerta.DANGER, ex.Message);
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            Ingresar();
        }

        #endregion EVENTOS

        #region METODOS

        private void InicializarControles()
        {
            __appSE.Value = ConfigurationManager.AppSettings["_CODAPP"].ToString();
            txtUsuario.Focus();

            //PRUEBA

            ////ALMACEN
            //txtUsuario.Text = "VAROTINCO";
            //txtContrasenha.Text = "3ilasur.sa";

            ////VENDEDOR
            //txtUsuario.Text = "JHUAMAN";
            //txtContrasenha.Text = "3ilasur.sa";

            ////COMERCIAL
            //txtUsuario.Text = "JCCASTRO";
            //txtContrasenha.Text = "3ilasur.sa";

            //Ingresar();

            //Utils.CargarCombo(ddlEnvasadora, EnvasadoraBL.Instancia.Listar(), "CODIGO", "DESCRIPCION", true);
        }

        private bool ValidaCampos()
        {
            string sControl = "";

            Utils.ValidaControles(ref sControl, txtUsuario, txtContrasenha);

            if (!string.IsNullOrEmpty(sControl))
            {
                MostrarMensaje(eTipoAlerta.WARNING, string.Format(Resources.Mensajes.msjValidaControl, sControl));
                return false;
            }

            return true;
        }

        private void MostrarMensaje(eTipoAlerta p_Tipo, string p_Mensaje)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "mensaje", "MostrarMensaje('" + p_Tipo.ToString() + "','" + p_Mensaje.Replace("'", "") + "');", true);
        }

        #endregion METODOS
    }
}