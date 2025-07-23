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
    </div>
</asp:Content>
