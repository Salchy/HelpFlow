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

                <asp:Panel ID="panelEdicion" runat="server">
                    <%-- Estado --%>
                    <div class="mb-3">
                        <label for="ddlEstado" class="form-label fw-bold">Estado</label>
                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Solicitado" Value="0"></asp:ListItem>
                            <asp:ListItem Text="En Progreso" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Resuelto" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Cerrado" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <%-- Solicitante --%>
                    <div class="mb-3">
                        <label for="ddlOwner" class="form-label fw-bold">Solicitante</label>
                        <input type="text" id="buscarSoporte" class="form-control mb-2" placeholder="Buscar solicitante..." onkeyup="filtrarLista('ddlOwner', this.value)" />

                        <asp:DropDownList ID="ddlOwner" runat="server" ClientIDMode="static" CssClass="form-select" />
                    </div>

                    <!-- Lista de disponibles -->
                    <div class="mb-3">
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <label class="form-label fw-bold">Asignar Colaboradores al Ticket:</label>
                                <div class="d-flex align-items-center">
                                    <div class="flex-fill me-2">
                                        <input type="text" id="buscadorDisponibles" class="form-control mb-2" placeholder="Buscar usuario..." onkeyup="filtrarLista('lstDisponibles', this.value)" />
                                        <select id="lstDisponibles" runat="server" ClientIDMode="static" class="form-select" size="10"/>
                                    </div>

                                    <!-- Botones agregar/quitar -->
                                    <div class="d-flex flex-column justify-content-center align-items-center me-2">
                                        <button type="button" class="btn btn-primary mb-2" onclick="moverSeleccion('lstDisponibles','lstAsignados')">
                                            &gt;&gt;
                                        </button>
                                        <button type="button" class="btn btn-secondary" onclick="moverSeleccion('lstAsignados','lstDisponibles')">
                                            &lt;&lt;
                                        </button>
                                    </div>

                                    <!-- Lista de asignados -->
                                    <div class="flex-fill ms-2">
                                        <label class="form-label">Asignados:</label>
                                        <select id="lstAsignados" runat="server" ClientIDMode="static" class="form-select" size="10" />
                                    </div>
                                </div>
                            </div>
                        </div>
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

    <script>
        function filtrarLista(idSelect, texto) {
            const list = document.getElementById(idSelect);
            const filtro = texto.toLowerCase();
            for (let i = 0; i < list.options.length; i++) {
                const opt = list.options[i];
                opt.style.display = opt.text.toLowerCase().includes(filtro) ? '' : 'none';
            }
        }

        // Mover elementos seleccionados de una lista a otra
        function moverSeleccion(idOrigen, idDestino) {
            const origen = document.getElementById(idOrigen);
            const destino = document.getElementById(idDestino);

            const seleccionados = Array.from(origen.selectedOptions);
            seleccionados.forEach(opt => {
                destino.appendChild(opt);
            });
        }
    </script>
</asp:Content>
