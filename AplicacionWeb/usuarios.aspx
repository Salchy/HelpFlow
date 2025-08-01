<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="usuarios.aspx.cs" Inherits="AplicacionWeb.usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-5">
        <h2 class="text-center mb-4" style="color: #ffffff;">Listado de usuarios</h2>
        <div class="card shadow rounded-4 p-4">
            <asp:Button ID="btnNuevoUsuario" runat="server" Text="Nuevo Usuario" CssClass="btn btn-success mb-3" OnClientClick="mostrarModalUsuario(); return false;" />

            <asp:GridView ID="gvUsuarios" runat="server" CssClass="table table-bordered table-dark table-hover" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Correo" HeaderText="Correo Electrónico" />
                    <asp:BoundField DataField="TipoUsuario" HeaderText="Tipo de Usuario" />
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <%--<asp:LinkButton ID="btnEditar" runat="server" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-sm btn-primary me-1" OnClick="btnEditar_Click">Editar</asp:LinkButton>--%>
                            <a href="javascript:void(0);"
                                class="btn btn-sm btn-primary"
                                data-nombre='<%# HttpUtility.JavaScriptStringEncode(Eval("Nombre").ToString()) %>'
                                data-correo='<%# HttpUtility.JavaScriptStringEncode(Eval("Correo").ToString()) %>'
                                data-nivel='<%# Eval("TipoUsuario").ToString() %>'
                                onclick="cargarDatosUsuario(this); return false;">Editar</a>
                            <asp:LinkButton ID="btnResetPass" runat="server" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-sm btn-warning text-dark" OnClick="btnResetPass_Click">Reestablecer Contraseña</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <!-- Modal de edición / creación -->
    <div class="modal fade" id="modalUsuario" tabindex="-1" aria-labelledby="modalUsuarioLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content bg-secondary text-white">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalUsuarioLabel">Crear usuario</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    <asp:HiddenField ID="hfUsuarioID" runat="server" />
                    <div class="mb-3">
                        <label for="txtNombre" class="form-label">Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label for="txtCorreo" class="form-label">Correo Electrónico</label>para
                        <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label for="ddlNivel" class="form-label">Nivel</label>
                        <asp:DropDownList ID="ddlNivel" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Administrador" Value="0" />
                            <asp:ListItem Text="Usuario" Value="1" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnGuardarUsuario" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardarUsuario_Click" />
                    <button type="button" class="btn btn-outline-light" data-bs-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Script para abrir el modal -->
    <script>
        function cargarDatosUsuario(elem) {
            const nombre = elem.getAttribute('data-nombre');
            const correo = elem.getAttribute('data-correo');
            const nivel = elem.getAttribute('data-nivel');

            document.getElementById('<%= txtNombre.ClientID %>').value = nombre;
            document.getElementById('<%= txtCorreo.ClientID %>').value = correo;
            document.getElementById('<%= ddlNivel.ClientID %>').value = nivel; // TODO: Hacer que aparezca preseleccionado el nivel del usuario

            var modal = new bootstrap.Modal(document.getElementById('modalUsuario'));
            modal.show();
        }

        function mostrarModalUsuario() {
            var myModal = new bootstrap.Modal(document.getElementById('modalUsuario'));
            limpiarYMostrarModal();
            myModal.show();
        }


        function limpiarYMostrarModal() {
            document.getElementById('<%= txtNombre.ClientID %>').value = '';
            document.getElementById('<%= txtCorreo.ClientID %>').value = '';
            document.getElementById('<%= ddlNivel.ClientID %>').value = '1';
        }
    </script>
</asp:Content>
