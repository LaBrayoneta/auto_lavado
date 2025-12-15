using System;
using System.Windows.Forms;

namespace Lavadero.Vistas
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.menuStrip = new MenuStrip();
            this.menuDuenos = new ToolStripMenuItem();
            this.menuAutos = new ToolStripMenuItem();
            this.menuTurnos = new ToolStripMenuItem();
            this.menuSalir = new ToolStripMenuItem();

            // FormPrincipal
            this.Text = "Sistema Lavadero";
            this.Size = new System.Drawing.Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.IsMdiContainer = true;

            // menuStrip
            this.menuStrip.Items.AddRange(new ToolStripItem[] {
                this.menuDuenos,
                this.menuAutos,
                this.menuTurnos,
                this.menuSalir
            });

            // menuDuenos
            this.menuDuenos.Text = "Dueños";
            this.menuDuenos.Click += new EventHandler(this.menuDuenos_Click);

            // menuAutos
            this.menuAutos.Text = "Autos";
            this.menuAutos.Click += new EventHandler(this.menuAutos_Click);

            // menuTurnos
            this.menuTurnos.Text = "Turnos";
            this.menuTurnos.Click += new EventHandler(this.menuTurnos_Click);

            // menuSalir
            this.menuSalir.Text = "Salir";
            this.menuSalir.Click += new EventHandler(this.menuSalir_Click);

            this.MainMenuStrip = this.menuStrip;
            this.Controls.Add(this.menuStrip);
        }

        private MenuStrip menuStrip;
        private ToolStripMenuItem menuDuenos;
        private ToolStripMenuItem menuAutos;
        private ToolStripMenuItem menuTurnos;
        private ToolStripMenuItem menuSalir;

        private void menuDuenos_Click(object sender, EventArgs e)
        {
            FormDuenos form = new FormDuenos();
            form.MdiParent = this;
            form.Show();
        }

        private void menuAutos_Click(object sender, EventArgs e)
        {
            FormAutos form = new FormAutos();
            form.MdiParent = this;
            form.Show();
        }

        private void menuTurnos_Click(object sender, EventArgs e)
        {
            FormTurnos form = new FormTurnos();
            form.MdiParent = this;
            form.Show();
        }

        private void menuSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
