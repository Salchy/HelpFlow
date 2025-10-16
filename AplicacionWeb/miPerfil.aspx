<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="miPerfil.aspx.cs" Inherits="AplicacionWeb.miPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <div class="card shadow-sm mx-auto" style="max-width: 500px;">
            <div class="card-header bg-primary text-white text-center">
                <h4>Mi Perfil</h4>
            </div>

            <div class="card-body">

                <!-- Usuario -->
                <div class="mb-3">
                    <label class="form-label fw-bold">Usuario</label>
                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" ReadOnly="true" />
                </div>

                <!-- Nombre -->
                <div class="mb-3">
                    <label class="form-label fw-bold">Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" ReadOnly="true" />
                </div>

                <!-- Correo -->
                <div class="mb-3">
                    <label class="form-label fw-bold">Correo</label>
                    <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" ReadOnly="true" />
                </div>

                <hr />

                <!-- Cambio de contraseña -->
                <h6 class="fw-bold text-secondary mb-3">Cambiar Contraseña</h6>

                <div class="mb-3">
                    <label class="form-label">Nueva Contraseña</label>
                    <asp:TextBox ID="txtNuevaClave" runat="server" CssClass="form-control" TextMode="Password" />
                </div>

                <div class="mb-3">
                    <label class="form-label">Confirmar Contraseña</label>
                    <asp:TextBox ID="txtConfirmarClave" runat="server" CssClass="form-control" TextMode="Password" />
                </div>

                <div class="text-center">
                    <asp:Button ID="btnGuardar" runat="server" Text="Actualizar Contraseña" CssClass="btn btn-success px-4" OnClick="btnGuardar_Click" />
                </div>

                <asp:Label ID="lblMensaje" runat="server" CssClass="mt-3 d-block text-center fw-bold"></asp:Label>

            </div>
        </div>
    </div>

</asp:Content>
