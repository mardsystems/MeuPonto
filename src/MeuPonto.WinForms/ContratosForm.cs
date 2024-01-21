using System;
using System.Windows.Forms;

namespace MeuPonto
{
    public partial class ContratosForm : Form
    {
        public ContratosForm()
        {
            InitializeComponent();
        }

        private void contratosBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.contratosBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.mainDataSet);

        }

        private void ContratosForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'mainDataSet.Contratos' table. You can move, or remove it, as needed.
            this.contratosTableAdapter.Fill(this.mainDataSet.Contratos);

        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
