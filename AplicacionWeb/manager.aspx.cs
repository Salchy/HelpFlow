using AccesoDatos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AplicacionWeb
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DatosClasificacionTicket datosClasificacion = new DatosClasificacionTicket();

            if (!IsPostBack)
            {
                List<Categoria> categorias = datosClasificacion.listarCategorias();
                lstCategorias.DataSource = categorias;
                lstCategorias.DataTextField = "Nombre";
                lstCategorias.DataValueField = "Id";
                lstCategorias.DataBind();

                lstCategorias.SelectedIndex = 0;

                loadSubCategories();
            }
        }

        private void loadSubCategories()
        {
            int categoryID = int.Parse(lstCategorias.SelectedValue);

            DatosClasificacionTicket datosClasificacion = new DatosClasificacionTicket();
            List<SubCategoria> subCategorias = datosClasificacion.listarSubCategorias(categoryID);

            lstSubCategorias.DataSource = subCategorias;
            lstSubCategorias.DataTextField = "Nombre";
            lstSubCategorias.DataValueField = "Id";
            lstSubCategorias.DataBind();

            if (subCategorias.Count == 0)
            {
                lstSubCategorias.Text = "No hay subcategorías disponibles.";
            }
        }

        private void AbrirModal()
        {
            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "AbrirModal",
                "abrirModal();",
                true
            );
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btnNuevaCategoria_Click(object sender, EventArgs e)
        {
            lblModalTitulo.Text = "Nueva Categoría";
            txtNombre.Text = "";

            hfModo.Value = "crear";
            hfTipo.Value = "categoria";
            hfId.Value = "";

            AbrirModal();
        }

        protected void btnEditarCategoria_Click(object sender, EventArgs e)
        {
            if (lstCategorias.SelectedItem == null)
                return;

            lblModalTitulo.Text = "Editar Categoría";
            txtNombre.Text = lstCategorias.SelectedItem.Text;

            hfModo.Value = "editar";
            hfTipo.Value = "categoria";
            hfId.Value = lstCategorias.SelectedValue;

            AbrirModal();
        }

        protected void btnEditarSubcategoria_Click(object sender, EventArgs e)
        {
            if (lstSubCategorias.SelectedItem == null)
                return;

            lblModalTitulo.Text = "Editar Subcategoría";
            txtNombre.Text = lstSubCategorias.SelectedItem.Text;

            hfModo.Value = "editar";
            hfTipo.Value = "subcategoria";
            hfId.Value = lstSubCategorias.SelectedValue;

            AbrirModal();
        }

        protected void btnNuevaSubcategoria_Click(object sender, EventArgs e)
        {
            lblModalTitulo.Text = "Nueva Subcategoria";
            txtNombre.Text = "";

            hfModo.Value = "crear";
            hfTipo.Value = "subcategoria";
            hfId.Value = "";

            AbrirModal();
        }

        protected void lstCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSubCategories();
        }
    }
}