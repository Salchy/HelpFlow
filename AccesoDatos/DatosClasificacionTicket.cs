using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace AccesoDatos
{
    public class DatosClasificacionTicket
    {
        Database database;
        public DatosClasificacionTicket()
        {
            database = new Database();
        }

        public List<Categoria> listarCategorias()
        {
            List<Categoria> Categorias = new List<Categoria>();
            try
            {
                database.SetQuery("SELECT Id, Nombre FROM Categorias");
                database.ExecQuery();

                while (database.reader.Read())
                {
                    Categoria categoria = new Categoria
                    {
                        Id = Convert.ToInt32(database.reader["Id"]),
                        Nombre = database.reader["Nombre"].ToString()
                    };
                    Categorias.Add(categoria);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar categorías: " + ex.Message);
            }
            finally
            {
                database.CloseConnection();
            }
            return Categorias;
        }

        public List<SubCategoria> listarSubCategorias(int idCategoria = 0)
        {
            List<SubCategoria> SubCategorias = new List<SubCategoria>();

            try
            {
                if (idCategoria != 0)
                {
                    database.SetQuery("SELECT Id, Nombre, IdCategoriaPadre FROM SubCategorias WHERE IdCategoriaPadre = @IdCategoria");
                    database.SetParameter("@IdCategoria", idCategoria);
                }
                else
                {
                    database.SetQuery("SELECT Id, Nombre, IdCategoriaPadre FROM SubCategorias");
                }

                database.ExecQuery();

                while (database.reader.Read())
                {
                    SubCategoria SubCategoria = new SubCategoria
                    {
                        Id = Convert.ToInt32(database.reader["Id"]),
                        Nombre = database.reader["Nombre"].ToString(),
                        IdCategoria = Convert.ToInt32(database.reader["IdCategoriaPadre"])
                    };
                    SubCategorias.Add(SubCategoria);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar subCategorías: " + ex.Message);
            }
            finally
            {
                database.CloseConnection();
            }
            return SubCategorias;
        }
    }
}
