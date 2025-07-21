<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="tickets.aspx.cs" Inherits="AplicacionWeb.tickets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mb-4">
        <div>
            <asp:GridView ID="dataGridTickets" AutoGenerateColumns="false" runat="server" CssClass="table table-dark table-hover">
                <Columns>
                    <asp:TemplateField HeaderText="Asunto">
                        <ItemTemplate>
                            <a href='<%# "ticket.aspx?id=" + Eval("Id") %>' class="text-info">
                                <%# Eval("Asunto") %>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="UsuarioCreador" HeaderText="Solicitante" />
                    <asp:BoundField DataField="Estado" HeaderText="Estado" />
                    <asp:BoundField DataField="FechaCreacion" HeaderText="Creado" />
                    <asp:BoundField DataField="Colaboradores" HeaderText="Colaboradores" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
