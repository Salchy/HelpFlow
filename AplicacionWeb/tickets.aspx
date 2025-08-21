<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="tickets.aspx.cs" Inherits="AplicacionWeb.tickets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-4">
        <div class="container mt-4 bg-light p-3 rounded shadow-sm">
            <asp:GridView ID="dataGridTickets" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover mb-0 bg-secondary">
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
