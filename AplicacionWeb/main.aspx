<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="AplicacionWeb.main1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2 class="text-center mb-4" style="color: #ffffff;">Dashboard de Tickets</h2>

        <div class="d-flex justify-content-center mb-4">
            <asp:LinkButton ID="btnHoy" runat="server" CssClass="btn btn-outline-light mx-1" CommandArgument="Hoy" OnClick="btnFiltro_Click">Hoy</asp:LinkButton>
            <asp:LinkButton ID="btnSemana" runat="server" CssClass="btn btn-outline-light mx-1" CommandArgument="Semana" OnClick="btnFiltro_Click">Esta semana</asp:LinkButton>
            <asp:LinkButton ID="btnMes" runat="server" CssClass="btn btn-outline-light mx-1" CommandArgument="Mes" OnClick="btnFiltro_Click">Este mes</asp:LinkButton>
            <asp:LinkButton ID="btnTodo" runat="server" CssClass="btn btn-light mx-1 fw-bold" CommandArgument="Todo" OnClick="btnFiltro_Click">Todo</asp:LinkButton>
        </div>

        <div class="row">
            <div class="col-md-3">
                <div class="card text-white bg-primary mb-3">
                    <div class="card-header">Solicitados</div>
                    <div class="card-body">
                        <h5 class="card-title">
                            <asp:Label ID="lblSolicitados" runat="server" Text="0"></asp:Label>
                        </h5>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card text-white bg-warning mb-3">
                    <div class="card-header">En progreso</div>
                    <div class="card-body">
                        <h5 class="card-title">
                            <asp:Label ID="lblEnProgreso" runat="server" Text="0"></asp:Label>
                        </h5>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card text-white bg-success mb-3">
                    <div class="card-header">Resueltos</div>
                    <div class="card-body">
                        <h5 class="card-title">
                            <asp:Label ID="lblResueltos" runat="server" Text="0"></asp:Label>
                        </h5>
                    </div>
                </div>
            </div>

            <div class="col-md-3">
                <div class="card text-white bg-secondary mb-3">
                    <div class="card-header">Cerrados</div>
                    <div class="card-body">
                        <h5 class="card-title">
                            <asp:Label ID="lblCerrados" runat="server" Text="0"></asp:Label>
                        </h5>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
