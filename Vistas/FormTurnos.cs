using Lavadero.Controladores;
using Lavadero.Modelos;
using System;
using System.Windows.Forms;

namespace Lavadero.Vistas
{
    public partial class FormTurnos : Form
    {
        private TurnoController controller = new TurnoController();
        private AutoController autoController = new AutoController();

        public FormTurnos()
        {
            InitializeComponent();
            CargarDatos();
            CargarAutos();
        }

        private void InitializeComponent()
        {
            this.dgvTurnos = new DataGridView();
            this.dtpFecha = new DateTimePicker();
            this.dtpHora = new DateTimePicker();
            this.cboAuto = new ComboBox();
            this.txtTipo = new TextBox();
            this.txtDescripcion = new TextBox();
            this.txtMonto = new TextBox();
            this.btnAgregar = new Button();
            this.btnEliminar = new Button();
            this.lblFecha = new Label();
            this.lblHora = new Label();
            this.lblAuto = new Label();
            this.lblTipo = new Label();
            this.lblDescripcion = new Label();
            this.lblMonto = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvTurnos)).BeginInit();

            // FormTurnos
            this.Text = "Gestión de Turnos";
            this.Size = new System.Drawing.Size(950, 550);

            // dgvTurnos
            this.dgvTurnos.Location = new System.Drawing.Point(20, 20);
            this.dgvTurnos.Size = new System.Drawing.Size(600, 450);
            this.dgvTurnos.ReadOnly = true;
            this.dgvTurnos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvTurnos.MultiSelect = false;
            this.dgvTurnos.AllowUserToAddRows = false;

            int x = 640, y = 30;

            // lblFecha
            this.lblFecha.Text = "Fecha:";
            this.lblFecha.Location = new System.Drawing.Point(x, y);
            this.lblFecha.Size = new System.Drawing.Size(100, 20);

            // dtpFecha
            this.dtpFecha.Location = new System.Drawing.Point(x, y + 20);
            this.dtpFecha.Size = new System.Drawing.Size(250, 25);
            this.dtpFecha.Format = DateTimePickerFormat.Short;

            y += 60;
            // lblHora
            this.lblHora.Text = "Hora:";
            this.lblHora.Location = new System.Drawing.Point(x, y);
            this.lblHora.Size = new System.Drawing.Size(100, 20);

            // dtpHora
            this.dtpHora.Location = new System.Drawing.Point(x, y + 20);
            this.dtpHora.Size = new System.Drawing.Size(250, 25);
            this.dtpHora.Format = DateTimePickerFormat.Time;
            this.dtpHora.ShowUpDown = true;

            y += 60;
            // lblAuto
            this.lblAuto.Text = "Auto (Patente):";
            this.lblAuto.Location = new System.Drawing.Point(x, y);
            this.lblAuto.Size = new System.Drawing.Size(100, 20);

            // cboAuto
            this.cboAuto.Location = new System.Drawing.Point(x, y + 20);
            this.cboAuto.Size = new System.Drawing.Size(250, 25);
            this.cboAuto.DropDownStyle = ComboBoxStyle.DropDownList;

            y += 60;
            // lblTipo
            this.lblTipo.Text = "Tipo de Lavado:";
            this.lblTipo.Location = new System.Drawing.Point(x, y);
            this.lblTipo.Size = new System.Drawing.Size(100, 20);

            // txtTipo
            this.txtTipo.Location = new System.Drawing.Point(x, y + 20);
            this.txtTipo.Size = new System.Drawing.Size(250, 25);

            y += 60;
            // lblDescripcion
            this.lblDescripcion.Text = "Descripción:";
            this.lblDescripcion.Location = new System.Drawing.Point(x, y);
            this.lblDescripcion.Size = new System.Drawing.Size(100, 20);

            // txtDescripcion
            this.txtDescripcion.Location = new System.Drawing.Point(x, y + 20);
            this.txtDescripcion.Size = new System.Drawing.Size(250, 60);
            this.txtDescripcion.Multiline = true;

            y += 90;
            // lblMonto
            this.lblMonto.Text = "Monto ($):";
            this.lblMonto.Location = new System.Drawing.Point(x, y);
            this.lblMonto.Size = new System.Drawing.Size(100, 20);

            // txtMonto
            this.txtMonto.Location = new System.Drawing.Point(x, y + 20);
            this.txtMonto.Size = new System.Drawing.Size(250, 25);

