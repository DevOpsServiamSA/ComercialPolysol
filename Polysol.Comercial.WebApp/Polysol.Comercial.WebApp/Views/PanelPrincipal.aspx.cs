using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Polysol.Comercial.WebApp.Views
{
    public partial class PanelPrincipal : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void Inicializar()
        {

        }
        protected void btnPROGRAMACION_Click(object sender, EventArgs e)
        {
            Response.Redirect("PedidosPendientes.aspx", true);
        }
        protected void btnCARGA_Click(object sender, EventArgs e)
        {
            Response.Redirect("CargaEtiquetas.aspx", true);
        }
        protected void btnLECTURA_Click(object sender, EventArgs e)
        {
            Response.Redirect("LecturaEtiqueta.aspx", true);
        }
    }
}