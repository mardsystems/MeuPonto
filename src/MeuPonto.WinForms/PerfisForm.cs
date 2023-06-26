using System;
using System.Windows.Forms;

namespace MeuPonto
{
    public partial class PerfisForm : Form
    {
        public PerfisForm()
        {
            InitializeComponent();
        }

        private void perfisBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.perfisBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.mainDataSet);

        }

        private void PerfisForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'mainDataSet.Perfis' table. You can move, or remove it, as needed.
            this.perfisTableAdapter.Fill(this.mainDataSet.Perfis);

        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
