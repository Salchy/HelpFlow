<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="DetalleTicket.aspx.cs" Inherits="AplicacionWeb.DetalleTicket" %>

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
                    <label class="fw-bold">Usuarios asignados:</label>
                    <asp:Label ID="lblUsuarioAsignado" runat="server" CssClass="form-control-plaintext" />
                </div>
            </div>

            <div class="mt-4">
                <asp:Button ID="btnVolver" runat="server" AutoPostBack="false" Text="← Volver al listado" CssClass="btn btn-outline-primary" OnClientClick="history.back(); return false;" />
                <asp:Button ID="btnMostrarFormulario" runat="server" AutoPostBack="false" Text="Agregar Comentario" CssClass="btn btn-outline-success" OnClientClick="mostrarFormulario(); return false;" />
            </div>
        </div>

        <div id="formComentario" style="display: none;">
            <div class="card mt-4">
                <div class="card-header bg-success text-white">
                    Registrar un comentario
                </div>
                <div class="card-body">
                    <div class="form-group mb-3">
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
                Comentarios
            </div>
            <div class="card-body">
                <asp:Repeater ID="rptCommits" runat="server">
                    <HeaderTemplate>
                        <div class="list-group">
                    </HeaderTemplate>

                    <ItemTemplate>
                        <div class="list-group-item bg-light list-group-item-action flex-column align-items-start mb-2">
                            <div class="d-flex w-100 justify-content-between">
                                <h5 class="mb-1"><%# Eval("AutorNombre") %></h5>
                                <small class="text-muted"><%# Eval("Fecha", "{0:dd/MM/yyyy HH:mm}") %></small>
                            </div>
                            <p class="mb-1"><%# Eval("Mensaje") %></p>
                        </div>
                    </ItemTemplate>

                    <FooterTemplate>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>

                <asp:Panel ID="pnlSinCommits" runat="server" CssClass="alert alert-secondary text-center">
                    Aún no hay comentarios.
                </asp:Panel>
            </div>
        </div>

        <script>
            function mostrarFormulario() {
                document.getElementById("formComentario").style.display = "block";
            }

            function ocultarFormulario() {
                document.getElementById("formComentario").style.display = "none";
            }
        </script>
</asp:Content>