            y += 60;
            // btnAgregar
            this.btnAgregar.Text = "Agregar Turno";
            this.btnAgregar.Location = new System.Drawing.Point(x, y);
            this.btnAgregar.Size = new System.Drawing.Size(120, 35);
            this.btnAgregar.Click += new EventHandler(this.btnAgregar_Click);

            // btnEliminar
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Location = new System.Drawing.Point(x + 130, y);
            this.btnEliminar.Size = new System.Drawing.Size(120, 35);
            this.btnEliminar.Click += new EventHandler(this.btnEliminar_Click);

            // Agregar controles al formulario
            this.Controls.Add(this.dgvTurnos);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.lblHora);
            this.Controls.Add(this.dtpHora);
            this.Controls.Add(this.lblAuto);
            this.Controls.Add(this.cboAuto);
            this.Controls.Add(this.lblTipo);
            this.Controls.Add(this.txtTipo);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.lblMonto);
            this.Controls.Add(this.txtMonto);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnEliminar);

            ((System.ComponentModel.ISupportInitialize)(this.dgvTurnos)).EndInit();
        }

        private DataGridView dgvTurnos;
        private DateTimePicker dtpFecha;
        private DateTimePicker dtpHora;
        private ComboBox cboAuto;
        private TextBox txtTipo;
        private TextBox txtDescripcion;
        private TextBox txtMonto;
        private Button btnAgregar;
        private Button btnEliminar;
        private Label lblFecha;
        private Label lblHora;
        private Label lblAuto;
        private Label lblTipo;
        private Label lblDescripcion;
        private Label lblMonto;

        private void CargarDatos()
        {
            dgvTurnos.DataSource = null;
            dgvTurnos.DataSource = controller.ObtenerTodos();

            // Ajustar columnas
            if (dgvTurnos.Columns.Count > 0)
            {
                dgvTurnos.Columns["IdTurno"].HeaderText = "ID";
                dgvTurnos.Columns["IdTurno"].Width = 50;
                dgvTurnos.Columns["Fecha"].HeaderText = "Fecha";
                dgvTurnos.Columns["Fecha"].Width = 100;
                dgvTurnos.Columns["Hora"].HeaderText = "Hora";
                dgvTurnos.Columns["Hora"].Width = 80;
                dgvTurnos.Columns["InfoAuto"].HeaderText = "Auto";
                dgvTurnos.Columns["InfoAuto"].Width = 150;
                dgvTurnos.Columns["TipoLavado"].HeaderText = "Tipo";
                dgvTurnos.Columns["TipoLavado"].Width = 100;
                dgvTurnos.Columns["Monto"].HeaderText = "Monto";
                dgvTurnos.Columns["Monto"].Width = 80;
                dgvTurnos.Columns["Monto"].DefaultCellStyle.Format = "C2";

                // Ocultar columnas innecesarias
                dgvTurnos.Columns["Patente"].Visible = false;
                dgvTurnos.Columns["Descripcion"].Visible = false;
            }
        }

        private void CargarAutos()
        {
            var autos = autoController.ObtenerTodos();
            cboAuto.DataSource = autos;
            cboAuto.DisplayMember = "Patente";
            cboAuto.ValueMember = "Patente";
        }

        private void LimpiarCampos()
        {
            dtpFecha.Value = DateTime.Now;
            dtpHora.Value = DateTime.Now;
            if (cboAuto.Items.Count > 0) cboAuto.SelectedIndex = 0;
            txtTipo.Clear();
            txtDescripcion.Clear();
            txtMonto.Clear();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (cboAuto.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un auto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtTipo.Text))
            {
                MessageBox.Show("Ingrese el tipo de lavado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtMonto.Text))
            {
                MessageBox.Show("Ingrese el monto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal monto;
            if (!decimal.TryParse(txtMonto.Text, out monto))
            {
                MessageBox.Show("El monto debe ser un número válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Crear turno
            Turno turno = new Turno
            {
                Fecha = dtpFecha.Value,
                Hora = dtpHora.Value.TimeOfDay,
                Patente = cboAuto.SelectedValue.ToString(),
                TipoLavado = txtTipo.Text,
                Descripcion = txtDescripcion.Text,
                Monto = monto
            };

            // Guardar
            if (controller.Agregar(turno))
            {
                MessageBox.Show("Turno agregado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatos();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al agregar el turno", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvTurnos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un turno para eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("¿Está seguro de eliminar este turno?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dgvTurnos.CurrentRow.Cells["IdTurno"].Value);

                if (controller.Eliminar(id))
                {
                    MessageBox.Show("Turno eliminado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al eliminar el turno", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}