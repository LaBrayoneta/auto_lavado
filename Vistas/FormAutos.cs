using Lavadero.Controladores;
using Lavadero.Modelos;
using System;
using System.Windows.Forms;
using System.Linq;

namespace Lavadero.Vistas
{
    public partial class FormAutos : Form
    {
        private AutoController controller = new AutoController();
        private DuenoController duenoController = new DuenoController();
        private bool modoEdicion = false;
        private string patenteOriginal = "";

        public FormAutos()
        {
            InitializeComponent();
            CargarDatos();
            CargarDuenos();
        }

        private void InitializeComponent()
        {
            this.dgvAutos = new DataGridView();
            this.txtPatente = new TextBox();
            this.txtMarca = new TextBox();
            this.txtModelo = new TextBox();
            this.txtAnio = new TextBox();
            this.txtBuscar = new TextBox();
            this.cboDueno = new ComboBox();
            this.btnAgregar = new Button();
            this.btnModificar = new Button();
            this.btnEliminar = new Button();
            this.btnLimpiar = new Button();
            this.lblBuscar = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvAutos)).BeginInit();
            this.SuspendLayout();

            // FormAutos
            this.Text = "Gestión de Autos";
            this.Size = new System.Drawing.Size(950, 550);
            this.StartPosition = FormStartPosition.CenterScreen;

            // lblBuscar
            this.lblBuscar.Text = "Buscar:";
            this.lblBuscar.Location = new System.Drawing.Point(20, 20);
            this.lblBuscar.Size = new System.Drawing.Size(60, 20);

            // txtBuscar
            this.txtBuscar.Location = new System.Drawing.Point(80, 18);
            this.txtBuscar.Size = new System.Drawing.Size(300, 25);
            this.txtBuscar.PlaceholderText = "Buscar por patente, marca, modelo o dueño...";
            this.txtBuscar.TextChanged += new EventHandler(this.txtBuscar_TextChanged);

            // dgvAutos
            this.dgvAutos.Location = new System.Drawing.Point(20, 55);
            this.dgvAutos.Size = new System.Drawing.Size(580, 420);
            this.dgvAutos.ReadOnly = true;
            this.dgvAutos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvAutos.MultiSelect = false;
            this.dgvAutos.AllowUserToAddRows = false;
            this.dgvAutos.SelectionChanged += new EventHandler(this.dgvAutos_SelectionChanged);

            int x = 630, y = 30;
            Label lbl;

            // lblPatente
            lbl = new Label { Text = "Patente: *", Location = new System.Drawing.Point(x, y), Size = new System.Drawing.Size(100, 20) };
            this.Controls.Add(lbl);
            this.txtPatente.Location = new System.Drawing.Point(x, y + 20);
            this.txtPatente.Size = new System.Drawing.Size(250, 25);
            this.txtPatente.CharacterCasing = CharacterCasing.Upper;
            this.txtPatente.MaxLength = 7;

            y += 60;
            // lblMarca
            lbl = new Label { Text = "Marca: *", Location = new System.Drawing.Point(x, y), Size = new System.Drawing.Size(100, 20) };
            this.Controls.Add(lbl);
            this.txtMarca.Location = new System.Drawing.Point(x, y + 20);
            this.txtMarca.Size = new System.Drawing.Size(250, 25);

            y += 60;
            // lblModelo
            lbl = new Label { Text = "Modelo: *", Location = new System.Drawing.Point(x, y), Size = new System.Drawing.Size(100, 20) };
            this.Controls.Add(lbl);
            this.txtModelo.Location = new System.Drawing.Point(x, y + 20);
            this.txtModelo.Size = new System.Drawing.Size(250, 25);

            y += 60;
            // lblAnio
            lbl = new Label { Text = "Año: *", Location = new System.Drawing.Point(x, y), Size = new System.Drawing.Size(100, 20) };
            this.Controls.Add(lbl);
            this.txtAnio.Location = new System.Drawing.Point(x, y + 20);
            this.txtAnio.Size = new System.Drawing.Size(250, 25);
            this.txtAnio.KeyPress += new KeyPressEventHandler(this.txtAnio_KeyPress);

            y += 60;
            // lblDueno
            lbl = new Label { Text = "Dueño: *", Location = new System.Drawing.Point(x, y), Size = new System.Drawing.Size(100, 20) };
            this.Controls.Add(lbl);
            this.cboDueno.Location = new System.Drawing.Point(x, y + 20);
            this.cboDueno.Size = new System.Drawing.Size(250, 25);
            this.cboDueno.DropDownStyle = ComboBoxStyle.DropDownList;

            y += 70;
            // btnAgregar
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.Location = new System.Drawing.Point(x, y);
            this.btnAgregar.Size = new System.Drawing.Size(90, 35);
            this.btnAgregar.Click += new EventHandler(this.btnAgregar_Click);

            // btnModificar
            this.btnModificar.Text = "Modificar";
            this.btnModificar.Location = new System.Drawing.Point(x + 100, y);
            this.btnModificar.Size = new System.Drawing.Size(90, 35);
            this.btnModificar.Click += new EventHandler(this.btnModificar_Click);

            y += 45;
            // btnEliminar
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Location = new System.Drawing.Point(x, y);
            this.btnEliminar.Size = new System.Drawing.Size(90, 35);
            this.btnEliminar.Click += new EventHandler(this.btnEliminar_Click);

            // btnLimpiar
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.Location = new System.Drawing.Point(x + 100, y);
            this.btnLimpiar.Size = new System.Drawing.Size(90, 35);
            this.btnLimpiar.Click += new EventHandler(this.btnLimpiar_Click);

