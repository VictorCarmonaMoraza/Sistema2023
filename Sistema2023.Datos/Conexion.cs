using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema2023.Datos
{
    public class Conexion
    {
        //Nombre de BBDD a la cual nos conectaremos
        private readonly string Base;

        //Servidor en el cual esta alojado la BBDD, puede ser tambien una IP
        public readonly string Servidor;

        //Usuario para poder acceder a la BBDD
        public readonly string Usuario;

        //Clave del usuario para acceder a BBDD
        public readonly string Clave;

        //Para poder trabajar con Autentication de Windows o de SQL Server
        public readonly bool Seguridad;

        private static Conexion Con = null;

        //Constructor privado para que no pueda ser instanciada desde otra clase
        private Conexion()
        {
            this.Base = "dbsistema";
            this.Servidor = "MSI\\SQLEXPRESS";
            //this.Usuario = "sa";
            this.Usuario = ConfigurationManager.AppSettings["usuario"];
            //this.Clave = "1234";
            this.Clave = ConfigurationManager.AppSettings["password"];
            this.Seguridad = true;
        }

        /// <summary>
        /// Crea la cadena de conexion 
        /// </summary>
        /// <returns>Devuelve la cadena de conexion</returns>
        public SqlConnection CrearConexion()
        {
            SqlConnection Cadena = new SqlConnection();
            try
            {
                Cadena.ConnectionString = "Server=" + this.Servidor + "; Database=" + this.Base + ";";
                //Si la seguridad es true es autenticacion de windows
                if (this.Seguridad)
                {
                    Cadena.ConnectionString = Cadena.ConnectionString + "Integrated Security = SSPI";
                }
                else
                {
                    Cadena.ConnectionString = Cadena.ConnectionString + ";User Id =" + this.Usuario + ";Password =" + this.Clave;
                }
            }
            catch (Exception ex)
            {
                Cadena = null;
                throw ex;
            }
            return Cadena;
        }

        /// <summary>
        /// Devuelve instancia de conexion
        /// </summary>
        /// <returns>Devolvemos instancia de la conexion</returns>
        public static Conexion getInstancia()
        {
            //verificamos que tenemos la instancia
            if (Con == null)
            {
                Con = new Conexion();
            }
            //Devolvemos la instancia
            return Con;
        }

    }
}
