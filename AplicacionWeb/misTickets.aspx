<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="misTickets.aspx.cs" Inherits="AplicacionWeb.misTickets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-5">
        <div class="card bg-dark text-white shadow-lg border-0 rounded-4">
            <div class="card-header bg-primary d-flex justify-content-between align-items-center rounded-top-4">
                <h4 class="mb-0">Mis Tickets</h4>
                <asp:Button ID="btnNuevo" runat="server" CssClass="btn btn-outline-light btn-sm" Text="+ Nuevo Ticket" OnClick="btnNuevo_Click" />
            </div>

            <div class="card-body p-4 bg-secondary rounded-bottom-4">
                <asp:GridView ID="gvTickets" runat="server" AutoGenerateColumns="False"
                    CssClass="table table-dark table-hover table-bordered mb-0 text-white"
                    GridLines="None" HeaderStyle-CssClass="table-active">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="#" />
                        <asp:BoundField DataField="Asunto" HeaderText="Título" />
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <%# ObtenerBadgeEstado(Eval("Estado").ToString()) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="FechaCreacion" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:TemplateField HeaderText="Acción">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlVer" runat="server" CssClass="btn btn-sm btn-outline-light"
                                    NavigateUrl='<%# "DetalleTicket.aspx?id=" + Eval("Id") %>' Text="Ver" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <asp:Panel ID="panelSinTicketsCreados" runat="server" CssClass="alert alert-secondary text-center" Visible="false">
                    No tienes tickets generados.
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
