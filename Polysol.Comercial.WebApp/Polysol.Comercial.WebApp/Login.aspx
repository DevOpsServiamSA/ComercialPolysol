<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Polysol.Comercial.WebApp.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="mobile-web-app-capable" content="yes" />
    <link rel="icon" sizes="192x192" href="/icon.png" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

    <title>Comercial - Polysol</title>

    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="Content/font-awesome.min.css" rel="stylesheet" />
    <link href="Content/Login.css" rel="stylesheet" />
    <link href="Content/animate.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/jquery-3.1.1.min.js" />
                <asp:ScriptReference Path="~/Scripts/bootstrap.min.js" />
                <asp:ScriptReference Path="~/Scripts/bootstrap_notify/bootstrap-notify.js" />
                <asp:ScriptReference Path="~/Scripts/ControlProcesos.js" />
                <asp:ScriptReference Path="~/Scripts/respond.js" />
            </Scripts>
        </asp:ScriptManager>

        <asp:HiddenField ID="__appSE" runat="server" />

        <%--<div class="navbar navbar-inverse navbar-fixed-top">
        <div class="container-fluid">
            <div class="navbar-header">
                <a class="navbar-brand" runat="server" href="~/Views/Login">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/favicon.jpg" style="display:inline-block; height:30px; margin-top:-5px" /> IK CONTROL DE PROCESOS
                </a>
            </div>
        </div>
    </div>--%>

        <div class="container">
            <div class="row">
                <div class="col-sm-4"></div>
                <div class="col-sm-4">
                    <div style="text-align: center; font-size: 2rem; font-weight: bold;">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/LOGOS_POLYSOL.jpg" Height="150" />
                        <p style="padding: 10px; border-radius: 5px; background-color: #033C74; color: #FFF;" class="text-center">
                            <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/favicon.jpg" style="display:inline-block; height:30px; margin-top:-5px" /> --%>PEDIDOS COMERCIALES
                        </p>
                        <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/favicon.jpg" style="display:inline-block; height:30px; margin-top:-5px" /> CONTROL DE PROCESOS <br /><br />--%>
                    </div>
                    <div class="panel panel-poly">
                        <%--<div class="panel-heading"><b>ACCESO AL SISTEMA</b></div>--%>
                        <div class="panel-body" style="text-align: center;">
                            <div class="form">

                               <%-- <div class="form-group">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="fa fa-exchange" aria-hidden="true"></i></span>
                                        <asp:DropDownList ID="ddlEnvasadora" runat="server" CssClass="form-control" data-label="ENVASADORA"></asp:DropDownList>
                                    </div>
                                </div>--%>

                                <div class="form-group">
                                    <%--<label for="txtUsuario"> USUARIO</label>--%>
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="fa fa-user" aria-hidden="true"></i></span>
                                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" data-label="USUARIO"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group has-feedback">
                                    <%--<label for="txtUsuario"> CONTRASEÑA</label>--%>
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="fa fa-asterisk" aria-hidden="true"></i></span>
                                        <asp:TextBox ID="txtContrasenha" runat="server" TextMode="Password" CssClass="form-control" data-label="CONTRASEÑA"></asp:TextBox>
                                    </div>
                                </div>

                                <%--<div class="form-group">
                                    <div class="form-check">
                                        <label class="form-check-label">
                                            <asp:CheckBox ID="chkUsaERP" runat="server" CssClass="form-check-input" />
                                            Usar credenciales de Exactus
                                        </label>
                                    </div>
                                </div>--%>

                                <asp:LinkButton ID="btnIngresar" runat="server" CssClass="btn btn-success btn-block" OnClick="btnIngresar_Click">
                                <i class="fa fa-sign-in" aria-hidden="true"></i> INGRESAR
                                </asp:LinkButton>

                                <%--<asp:LinkButton ID="btnSalir" runat="server" CssClass="btn btn-danger btn-block" Style="margin-top:15px;" OnClientClick="window.close();">
                                    <i class="fa fa-times" aria-hidden="true"></i> SALIR
                                </asp:LinkButton>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-4"></div>
            </div>
        </div>

    </form>
</body>
</html>
