<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="tickets.aspx.cs" Inherits="AplicacionWeb.tickets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-4">
        <div class="card shadow rounded-4 p-4">
            <div class="card-body">
                <div class="row g-2 align-items-end">
                    <!-- Texto a buscar -->
                    <div class="col-md-3">
                        <label for="txtId" class="form-label">Buscador</label>
                        <asp:TextBox ID="txtId" runat="server" CssClass="form-control" />
                    </div>

                    <%-- Buscar por --%>
                    <div class="col-md-2">
                        <label for="Criterio" class="form-label">Criterio</label>
                        <asp:DropDownList ID="ddlCriterio" runat="server" CssClass="form-select" AutoPostBack="true">
                            <asp:ListItem Text="Solicitante" Value="1" />
                            <asp:ListItem Text="Colaboradores" Value="2" />
                            <asp:ListItem Text="Asunto" Value="3" />
                            <asp:ListItem Text="Codigo" Value="4" />
                        </asp:DropDownList>
                    </div>

                    <!-- Categoría -->
                    <div class="col-md-3">
                        <label for="ddlCategoria" class="form-label">Categoría</label>
                        <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged" />
                    </div>

                    <!-- SubCategoría -->
                    <div class="col-md-3">
                        <label for="ddlSubCategoria" class="form-label">Subcategoría</label>
                        <asp:DropDownList ID="ddlSubCategoria" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlSubCategoria_SelectedIndexChanged" />
                    </div>

                    <!-- Estado -->
                    <div class="col-md-2">
                        <label for="ddlEstado" class="form-label">Estado</label>
                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlEstadoFilter_SelectedIndexChanged">
                            <asp:ListItem Text="Todos" Value="-1" />
                            <asp:ListItem Text="Solicitado" Value="0" />
                            <asp:ListItem Text="En progreso" Value="1" />
                            <asp:ListItem Text="Resuelto" Value="2" />
                            <asp:ListItem Text="Cerrado" Value="3" />
                        </asp:DropDownList>
                    </div>

                    <!-- Botón buscar -->
                    <div class="col-md-2 d-grid">
                        <asp:Button ID="Button2" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
                    </div>
                    <div class="col-md-2 d-grid">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-success" Text="Nuevo Ticket" />
                    </div>
                </div>
            </div>

            <asp:GridView ID="dataGridTickets" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover bg-secondary">
                <HeaderStyle CssClass="table-secondary" />

                <Columns>
                    <asp:TemplateField HeaderText="Código">
                        <ItemTemplate>
                            <a href='<%# "TKT-" + Eval("Id") %>' class="fw-bold text-decoration-none text-primary">
                                <%# "TKT-" + Eval("Id") %>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Asunto">
                        <ItemTemplate>
                            <a href='<%# "TKT-" + Eval("Id") %>' class="fw-bold text-decoration-none text-primary">
                                <%# Eval("Asunto") %>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="UsuarioCreador" HeaderText="Solicitante"
                        ItemStyle-CssClass="text" />

                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <span class='<%# GetEstadoCss(Eval("Estado").ToString()) %>'>
                                <%# Eval("Estado") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="FechaCreacion" HeaderText="Creado"
                        DataFormatString="{0:dd/MM/yyyy HH:mm}"
                        ItemStyle-CssClass="text" />

                    <asp:BoundField DataField="Colaboradores" HeaderText="Colaboradores"
                        ItemStyle-CssClass="text" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
