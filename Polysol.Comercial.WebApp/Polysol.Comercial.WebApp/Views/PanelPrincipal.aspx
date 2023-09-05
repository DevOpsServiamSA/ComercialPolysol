<%@ Page Language="C#" MasterPageFile="~/Views/Site.Master" AutoEventWireup="true" CodeBehind="PanelPrincipal.aspx.cs" Inherits="Polysol.Comercial.WebApp.Views.PanelPrincipal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" runat="server">

    <div class="panel panel-ik">
        <div class="panel-heading">
            <b><i class="fa fa-address-card" aria-hidden="true"></i>&nbspPANEL PRINCIPAL</b>
        </div>
        <div class="panel-body" style="padding: 5px; text-align: left;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-xs-12">
                            <asp:LinkButton ID="btnPROGRAMACION" runat="server" CssClass="btn btn-sm btn-success" Width="180px" OnClick="btnPROGRAMACION_Click" ToolTip="">
                                        <i class="fa fa-calendar" aria-hidden="true"></i> Programación de Pedidos
                            </asp:LinkButton>
                        </div>
                        <div class="col-xs-12">
                            <asp:LinkButton ID="btnLECTURA" runat="server" CssClass="btn btn-sm btn-success" Width="180px" OnClick="btnLECTURA_Click" ToolTip="">
                                        <i class="fa fa-laptop"></i> Lectura de Etiquetas
                            </asp:LinkButton>
                        </div>
                        <div class="col-xs-12">
                            <asp:LinkButton ID="btnCARGA" runat="server" CssClass="btn btn-sm btn-success" Width="180px" OnClick="btnCARGA_Click" ToolTip="">
                                        <i class="fa fa-check"></i> Carga de Etiquetas
                            </asp:LinkButton>
                        </div>
                    </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <script type="text/javascript">
        //Funcion que corrige el bug de ventanas modales contrapuestas (OBLIGATORIO EN TODAS LAS PAGINAS)
        $('body').on('hidden.bs.modal', function () {
            if ($('.modal.in').length > 0) {
                $('body').addClass('modal-open');
            }
        });

    </script>

</asp:Content>
