<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="TicketForm.aspx.cs" Inherits="AplicacionWeb.crearTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-4">
        <div class="card shadow-sm">
            <div class="card-header bg-primary text-white">
                <h5 id="lblTitulo" runat="server" class="mb-0">Crear Nuevo Ticket</h5>
            </div>
            <div class="card-body">
                <asp:HiddenField ID="hfTicketID" runat="server" />

                <asp:Panel ID="tittleSection" runat="server">
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
                </asp:Panel>

                <!-- Descripción -->
                <asp:Panel ID="descriptionSection" runat="server">
                    <div class="mb-3">
                        <label for="txtDescripcion" class="form-label fw-bold">Descripción</label>
                        <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" placeholder="Describe el problema en detalle" />
                    </div>
                </asp:Panel>

                <asp:Panel ID="panelEdicion" runat="server">
                    <%-- Estado --%>
                    <asp:Panel ID="statusSection" runat="server">
                        <div class="mb-3">
                            <label for="ddlEstado" class="form-label fw-bold">Estado</label>
                            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select">
                                <asp:ListItem Text="Solicitado" Value="0"></asp:ListItem>
                                <asp:ListItem Text="En Progreso" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Resuelto" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Cerrado" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </asp:Panel>

                    <%-- Solicitante --%>
                    <asp:Panel ID="ownerSection" runat="server">
                        <div class="mb-3">
                            <label for="ddlOwner" class="form-label fw-bold">Solicitante</label>
                            <input type="text" id="buscarSoporte" class="form-control mb-2" placeholder="Buscar solicitante..." onkeyup="filtrarLista('ddlOwner', this.value)" />

                            <asp:DropDownList ID="ddlOwner" runat="server" ClientIDMode="static" CssClass="form-select" />
                        </div>
                    </asp:Panel>

                    <!-- Supporters -->
                    <asp:Panel ID="supportersSection" runat="server">
                        <div class="mb-3">
                            <div class="row mb-3">
                                <div class="col-md-12">
                                    <label class="form-label fw-bold">Asignar Colaboradores al Ticket:</label>
                                    <div class="d-flex align-items-center">

                                        <%-- Lista de disponibles --%>
                                        <div class="flex-fill me-2">
                                            <input type="text" id="buscadorDisponibles" class="form-control mb-2" placeholder="Buscar usuario..." />
                                            <asp:ListBox ID="lstDisponibles" runat="server" CssClass="form-control" SelectionMode="Single"></asp:ListBox>
                                        </div>

                                        <!-- Botones agregar/quitar -->
                                        <div class="d-flex flex-column justify-content-center align-items-center me-2">
                                            <asp:Button ID="btnAddSupport" Text="&gt;&gt;" runat="server" CssClass="btn btn-primary mb-2" OnClick="btnAddSupport_Click" />
                                            <asp:Button ID="btnRemoveSupport" Text="&lt;&lt;" runat="server" CssClass="btn btn-secondary" OnClick="btnRemoveSupport_Click" />
                                        </div>

                                        <!-- Lista de asignados -->
                                        <div class="flex-fill ms-2">
                                            <label class="form-label">Asignados:</label>
                                            <div class="card shadow-sm p-3">
                                                <asp:ListBox ID="lstAsignados" runat="server" CssClass="form-control" SelectionMode="Single"></asp:ListBox>
                                                <
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="dateSection" runat="server">
                        <div class="row mb-3">
                            <div class="col">
                                <label for="txtFecha" class="form-label">Fecha</label>
                                <input type="date" id="txtFecha" runat="server" class="form-control" />
                            </div>
                            <div class="col">
                                <label for="txtHora" class="form-label">Hora</label>
                                <input type="time" id="txtHora" runat="server" class="form-control" />
                            </div>
                        </div>
                    </asp:Panel>
                </asp:Panel>

                <!-- Botones -->
                <div class="text-end">
                    <asp:Button ID="btnCancelar" runat="server" AutoPostBack="false" Text="Cancelar" CssClass="btn btn-secondary me-2" OnClientClick="history.back(); return false;" />
                    <asp:Button ID="btnCrearTicket" runat="server" Text="Crear Ticket" CssClass="btn btn-primary" OnClick="btnCrearTicket_Click" />
                </div>
            </div>
        </div>
    </div>

    <script>
        function filtrarLista(idSelect, texto) {
            const list = document.getElementById(idSelect);
            const filtro = texto.toLowerCase();
            for (let i = 0; i < list.options.length; i++) {
                const opt = list.options[i];
                opt.style.display = opt.text.toLowerCase().includes(filtro) ? '' : 'none';
            }
        }
    </script>
</asp:Content>
