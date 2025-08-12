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
                    <asp:BoundField DataField="UserName" HeaderText="Usuario" />
                    <asp:BoundField DataField="Correo" HeaderText="Correo Electrónico" />
                    <asp:BoundField DataField="TipoUsuario" HeaderText="Tipo de Usuario" />
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <%--<asp:LinkButton ID="btnEditar" runat="server" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-sm btn-primary me-1" OnClick="btnEditar_Click">Editar</asp:LinkButton>--%>
                            <a href="javascript:void(0);"
                                class="btn btn-sm btn-primary"
                                data-id='<%# Eval("Id") %>'
                                data-nombre='<%# Eval("Nombre").ToString() %>'
                                data-username='<%# Eval("UserName").ToString() %>'
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
                        <span class="invalid-feedback" id="errorNombre"></span>
                    </div>
                    <div class="mb-3">
                        <label for="txtCorreo" class="form-label">Correo Electrónico</label>
                        <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" />
                        <span class="invalid-feedback" id="errorCorreo"></span>
                    </div>
                    <%-- Esto va a aparecer dependiendo de si es un registro o es una modificacion --%>
                    <div id="divPassword">
                        <div class="mb-3">
                            <label for="txtUserName" class="form-label">Nombre de Usuario</label>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" />
                            <span class="invalid-feedback" id="errorUserName"></span>
                        </div>
                        <div class="mb-3">
                            <label for="txtPassword" class="form-label">Contraseña</label>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" />
                            <span class="invalid-feedback" id="errorPassword"></span>
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
                        <span class="invalid-feedback" id="errorPassword2"></span>
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
            const username = elem.getAttribute('data-username');

            document.getElementById("modalUsuarioLabel").innerText = "Modificar usuario";
            document.getElementById('<%= hfUsuarioID.ClientID %>').value = id;
            document.getElementById('<%= txtNombre.ClientID %>').value = nombre;
            document.getElementById('<%= txtUserName.ClientID %>').value = username;
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
            limpiarModal();
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
            limpiarErroresUsuario();
        }

        function limpiarErroresUsuario() {
            // Quitar clases de error y mensajes
            const campos = [
                { id: '<%= txtNombre.ClientID %>', error: 'errorNombre' },
                { id: '<%= txtCorreo.ClientID %>', error: 'errorCorreo' },
                { id: '<%= txtUserName.ClientID %>', error: 'errorUserName' },
                { id: '<%= txtPassword.ClientID %>', error: 'errorPassword' },
                { id: '<%= txtNuevaPassword.ClientID %>', error: 'errorPassword2' }
            ];
            campos.forEach(c => {
                document.getElementById(c.id).classList.remove('is-invalid');
                document.getElementById(c.error).innerText = '';
            });
        }

        function validarUsuario() {
            limpiarErroresUsuario();
            let valido = true;

            const nombre = document.getElementById('<%= txtNombre.ClientID %>');
            const correo = document.getElementById('<%= txtCorreo.ClientID %>');
            const user = document.getElementById('<%= txtUserName.ClientID %>');
            const pass = document.getElementById('<%= txtPassword.ClientID %>');
            const idUsuario = document.getElementById('<%= hfUsuarioID.ClientID %>').value;

            if (nombre.value.trim() === '') {
                nombre.classList.add('is-invalid');
                document.getElementById('errorNombre').innerText = 'El nombre es obligatorio.';
                valido = false;
            }
            if (correo.value.trim() === '') {
                correo.classList.add('is-invalid');
                document.getElementById('errorCorreo').innerText = 'El correo es obligatorio.';
                valido = false;
            }
            // Solo validar usuario y contraseña si es creación
            if (idUsuario == -1) {
                if (user.value.trim() === '') {
                    user.classList.add('is-invalid');
                    document.getElementById('errorUserName').innerText = 'El usuario es obligatorio.';
                    valido = false;
                }
                const passwordString = pass.value.trim();
                if (!validarPassword(passwordString, pass, 'errorPassword')) {
                    valido = false;
                }
            }
            return valido;
        }

        function validarPassword(passwordString, txtControl, spanControlID) {
            if (passwordString === '') {
                document.getElementById(spanControlID).innerText = 'La contraseña es obligatoria.';
                txtControl.classList.add('is-invalid');
                return false;
            }
            if (passwordString.length < 6) {
                document.getElementById(spanControlID).innerText = 'La contraseña debe tener 6 o más caracteres.';
                txtControl.classList.add('is-invalid');
                return false;
            }
            return true;
        }

        // Hook al botón guardar
        document.addEventListener('DOMContentLoaded', function () {
            const btn = document.getElementById('<%= btnGuardarUsuario.ClientID %>');
            if (btn) {
                btn.onclick = function (e) {
                    if (!validarUsuario()) {
                        e.preventDefault();
                        e.stopPropagation();
                    }
                };
            }
        });

        document.addEventListener('DOMContentLoaded', function () {
            const btn = document.getElementById('<%= btnGuardarResetPassword.ClientID %>');
            if (btn) {
                btn.onclick = function (e) {
                    const pass = document.getElementById('<%= txtNuevaPassword.ClientID %>');
                    const passwordString = pass.value.trim();
                    if (!validarPassword(passwordString, pass, 'errorPassword2')) {
                        e.preventDefault();
                        e.stopPropagation();
                        pass.classList.add('is-invalid');
                    }
                };
            }
        });
    </script>
</asp:Content>
