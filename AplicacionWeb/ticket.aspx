<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="ticket.aspx.cs" Inherits="AplicacionWeb.ticket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-4">
        <div class="card shadow rounded-4 p-4">
            <h2 class="mb-3 text-primary" id="tituloTicket" runat="server">Detalle del Ticket</h2>

            <div class="row mb-3">
                <div class="col-md-5">
                    <label class="fw-bold">Título:</label>
                    <asp:Label ID="lblTitulo" runat="server" CssClass="form-control-plaintext" />
                </div>
                <div class="col-md-5">
                    <label class="fw-bold">Estado:</label>
                    <asp:Label ID="lblEstado" runat="server" CssClass="badge bg-secondary" />
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-5">
                    <label class="fw-bold">Solicitante:</label>
                    <asp:Label ID="lblSolicitante" runat="server" CssClass="form-control-plaintext" />
                </div>
                <div class="col-md-5">
                    <label class="fw-bold">Fecha de creación:</label>
                    <asp:Label ID="lblFecha" runat="server" CssClass="form-control-plaintext" />
                </div>
            </div>

            <div class="mb-3">
                <label class="fw-bold">Descripción:</label>
                <asp:Label ID="lblDescripcion" runat="server" CssClass="form-control-plaintext" />
            </div>

            <div class="row mb-3">
                <div class="col-md-4">
                    <label class="fw-bold">Usuario asignado:</label>
                    <asp:Label ID="lblUsuarioAsignado" runat="server" CssClass="form-control-plaintext" />
                </div>
            </div>

            <div class="mt-4">
                <a href="Tickets.aspx" class="btn btn-outline-primary">← Volver al listado</a>
                <asp:Button ID="btnMostrarFormulario" runat="server" AutoPostBack="false" Text="Agregar Commit" CssClass="btn btn-outline-success" OnClientClick="mostrarFormulario(); return false;" />
            </div>
        </div>

        <div id="formCommit" style="display: none;">
            <div class="card mt-4">
                <div class="card-header bg-success text-white">
                    Registrar nuevo commit
                </div>
                <div class="card-body">
                    <div class="form-group mb-3">
                        <div class="btn-group d-flex" data-toggle="buttons">
                            <label class="btn btn-outline-primary w-50 radioButtonStyle active">
                                <asp:RadioButton ID="rbCliente" runat="server" GroupName="TipoCommit" AutoPostBack="false" CssClass="typeCommit-group d-none" Checked="true" />
                                Respuesta al Cliente
                            </label>
                            <label class="btn btn-outline-warning w-50 radioButtonStyle">
                                <asp:RadioButton ID="rbInterno" runat="server" GroupName="TipoCommit" AutoPostBack="false" CssClass="typeCommit-group d-none" />
                                Uso Interno
                            </label>
                        </div>
                    </div>

                    <div class="form-group mb-3">
                        <asp:TextBox ID="txtMensaje" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" Placeholder="Escriba su commit..." />
                    </div>

                    <asp:Button ID="btnCancelCommit" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClientClick="ocultarFormulario(); return false;" AutoPostBack="false" />
                    <asp:Button ID="btnRegistrarCommit" runat="server" Text="Registrar Commit" CssClass="btn btn-success" OnClick="btnRegistrarCommit_Click" />
                </div>
            </div>
        </div>

        <div class="card mt-4">
            <div class="card-header bg-primary text-white">
                Commits relacionados
            </div>
            <div class="card-body">

                <div class="form-group mb-3">
                    <div class="btn-group" data-toggle="buttons">
                        <label class="btn btn-outline-primary w-50 radioButtonStyle">
                            <asp:RadioButton ID="radioBtn1" runat="server" GroupName="FilterActivity" AutoPostBack="false" CssClass="filterActivity-group d-none" />
                            Todo
                        </label>
                        <label class="btn btn-outline-primary w-50 radioButtonStyle active">
                            <asp:RadioButton ID="radioBtn2" runat="server" GroupName="FilterActivity" AutoPostBack="false" CssClass="filterActivity-group d-none" Checked="true" />
                            Comentarios
                        </label>
                        <label class="btn btn-outline-primary w-50 radioButtonStyle">
                            <asp:RadioButton ID="radioBtn3" runat="server" GroupName="FilterActivity" AutoPostBack="false" CssClass="filterActivity-group d-none" />
                            Actividad
                        </label>
                    </div>
                </div>

                <asp:Repeater ID="rptCommits" runat="server">
                    <HeaderTemplate>
                        <div class="list-group">
                    </HeaderTemplate>

                    <ItemTemplate>
                        <div class='<%# (Convert.ToBoolean(Eval("TipoCommit")) ? "list-group-item bg-warning" : "list-group-item bg-light") %> list-group-item-action flex-column align-items-start mb-2'>
                            <div class="d-flex w-100 justify-content-between">
                                <h5 class="mb-1"><%# Eval("Autor") %></h5>
                                <small class="text-muted"><%# Eval("Fecha", "{0:dd/MM/yyyy HH:mm}") %></small>
                            </div>
                            <p class="mb-1"><%# Eval("Mensaje") %></p>
                        </div>
                    </ItemTemplate>

                    <FooterTemplate>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>

                <asp:Panel ID="pnlSinCommits" runat="server" CssClass="alert alert-secondary text-center" Visible="false">
                    Aún no hay comentarios.
                </asp:Panel>
            </div>
        </div>
    </div>

    <script>
        document.addEventListener("DOMContentLoaded", function () {
            // Aplica a cualquier grupo de radio buttons dentro de btn-group
            document.querySelectorAll(".btn-group").forEach(function (group) {
                const radios = group.querySelectorAll(".radioButtonStyle"); // compatibilidad con ASP.NET

                radios.forEach(function (radio) {
                    radio.addEventListener("change", function () {
                        // Quitar clase activa de todos los botones del grupo
                        group.querySelectorAll(".btn").forEach(btn => btn.classList.remove("active"));

                        // Agregar clase activa al label que contiene el radio clickeado
                        const label = radio.closest("label");
                        if (label) label.classList.add("active");
                    });

                    // Marcar activo el seleccionado por defecto al cargar
                    if (radio.checked) {
                        const label = radio.closest("label");
                        if (label) label.classList.add("active");
                    }
                });
            });
        });

        function mostrarFormulario() {
            document.getElementById("formCommit").style.display = "block";
        }

        function ocultarFormulario() {
            document.getElementById("formCommit").style.display = "none";
        }
    </script>
</asp:Content>
