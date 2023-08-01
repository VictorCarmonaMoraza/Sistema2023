using Sistema2023.Negocio;
using System;
using System.Windows.Forms;

namespace Sistema2023.Presentacion
{
    public partial class FrmCategoria : Form
    {
        public FrmCategoria()
        {
            InitializeComponent();
        }

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

        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            //Le decimos que cuando cargue el formulario haga referencia al metodo listar
            this.Listar();
        }
    }
}
