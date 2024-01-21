using System;
using System.Windows.Forms;

namespace MeuPonto
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void pontosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var pontosForm = new PontosForm();

            pontosForm.Show();
        }

        private void contratosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var contratosForm = new ContratosForm();

            contratosForm.Show();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
