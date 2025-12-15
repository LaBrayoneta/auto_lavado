using Lavadero.Controladores;
using System;
using System.Windows.Forms;

namespace Lavadero.Vistas
{
    public partial class FormLogin : Form
    {
        private UsuarioController controller = new UsuarioController();

        public FormLogin()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.txtUsuario = new TextBox();
            this.txtContrasena = new TextBox();
            this.btnIngresar = new Button();
            this.lblUsuario = new Label();
            this.lblContrasena = new Label();

            // FormLogin
            this.Text = "Login - Lavadero";
            this.Size = new System.Drawing.Size(350, 250);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // lblUsuario
            this.lblUsuario.Text = "Usuario:";
            this.lblUsuario.Location = new System.Drawing.Point(50, 40);
            this.lblUsuario.Size = new System.Drawing.Size(100, 20);

            // txtUsuario
            this.txtUsuario.Location = new System.Drawing.Point(50, 65);
            this.txtUsuario.Size = new System.Drawing.Size(240, 25);

            // lblContrasena
            this.lblContrasena.Text = "Contraseña:";
            this.lblContrasena.Location = new System.Drawing.Point(50, 100);
            this.lblContrasena.Size = new System.Drawing.Size(100, 20);

            // txtContrasena
            this.txtContrasena.Location = new System.Drawing.Point(50, 125);
            this.txtContrasena.Size = new System.Drawing.Size(240, 25);
            this.txtContrasena.PasswordChar = '*';

            // btnIngresar
            this.btnIngresar.Text = "Ingresar";
            this.btnIngresar.Location = new System.Drawing.Point(110, 165);
            this.btnIngresar.Size = new System.Drawing.Size(120, 35);
            this.btnIngresar.Click += new EventHandler(this.btnIngresar_Click);

            // Agregar controles
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.lblContrasena);
            this.Controls.Add(this.txtContrasena);
            this.Controls.Add(this.btnIngresar);
        }

        private TextBox txtUsuario;
        private TextBox txtContrasena;
        private Button btnIngresar;
        private Label lblUsuario;
        private Label lblContrasena;

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtContrasena.Text))
            {
                MessageBox.Show("Complete todos los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (controller.ValidarLogin(txtUsuario.Text, txtContrasena.Text))
            {
                MessageBox.Show("Bienvenido!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormPrincipal formPrincipal = new FormPrincipal();
                this.Hide();
                formPrincipal.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}