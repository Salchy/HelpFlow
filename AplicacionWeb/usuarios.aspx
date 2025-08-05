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
                                data-id='<%# Eval("Id") %>'
                                data-nombre='<%# Eval("Nombre").ToString() %>'
                                data-correo='<%# Eval("Correo").ToString() %>'
                                data-nivel='<%# (int)Eval("TipoUsuario") %>'
                                onclick="cargarDatosUsuario(this); return false;">Editar</a>
                            <a href="javascript:void(0);"
                                class="btn btn-sm btn-warning text-dark"
                                data-id='<%# Eval("Id") %>'
                                data-nombre='<%# Eval("Nombre") %>'
                                onclick="abrirModalResetPassword(this); return false;">Reestablecer Contraseña</a>
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
                        <label for="txtNombre" class="form-label">Nombre Completo</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label for="txtCorreo" class="form-label">Correo Electrónico</label>
                        <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" />
                    </div>
                    <%-- Esto va a aparecer dependiendo de si es un registro o es una modificacion --%>
                    <div id="divPassword">
                        <div class="mb-3">
                            <label for="txtUserName" class="form-label">Nombre de Usuario</label>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" />
                        </div>
                        <div class="mb-3">
                            <label for="txtPassword" class="form-label">Contraseña</label>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" />
                        </div>
                    </div>
                    <%--  --%>
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

    <!-- Modal de Reestablecer Contraseña -->
    <div class="modal fade" id="modalResetPassword" tabindex="-1" aria-labelledby="modalResetPasswordLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content bg-secondary text-white">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalResetPasswordLabel">Reestablecer Contraseña</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    <asp:HiddenField ID="hfUsuarioResetID" runat="server" />

                    <div class="mb-3">
                        <label class="form-label">Usuario</label>
                        <asp:Label ID="lblUsuarioResetNombre" runat="server" CssClass="form-control text-white-50 bg-dark" />
                    </div>

                    <div class="mb-3">
                        <label for="txtNuevaPassword" class="form-label">Nueva Contraseña</label>
                        <asp:TextBox ID="txtNuevaPassword" runat="server" TextMode="Password" CssClass="form-control" />
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnGuardarResetPassword" runat="server" Text="Guardar" CssClass="btn btn-warning text-dark" OnClick="btnGuardarResetPassword_Click" />
                    <button type="button" class="btn btn-outline-light" data-bs-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Script para abrir el modal -->
    <script>
        function cargarDatosUsuario(elem) {

            const id = elem.getAttribute('data-id');
            const nombre = elem.getAttribute('data-nombre');
            const correo = elem.getAttribute('data-correo');
            const nivel = elem.getAttribute('data-nivel');

            document.getElementById("modalUsuarioLabel").innerText = "Modificar usuario";
            document.getElementById('<%= hfUsuarioID.ClientID %>').value = id;
            document.getElementById('<%= txtNombre.ClientID %>').value = nombre;
            document.getElementById('<%= txtCorreo.ClientID %>').value = correo;
            document.getElementById('<%= ddlNivel.ClientID %>').value = nivel;
            document.getElementById("divPassword").style.display = "none";

            const modal = new bootstrap.Modal(document.getElementById('modalUsuario'));
            modal.show();
        }

        function mostrarModalUsuario() {
            document.getElementById("modalUsuarioLabel").innerText = "Crear usuario";
            document.getElementById("divPassword").style.display = "block";
            document.getElementById('<%= hfUsuarioID.ClientID %>').value = -1;
            const myModal = new bootstrap.Modal(document.getElementById('modalUsuario'));
            limpiarModal();
            myModal.show();
        }

        function abrirModalResetPassword(elem) {
            const id = elem.getAttribute('data-id');
            const nombre = elem.getAttribute('data-nombre');

            document.getElementById('<%= hfUsuarioResetID.ClientID %>').value = id;
            document.getElementById('<%= lblUsuarioResetNombre.ClientID %>').innerText = nombre;
            document.getElementById('<%= txtNuevaPassword.ClientID %>').value = '';

            const modal = new bootstrap.Modal(document.getElementById('modalResetPassword'));
            modal.show();
        }

        function limpiarModal() {
            document.getElementById('<%= txtNombre.ClientID %>').value = '';
            document.getElementById('<%= txtCorreo.ClientID %>').value = '';
            document.getElementById('<%= txtUserName.ClientID %>').value = '';
            document.getElementById('<%= txtPassword.ClientID %>').value = '';
            document.getElementById('<%= ddlNivel.ClientID %>').value = '1';
        }
    </script>
</asp:Content>
