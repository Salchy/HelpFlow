<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="crearTicket.aspx.cs" Inherits="AplicacionWeb.crearTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-4">
        <div class="card shadow-sm">
            <div class="card-header bg-primary text-white">
                <h5 class="mb-0">Crear Nuevo Ticket</h5>
            </div>
            <div class="card-body">

                <div class="row">
                    <!-- Categoría -->
                    <div class="col-md-6 mb-3">
                        <label for="ddlCategoria" class="form-label fw-bold">Categoría</label>
                        <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged" />
                    </div>

                    <!-- Subcategoría -->
                    <div class="col-md-6 mb-3">
                        <label for="ddlSubCategoria" class="form-label fw-bold">Subcategoría</label>
                        <asp:DropDownList ID="ddlSubCategoria" runat="server" CssClass="form-select" />
                    </div>
                </div>

                <!-- Descripción -->
                <div class="mb-3">
                    <label for="txtDescripcion" class="form-label fw-bold">Descripción</label>
                    <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" placeholder="Describe el problema en detalle" />
                </div>

                <!-- Botones -->
                <div class="text-end">
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary me-2" />
                    <asp:Button ID="btnCrearTicket" runat="server" Text="Crear Ticket" CssClass="btn btn-primary" OnClick="btnCrearTicket_Click"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
