using Sistema2023.Negocio;
using System;
using System.Windows.Forms;

namespace Sistema2023.Presentacion
{
    public partial class FrmCategoria : Form
    {

        private string NombreAnt;

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
                this.Limpiar();
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
            BtnActualizar.Visible = false;
            //Limpia todas las cjas de texto que se esten validando
            ErrorIcons.Clear();

            //Hacemos visible la columna cero
            DgvListado.Columns[0].Visible = false;
            BtnActivar.Visible = false;
            BtnDesactivar.Visible = false;
            BtnEliminar.Visible = false;
            ChkSeleccionar.Checked = false;
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

        /// <summary>
        /// Vuando hago click sobre un registro lo estoy seleccionando
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Limpiar();
                //Hago visible boton Actualizar
                BtnActualizar.Visible = true;
                //Oculto boton Insertar
                BtnInsertar.Visible = false;
                TxtId.Text = Convert.ToString(DgvListado.CurrentRow.Cells["ID"].Value);
                this.NombreAnt = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                TxtNombre.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                TxtDescripcion.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Descripcion"].Value);
                //Pasar a la pantalla de mantenimiento
                TabGeneral.SelectedIndex = 1;
            }
            catch (Exception)
            {
                MessageBox.Show("Seleccione desde la celda nombre.");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                if (TxtNombre.Text == string.Empty || TxtId.Text == string.Empty)
                {
                    MensajeError("Faltan algunos datos. Seran remarcados");
                    //Nos remarcara solo el nombre porque ID esta oculto
                    ErrorIcons.SetError(TxtNombre, "Ingrese un nombre");
                }
                else
                {
                    Rpta = NCategoria.Actualizar(Convert.ToInt32(TxtId.Text), this.NombreAnt, TxtNombre.Text.Trim(), TxtDescripcion.Text.Trim());
                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOK("Se actualizo de forma correcta el registro");
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

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            //Id el checkbox esta seleccionado 
            if (ChkSeleccionar.Checked)
            {
                //Hacemos visible la columna cero
                DgvListado.Columns[0].Visible = true;
                BtnActivar.Visible = true;
                BtnDesactivar.Visible = true;
                BtnEliminar.Visible = true;
            }
            else
            {
                //Hacemos visible la columna cero
                DgvListado.Columns[0].Visible = false;
                BtnActivar.Visible = false;
                BtnDesactivar.Visible = false;
                BtnEliminar.Visible = false;
            }
        }

        private void DgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DgvListado.Columns["Seleccionar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)DgvListado.Rows[e.RowIndex].Cells["Seleccionar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Realmente deseas eliminar el(los) registro(s)?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion ==  DialogResult.OK)
                {
                    int Codigo;
                    string Rpta = "";

                    foreach(DataGridViewRow row in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToInt32(row.Cells[1].Value);
                            Rpta = NCategoria.Eliminar(Codigo);

                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOK("Se elimino el registro: " + Convert.ToString(row.Cells[2].Value));
                            }
                            else
                            {
                                this.MensajeError(Rpta);
                            }
                        }
                    }
                    this.Listar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
                throw;
            }
        }
    }
    #endregion Eventos
}
