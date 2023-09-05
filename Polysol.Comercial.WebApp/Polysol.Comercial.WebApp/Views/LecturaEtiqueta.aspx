<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Master" AutoEventWireup="true" CodeBehind="LecturaEtiqueta.aspx.cs" Inherits="Polysol.Comercial.WebApp.Views.LecturaEtiqueta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" runat="server">
    <div class="panel panel-ik">
        <div class="panel-heading">
            <b><i class="fa fa-address-card" aria-hidden="true"></i>&nbspLECTURA DE ETIQUETAS - ALMACÉN</b>
        </div>
        <div class="panel-body" style="padding: 5px; text-align: left;">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="form-inline">
                        <asp:Panel runat="server" DefaultButton="btnBuscar">
                            <div class="form-group">
                                <label runat="server" id="lblPed" for="txtPEDIDO" style="font-size: smaller">FILTRO</label>
                                <asp:TextBox ID="txtPEDIDO" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            </div>
                            <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-sm btn-primary" OnClick="btnBuscar_Click">
                            <i class="fa fa-search" aria-hidden="true"></i> Buscar
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnFiltrar" runat="server" CssClass="btn btn-sm btn-success" OnClick="btnFiltrar_Click" ToolTip="ACTIVAR FILTROS">
                            <i class="fa fa-filter"></i>
                            </asp:LinkButton>
                        </asp:Panel>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grvPedidos" Font-Size="Smaller" Caption="Pedidos pendientes" runat="server" CssClass="table table-bordered table-condensed" SelectedRowStyle-BackColor="#CCCCCC" OnRowCommand="grvPedidos_RowCommand"
                                            ShowHeaderWhenEmpty="True" AutoGenerateColumns="false" OnRowDataBound="grvPedidos_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnSeleccionar" CssClass="btn btn-sm btn-success" runat="server" CommandName="SELECCIONAR"
                                                            CommandArgument='<%# Eval("PEDIDO") + ";" + Eval("PROGRAMADO") %>' ToolTip="Seleccionar">
                                                            <i class="glyphicon glyphicon-chevron-right" aria-hidden="true"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="PEDIDO" DataField="PEDIDO" Visible="true" />
                                                <asp:BoundField HeaderText="F. PROGRAMADA" DataField="PROGRAMADO" />
                                                <asp:BoundField HeaderText="NOMBRE CLIENTE" DataField="NOMBRE_CLIENTE" />
                                                <asp:BoundField HeaderText="STOCK DISP." DataField="STOCK_DISPONIBLE" />
                                                <asp:BoundField HeaderText="ESTADO" DataField="ESTADO_DESCRIPCION" />
                                                <asp:BoundField HeaderText="OBS. VENDEDOR" DataField="OBSERVACION_VENDEDOR" />
                                                <asp:BoundField HeaderText="" DataField="ESTADO" Visible="false" />
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <%--Ventanas emergentes: Ini--%>
    <div class="modal fade" id="ModalDetalleProgramacion" tabindex="-1" role="dialog" data-backdrop="static" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="upModal" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="__hfPedido" runat="server" />
                    <asp:HiddenField ID="__hfProgramado" runat="server" />
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="lblDetalle" runat="server" Font-Size="Medium" Font-Bold="true" Text=""></asp:Label></h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <label runat="server" id="Label1" style="font-size: small">ESTADO: </label>
                                <asp:Label ID="lblEstadoPedido" runat="server" Style="font-size: small" Text=""></asp:Label>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblObservacionVendedor" runat="server" Style="font-size: small" Text=""></asp:Label>
                            </div>
                            <%--<div class="form-group">                                
                                <b><asp:Label ID="Label3" runat="server" Style="font-size: small" Text="Obs. Almacén: "></asp:Label></b>
                                <asp:TextBox ID="txtObservacionAlm" runat="server" CssClass="form-control input-sm" TextMode="MultiLine" Height="50px" MaxLength="300"></asp:TextBox>
                            </div>--%>
                            <div class="form-group">
                                <div class="table-responsive">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="grvDetalleProgramacion" Font-Size="Smaller" runat="server" CssClass="table table-bordered table-condensed" SelectedRowStyle-BackColor="#CCCCCC"
                                                ShowHeaderWhenEmpty="True" AutoGenerateColumns="false" Caption="Detalle de la programación" OnRowDataBound="grvDetalleProgramacion_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField HeaderText="LÍNEA" DataField="ENC_PEDIDO_LINEA" Visible="false" />
                                                    <asp:BoundField HeaderText="ARTICULO" DataField="ARTICULO" />
                                                    <asp:BoundField HeaderText="UND." DataField="UNIDAD" />
                                                    <asp:BoundField HeaderText="SALDO" DataField="CANTIDAD" ItemStyle-HorizontalAlign="Right" />
                                                    <asp:BoundField HeaderText="COLOR" DataField="COLOR" />
                                                    <asp:BoundField HeaderText="CARG." DataField="CARGA" ItemStyle-HorizontalAlign="Right" />
                                                    <asp:BoundField HeaderText="PEND." DataField="PENDIENTE" ItemStyle-HorizontalAlign="Right" />
                                                    <asp:BoundField HeaderText="ARTICULO_CODIGO" DataField="ARTICULO_CODIGO" Visible="false" />
                                                    <asp:BoundField HeaderText="PEDIDO_PROGRAMACION" DataField="PEDIDO_PROGRAMACION" Visible="false" />
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:LinkButton ID="btnAnadirEtq" runat="server" CssClass="btn btn-light btn-sm" OnClick="btnAnadirEtq_Click">
                                        <i class="fa fa-plus" aria-hidden="true"></i> Añadir
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnQuitarEtq" runat="server" CssClass="btn btn-light btn-sm" OnClick="btnQuitarEtq_Click">
                                        <i class="fa fa-trash" aria-hidden="true"></i> Quitar
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnErroresEtq" Text="10 Errores " runat="server" Style="color: black" Font-Bold="true" CssClass="btn btn-warning btn-sm" OnClick="btnErroresEtq_Click">
                                </asp:LinkButton>
                            </div>
                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:Panel runat="server" DefaultButton="btnBUSCARETIQUETA">
                                        <div class="col-xs-10">
                                            <asp:TextBox ID="txtETIQUETAL" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                        </div>
                                        <div class="col-xs-2">
                                            <asp:LinkButton ID="btnBUSCARETIQUETA" runat="server" CssClass="btn btn-info btn-sm" OnClick="btnBUSCARETIQUETA_Click">
                                        <i class="fa fa-search" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="form-group">
                                <label runat="server" id="Label2" style="font-size: small">Etiquetas cargadas: </label>
                                <asp:Label ID="lblNroEtiquetasCargadas" runat="server" Style="font-size: small" Text=""></asp:Label>
                            </div>
                            <div class="form-group">
                                <div class="table-responsive">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="grvDetalleEtiquetas" Font-Size="Small" runat="server" CssClass="table table-bordered table-condensed" SelectedRowStyle-BackColor="#CCCCCC"
                                                ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" Caption="Detalle de etiquetas">
                                                <Columns>
                                                    <asp:BoundField HeaderText="NRO." DataField="NRO" />
                                                    <asp:BoundField HeaderText="LINEA" DataField="ENC_PEDIDO_LINEA" Visible="false" />
                                                    <asp:BoundField HeaderText="ETIQUETA" DataField="ETIQUETA" />
                                                    <asp:BoundField HeaderText="ARTICULO" DataField="ARTICULO" Visible="false" />
                                                    <asp:BoundField HeaderText="ARTÍCULO" DataField="ARTICULO_DESCRIPCION" />
                                                    <asp:BoundField HeaderText="UNIDAD" DataField="UNIDAD" Visible="false" />
                                                    <asp:BoundField HeaderText="CANT." DataField="CANTIDAD" ItemStyle-HorizontalAlign="Right" />
                                                    <asp:BoundField HeaderText="LOTE" DataField="LOTE" />
                                                    <asp:BoundField HeaderText="COLOR" DataField="COLOR" />
                                                    <asp:BoundField HeaderText="OBSERVACIÓN" DataField="OBSERVACION" />
                                                    <asp:BoundField HeaderText="PEDIDO_PROGRAMACION" DataField="PEDIDO_PROGRAMACION" Visible="false" />
                                                    <asp:BoundField HeaderText="ID_ETIQUETA" DataField="ID_ETIQUETA" Visible="false" />
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btnEnviarCarga" runat="server" CssClass="btn btn-success" OnClick="btnEnviarCarga_Click">
                                <i class="fa fa-send-o" aria-hidden="true"></i> Enviar
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnCancelarCarga" runat="server" CssClass="btn btn-warning" OnClick="btnCancelarCarga_Click" OnClientClick="return confirm('¿Está seguro de DESCARTAR la lectura de TODAS estas etiquetas?');">
                                <i class="fa fa-trash-o" aria-hidden="true"></i> Cancelar
                            </asp:LinkButton>
                            <button class="btn btn-danger" data-dismiss="modal" aria-hidden="true">
                                <i class="fa fa-times" aria-hidden="true"></i>Cerrar</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal fade" id="ModalLogErrores" tabindex="-1" role="dialog" data-backdrop="static" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="lblDetalle2" runat="server" Font-Size="Medium" Font-Bold="false" Text="Lecturas con error"></asp:Label></h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-inline">
                                <div class="table-responsive">
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="grvLogErrores" Font-Size="Smaller" runat="server" CssClass="table table-bordered table-condensed" SelectedRowStyle-BackColor="#CCCCCC"
                                                ShowHeaderWhenEmpty="True" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:BoundField HeaderText="ETIQUETA" DataField="ETIQUETA" />
                                                    <asp:BoundField HeaderText="LECTURA" DataField="LECTURA" />
                                                    <asp:BoundField HeaderText="OPER." DataField="OPERACION" />
                                                    <asp:BoundField HeaderText="RESULTADO" DataField="RESULTADO" />
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btnLimpiar" runat="server" CssClass="btn btn-info" OnClick="btnLimpiar_Click" OnClientClick="return confirm('¿Está seguro de LIMPIAR la lectura con error de estas etiquetas?');">
                                <i class="fa fa-pencil" aria-hidden="true"></i> Limpiar
                            </asp:LinkButton>
                            <button class="btn btn-danger" data-dismiss="modal" aria-hidden="true">
                                <i class="fa fa-times" aria-hidden="true"></i>Cerrar</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div id="modalEnviar" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">            
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>                    
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title"><b>
                                <asp:Label ID="lblObservacion" runat="server"></asp:Label>Envío de etiquetas</b></h5>
                        </div>
                        <div class="modal-body">
                            <div class="form">
                                <div class="form-group">
                                    <label>Ingrese una observación</label>
                                    <asp:TextBox ID="txtObservacion" runat="server" CssClass="form-control input-sm" TextMode="MultiLine" Height="100px" MaxLength="300"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btnEnviarEtiquetas" runat="server" CssClass="btn btn-success" OnClientClick="return confirm('¿Está seguro de enviar TODAS estas etiquetas a FACTURAR?');" OnClick="btnEnviarEtiquetas_Click">
                                    <i class="fa fa-send-o" aria-hidden="true"></i> Enviar
                            </asp:LinkButton>
                            <button type="button" class="btn btn-danger" data-dismiss="modal">
                                <i class="fa fa-times" aria-hidden="true"></i>Cancelar
                            </button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <%--Ventanas emergentes: Fin--%>

    <script type="text/javascript">
        //Funcion que corrige el bug de ventanas modales contrapuestas (OBLIGATORIO EN TODAS LAS PAGINAS)
        $('body').on('hidden.bs.modal', function () {
            if ($('.modal.in').length > 0) {
                $('body').addClass('modal-open');
            }
        });
    </script>
</asp:Content>
