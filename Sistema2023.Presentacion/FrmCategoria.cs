using Sistema2023.Negocio;
using System;
using System.Windows.Forms;

namespace Sistema2023.Presentacion
{
    public partial class FrmCategoria : Form
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FrmCategoria()
        {
            InitializeComponent();
        }
 
        #region metodos no asociados a eventos

        /// <summary>
        /// Metodo para cargar listado desde BBDD
        /// </summary>
        private void Listar()
        {
            try
            {
                DgvListado.DataSource = NCategoria.Listar();
                this.Formato();
                LblTotal.Text = "Total registros: " + Convert.ToString(DgvListado.RowCount);

            }
            catch (Exception ex)
            {
                //Muestra excepcion y la pila de llamada antes de la excepcion
                //StackTrace muestra informacion de lo ocurrido
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// Formato de la tabla que muestra el listado
        /// </summary>
        private void Formato()
        {
            //La primera columna del datagridview
            DgvListado.Columns[0].Visible = false;
            //Columna Id no sera visible
            DgvListado.Columns[1].Visible = false;
            //le indicamos ancho a las siguientes columnas
            DgvListado.Columns[2].Width = 420; //Columna Nombre
            DgvListado.Columns[3].Width = 450; //Columna Descripcion
            //Modificar texto de la cabecera
            DgvListado.Columns[3].HeaderText = "Descripcion";
            DgvListado.Columns[4].Width = 450; //Columna Estado
        }

        /// <summary>
        /// Metodo para dejar en blanco las cajas de texto
        /// </summary>
        private void Limpiar()
        {
            TxtBuscar.Clear();
            TxtNombre.Clear();
            TxtId.Clear();
            TxtDescripcion.Clear();
            BtnInsertar.Visible = true;
            //Limpia todas las cjas de texto que se esten validando
            ErrorIcons.Clear();
        }

        /// <summary>
        /// Metodo para buscar dentro de una lista
        /// </summary>
        private void Buscar()
        {
            try
            {
                //TxtBuscar.Text contenido de la caja de Texto
                DgvListado.DataSource = NCategoria.Buscar(TxtBuscar.Text);
                this.Formato();
                LblTotal.Text = "Total registros: " + Convert.ToString(DgvListado.RowCount);

            }
            catch (Exception ex)
            {
                //Muestra excepcion y la pila de llamada antes de la excepcion
                //StackTrace muestra informacion de lo ocurrido
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// Muestra un mensaje de error
        /// </summary>
        /// <param name="Mensaje">Mensaje</param>
        private void MensajeError(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Sistema de ventas 2023", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Muestra un mensaje de todo ok
        /// </summary>
        /// <param name="Mensaje">Mensaje</param>
        private void MensajeOK(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Sistema de ventas 2023", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion metodos no asociados a eventos


        #region Eventos

        /// <summary>
        /// Carga la tabla por defecto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            //Le decimos que cuando cargue el formulario haga referencia al metodo listar
            this.Listar();
        }

        /// <summary>
        /// Evento boton Buscar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        /// <summary>
        /// Evento Boton Insertar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                if (TxtNombre.Text == string.Empty)
                {
                    MensajeError("Faltan algunos datos. Seran remarcados");
                    ErrorIcons.SetError(TxtNombre, "Ingrese un nombre");
                }
                else
                {
                    Rpta = NCategoria.Insertar(TxtNombre.Text.Trim(), TxtDescripcion.Text.Trim());
                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOK("Se inserto de forma correcta el registro");
                        //Limpiamos las cajas de textBox
                        this.Limpiar();
                        //Volvemos a listar de nuevo
                        this.Listar();
                        //Volvemos a la pestaña de listado
                        TabGeneral.SelectedIndex = 0;
                    }
                    else
                    {
                        this.MensajeError(Rpta);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// Evento Boton Cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            //Llamada al metod limpiar
            this.Limpiar();
            //Volvemos a la pestaña de listado
            TabGeneral.SelectedIndex = 0;
        }
    }
    #endregion Eventos
}
