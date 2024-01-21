namespace MeuPonto
{
    partial class ContratosForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContratosForm));
            System.Windows.Forms.Label idLabel;
            System.Windows.Forms.Label matriculaLabel;
            System.Windows.Forms.Label nomeLabel;
            System.Windows.Forms.Label pisLabel;
            System.Windows.Forms.Label empresa_CnpjLabel;
            System.Windows.Forms.Label empresa_NomeLabel;
            System.Windows.Forms.Label empresa_InscricaoEstadualLabel;
            System.Windows.Forms.Label empresa_EnderecoLabel;
            System.Windows.Forms.Label creationDateLabel;
            this.mainDataSet = new MeuPonto.MainDataSet();
            this.contratosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.contratosTableAdapter = new MeuPonto.MainDataSetTableAdapters.ContratosTableAdapter();
            this.tableAdapterManager = new MeuPonto.MainDataSetTableAdapters.TableAdapterManager();
            this.contratosBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.contratosBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.contratosDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.formulárioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.matriculaTextBox = new System.Windows.Forms.TextBox();
            this.nomeTextBox = new System.Windows.Forms.TextBox();
            this.pisTextBox = new System.Windows.Forms.TextBox();
            this.empresa_CnpjTextBox = new System.Windows.Forms.TextBox();
            this.empresa_NomeTextBox = new System.Windows.Forms.TextBox();
            this.empresa_InscricaoEstadualTextBox = new System.Windows.Forms.TextBox();
            this.empresa_EnderecoTextBox = new System.Windows.Forms.TextBox();
            this.creationDateDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            idLabel = new System.Windows.Forms.Label();
            matriculaLabel = new System.Windows.Forms.Label();
            nomeLabel = new System.Windows.Forms.Label();
            pisLabel = new System.Windows.Forms.Label();
            empresa_CnpjLabel = new System.Windows.Forms.Label();
            empresa_NomeLabel = new System.Windows.Forms.Label();
            empresa_InscricaoEstadualLabel = new System.Windows.Forms.Label();
            empresa_EnderecoLabel = new System.Windows.Forms.Label();
            creationDateLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contratosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contratosBindingNavigator)).BeginInit();
            this.contratosBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contratosDataGridView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainDataSet
            // 
            this.mainDataSet.DataSetName = "MainDataSet";
            this.mainDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // contratosBindingSource
            // 
            this.contratosBindingSource.DataMember = "Contratos";
            this.contratosBindingSource.DataSource = this.mainDataSet;
            // 
            // contratosTableAdapter
            // 
            this.contratosTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.ContratosTableAdapter = this.contratosTableAdapter;
            this.tableAdapterManager.PontoComprovantesTableAdapter = null;
            this.tableAdapterManager.PontosTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = MeuPonto.MainDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // contratosBindingNavigator
            // 
            this.contratosBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.contratosBindingNavigator.BindingSource = this.contratosBindingSource;
            this.contratosBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.contratosBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.contratosBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.contratosBindingNavigatorSaveItem});
            this.contratosBindingNavigator.Location = new System.Drawing.Point(0, 24);
            this.contratosBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.contratosBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.contratosBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.contratosBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.contratosBindingNavigator.Name = "contratosBindingNavigator";
            this.contratosBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.contratosBindingNavigator.Size = new System.Drawing.Size(800, 25);
            this.contratosBindingNavigator.TabIndex = 0;
            this.contratosBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // contratosBindingNavigatorSaveItem
            // 
            this.contratosBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.contratosBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("contratosBindingNavigatorSaveItem.Image")));
            this.contratosBindingNavigatorSaveItem.Name = "contratosBindingNavigatorSaveItem";
            this.contratosBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.contratosBindingNavigatorSaveItem.Text = "Save Data";
            this.contratosBindingNavigatorSaveItem.Click += new System.EventHandler(this.contratosBindingNavigatorSaveItem_Click);
            // 
            // contratosDataGridView
            // 
            this.contratosDataGridView.AutoGenerateColumns = false;
            this.contratosDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.contratosDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewImageColumn1});
            this.contratosDataGridView.DataSource = this.contratosBindingSource;
            this.contratosDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contratosDataGridView.Location = new System.Drawing.Point(0, 0);
            this.contratosDataGridView.Name = "contratosDataGridView";
            this.contratosDataGridView.Size = new System.Drawing.Size(266, 379);
            this.contratosDataGridView.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Matricula";
            this.dataGridViewTextBoxColumn2.HeaderText = "Matricula";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Nome";
            this.dataGridViewTextBoxColumn3.HeaderText = "Nome";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Pis";
            this.dataGridViewTextBoxColumn4.HeaderText = "Pis";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Empresa_Cnpj";
            this.dataGridViewTextBoxColumn5.HeaderText = "Empresa_Cnpj";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Empresa_Nome";
            this.dataGridViewTextBoxColumn6.HeaderText = "Empresa_Nome";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Empresa_InscricaoEstadual";
            this.dataGridViewTextBoxColumn7.HeaderText = "Empresa_InscricaoEstadual";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "Empresa_Endereco";
            this.dataGridViewTextBoxColumn8.HeaderText = "Empresa_Endereco";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "CreationDate";
            this.dataGridViewTextBoxColumn9.HeaderText = "CreationDate";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.DataPropertyName = "Version";
            this.dataGridViewImageColumn1.HeaderText = "Version";
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formulárioToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.contratosDataGridView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(800, 379);
            this.splitContainer1.SplitterDistance = 266;
            this.splitContainer1.TabIndex = 4;
            // 
            // formulárioToolStripMenuItem
            // 
            this.formulárioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sairToolStripMenuItem});
            this.formulárioToolStripMenuItem.Name = "formulárioToolStripMenuItem";
            this.formulárioToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.formulárioToolStripMenuItem.Text = "Formulário";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(idLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.idTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(matriculaLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.matriculaTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(nomeLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.nomeTextBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(pisLabel, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.pisTextBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(empresa_CnpjLabel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.empresa_CnpjTextBox, 1, 4);
            this.tableLayoutPanel1.Controls.Add(empresa_NomeLabel, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.empresa_NomeTextBox, 1, 5);
            this.tableLayoutPanel1.Controls.Add(empresa_InscricaoEstadualLabel, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.empresa_InscricaoEstadualTextBox, 1, 6);
            this.tableLayoutPanel1.Controls.Add(empresa_EnderecoLabel, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.empresa_EnderecoTextBox, 1, 7);
            this.tableLayoutPanel1.Controls.Add(creationDateLabel, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.creationDateDateTimePicker, 1, 8);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(530, 379);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // idLabel
            // 
            idLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            idLabel.AutoSize = true;
            idLabel.Location = new System.Drawing.Point(3, 6);
            idLabel.Name = "idLabel";
            idLabel.Size = new System.Drawing.Size(19, 13);
            idLabel.TabIndex = 0;
            idLabel.Text = "Id:";
            // 
            // idTextBox
            // 
            this.idTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.contratosBindingSource, "Id", true));
            this.idTextBox.Location = new System.Drawing.Point(150, 3);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(200, 20);
            this.idTextBox.TabIndex = 1;
            // 
            // matriculaLabel
            // 
            matriculaLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            matriculaLabel.AutoSize = true;
            matriculaLabel.Location = new System.Drawing.Point(3, 32);
            matriculaLabel.Name = "matriculaLabel";
            matriculaLabel.Size = new System.Drawing.Size(53, 13);
            matriculaLabel.TabIndex = 2;
            matriculaLabel.Text = "Matricula:";
            // 
            // matriculaTextBox
            // 
            this.matriculaTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.contratosBindingSource, "Matricula", true));
            this.matriculaTextBox.Location = new System.Drawing.Point(150, 29);
            this.matriculaTextBox.Name = "matriculaTextBox";
            this.matriculaTextBox.Size = new System.Drawing.Size(200, 20);
            this.matriculaTextBox.TabIndex = 3;
            // 
            // nomeLabel
            // 
            nomeLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            nomeLabel.AutoSize = true;
            nomeLabel.Location = new System.Drawing.Point(3, 58);
            nomeLabel.Name = "nomeLabel";
            nomeLabel.Size = new System.Drawing.Size(38, 13);
            nomeLabel.TabIndex = 4;
            nomeLabel.Text = "Nome:";
            // 
            // nomeTextBox
            // 
            this.nomeTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.contratosBindingSource, "Nome", true));
            this.nomeTextBox.Location = new System.Drawing.Point(150, 55);
            this.nomeTextBox.Name = "nomeTextBox";
            this.nomeTextBox.Size = new System.Drawing.Size(200, 20);
            this.nomeTextBox.TabIndex = 5;
            // 
            // pisLabel
            // 
            pisLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            pisLabel.AutoSize = true;
            pisLabel.Location = new System.Drawing.Point(3, 84);
            pisLabel.Name = "pisLabel";
            pisLabel.Size = new System.Drawing.Size(24, 13);
            pisLabel.TabIndex = 6;
            pisLabel.Text = "Pis:";
            // 
            // pisTextBox
            // 
            this.pisTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.contratosBindingSource, "Pis", true));
            this.pisTextBox.Location = new System.Drawing.Point(150, 81);
            this.pisTextBox.Name = "pisTextBox";
            this.pisTextBox.Size = new System.Drawing.Size(200, 20);
            this.pisTextBox.TabIndex = 7;
            // 
            // empresa_CnpjLabel
            // 
            empresa_CnpjLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            empresa_CnpjLabel.AutoSize = true;
            empresa_CnpjLabel.Location = new System.Drawing.Point(3, 110);
            empresa_CnpjLabel.Name = "empresa_CnpjLabel";
            empresa_CnpjLabel.Size = new System.Drawing.Size(75, 13);
            empresa_CnpjLabel.TabIndex = 8;
            empresa_CnpjLabel.Text = "Empresa Cnpj:";
            // 
            // empresa_CnpjTextBox
            // 
            this.empresa_CnpjTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.contratosBindingSource, "Empresa_Cnpj", true));
            this.empresa_CnpjTextBox.Location = new System.Drawing.Point(150, 107);
            this.empresa_CnpjTextBox.Name = "empresa_CnpjTextBox";
            this.empresa_CnpjTextBox.Size = new System.Drawing.Size(200, 20);
            this.empresa_CnpjTextBox.TabIndex = 9;
            // 
            // empresa_NomeLabel
            // 
            empresa_NomeLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            empresa_NomeLabel.AutoSize = true;
            empresa_NomeLabel.Location = new System.Drawing.Point(3, 136);
            empresa_NomeLabel.Name = "empresa_NomeLabel";
            empresa_NomeLabel.Size = new System.Drawing.Size(82, 13);
            empresa_NomeLabel.TabIndex = 10;
            empresa_NomeLabel.Text = "Empresa Nome:";
            // 
            // empresa_NomeTextBox
            // 
            this.empresa_NomeTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.contratosBindingSource, "Empresa_Nome", true));
            this.empresa_NomeTextBox.Location = new System.Drawing.Point(150, 133);
            this.empresa_NomeTextBox.Name = "empresa_NomeTextBox";
            this.empresa_NomeTextBox.Size = new System.Drawing.Size(200, 20);
            this.empresa_NomeTextBox.TabIndex = 11;
            // 
            // empresa_InscricaoEstadualLabel
            // 
            empresa_InscricaoEstadualLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            empresa_InscricaoEstadualLabel.AutoSize = true;
            empresa_InscricaoEstadualLabel.Location = new System.Drawing.Point(3, 162);
            empresa_InscricaoEstadualLabel.Name = "empresa_InscricaoEstadualLabel";
            empresa_InscricaoEstadualLabel.Size = new System.Drawing.Size(141, 13);
            empresa_InscricaoEstadualLabel.TabIndex = 12;
            empresa_InscricaoEstadualLabel.Text = "Empresa Inscricao Estadual:";
            // 
            // empresa_InscricaoEstadualTextBox
            // 
            this.empresa_InscricaoEstadualTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.contratosBindingSource, "Empresa_InscricaoEstadual", true));
            this.empresa_InscricaoEstadualTextBox.Location = new System.Drawing.Point(150, 159);
            this.empresa_InscricaoEstadualTextBox.Name = "empresa_InscricaoEstadualTextBox";
            this.empresa_InscricaoEstadualTextBox.Size = new System.Drawing.Size(200, 20);
            this.empresa_InscricaoEstadualTextBox.TabIndex = 13;
            // 
            // empresa_EnderecoLabel
            // 
            empresa_EnderecoLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            empresa_EnderecoLabel.AutoSize = true;
            empresa_EnderecoLabel.Location = new System.Drawing.Point(3, 188);
            empresa_EnderecoLabel.Name = "empresa_EnderecoLabel";
            empresa_EnderecoLabel.Size = new System.Drawing.Size(100, 13);
            empresa_EnderecoLabel.TabIndex = 14;
            empresa_EnderecoLabel.Text = "Empresa Endereco:";
            // 
            // empresa_EnderecoTextBox
            // 
            this.empresa_EnderecoTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.contratosBindingSource, "Empresa_Endereco", true));
            this.empresa_EnderecoTextBox.Location = new System.Drawing.Point(150, 185);
            this.empresa_EnderecoTextBox.Name = "empresa_EnderecoTextBox";
            this.empresa_EnderecoTextBox.Size = new System.Drawing.Size(200, 20);
            this.empresa_EnderecoTextBox.TabIndex = 15;
            // 
            // creationDateLabel
            // 
            creationDateLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            creationDateLabel.AutoSize = true;
            creationDateLabel.Location = new System.Drawing.Point(3, 214);
            creationDateLabel.Name = "creationDateLabel";
            creationDateLabel.Size = new System.Drawing.Size(75, 13);
            creationDateLabel.TabIndex = 16;
            creationDateLabel.Text = "Creation Date:";
            // 
            // creationDateDateTimePicker
            // 
            this.creationDateDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.contratosBindingSource, "CreationDate", true));
            this.creationDateDateTimePicker.Location = new System.Drawing.Point(150, 211);
            this.creationDateDateTimePicker.Name = "creationDateDateTimePicker";
            this.creationDateDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.creationDateDateTimePicker.TabIndex = 17;
            // 
            // sairToolStripMenuItem
            // 
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sairToolStripMenuItem.Text = "Sair";
            this.sairToolStripMenuItem.Click += new System.EventHandler(this.sairToolStripMenuItem_Click);
            // 
            // ContratosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.contratosBindingNavigator);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ContratosForm";
            this.Text = "Contratos";
            this.Load += new System.EventHandler(this.ContratosForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contratosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contratosBindingNavigator)).EndInit();
            this.contratosBindingNavigator.ResumeLayout(false);
            this.contratosBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contratosDataGridView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MainDataSet mainDataSet;
        private System.Windows.Forms.BindingSource contratosBindingSource;
        private MainDataSetTableAdapters.ContratosTableAdapter contratosTableAdapter;
        private MainDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.BindingNavigator contratosBindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton contratosBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView contratosDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem formulárioToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.TextBox matriculaTextBox;
        private System.Windows.Forms.TextBox nomeTextBox;
        private System.Windows.Forms.TextBox pisTextBox;
        private System.Windows.Forms.TextBox empresa_CnpjTextBox;
        private System.Windows.Forms.TextBox empresa_NomeTextBox;
        private System.Windows.Forms.TextBox empresa_InscricaoEstadualTextBox;
        private System.Windows.Forms.TextBox empresa_EnderecoTextBox;
        private System.Windows.Forms.DateTimePicker creationDateDateTimePicker;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
    }
}