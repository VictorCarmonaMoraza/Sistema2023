using Sistema2023.Datos;
using Sistema2023.Entidades;
using System.Data;

namespace Sistema2023.Negocio
{
    public class NCategoria
    {
        //Funciones para poder comunicarnos con la CAPA DATOS
        public static DataTable Listar()
        {
            DCategoria Datos = new DCategoria();
            return Datos.Listar();
        }

        public static DataTable Buscar(string valor)
        {
            DCategoria Datos = new DCategoria();
            return Datos.Buscar(valor);
        }

        public static string Insertar(string Nombre, string Descripcion)
        {
            DCategoria Datos = new DCategoria();

            //Comrpuebo si exite la categoria
            string Existe = Datos.Existe(Nombre);

            //Validamos el paremtro existe
            if (Existe.Equals("1"))
            {
                return "La categoria ya existe";
            }
            else
            {
                Categoria categoria = new Categoria()
                {
                    Nombre = Nombre,
                    Descripcion = Descripcion
                };
                return Datos.Insertar(categoria);
            }
        }

        public static string Actualizar(int Id, string Nombre, string Descripcion)
        {
            DCategoria Datos = new DCategoria();

            //Comrpuebo si exite la categoria
            string Existe = Datos.Existe(Nombre);

            //Validamos el paremtro existe
            if (Existe.Equals("1"))
            {
                return "La categoria ya existe";
            }
            else{
                Categoria categoria = new Categoria()
                {
                    IdCategoria = Id,
                    Nombre = Nombre,
                    Descripcion = Descripcion
                };
                return Datos.Actualizar(categoria);
            }
        }

        public static string Eliminar(int Id)
        {
            DCategoria Datos = new DCategoria();
            return Datos.Eliminar(Id);
        }

        public static string Activar(int Id)
        {
            DCategoria Datos = new DCategoria();
            return Datos.Activar(Id);
        }

        public static string Desactivar(int Id)
        {
            DCategoria Datos = new DCategoria();
            return Datos.Desactivar(Id);
        }
    }
}
