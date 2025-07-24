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
            </div>
        </div>

        <div class="card mt-4">
            <div class="card-header bg-primary text-white">
                Commits relacionados
            </div>
            <div class="card-body">
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
</asp:Content>
