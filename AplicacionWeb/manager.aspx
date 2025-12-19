<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="manager.aspx.cs" Inherits="AplicacionWeb.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="container my-4">
        <h2 class="text-center mb-4 text-white">Administración de Categorías</h2>
        <div class="card shadow rounded-4 p-4">
            <div class="card-body">

                <div class="row g-4">

                    <!-- CATEGORÍAS -->
                    <div class="col-md-4">
                        <h5 class="mb-3">Categorías</h5>

                        <asp:ListBox ID="lstCategorias" runat="server" CssClass="form-select" Rows="12" AutoPostBack="true" OnSelectedIndexChanged="lstCategorias_SelectedIndexChanged"></asp:ListBox>

                        <div class="d-flex gap-2 mt-3">
                            <asp:Button ID="btnNuevaCategoria" runat="server" CssClass="btn btn-success btn-sm" Text="Nueva" OnClick="btnNuevaCategoria_Click" />
                            <asp:Button ID="btnEditarCategoria" runat="server" CssClass="btn btn-warning btn-sm" Text="Modificar" OnClick="btnEditarCategoria_Click" />
                            <asp:Button ID="btnEliminarCategoria" runat="server" CssClass="btn btn-danger btn-sm" Text="Eliminar" />
                        </div>
                    </div>

                    <!-- SUBCATEGORÍAS -->
                    <div class="col-md-8">
                        <h5 class="mb-3">Subcategorías</h5>

                        <asp:ListBox ID="lstSubCategorias" runat="server" CssClass="form-select" Rows="12" AutoPostBack="false"></asp:ListBox>

                        <div class="d-flex gap-2 mt-3 flex-wrap">
                            <asp:Button ID="btnNuevaSubcategoria" runat="server" CssClass="btn btn-success btn-sm" Text="Nueva" OnClick="btnNuevaSubcategoria_Click"/>
                            <asp:Button ID="btnEditarSubcategoria" runat="server" CssClass="btn btn-warning btn-sm" Text="Modificar" OnClick="btnEditarSubcategoria_Click" />
                            <asp:Button ID="btnEliminarSubcategoria" runat="server" CssClass="btn btn-danger btn-sm" Text="Eliminar" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- MODAL CATEGORÍA / SUBCATEGORÍA -->
        <div class="modal fade" id="modalItem" tabindex="-1" aria-hidden="true" clientidmode="Static">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content rounded-4">

                    <div class="modal-header">
                        <h5 class="modal-title">
                            <asp:Label ID="lblModalTitulo" runat="server" />
                        </h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                    </div>

                    <div class="modal-body">
                        <div class="mb-3">
                            <label class="form-label">Nombre</label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                        </div>
                        <asp:HiddenField ID="hfModo" runat="server" />
                        <asp:HiddenField ID="hfTipo" runat="server" />
                        <asp:HiddenField ID="hfId" runat="server" />
                    </div>

                    <div class="modal-footer">
                        <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary" Text="Guardar" OnClick="btnGuardar_Click" />
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        function abrirModal() {
            const modalEl = document.getElementById('modalItem');
            const modal = new bootstrap.Modal(modalEl);
            modal.show();
        }

        function cerrarModal() {
            const modalEl = document.getElementById('modalItem');
            const modal = bootstrap.Modal.getInstance(modalEl);
            modal.hide();
        }
    </script>
</asp:Content>
