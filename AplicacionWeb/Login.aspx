<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AplicacionWeb.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.7/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-LN+7fdVzj6u52u30Kp6M/trliBMCMKTyK833zpbD+pXdCLuTusPj697FH4R/5mcr" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.7/dist/js/bootstrap.bundle.min.js" integrity="sha384-ndDqU0Gzau9qJ1lfW4pNLlhNTkCfHzAVBReH9diLvGRem5+R9g2FzA8ZGN954O5Q" crossorigin="anonymous"></script>
</head>
<body class="bg-dark d-flex justify-content-center align-items-center" style="height: 100vh;">
    <form id="form1" runat="server" class="w-100" style="max-width: 400px;">
        <div class="card shadow-lg rounded-4 border-0">
            <div class="card-header bg-primary text-white text-center rounded-top-4">
                <h4 class="mb-0">HelpFlow</h4>
                <small class="text-light">Iniciar sesión</small>
            </div>
            <div class="card-body bg-secondary text-white rounded-bottom-4">
                <div class="mb-3">
                    <label for="txtUsuario" class="form-label">Usuario</label>
                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="txtPassword" class="form-label">Contraseña</label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" />
                </div>
                <div class="d-grid">
                    <asp:Button ID="btnLogin" runat="server" Text="Ingresar" CssClass="btn btn-primary" OnClick="btnLogin_Click" />
                </div>
            </div>
            <div class="text-center pb-3">
                <img src="imgs/LOGO APSA.bmp" alt="Logo Empresa" style="height: 28px; opacity: 0.8; margin-top: 20px">
                <div class="text-muted small mt-1">© 2025 Leandro Correa</div>
            </div>
        </div>

    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.7/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
