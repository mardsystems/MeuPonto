using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using static MeuPonto.MainDataSet;

namespace MeuPonto
{
    public partial class PontosForm : Form
    {
        public PontosForm()
        {
            InitializeComponent();
        }

        private void pontosBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.pontosBindingSource.EndEdit();
            this.pontoComprovantesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.mainDataSet);

        }

        private void PontosForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'mainDataSet.PontoComprovanteImagemTipos' table. You can move, or remove it, as needed.
            this.pontoComprovanteImagemTiposTableAdapter.Fill(this.mainDataSet.PontoComprovanteImagemTipos);
            // TODO: This line of code loads data into the 'mainDataSet.Perfis' table. You can move, or remove it, as needed.
            this.perfisTableAdapter.Fill(this.mainDataSet.Perfis);
            // TODO: This line of code loads data into the 'mainDataSet.Comprovantes' table. You can move, or remove it, as needed.
            this.pontoComprovantesTableAdapter.Fill(this.mainDataSet.Comprovantes);
            // TODO: This line of code loads data into the 'mainDataSet.Pontos' table. You can move, or remove it, as needed.
            this.pontosTableAdapter.Fill(this.mainDataSet.Pontos);

        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void carregarImagemToolStripButton_Click(object sender, EventArgs e)
        {
            var result = imagemOpenFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                byte[] imagem;
                
                using (var fileStream = new FileStream(imagemOpenFileDialog.FileName, FileMode.Open))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await fileStream.CopyToAsync(memoryStream);

                        imagem = memoryStream.ToArray();
                    }
                }

                var rowView = (DataRowView)pontoComprovantesBindingSource.Current;

                var row = (PontoComprovantesRow)rowView.Row;

                row.Numero = imagemOpenFileDialog.FileName;

                row.Imagem = imagem;
            }
        }
    }
}
