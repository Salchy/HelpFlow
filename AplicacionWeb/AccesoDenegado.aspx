
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccesoDenegado.aspx.cs" Inherits="AplicacionWeb.AccesoDenegado" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Acceso denegado | HelpFlow</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.7/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body class="bg-light">
    <form id="form1" runat="server">
        <main class="container min-vh-100 d-flex align-items-center justify-content-center">
            <div class="card shadow-sm border-0 text-center p-4 p-md-5"
                 style="max-width: 500px; width: 100%;">

                <div class="mb-3">
                    <h4 class="fw-bold text-primary">HelpFlow</h4>
                </div>

                <div class="display-1 text-danger mb-3">
                    <i class="bi bi-shield-lock"></i>
                </div>

                <h1 class="h3 fw-bold mb-3">Acceso denegado</h1>

                <p class="text-secondary mb-4">
                    No tenés permisos para acceder a esta página.
                    Si creés que se trata de un error, contactá al
                    administrador del sistema.
                </p>

                <asp:Button ID="btnVolver" runat="server" Text="Volver al inicio" CssClass="btn btn-primary w-100" OnClick="btnVolver_Click" />
            </div>
        </main>
    </form>
</body>
</html>
