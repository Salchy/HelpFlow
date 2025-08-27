using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccesoDatos;
using Dominio;
using DTO;
using AplicacionWeb.Helpers;

namespace AplicacionWeb
{
    public partial class usuarios : System.Web.UI.Page
    {
        private UsuarioDatos usuarioDatos = new UsuarioDatos();
        private List<UsuarioDTO> listaUsuarios
        {
            get
            {
                if (Session["Users"] == null)
                    Session["Users"] = new List<UsuarioDTO>();
                return (List<UsuarioDTO>)Session["Users"];
            }
            set
            {
                Session["Users"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listaUsuarios = usuarioDatos.GetUsuarios();
                bindearDatos();

                gvUsuarios.DataSource = null;
                ddlEmpresa.DataTextField = "Nombre";
                ddlEmpresa.DataValueField = "Id";
                ddlEmpresa.DataSource = new EmpresaDatos().GetEmpresas();
                ddlEmpresa.DataBind();
                ddlEmpresa.Items.Insert(0, new ListItem("-- Seleccione una empresa --", "0"));
            }
        }
        private void bindearDatos()
        {
            gvUsuarios.DataSource = null;
            gvUsuarios.DataSource = listaUsuarios;
            gvUsuarios.DataBind();
        }

        protected void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                string nombreCompleto = txtNombre.Text.Trim();
                string correoUsuario = txtCorreo.Text.Trim();
                int empresaSeleccionada = int.Parse(ddlEmpresa.SelectedItem.Value);

                // Validar si estoy haciendo una modificacion o un registro
                int idUsuario = int.Parse(hfUsuarioID.Value);
                if (idUsuario != -1)
                {
                    // Modificación de usuario existente
                    UsuarioDTO usuarioExistente = listaUsuarios.FirstOrDefault(u => u.Id == idUsuario);

                    if (correoEnUso(correoUsuario) && usuarioExistente.Correo != correoUsuario)
                    {
                        return;
                    }

                    usuarioExistente.Nombre = nombreCompleto;
                    usuarioExistente.Correo = correoUsuario;
                    usuarioExistente.IdEmpresa = empresaSeleccionada;
                    usuarioExistente.TipoUsuario = (UsuarioDTO.nivelUsuario)int.Parse(ddlNivel.SelectedValue);
                    usuarioDatos.actualizarUsuario(usuarioExistente);
                    Modal.Mostrar(this, "Éxito", "Usuario modificado correctamente.", "exito");
                    bindearDatos();
                    return;
                }

                // Es una nueva creación de usuario
                string nombreUsuario = txtUserName.Text.Trim();
                string password = txtPassword.Text.Trim();
                if (correoEnUso(correoUsuario))
                {
                    string script = $@"
                    document.getElementById('{txtCorreo.ClientID}').classList.add('is-invalid');
                    document.getElementById('errorCorreo').innerText = 'El correo ya está en uso.';
                    var modal = new bootstrap.Modal(document.getElementById('modalUsuario'));
                    modal.show();";
                    ScriptManager.RegisterStartupScript(this, GetType(), "errorCorreo", script, true);
                    return;
                }
                if (usuarioDatos.GetUsuario(nombreUsuario) != null)
                {
                    string script = $@"
                    document.getElementById('{txtUserName.ClientID}').classList.add('is-invalid');
                    document.getElementById('errorUserName').innerText = 'El usuario ya está en uso.';
                    var modal = new bootstrap.Modal(document.getElementById('modalUsuario'));
                    modal.show();";
                    ScriptManager.RegisterStartupScript(this, GetType(), "errorUserName", script, true);
                    return;
                }

                UsuarioDTO nuevoUsuario = new UsuarioDTO
                {
                    Nombre = nombreCompleto,
                    UserName = nombreUsuario,
                    Correo = correoUsuario,
                    IdEmpresa = empresaSeleccionada,
                    TipoUsuario = (UsuarioDTO.nivelUsuario)int.Parse(ddlNivel.SelectedValue),
                };

                if (usuarioDatos.registrarUsuario(nuevoUsuario, password))
                {
                    listaUsuarios.Add(nuevoUsuario);
                    Modal.Mostrar(this, "Éxito", "Usuario creado correctamente.", "exito");
                    bindearDatos();
                    return;
                }
            }
            catch (Exception ex)
            {
                Modal.Mostrar(this, "Error", "No se pudo crear el usuario: " + ex.Message, "error");
            }
        }

        private bool correoEnUso(string correoUsuario)
        {
            if (usuarioDatos.emailInUse(correoUsuario))
            {
                return true;
            }
            return false;
        }

        protected void btnGuardarResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                int idUsuario = int.Parse(hfUsuarioResetID.Value);
                string nuevaContrasena = txtNuevaPassword.Text.Trim();
                usuarioDatos.updatePassword(idUsuario, nuevaContrasena);
                Modal.Mostrar(this, "Éxito", "Contraseña restablecida correctamente.", "exito");
            }
            catch (Exception ex)
            {
                Modal.Mostrar(this, "Error", "No se pudo restablecer la contraseña: " + ex.Message, "error");
            }
        }
    }
}