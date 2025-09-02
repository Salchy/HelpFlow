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
using System.Collections;

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
                imprimirUsuarios(listaUsuarios);

                cargarEmpresasDropDown(ddlEmpresa);
                ddlEmpresa.Items.Insert(0, new ListItem("-- Seleccione una empresa --", "0"));
                cargarEmpresasDropDown(ddlEmpresasFilter);
                ddlEmpresasFilter.Items.Insert(0, new ListItem("Todas las empresas", "0"));
            }
        }

        private void cargarEmpresasDropDown(DropDownList desplegable)
        {
            desplegable.DataSource = null;
            desplegable.DataTextField = "Nombre";
            desplegable.DataValueField = "Id";
            desplegable.DataSource = new EmpresaDatos().GetEmpresas();
            desplegable.DataBind();
        }
        private void imprimirUsuarios(List<UsuarioDTO> lista)
        {
            gvUsuarios.DataSource = null;
            gvUsuarios.DataSource = lista;
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
                    imprimirUsuarios(listaUsuarios);
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
                    imprimirUsuarios(listaUsuarios);
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

        protected void ddlEstadoFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            imprimirUsuarios(filtrarUsuarios());
        }

        private List<UsuarioDTO> filtrarUsuarios()
        {
            string empresa = ddlEmpresasFilter.SelectedValue;
            string estado = ddlEstadoFilter.SelectedValue;
            string toFind = txtBuscar.Text;

            List<UsuarioDTO> listaFiltrada = listaUsuarios;

            if (empresa != "0")
            {
                int idEmpresa = int.Parse(empresa);
                listaFiltrada = listaUsuarios.Where(u => u.IdEmpresa == idEmpresa).ToList();
            }
            else
            {
                listaFiltrada = listaUsuarios;
            }
            if (estado != "2")
            {
                bool estadoBool = estado == "1";
                listaFiltrada = listaFiltrada.Where(u => u.Estado == estadoBool).ToList();
            }
            if (toFind != "")
            {
                listaFiltrada = listaFiltrada.Where(u => u.Nombre.ToLower().Contains(toFind.ToLower()) || u.Correo.ToLower().Contains(toFind.ToLower()) || u.UserName.ToLower().Contains(toFind.ToLower())).ToList();
            }

            return listaFiltrada;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            imprimirUsuarios(filtrarUsuarios());
        }
    }
}