using Lavadero.Controladores;
using Lavadero.Modelos;
using System;
using System.Windows.Forms;

namespace Lavadero.Vistas
{
    public partial class FormDuenos : Form
    {
        private DuenoController controller = new DuenoController();

        public FormDuenos()
        {
            InitializeComponent();
            CargarDatos();
            CargarLocalidades();
        }

        private void InitializeComponent()
        {
            this.dgvDuenos = new DataGridView();
            this.txtDni = new TextBox();
            this.txtApellido = new TextBox();
            this.txtNombres = new TextBox();
            this.txtDomicilio = new TextBox();
            this.txtTelefono = new TextBox();
            this.cboLocalidad = new ComboBox();
            this.btnAgregar = new Button();
            this.btnModificar = new Button();
            this.btnEliminar = new Button();
            this.lblDni = new Label();
            this.lblApellido = new Label();
            this.lblNombres = new Label();
            this.lblDomicilio = new Label();
            this.lblLocalidad = new Label();
            this.lblTelefono = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvDuenos)).BeginInit();

            // FormDuenos
            this.Text = "Gestión de Dueños";
            this.Size = new System.Drawing.Size(900, 600);

            // dgvDuenos
            this.dgvDuenos.Location = new System.Drawing.Point(20, 20);
            this.dgvDuenos.Size = new System.Drawing.Size(550, 500);
            this.dgvDuenos.ReadOnly = true;
            this.dgvDuenos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvDuenos.MultiSelect = false;
            this.dgvDuenos.AllowUserToAddRows = false;
            this.dgvDuenos.SelectionChanged += new EventHandler(this.dgvDuenos_SelectionChanged);

            // Labels y TextBoxes
            int x = 600, y = 30;

            this.lblDni.Text = "DNI:";
            this.lblDni.Location = new System.Drawing.Point(x, y);
            this.lblDni.Size = new System.Drawing.Size(100, 20);

            this.txtDni.Location = new System.Drawing.Point(x, y + 20);
            this.txtDni.Size = new System.Drawing.Size(240, 25);

            y += 60;
            this.lblApellido.Text = "Apellido:";
            this.lblApellido.Location = new System.Drawing.Point(x, y);
            this.txtApellido.Location = new System.Drawing.Point(x, y + 20);
            this.txtApellido.Size = new System.Drawing.Size(240, 25);

            y += 60;
            this.lblNombres.Text = "Nombres:";
            this.lblNombres.Location = new System.Drawing.Point(x, y);
            this.txtNombres.Location = new System.Drawing.Point(x, y + 20);
            this.txtNombres.Size = new System.Drawing.Size(240, 25);

            y += 60;
            this.lblDomicilio.Text = "Domicilio:";
            this.lblDomicilio.Location = new System.Drawing.Point(x, y);
            this.txtDomicilio.Location = new System.Drawing.Point(x, y + 20);
            this.txtDomicilio.Size = new System.Drawing.Size(240, 25);

            y += 60;
            this.lblLocalidad.Text = "Localidad:";
            this.lblLocalidad.Location = new System.Drawing.Point(x, y);
            this.cboLocalidad.Location = new System.Drawing.Point(x, y + 20);
            this.cboLocalidad.Size = new System.Drawing.Size(240, 25);
            this.cboLocalidad.DropDownStyle = ComboBoxStyle.DropDownList;

            y += 60;
            this.lblTelefono.Text = "Teléfono:";
            this.lblTelefono.Location = new System.Drawing.Point(x, y);
            this.txtTelefono.Location = new System.Drawing.Point(x, y + 20);
            this.txtTelefono.Size = new System.Drawing.Size(240, 25);

            // Botones
            y += 70;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.Location = new System.Drawing.Point(x, y);
            this.btnAgregar.Size = new System.Drawing.Size(75, 30);
            this.btnAgregar.Click += new EventHandler(this.btnAgregar_Click);

            this.btnModificar.Text = "Modificar";
            this.btnModificar.Location = new System.Drawing.Point(x + 85, y);
            this.btnModificar.Size = new System.Drawing.Size(75, 30);
            this.btnModificar.Click += new EventHandler(this.btnModificar_Click);

            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Location = new System.Drawing.Point(x + 170, y);
            this.btnEliminar.Size = new System.Drawing.Size(75, 30);
            this.btnEliminar.Click += new EventHandler(this.btnEliminar_Click);

