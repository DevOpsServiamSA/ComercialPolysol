using Polysol.Comercial.Logics;
using Polysol.Comercial.WebApp.AccesoService;
using System;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;

namespace Polysol.Comercial.WebApp.Views
{
    public partial class Site : MasterPage
    {
        #region PROPIEDADES

        ISoftEmpAcceso _acceso = new SoftEmpAccesoClient();

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
                Credencial _usuario = (Credencial)Session["Session.POLYCO.Usuario"];
                return _usuario;
            }
        }

        //public int EnvasadoraActiva
        //{
        //    get
        //    {
        //        int _envasadora = Convert.ToInt32(Session["Session.IKCE.Envasadora"].ToString().Trim());
        //        return _envasadora;
        //    }
        //    set
        //    {
        //        Session["Session.IKCE.Envasadora"] = value;
        //    }
        //}

        //public int EvaluacionActiva
        //{
        //    get
        //    {
        //        int _evaluacion = Convert.ToInt32(Session["Session.IKCE.Evaluacion"].ToString().Trim());
        //        return _evaluacion;
        //    }
        //    set
        //    {
        //        Session["Session.IKCE.Evaluacion"] = value;
        //    }
        //}

        //public int EstadoEvaluacion
        //{
        //    get
        //    {
        //        int _estado_evaluacion = Convert.ToInt32(Session["Session.IKCE.EstadoEvaluacion"].ToString().Trim());
        //        return _estado_evaluacion;
        //    }
        //    set
        //    {
        //        Session["Session.IKCE.EstadoEvaluacion"] = value;
        //    }
        //}

        //public string IndicadorEvaluacion
        //{
        //    get
        //    {
        //        string _estado_evaluacion = Session["Session.IKCE.IndicadorEvaluacion"].ToString().Trim();
        //        return _estado_evaluacion;
        //    }
        //    set
        //    {
        //        Session["Session.IKCE.IndicadorEvaluacion"] = value;
        //    }
        //}

        public Rol Rol
        {
            get
            {
                return (Rol)(Session["Session.POLYCO.Rol"]);
            }
            set
            {
                Session["Session.POLYCO.Rol"] = value;
            }
        }

        #endregion PROPIEDADES

        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            InicializarControles();
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        protected void btnCambiarClave_Click(object sender, EventArgs e)
        {
            try
            {
                txtClaveActual.Text = "";
                txtClaveNueva.Text = "";
                txtClaveActual.Focus();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyEjec", "$('#myModalContra').modal('show');", true);
            }
            catch (Exception)
            {

            }
        }

        protected void btnGuardarClave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtClaveActual.Text != "" && txtClaveNueva.Text != "")
                {
                    int _result = _acceso.CambiarPassword(CredencialActiva.UsuarioAlias, txtClaveActual.Text, txtClaveNueva.Text);

                    if (_result == 1)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "mensaje", "MostrarMensaje('SUCCESS','Contraseña cambiada.');", true);
                    else
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "mensaje", "MostrarMensaje('DANGER','No se pudo cambiar la contraseña.');", true);
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyEjec", "$('#myModalContra').modal('hide');", true);
            }
            catch (Exception)
            {

            }
        }

        #endregion EVENTOS

        #region METODOS

        private void InicializarControles()
        {
            ltrUsuario.Text = CredencialActiva.NombrePersonal;
            //btnEnvasadora.Text = EnvasadoraBL.Instancia.ListarUno(EnvasadoraActiva).Rows[0]["DESCRIPCION"].ToString();
            //btnEnvasadora.Visible = btnEnvasadora.Text.Trim() == "" ? false : true;
        }

        #endregion METODOS

        //protected void btnEnvasadora_Click(object sender, EventArgs e)
        //{
        //    EnvasadoraActiva = 0;
        //    Response.Redirect("~/Views/Envasadoras.aspx");
        //}
    }
}