            // Agregar controles
            this.Controls.Add(this.lblBuscar);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.dgvAutos);
            this.Controls.Add(this.txtPatente);
            this.Controls.Add(this.txtMarca);
            this.Controls.Add(this.txtModelo);
            this.Controls.Add(this.txtAnio);
            this.Controls.Add(this.cboDueno);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnLimpiar);

            ((System.ComponentModel.ISupportInitialize)(this.dgvAutos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private DataGridView? dgvAutos;
        private TextBox? txtPatente, txtMarca, txtModelo, txtAnio, txtBuscar;
        private ComboBox? cboDueno;
        private Button? btnAgregar, btnModificar, btnEliminar, btnLimpiar;
        private Label? lblBuscar;

        private void CargarDatos()
        {
            try
            {
                dgvAutos.DataSource = null;
                dgvAutos.DataSource = controller.ObtenerTodos();

                // Configurar columnas
                if (dgvAutos.Columns.Count > 0)
                {
                    dgvAutos.Columns["Patente"].Width = 80;
                    dgvAutos.Columns["Marca"].Width = 100;
                    dgvAutos.Columns["Modelo"].Width = 120;
                    dgvAutos.Columns["Anio"].Width = 60;
                    dgvAutos.Columns["Anio"].HeaderText = "Año";
                    dgvAutos.Columns["NombreDueno"].Width = 180;
                    dgvAutos.Columns["NombreDueno"].HeaderText = "Dueño";
                    dgvAutos.Columns["IdDueno"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDuenos()
        {
            try
            {
                var duenos = duenoController.ObtenerTodos();
                cboDueno.DataSource = duenos;
                cboDueno.DisplayMember = "Apellido";
                cboDueno.ValueMember = "IdDueno";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar dueños: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object? sender, EventArgs e)
        {
            try
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(txtPatente.Text))
                {
                    MessageBox.Show("Ingrese la patente", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPatente.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtMarca.Text))
                {
                    MessageBox.Show("Ingrese la marca", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMarca.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtModelo.Text))
                {
                    MessageBox.Show("Ingrese el modelo", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtModelo.Focus();
                    return;
                }

                if (!int.TryParse(txtAnio.Text, out int anio))
                {
                    MessageBox.Show("Ingrese un año válido", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAnio.Focus();
                    return;
                }

                if (cboDueno.SelectedValue == null)
                {
                    MessageBox.Show("Seleccione un dueño", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Auto auto = new Auto
                {
                    Patente = txtPatente.Text.Trim().ToUpper(),
                    Marca = txtMarca.Text.Trim(),
                    Modelo = txtModelo.Text.Trim(),
                    Anio = anio,
                    IdDueno = Convert.ToInt32(cboDueno.SelectedValue),
                    NombreDueno = cboDueno.Text // Se agrega el valor requerido
                };

                controller.Agregar(auto);
                MessageBox.Show("Auto agregado correctamente", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvAutos.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un auto para modificar", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validaciones
                if (string.IsNullOrWhiteSpace(txtMarca.Text))
                {
                    MessageBox.Show("Ingrese la marca", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMarca.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtModelo.Text))
                {
                    MessageBox.Show("Ingrese el modelo", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtModelo.Focus();
                    return;
                }

                if (!int.TryParse(txtAnio.Text, out int anio))
                {
                    MessageBox.Show("Ingrese un año válido", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAnio.Focus();
                    return;
                }

                var result = MessageBox.Show("¿Está seguro de modificar este auto?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Auto auto = new Auto
                    {
                        Patente = txtPatente.Text.Trim().ToUpper(),
                        Marca = txtMarca.Text.Trim(),
                        Modelo = txtModelo.Text.Trim(),
                        Anio = anio,
                        IdDueno = Convert.ToInt32(cboDueno.SelectedValue),
                        NombreDueno = cboDueno.Text // Se agrega el valor requerido
                    };

                    controller.Modificar(auto);
                    MessageBox.Show("Auto modificado correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos();
                    LimpiarCampos();
                    modoEdicion = false;
                    txtPatente.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvAutos.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un auto para eliminar", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string patente = dgvAutos.CurrentRow.Cells["Patente"].Value.ToString();
                var result = MessageBox.Show(
                    $"¿Está seguro de eliminar el auto con patente {patente}?",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    controller.Eliminar(patente);
                    MessageBox.Show("Auto eliminado correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos();
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            modoEdicion = false;
            txtPatente.Enabled = true;
        }

        private void LimpiarCampos()
        {
            txtPatente.Clear();
            txtMarca.Clear();
            txtModelo.Clear();
            txtAnio.Clear();
            txtBuscar.Clear();
            if (cboDueno.Items.Count > 0)
                cboDueno.SelectedIndex = 0;
            txtPatente.Focus();
        }

        private void dgvAutos_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvAutos.CurrentRow != null)
            {
                txtPatente.Text = dgvAutos.CurrentRow.Cells["Patente"].Value.ToString();
                txtMarca.Text = dgvAutos.CurrentRow.Cells["Marca"].Value.ToString();
                txtModelo.Text = dgvAutos.CurrentRow.Cells["Modelo"].Value.ToString();
                txtAnio.Text = dgvAutos.CurrentRow.Cells["Anio"].Value.ToString();
                cboDueno.SelectedValue = dgvAutos.CurrentRow.Cells["IdDueno"].Value;

                modoEdicion = true;
                txtPatente.Enabled = false; // No permitir cambiar la patente (es PK)
            }
        }

        private void txtBuscar_TextChanged(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    CargarDatos();
                }
                else
                {
                    dgvAutos.DataSource = null;
                    dgvAutos.DataSource = controller.Buscar(txtBuscar.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAnio_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // Solo permitir números y teclas de control (backspace, delete, etc.)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}