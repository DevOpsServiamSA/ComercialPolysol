using System.Web.UI.WebControls;

namespace Polysol.Comercial.Entities
{
    public static class Utils
    {
        public static void CargarCombo(DropDownList p_Combo, object p_Lista, string p_Valor, string p_Texto, bool p_SelectItem = false, bool p_Filtro = false)
        {
            p_Combo.DataSource = p_Lista;
            p_Combo.DataValueField = p_Valor;
            p_Combo.DataTextField = p_Texto;
            p_Combo.DataBind();

            if (p_SelectItem)
            {
                ListItem _itemSelect = new ListItem() { Value = "0", Text = "-- Seleccione --", Selected = true };
                if (p_Filtro) _itemSelect.Text = "-- Todos --";
                p_Combo.Items.Insert(0, _itemSelect);
            }
        }

        public static bool ValidaControles(ref string p_Label, params object[] p_Controls)
        {
            foreach (var p_Control in p_Controls)
            {
                if (p_Control is TextBox)
                {
                    if (((TextBox)p_Control).Text == "")
                    {
                        p_Label = ((TextBox)p_Control).Attributes["data-label"];
                        ((TextBox)p_Control).Focus();
                        return false;
                    }

                }

                else if (p_Control is DropDownList)
                {
                    if (((DropDownList)p_Control).SelectedValue == "0" || ((DropDownList)p_Control).SelectedValue == "")
                    {
                        p_Label = ((DropDownList)p_Control).Attributes["data-label"];
                        ((DropDownList)p_Control).Focus();
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
