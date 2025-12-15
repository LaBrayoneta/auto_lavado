using Lavadero.Controladores;
using Lavadero.Modelos;
using System;
using System.Windows.Forms;

namespace Lavadero.Vistas
{
    public partial class FormAutos : Form
    {
        private AutoController controller = new AutoController();
        private DuenoController duenoController = new DuenoController();

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
            this.cboDueno = new ComboBox();
            this.btnAgregar = new Button();
            this.btnEliminar = new Button();

            this.Text = "Gestión de Autos";
            this.Size = new System.Drawing.Size(900, 500);

            this.dgvAutos.Location = new System.Drawing.Point(20, 20);
            this.dgvAutos.Size = new System.Drawing.Size(550, 400);
            this.dgvAutos.ReadOnly = true;
            this.dgvAutos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            int x = 600, y = 30;
            Label lbl;

            lbl = new Label { Text = "Patente:", Location = new System.Drawing.Point(x, y) };
            this.Controls.Add(lbl);
            this.txtPatente.Location = new System.Drawing.Point(x, y + 20);
            this.txtPatente.Size = new System.Drawing.Size(200, 25);

            y += 60;
            lbl = new Label { Text = "Marca:", Location = new System.Drawing.Point(x, y) };
            this.Controls.Add(lbl);
            this.txtMarca.Location = new System.Drawing.Point(x, y + 20);
            this.txtMarca.Size = new System.Drawing.Size(200, 25);

            y += 60;
            lbl = new Label { Text = "Modelo:", Location = new System.Drawing.Point(x, y) };
            this.Controls.Add(lbl);
            this.txtModelo.Location = new System.Drawing.Point(x, y + 20);
            this.txtModelo.Size = new System.Drawing.Size(200, 25);

            y += 60;
            lbl = new Label { Text = "Año:", Location = new System.Drawing.Point(x, y) };
            this.Controls.Add(lbl);
            this.txtAnio.Location = new System.Drawing.Point(x, y + 20);
            this.txtAnio.Size = new System.Drawing.Size(200, 25);

            y += 60;
            lbl = new Label { Text = "Dueño:", Location = new System.Drawing.Point(x, y) };
            this.Controls.Add(lbl);
            this.cboDueno.Location = new System.Drawing.Point(x, y + 20);
            this.cboDueno.Size = new System.Drawing.Size(200, 25);
            this.cboDueno.DropDownStyle = ComboBoxStyle.DropDownList;

            y += 60;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.Location = new System.Drawing.Point(x, y);
            this.btnAgregar.Size = new System.Drawing.Size(90, 30);
            this.btnAgregar.Click += btnAgregar_Click;

            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Location = new System.Drawing.Point(x + 100, y);
            this.btnEliminar.Size = new System.Drawing.Size(90, 30);
            this.btnEliminar.Click += btnEliminar_Click;

            this.Controls.Add(this.dgvAutos);
            this.Controls.Add(this.txtPatente);
            this.Controls.Add(this.txtMarca);
            this.Controls.Add(this.txtModelo);
            this.Controls.Add(this.txtAnio);
            this.Controls.Add(this.cboDueno);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnEliminar);
        }

        private DataGridView dgvAutos;
        private TextBox txtPatente, txtMarca, txtModelo, txtAnio;
        private ComboBox cboDueno;
        private Button btnAgregar, btnEliminar;

        private void CargarDatos()
        {
            dgvAutos.DataSource = null;
            dgvAutos.DataSource = controller.ObtenerTodos();
        }

        private void CargarDuenos()
        {
            var duenos = duenoController.ObtenerTodos();
            cboDueno.DataSource = duenos;
            cboDueno.DisplayMember = "Apellido";
            cboDueno.ValueMember = "IdDueno";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Auto auto = new Auto
            {
                Patente = txtPatente.Text,
                Marca = txtMarca.Text,
                Modelo = txtModelo.Text,
                Anio = Convert.ToInt32(txtAnio.Text),
                IdDueno = Convert.ToInt32(cboDueno.SelectedValue)
            };

            if (controller.Agregar(auto))
            {
                MessageBox.Show("Auto agregado");
                CargarDatos();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvAutos.CurrentRow != null)
            {
                string patente = dgvAutos.CurrentRow.Cells["Patente"].Value.ToString();
                if (controller.Eliminar(patente))
                {
                    MessageBox.Show("Auto eliminado");
                    CargarDatos();
                }
            }
        }
    }
}