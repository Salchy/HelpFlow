<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="TicketForm.aspx.cs" Inherits="AplicacionWeb.crearTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-4">
        <div class="card shadow-sm">
            <div class="card-header bg-primary text-white">
                <h5 class="mb-0">Crear Nuevo Ticket</h5>
            </div>
            <div class="card-body">
                <asp:HiddenField ID="hfTicketID" runat="server" />

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

                <%-- A partir de acá, son los campos que van a ser para edicion --%>
                <asp:Panel ID="panelEdicion" runat="server">
                    <div class="mb-3">
                        <label for="ddlUsuario" class="form-label">Usuario</label>
                        <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="form-select" />
                    </div>

                    <div class="mb-3">
                        <label for="ddlSoporte" class="form-label">Soporte Asignado</label>
                        <asp:DropDownList ID="ddlSoporte" runat="server" CssClass="form-select" />
                    </div>

                    <div class="mb-3">
                        <label for="ddlEstado" class="form-label">Estado</label>
                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Solicitado" Value="0"></asp:ListItem>
                            <asp:ListItem Text="En Progreso" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Resuelto" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Cerrado" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </asp:Panel>

                <!-- Botones -->
                <div class="text-end">
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary me-2" />
                    <asp:Button ID="btnCrearTicket" runat="server" Text="Crear Ticket" CssClass="btn btn-primary" OnClick="btnCrearTicket_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
