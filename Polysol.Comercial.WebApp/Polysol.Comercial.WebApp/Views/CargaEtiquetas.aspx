<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Master" AutoEventWireup="true" CodeBehind="CargaEtiquetas.aspx.cs" Inherits="Polysol.Comercial.WebApp.Views.CargaEtiquetas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" runat="server">
    <div class="panel panel-ik">
        <div class="panel-heading">
            <b><i class="fa fa-address-card" aria-hidden="true"></i>&nbspCARGA DE ETIQUETAS</b>
        </div>
        <div class="panel-body" style="padding: 5px; text-align: left;">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="form-inline">
                        <asp:Panel runat="server" DefaultButton="btnBuscar">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="form-group">
                                        <asp:DropDownList ID="cbo_TipoFiltro" AutoPostBack="false" CssClass="form-control" runat="server" Visible="false"></asp:DropDownList>
                                        <asp:TextBox ID="txt_ValorFiltro" MaxLength="20" AutoPostBack="false" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
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
                                        <asp:GridView ID="grvPedidos" Font-Size="Smaller" runat="server" CssClass="table table-bordered table-condensed" SelectedRowStyle-BackColor="#CCCCCC" OnRowCommand="grvPedidos_RowCommand"
                                            ShowHeaderWhenEmpty="True" AutoGenerateColumns="false" Caption="Pedidos pendientes">
                                            <Columns>
                                                <asp:BoundField HeaderText="COD. CLIENTE" DataField="COD_CLIENTE" />
                                                <asp:BoundField HeaderText="NOMBRE CLIENTE" DataField="CLIENTE" />
                                                <asp:BoundField HeaderText="PEDIDO" DataField="PEDIDO" />
                                                <asp:BoundField HeaderText="MONEDA." DataField="MONEDA" />
                                                <asp:BoundField HeaderText="LINEAS CARGADAS" DataField="LINEAS_CARGADAS_REM" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField HeaderText="OBS. ALMACÉN" DataField="OBSERVACION_ALMACEN" />
                                                <%--<asp:BoundField HeaderText="TOTAL A FACT. (SIN IGV)" DataField="TOTAL_A_FACTURAR" ItemStyle-HorizontalAlign="Right" Visible="false" />--%>
                                                <asp:BoundField HeaderText="UND. PEDIDO" DataField="UNIDAD_PEDIDO" ItemStyle-HorizontalAlign="Center" />

                                                <asp:TemplateField HeaderStyle-Width="110px">
                                                    <HeaderTemplate>
                                                        ETIQUETAS PEND. TRAN.     
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnETIQUETASPENDIENTES" Width="105px" runat="server" CssClass="btn btn-s btn-success" CommandName="DETALLE" CommandArgument='<%# Eval("PEDIDO") %>' ToolTip="ETIQUETAS PENDIENTES DE TRANSFERIR">
                                                        <%# Eval("ETIQUETAS_PEND_TRA") %>
                                                        <i class="fa fa-search" aria-hidden="true">
                                                        </i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="110px">
                                                    <HeaderTemplate>
                                                        ETIQUETAS PEND. REM.     
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnETIQUETASPENDREMITIR" Width="105px" runat="server" CssClass="btn btn-s btn-success" CommandName="PORREMITIR" CommandArgument='<%# Eval("PEDIDO") %>' ToolTip="ETIQUETAS PENDIENTES DE REMITIR">
                                                        <%# Eval("ETIQUETAS_PEND_REM") %>
                                                        <i class="fa fa-search" aria-hidden="true">
                                                        </i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderStyle-Width="110px">
                                                    <HeaderTemplate>
                                                        ETIQUETAS PEND. FAC.     
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnETIQUETASPENDFACTURAR" Width="105px" runat="server" CssClass="btn btn-s btn-success" CommandName="PORFACTURAR" CommandArgument='<%# Eval("PEDIDO") %>' ToolTip="ETIQUETAS PENDIENTES DE FACTURAR">
                                                        <%# Eval("ETIQUETAS_PEND_FAC") %>
                                                        <i class="fa fa-search" aria-hidden="true">
                                                        </i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
        <%-- <div class="panel-footer" style="padding: 5px;">
            <div class="table-responsive">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>--%>
    </div>

    <%--Ventanas emergentes: Ini--%>
    <div class="modal fade" id="ModalDetalleEtiquetas" tabindex="-1" role="dialog" data-backdrop="static" aria-hidden="true">
        <div class="modal-dialog" style="width: 900px">
            <asp:UpdatePanel ID="upModal" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="__hfPedido" runat="server" />
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="lblDetalle" runat="server" Font-Size="Medium" Font-Bold="true" Text=""></asp:Label></h4>
                        </div>
                        <div class="modal-body">
                            <div class="table-responsive">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grvDetalleProgramacion" Font-Size="Smaller" runat="server" CssClass="table table-bordered table-condensed" SelectedRowStyle-BackColor="#CCCCCC"
                                            ShowHeaderWhenEmpty="True" AutoGenerateColumns="false" Caption="Detalle de Etiquetas disponibles">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width="25px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="CHKALL" Width="25px" runat="server" AutoPostBack="true" OnCheckedChanged="CHKALL_CheckedChanged" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="__PEDIDO" runat="server" Value='<%#Eval("PEDIDO") %>' />
                                                        <asp:HiddenField ID="__ENC_PEDIDO_LINEA" runat="server" Value='<%#Eval("ENC_PEDIDO_LINEA") %>' />
                                                        <asp:HiddenField ID="__PEDIDO_PROGRAMACION" runat="server" Value='<%#Eval("PEDIDO_PROGRAMACION") %>' />
                                                        <asp:HiddenField ID="__ID_ETIQUETA" runat="server" Value='<%#Eval("ID_ETIQUETA") %>' />
                                                        <asp:HiddenField ID="__CANTIDAD" runat="server" Value='<%#Eval("CANTIDAD") %>' />
                                                        <asp:HiddenField ID="__SUBTOTAL" runat="server" Value='<%#Eval("SUBTOTAL") %>' />
                                                        <asp:HiddenField ID="__ARTICULO" runat="server" Value='<%#Eval("ARTICULO") %>' />
                                                        <asp:HiddenField ID="__LOTE" runat="server" Value='<%#Eval("LOTE") %>' />
                                                        <asp:CheckBox ID="CHK" Width="25px" runat="server" AutoPostBack="true" OnCheckedChanged="CHK_CheckedChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="LÍNEA" DataField="ENC_PEDIDO_LINEA" Visible="false" />
                                                <asp:BoundField HeaderText="ARTICULO" DataField="ARTICULO" />
                                                <asp:BoundField HeaderText="DESCRIPCIÓN" DataField="ARTICULO_DESCRIPCION" />
                                                <asp:BoundField HeaderText="ETIQUETA" DataField="ETIQUETA" />
                                                <asp:BoundField HeaderText="COLOR" DataField="COLOR" />
                                                <asp:BoundField HeaderText="LOTE" DataField="LOTE" />
                                                <asp:BoundField HeaderText="UND." DataField="UNIDAD" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="CANTIDAD" DataField="CANTIDAD" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField HeaderText="SUBTOTAL" DataField="SUBTOTAL" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField HeaderText="OBS. ALMACÉN" DataField="OBSERVACION_ALMACEN" />
                                                <asp:BoundField HeaderText="ID_ETIQUETA" DataField="ID_ETIQUETA" Visible="false" />
                                                <asp:BoundField HeaderText="PEDIDO" DataField="PEDIDO" Visible="false" />
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="form-group">
                                <div class="col-xs-12">
                                    <label runat="server" id="Label2" style="font-size: small">Líneas seleccionadas: </label>
                                    <asp:Label ID="lblNroLineasSeleccionadas" runat="server" Style="font-size: small" Text=""></asp:Label>
                                    <label>&nbsp&nbsp|&nbsp&nbsp</label>
                                    <label runat="server" id="Label1" style="font-size: small">Cant. seleccionada: </label>
                                    <asp:Label ID="lblCantidadSeleccionada" runat="server" Style="font-size: small" Text=""></asp:Label>
                                    <label>&nbsp&nbsp|&nbsp&nbsp</label>
                                    <label runat="server" id="Label3" style="font-size: small">Monto seleccionado: </label>
                                    <asp:Label ID="lblMontoSeleccionado" runat="server" Style="font-size: small" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btnCargar" runat="server" CssClass="btn btn-success" OnClick="btnCargar_Click" OnClientClick="return confirm('¿Está seguro de CARGAR las ETIQUETAS seleccionadas al PEDIDO en EXACTUS?');">
                                <i class="fa fa-check" aria-hidden="true"></i> Cargar
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnRechazar" runat="server" CssClass="btn btn-warning" OnClick="btnRechazar_Click" OnClientClick="return confirm('¿Está seguro de RECHAZAR las ETIQUETAS seleccionadas?');">
                                <i class="fa fa-trash" aria-hidden="true"></i> Rechazar
                            </asp:LinkButton>
                            <button class="btn btn-danger" data-dismiss="modal" aria-hidden="true">
                                <i class="fa fa-times" aria-hidden="true"></i>Cancelar</button>
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
