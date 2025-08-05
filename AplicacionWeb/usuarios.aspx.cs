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
            UsuarioDatos usuarioDatos = new UsuarioDatos();
            if (!IsPostBack)
            {
                listaUsuarios = usuarioDatos.GetUsuarios();
                bindearDatos();
            }
        }
        private void bindearDatos()
        {
            gvUsuarios.DataSource = null;
            gvUsuarios.DataSource = listaUsuarios;
            gvUsuarios.DataBind();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {

        }

        protected void btnResetPass_Click(object sender, EventArgs e)
        {

        }

        protected void btnNuevoUsuario_Click(object sender, EventArgs e)
        {

        }

        protected void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            UsuarioDatos usuarioDatos = new UsuarioDatos();
            try
            {
                string nombreCompleto = txtNombre.Text.Trim();
                string correoUsuario = txtCorreo.Text.Trim();
                if (string.IsNullOrEmpty(nombreCompleto) || string.IsNullOrEmpty(correoUsuario))
                {
                    Modal.Mostrar(this, "Error", "Todos los campos son obligatorios.", "error");
                    return;
                }

                // Validar si estoy haciendo una modificacion o un registro
                int idUsuario = int.Parse(hfUsuarioID.Value);
                if (idUsuario != -1)
                {
                    // Modificación de usuario existente
                    UsuarioDTO usuarioExistente = listaUsuarios.FirstOrDefault(u => u.Id == idUsuario);
                    usuarioExistente.Nombre = nombreCompleto;
                    usuarioExistente.Correo = correoUsuario;
                    usuarioExistente.TipoUsuario = (UsuarioDTO.nivelUsuario)int.Parse(ddlNivel.SelectedValue);
                    usuarioDatos.actualizarUsuario(usuarioExistente);
                    Modal.Mostrar(this, "Éxito", "Usuario modificado correctamente.", "exito");
                    bindearDatos();
                    return;
                }
                // Es una nueva creación de usuario
                string nombreUsuario = txtUserName.Text.Trim();
                string password = txtPassword.Text.Trim();
                if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(nombreUsuario))
                {
                    Modal.Mostrar(this, "Error", "El nombre de usuario y la contraseña son obligatorios.", "error");
                    return;
                }
                if (usuarioDatos.GetUsuario(nombreUsuario) != null)
                {
                    Modal.Mostrar(this, "Error", "El nombre de usuario ya está en uso.", "error");
                    return;
                }
                
                UsuarioDTO nuevoUsuario = new UsuarioDTO
                {
                    Nombre = nombreCompleto,
                    UserName = nombreUsuario,
                    Correo = correoUsuario,
                    TipoUsuario = (UsuarioDTO.nivelUsuario)int.Parse(ddlNivel.SelectedValue),
                };
                if (usuarioDatos.registrarUsuario(nuevoUsuario, password))
                {
                    listaUsuarios.Add(nuevoUsuario);
                    Modal.Mostrar(this, "Éxito", "Usuario creado correctamente.", "exito");
                    bindearDatos();
                }
                else
                {
                    Modal.Mostrar(this, "Error", "No se pudo crear el usuario.", "error");
                    return;
                }
            }
            catch (Exception ex)
            {
                Modal.Mostrar(this, "Error", "No se pudo crear el usuario: " + ex.Message, "error");
            }
        }

        protected void btnGuardarResetPassword_Click(object sender, EventArgs e)
        {
            UsuarioDatos usuarioDatos = new UsuarioDatos();
            try
            {
                int idUsuario = int.Parse(hfUsuarioResetID.Value);
                string nuevaContrasena = txtNuevaPassword.Text.Trim();
                if (nuevaContrasena.Length < 6)
                {
                    Modal.Mostrar(this, "Error", "La contraseña debe tener al menos 6 caracteres.", "error");
                    return;
                }
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