using Lavadero.Controladores;

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
            txtUsuario = new TextBox();
            txtContrasena = new TextBox();
            btnIngresar = new Button();
            lblUsuario = new Label();
            lblContrasena = new Label();
            SuspendLayout();
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(50, 65);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(240, 27);
            txtUsuario.TabIndex = 1;
            // 
            // txtContrasena
            // 
            txtContrasena.Location = new Point(50, 125);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.PasswordChar = '*';
            txtContrasena.Size = new Size(240, 27);
            txtContrasena.TabIndex = 3;
            txtContrasena.TextChanged += txtContrasena_TextChanged;
            // 
            // btnIngresar
            // 
            btnIngresar.Location = new Point(110, 165);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(120, 35);
            btnIngresar.TabIndex = 4;
            btnIngresar.Text = "Ingresar";
            btnIngresar.Click += btnIngresar_Click;
            // 
            // lblUsuario
            // 
            lblUsuario.Location = new Point(50, 40);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(100, 20);
            lblUsuario.TabIndex = 0;
            lblUsuario.Text = "Usuario:";
            // 
            // lblContrasena
            // 
            lblContrasena.Location = new Point(50, 100);
            lblContrasena.Name = "lblContrasena";
            lblContrasena.Size = new Size(100, 20);
            lblContrasena.TabIndex = 2;
            lblContrasena.Text = "Contraseña:";
            // 
            // FormLogin
            // 
            ClientSize = new Size(324, 221);
            Controls.Add(lblUsuario);
            Controls.Add(txtUsuario);
            Controls.Add(lblContrasena);
            Controls.Add(txtContrasena);
            Controls.Add(btnIngresar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login - Lavadero";
            Load += FormLogin_Load;
            ResumeLayout(false);
            PerformLayout();
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

        private void txtContrasena_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}