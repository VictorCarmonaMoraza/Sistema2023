using Sistema2023.Entidades;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Sistema2023.Datos
{

    public class DCategoria
    {
        //Propiedades
        private SqlDataReader Resultado = null;
        private DataTable Tabla = null;
        private string Rpta = "";


        /// <summary>
        /// Listado de categorias
        /// </summary>
        /// <returns>Listado de categorias</returns>
        public DataTable Listar()
        {
            //SqlDataReader Resultado = null;
            //DataTable Tabla  = new DataTable();
            Tabla = new DataTable();
            //Variable para establecer conexion con la BBDD
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("categoria_listar",SqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                //Abrimos conexion
                SqlCon.Open();
                //Guardamos en Resultado los datos obtenidos
                Resultado = cmd.ExecuteReader();
                //Rellenamos el DataTable
                Tabla.Load(Resultado); 
                return Tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                //Compruebo si la conexion esta abierta
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close(); 
            }
        }

        /// <summary>
        /// Listado de categorias
        /// </summary>
        /// <param name="valor">categoria a buscar</param>
        /// <returns>listado de categoria encontrado</returns>
        public DataTable Buscar(string valor)
        {
            //SqlDataReader Resultado = null;
            //DataTable Tabla = new DataTable();
            Tabla = new DataTable();
            //Variable para establecer conexion con la BBDD
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("categoria_buscar", SqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                //Agregamos un comando al procedimiento almacenado
                cmd.Parameters.Add("@valor", SqlDbType.VarChar).Value = valor;
                //Abrimos conexion
                SqlCon.Open();
                //Guardamos en Resultado los datos obtenidos
                Resultado = cmd.ExecuteReader();
                //Rellenamos el DataTable
                Tabla.Load(Resultado);
                return Tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //Compruebo si la conexion esta abierta
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
        }

        /// <summary>
        /// Insertar una categoria
        /// </summary>
        /// <param name="Obj"></param>
        /// <returns></returns>
        public string Insertar(Categoria categoria)
        {
            //string Rpta = "";
            //Variable para establecer conexion con la BBDD
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("categoria_insertar", SqlCon);
                //Ejecutamos procedimiento almacenado
                cmd.CommandType = CommandType.StoredProcedure;
                //Enviamos parametros al procedimiento 
                cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = categoria.Nombre;
                cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = categoria.Descripcion;
                //Abtimos la conexion
                SqlCon.Open();
                //Ejecutamos el comando
                Rpta = cmd.ExecuteNonQuery() == 1 ? "OK" : "No se pudo ingresar el registro";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                //Compruebo si la conexion esta abierta
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return Rpta;

        }

        /// <summary>
        /// Actualiza una categoria
        /// </summary>
        /// <param name="Obj"></param>
        /// <returns></returns>
        public string Actualizar(Categoria categoria)
        {
            //string Rpta = "";
            //Variable para establecer conexion con la BBDD
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("categoria_actualizar", SqlCon);
                //Ejecutamos procedimiento almacenado
                cmd.CommandType = CommandType.StoredProcedure;
                //Enviamos parametros al procedimiento 
                cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = categoria.Nombre;
                cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = categoria.Descripcion;
                cmd.Parameters.Add("@idcategoria", SqlDbType.Int).Value = categoria.IdCategoria;
                //Abtimos la conexion
                SqlCon.Open();
                //Ejecutamos el comando
                Rpta = cmd.ExecuteNonQuery() == 1 ? "OK" : "No se pudo actualizar el registro";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                //Compruebo si la conexion esta abierta
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return Rpta;
        }

        /// <summary>
        /// Elimina una categoria por su id
        /// </summary>
        /// <param name="IdCategoria">Id de la categoria a eliminar</param>
        /// <returns></returns>
        public string Eliminar(int IdCategoria)
        {
            //string Rpta = "";
            //Variable para establecer conexion con la BBDD
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("categoria_eliminar", SqlCon);
                //Ejecutamos procedimiento almacenado
                cmd.CommandType = CommandType.StoredProcedure;
                //Enviamos parametros al procedimiento 
                cmd.Parameters.Add("@idcategoria", SqlDbType.Int).Value = IdCategoria;
                //Abtimos la conexion
                SqlCon.Open();
                //Ejecutamos el comando
                Rpta = cmd.ExecuteNonQuery() == 1 ? "OK" : "No se pudo eliminar el registro";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                //Compruebo si la conexion esta abierta
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return Rpta;
        }

        /// <summary>
        /// Activa una categoria por su id
        /// </summary>
        /// <param name="IdCategoria">Id de la categoria a activar</param>
        /// <returns></returns>
        public string Activar(int IdCategoria)
        {
            //string Rpta = "";
            //Variable para establecer conexion con la BBDD
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("categoria_activar", SqlCon);
                //Ejecutamos procedimiento almacenado
                cmd.CommandType = CommandType.StoredProcedure;
                //Enviamos parametros al procedimiento 
                cmd.Parameters.Add("@idcategoria", SqlDbType.Int).Value = IdCategoria;
                //Abtimos la conexion
                SqlCon.Open();
                //Ejecutamos el comando
                Rpta = cmd.ExecuteNonQuery() == 1 ? "OK" : "No se pudo activar el registro";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                //Compruebo si la conexion esta abierta
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return Rpta;
        }

        /// <summary>
        /// Desactiva una categoria por su id
        /// </summary>
        /// <param name="Id">Id de la categoria a desactivar</param>
        /// <returns></returns>
        public string Desactivar(int IdCategoria)
        {
            //string Rpta = "";
            //Variable para establecer conexion con la BBDD
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("categoria_desactivar", SqlCon);
                //Ejecutamos procedimiento almacenado
                cmd.CommandType = CommandType.StoredProcedure;
                //Enviamos parametros al procedimiento 
                cmd.Parameters.Add("@idcategoria", SqlDbType.Int).Value = IdCategoria;
                //Abtimos la conexion
                SqlCon.Open();
                //Ejecutamos el comando
                Rpta = cmd.ExecuteNonQuery() == 1 ? "OK" : "No se pudo deactivar el registro";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                //Compruebo si la conexion esta abierta
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return Rpta;
        }
    }
}
