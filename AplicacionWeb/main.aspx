<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="AplicacionWeb.main1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2 class="text-center mb-4" style="color: #ffffff;">Dashboard de Tickets</h2>

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
