<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Master" AutoEventWireup="true" CodeBehind="PedidosPendientes.aspx.cs" Inherits="Polysol.Comercial.WebApp.Views.PedidosPendientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" runat="server">

    <div class="panel panel-ik">
        <div class="panel-heading">
            <b><i class="fa fa-address-card" aria-hidden="true"></i>&nbspPROGRAMACIÓN DE DESPACHO DE PEDIDOS</b>
        </div>
        <div class="panel-body" style="padding: 5px; text-align: left;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="form-inline">
                        <asp:Panel runat="server" DefaultButton="btnBuscar">

                            <div class="row">
                                <div class="col-xs-12">
                                    <asp:Label runat="server" ID="lblMostrar" Text="Mostrar&nbsp&nbsp"></asp:Label>
                                    <asp:RadioButton ID="chk_NOPROGRAMADO" GroupName="ESTADO" AutoPostBack="true" ValidationGroup="ESTADO" Text="&nbspNo prog." runat="server" OnCheckedChanged="chk_NOPROGRAMADO_CheckedChanged" />
                                    <asp:Label ID="Label1" runat="server" Text="&nbsp&nbsp"></asp:Label>
                                    <asp:RadioButton ID="chk_PROGRAMADO" GroupName="ESTADO" AutoPostBack="true" ValidationGroup="ESTADO" Text="&nbspProg." runat="server" OnCheckedChanged="chk_PROGRAMADO_CheckedChanged" />
                                    <asp:Label ID="lblSeparador" runat="server" Text="&nbsp&nbsp"></asp:Label>
                                    <asp:RadioButton ID="chk_TODOS" GroupName="ESTADO" AutoPostBack="true" ValidationGroup="ESTADO" Text="&nbspTodos" runat="server" OnCheckedChanged="chk_TODOS_CheckedChanged" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="form-group">
                                        <%--<asp:DropDownList ID="cbo_TipoFiltro" AutoPostBack="false" CssClass="form-control" runat="server" Visible="false"></asp:DropDownList>--%>
                                        <asp:Label ID="lblFiltro" Visible="false" runat="server">Filtro</asp:Label>
                                        <asp:TextBox ID="txt_ValorFiltro" MaxLength="20" AutoPostBack="false" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-sm btn-primary" OnClick="btnBuscar_Click" ToolTip="BUSCAR">
                                        <i class="fa fa-search" aria-hidden="true"></i> Buscar
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnMasivo" runat="server" CssClass="btn btn-sm btn-info" OnClick="btnMasivo_Click" ToolTip="PROGRAMACIÓN MASIVA" OnClientClick="return confirm('Está seguro de programar automáticamente los pedidos seleccionados?')">
                                        <i class="fa fa-check"></i> Prog. masiva
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnFiltrar" runat="server" CssClass="btn btn-sm btn-success" OnClick="btnFiltrar_Click" ToolTip="ACTIVAR FILTROS">
                                        <i class="fa fa-filter"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <asp:Label ID="lblGlosa" runat="server"></asp:Label>
                                    <asp:Label ID="lblTotalMontoSoles" runat="server"></asp:Label>
                                    <asp:Label ID="lblSepara1" runat="server"></asp:Label>
                                    <asp:Label ID="lblTotalMontoDolares" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <asp:Label ID="lblTotalGruesas" runat="server"></asp:Label>
                                </div>
                            </div>

                        </asp:Panel>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <div class="table-responsive">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grvDatos" runat="server" Caption="Pedidos pendientes" Font-Size="Smaller" AutoGenerateColumns="false" CssClass="table table-bordered"
                            ShowHeader="true" ShowHeaderWhenEmpty="true" OnRowCommand="grvDatos_RowCommand" OnRowDataBound="grvDatos_RowDataBound">
                            <Columns>

                                <asp:TemplateField HeaderStyle-Width="25px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="CHKALL" Width="25px" runat="server" AutoPostBack="true" OnCheckedChanged="CHKALL_CheckedChanged" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="__PEDIDO" runat="server" Value='<%#Eval("PEDIDO") %>' />
                                        <asp:CheckBox ID="CHK" Width="25px" runat="server" AutoPostBack="true" OnCheckedChanged="CHK_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnProgramar" runat="server" CssClass="btn btn-xs btn-success" ToolTip="PROGRAMAR ESTE PEDIDO" CommandName="PROGRAMAR" CommandArgument='<%# Eval("PEDIDO") + "," + Eval("OBSERVACION") + "," + Eval("CONDICION_PAGO") %>'>
                                            <i class="fa fa-check-circle" aria-hidden="true"></i>                                             
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnNoProgramar" runat="server" CssClass="btn btn-xs btn-danger" ToolTip="NO PROGRAMAR ESTE PEDIDO" CommandName="NOPROGRAMAR" CommandArgument='<%# Eval("PEDIDO")+ "," + Eval("OBSERVACION") %>'>
                                            <i class="fa fa-exclamation-triangle" aria-hidden="true"></i> 
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField HeaderText="CLIENTE" DataField="CLIENTE" />
                                <asp:BoundField HeaderText="PEDIDO" DataField="PEDIDO" />
                                <asp:BoundField HeaderText="ORDEN COMPRA" DataField="ORDEN_COMPRA" />
                                <asp:BoundField HeaderText="FECHA PROM." DataField="FECHA_PROMETIDA" />
                                <asp:BoundField HeaderText="COND. PAGO" DataField="CONDICION_PAGO" Visible="false" />
                                <asp:BoundField HeaderText="MONEDA" DataField="MONEDA" />
                                <asp:BoundField HeaderText="TOTAL FACT." DataField="TOTAL_FACTURAR" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField HeaderText="DEUDA VENCIDA" DataField="DEUDA_VENCIDA" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField HeaderText="EXCEDE CRED." DataField="EXCEDE_CREDITO" />
                                <asp:BoundField HeaderText="ESTADO PEDIDO" DataField="ESTADO_PEDIDO" Visible="false" />
                                <asp:BoundField HeaderText="STOCK" DataField="STOCK" />
                                <asp:BoundField HeaderText="F. PROGRAMADA" DataField="FECHA_PROGRAMADA" />
                                <asp:BoundField HeaderText="ESTADO" DataField="ESTADO_PROGRAMACION_DESCRIPCION" />
                                <asp:BoundField HeaderText="OBSERVACIÓN" DataField="OBSERVACION" />
                                <asp:BoundField HeaderText="ESTADO PROG." DataField="ESTADO_PROGRAMACION" Visible="false" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
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
    <div class="modal fade" id="modalDetalleProgramacion" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">
                                <asp:Label ID="lblDetalle" runat="server" Font-Size="Medium" Font-Bold="true" Text=""></asp:Label></h5>
                        </div>
                        <div class="modal-body table-responsive">
                            <div class="modal-responsive">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <label for="txtObservacionProg">Observación</label>
                                        <asp:TextBox ID="txtObservacionProg" runat="server" CssClass="form-control input-sm" TextMode="MultiLine" Height="50px" MaxLength="300"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <b>
                                            <label for="lblCONDPAGO">Condición de Pago: </label>
                                        </b>
                                        <asp:Label ID="lblCONDICIONPAGO" runat="server" Text="CONDICIÓN DE PAGO"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:GridView ID="grvProgramacionDetalle" Font-Size="Smaller" Caption="Detalle de programación" runat="server" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false"
                                            SelectedRowStyle-BackColor="#CCCCCC" HeaderStyle-BackColor="#033C74" HeaderStyle-ForeColor="White" OnRowCommand="grvProgramacionDetalle_RowCommand" OnRowDataBound="grvProgramacionDetalle_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width="25" ItemStyle-Width="25" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>

                                                        <asp:HiddenField ID="__PEDIDO" runat="server" Value='<%#Eval("PEDIDO") %>' />
                                                        <asp:HiddenField ID="__ENC_PEDIDO_LINEA" runat="server" Value='<%#Eval("ENC_PEDIDO_LINEA") %>' />
                                                        <asp:HiddenField ID="__PEDIDO_PROGRAMACION" runat="server" Value='<%#Eval("PEDIDO_PROGRAMACION") %>' />
                                                        <asp:HiddenField ID="__COLOR" runat="server" Value='<%#Eval("COLOR") %>' />

                                                        <asp:LinkButton ID="btnPartirProgramacion" runat="server" CssClass="btn btn-xs btn-success" ToolTip="PARTIR PROGRAMACIÓN" CommandName="PARTIR" CommandArgument='<%# Eval("PEDIDO") + "," + Eval("ENC_PEDIDO_LINEA")+ "," + Eval("PEDIDO_PROGRAMACION") + "," + Eval("ESTADO") + "," + Eval("ESTADO_DESCRIPCION") + "," + Eval("UNIDAD") + "," + Eval("CANTIDAD") + "," + Eval("PROGRAMADO") %>'>
                                                                <i class="fa fa-scissors" aria-hidden="true"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ARTICULO_DESCRIPCION" HeaderText="ARTÍCULO" />

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEditarColor" runat="server" CssClass="btn btn-xs btn-success" ToolTip="EDITAR COLOR" CommandName="EDITAR_COLOR" CommandArgument='<%# Eval("PEDIDO") + "," + Eval("ENC_PEDIDO_LINEA")+ "," + Eval("PEDIDO_PROGRAMACION") + "," + Eval("COLOR")  %>'>
                                                                <i class="fa fa-edit" aria-hidden="true"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="COLOR" HeaderText="COLOR" />


                                                <%--prueba: ini--%>
                                                <%--<asp:TemplateField HeaderText="COLOR">
                                                    <ItemTemplate>
                                                <asp:DropDownList ID="ddlCOLOR" AutoPostBack="false" runat="server" Width="130px"></asp:DropDownList>--%>
                                                <%--<asp:TextBox ID="txtCOLOR" runat="server" MaxLength="100" Width="100px" CssClass="form-control input-sm" Value='<%#Eval("COLOR") %>'></asp:TextBox>--%>
                                                <%--</ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <%--prueba: fin--%>

                                                <asp:BoundField DataField="UNIDAD" HeaderText="UND." />
                                                <asp:BoundField DataField="STOCK" HeaderText="STOCK" />

                                                <asp:BoundField DataField="CANTIDAD" HeaderText="SALDO" />

                                                <asp:TemplateField HeaderText="F. PROGRAMADA">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtPROGRAMADO" MaxLength="10" Width="100px" runat="server" CssClass="form-control input-sm" Text='<%#Eval("PROGRAMADO") %>' Value='<%#Eval("PROGRAMADO") %>'>
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="ESTADO_DESCRIPCION" HeaderText="ESTADO" Visible="false" />
                                                <asp:BoundField DataField="ESTADO" HeaderText="ESTADO" Visible="false" />
                                                <asp:BoundField DataField="PEDIDO" HeaderText="PEDIDO" Visible="false" />
                                                <asp:BoundField DataField="COLOR" HeaderText="COLOR" Visible="false" />
                                                <asp:BoundField DataField="ENC_PEDIDO_LINEA" HeaderText="ENC_PEDIDO_LINEA" Visible="false" />
                                                <asp:BoundField DataField="PEDIDO_PROGRAMACION" HeaderText="PEDIDO_PROGRAMACION" Visible="false" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btnGrabarParticion" runat="server" CssClass="btn btn-success" OnClientClick="return confirm('Está seguro de guardar los cambios realizados a la programación?')" OnClick="btnGrabarParticion_Click">
                                    <i class="fa fa-floppy-o" aria-hidden="true"></i> Grabar
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnCerrar" runat="server" CssClass="btn btn-danger" OnClick="btnCerrar_Click">
                                    <i class="fa fa-times" aria-hidden="true"></i> Salir
                            </asp:LinkButton>

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div id="modalObservacion" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <%--modal-sm--%>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="__hfOperacion" runat="server" />
                    <asp:HiddenField ID="__hfPedidoPro" runat="server" />
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title"><b>
                                <asp:Label ID="lblObservacion" runat="server"></asp:Label></b></h5>
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
                            <asp:LinkButton ID="btnOperacion" runat="server" CssClass="btn btn-success" OnClientClick="return confirm('Esta seguro de realizar esta operación?')" OnClick="btnOperacion_Click">
                                    <i class="fa fa-floppy-o" aria-hidden="true"></i> Grabar
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

    <div id="modalPartirProgramacion" class="modal fade" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" style="width: 300px">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="__hfPEDIDO" runat="server" />
                    <asp:HiddenField ID="__hfENC_PEDIDO_LINEA" runat="server" />
                    <asp:HiddenField ID="__hfPEDIDO_PROGRAMACION" runat="server" />
                    <asp:HiddenField ID="__hfCANTIDAD" runat="server" />
                    <asp:HiddenField ID="__hfUNIDAD" runat="server" />
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">PARTICIÓN</h5>
                        </div>
                        <div class="modal-body">
                            <div class="form-inline">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="form-group">
                                            <asp:Label ID="lblCANTIDADACTUAL" runat="server" Font-Bold="true" Text="Cantidad actual: "></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-xs-12">
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblNUEVACANTIDAD" Font-Bold="true"> Nueva cantidad </asp:Label>&nbsp<asp:Label runat="server" ID="lblUNIDADPART" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtCANTPROG1" runat="server" CssClass="form-control input-sm" Style="text-align: left" TextMode="Number" MaxLength="15"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btnPartir" runat="server" CssClass="btn btn-success" OnClientClick="return confirm('Está seguro de partir esta programación?')" OnClick="btnPartir_Click">
                                    <i class="fa fa-floppy-o" aria-hidden="true"></i> Grabar
                            </asp:LinkButton>
                            <button class="btn btn-danger" data-dismiss="modal" aria-hidden="true">
                                <i class="fa fa-times" aria-hidden="true"></i>Cancelar</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div id="modalSeleccionarColor" class="modal fade" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" style="width:400px">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="__hfCPedido" runat="server" />
                    <asp:HiddenField ID="__hfCEnc_Pedido_Linea" runat="server" />
                    <asp:HiddenField ID="__hfCPedido_Programacion" runat="server" />
                    <asp:HiddenField ID="__hfColor" runat="server" />
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title"><b>CAMBIO DE COLOR</b></h5>
                        </div>
                        <div class="modal-body table-responsive">

                            <div class="modal-responsive">

                                <div class="form-inline">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="form-group">
                                                <div class="form-inline">
                                                    <asp:Label runat="server" ID="Label3" Font-Bold="true"> Seleccione: </asp:Label>
                                                     <asp:DropDownList ID="ddlColor" Width="350px" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>

                        </div>
                        <div class="modal-footer">
                            <%--<asp:LinkButton ID="btnGrabarColor" runat="server" CssClass="btn btn-success" OnClientClick="return confirm('Está seguro de grabar este color?')" OnClick="btnGrabarColor_Click">
                                    <i class="fa fa-floppy-o" aria-hidden="true"></i> Grabar
                            </asp:LinkButton>--%>
                            <asp:LinkButton ID="btnGrabarColor" runat="server" CssClass="btn btn-success" OnClick="btnGrabarColor_Click">
                                    <i class="fa fa-floppy-o" aria-hidden="true"></i> Grabar
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnCancelarCambio" runat="server" CssClass="btn btn-danger" OnClick="btnCancelarCambio_Click">
                                    <i class="fa fa-times" aria-hidden="true"></i> Cancelar
                            </asp:LinkButton>
                            <%--<button class="btn btn-danger" data-dismiss="modal" aria-hidden="true">
                                <i class="fa fa-times" aria-hidden="true"></i>Cancelar</button>--%>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <%--    <div class="modal-body table-responsive">
                            <div class="modal-responsive">
                                <div class="row">
                                    <div class="col-sm-12">--%>


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