            // Agregar controles
            this.Controls.Add(this.dgvDuenos);
            this.Controls.Add(this.lblDni);
            this.Controls.Add(this.txtDni);
            this.Controls.Add(this.lblApellido);
            this.Controls.Add(this.txtApellido);
            this.Controls.Add(this.lblNombres);
            this.Controls.Add(this.txtNombres);
            this.Controls.Add(this.lblDomicilio);
            this.Controls.Add(this.txtDomicilio);
            this.Controls.Add(this.lblLocalidad);
            this.Controls.Add(this.cboLocalidad);
            this.Controls.Add(this.lblTelefono);
            this.Controls.Add(this.txtTelefono);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnEliminar);

            ((System.ComponentModel.ISupportInitialize)(this.dgvDuenos)).EndInit();
        }

        private DataGridView dgvDuenos;
        private TextBox txtDni, txtApellido, txtNombres, txtDomicilio, txtTelefono;
        private ComboBox cboLocalidad;
        private Button btnAgregar, btnModificar, btnEliminar;
        private Label lblDni, lblApellido, lblNombres, lblDomicilio, lblLocalidad, lblTelefono;

        private void CargarDatos()
        {
            dgvDuenos.DataSource = null;
            dgvDuenos.DataSource = controller.ObtenerTodos();
        }

        private void CargarLocalidades()
        {
            cboLocalidad.DataSource = controller.ObtenerLocalidades();
            cboLocalidad.DisplayMember = "nombre";
            cboLocalidad.ValueMember = "codigoPostal";
        }

        private void LimpiarCampos()
        {
            txtDni.Clear();
            txtApellido.Clear();
            txtNombres.Clear();
            txtDomicilio.Clear();
            txtTelefono.Clear();
            if (cboLocalidad.Items.Count > 0) cboLocalidad.SelectedIndex = 0;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDni.Text) || string.IsNullOrEmpty(txtApellido.Text))
            {
                MessageBox.Show("Complete los campos obligatorios", "Error");
                return;
            }

            Dueno dueno = new Dueno
            {
                Dni = txtDni.Text,
                Apellido = txtApellido.Text,
                Nombres = txtNombres.Text,
                Domicilio = txtDomicilio.Text,
                CodigoPostal = cboLocalidad.SelectedValue.ToString(),
                Telefono = txtTelefono.Text
            };

            if (controller.Agregar(dueno))
            {
                MessageBox.Show("Dueño agregado correctamente");
                CargarDatos();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al agregar");
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvDuenos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un dueño");
                return;
            }

            Dueno dueno = new Dueno
            {
                IdDueno = Convert.ToInt32(dgvDuenos.CurrentRow.Cells["IdDueno"].Value),
                Dni = txtDni.Text,
                Apellido = txtApellido.Text,
                Nombres = txtNombres.Text,
                Domicilio = txtDomicilio.Text,
                CodigoPostal = cboLocalidad.SelectedValue.ToString(),
                Telefono = txtTelefono.Text
            };

            if (controller.Modificar(dueno))
            {
                MessageBox.Show("Dueño modificado correctamente");
                CargarDatos();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al modificar");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvDuenos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un dueño");
                return;
            }

            var result = MessageBox.Show("¿Está seguro de eliminar?", "Confirmar", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dgvDuenos.CurrentRow.Cells["IdDueno"].Value);
                if (controller.Eliminar(id))
                {
                    MessageBox.Show("Dueño eliminado correctamente");
                    CargarDatos();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al eliminar");
                }
            }
        }

        private void dgvDuenos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDuenos.CurrentRow != null)
            {
                txtDni.Text = dgvDuenos.CurrentRow.Cells["Dni"].Value.ToString();
                txtApellido.Text = dgvDuenos.CurrentRow.Cells["Apellido"].Value.ToString();
                txtNombres.Text = dgvDuenos.CurrentRow.Cells["Nombres"].Value.ToString();
                txtDomicilio.Text = dgvDuenos.CurrentRow.Cells["Domicilio"].Value.ToString();
                txtTelefono.Text = dgvDuenos.CurrentRow.Cells["Telefono"].Value.ToString();
                cboLocalidad.SelectedValue = dgvDuenos.CurrentRow.Cells["CodigoPostal"].Value.ToString();
            }
        }
    }
}