namespace FileFuardSetup
{
    partial class FunctionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FunctionForm));
            this.OpenFile = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.scanResultsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.fileGuardDataSet2 = new FileFuardSetup.FileGuardDataSet2();
            this.timer_ = new System.Windows.Forms.Timer(this.components);
            this.Reasercher = new System.Windows.Forms.TextBox();
            this.OpenPDFgrafics = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.scanResultsTableAdapter1 = new FileFuardSetup.FileGuardDataSet2TableAdapters.ScanResultsTableAdapter();
            this.fileGuardDataSet = new FileFuardSetup.FileGuardDataSet();
            this.scanResultsBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.scanResultsTableAdapter = new FileFuardSetup.FileGuardDataSetTableAdapters.ScanResultsTableAdapter();
            this.fileGuardDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scanResultsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileGuardDataSet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileGuardDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scanResultsBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileGuardDataSetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenFile
            // 
            this.OpenFile.Location = new System.Drawing.Point(645, 207);
            this.OpenFile.Name = "OpenFile";
            this.OpenFile.Size = new System.Drawing.Size(131, 41);
            this.OpenFile.TabIndex = 0;
            this.OpenFile.Text = "Открыть файл";
            this.OpenFile.UseVisualStyleBackColor = true;
            this.OpenFile.Click += new System.EventHandler(this.OpenFile_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dataGridView1.DataSource = this.scanResultsBindingSource;
            this.dataGridView1.GridColor = System.Drawing.Color.PaleGreen;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(521, 284);
            this.dataGridView1.TabIndex = 1;
            // 
            // scanResultsBindingSource
            // 
            this.scanResultsBindingSource.DataMember = "ScanResults";
            this.scanResultsBindingSource.DataSource = this.fileGuardDataSet2;
            // 
            // fileGuardDataSet2
            // 
            this.fileGuardDataSet2.DataSetName = "FileGuardDataSet2";
            this.fileGuardDataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Reasercher
            // 
            this.Reasercher.Location = new System.Drawing.Point(539, 86);
            this.Reasercher.Name = "Reasercher";
            this.Reasercher.Size = new System.Drawing.Size(100, 20);
            this.Reasercher.TabIndex = 2;
            this.Reasercher.TextChanged += new System.EventHandler(this.Reasercher_TextChanged);
            // 
            // OpenPDFgrafics
            // 
            this.OpenPDFgrafics.Location = new System.Drawing.Point(539, 209);
            this.OpenPDFgrafics.Name = "OpenPDFgrafics";
            this.OpenPDFgrafics.Size = new System.Drawing.Size(100, 36);
            this.OpenPDFgrafics.TabIndex = 3;
            this.OpenPDFgrafics.Text = "Отчет";
            this.OpenPDFgrafics.UseVisualStyleBackColor = true;
            this.OpenPDFgrafics.Click += new System.EventHandler(this.OpenPDFgrafics_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.label1.Location = new System.Drawing.Point(645, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Поиск";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Aquamarine;
            this.label2.Location = new System.Drawing.Point(546, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Открытие отчета";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(645, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 41);
            this.button1.TabIndex = 6;
            this.button1.Text = "Вернуться в главное меню";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(539, 254);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 42);
            this.button2.TabIndex = 7;
            this.button2.Text = "Удалить запись";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(645, 254);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(131, 42);
            this.button3.TabIndex = 8;
            this.button3.Text = "Добавить в исключение";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // scanResultsTableAdapter1
            // 
            this.scanResultsTableAdapter1.ClearBeforeFill = true;
            // 
            // fileGuardDataSet
            // 
            this.fileGuardDataSet.DataSetName = "FileGuardDataSet";
            this.fileGuardDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // scanResultsBindingSource1
            // 
            this.scanResultsBindingSource1.DataMember = "ScanResults";
            this.scanResultsBindingSource1.DataSource = this.fileGuardDataSet;
            // 
            // scanResultsTableAdapter
            // 
            this.scanResultsTableAdapter.ClearBeforeFill = true;
            // 
            // fileGuardDataSetBindingSource
            // 
            this.fileGuardDataSetBindingSource.DataSource = this.fileGuardDataSet;
            this.fileGuardDataSetBindingSource.Position = 0;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Индефикатор записи";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "FileName";
            this.dataGridViewTextBoxColumn1.HeaderText = "Полное имя файла";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "FilePath";
            this.dataGridViewTextBoxColumn2.HeaderText = "Путь файла";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "ScanTime";
            this.dataGridViewTextBoxColumn3.HeaderText = "Время сканирования";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Vulnerability";
            this.dataGridViewTextBoxColumn4.HeaderText = "Уязвимость";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // FunctionForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(788, 314);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OpenPDFgrafics);
            this.Controls.Add(this.Reasercher);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.OpenFile);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FunctionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Главная страница";
            this.TransparencyKey = System.Drawing.SystemColors.ButtonFace;
            this.Load += new System.EventHandler(this.FunctionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scanResultsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileGuardDataSet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileGuardDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scanResultsBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileGuardDataSetBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenFile;
        private System.Windows.Forms.DataGridView dataGridView1;
  
        private System.Windows.Forms.DataGridViewTextBoxColumn fileNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn filePathDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn scanTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vulnerabilityDataGridViewTextBoxColumn;
        private System.Windows.Forms.Timer timer_;
        private FileGuardDataSet2 fileGuardDataSet2;
        private System.Windows.Forms.BindingSource scanResultsBindingSource;
        private FileGuardDataSet2TableAdapters.ScanResultsTableAdapter scanResultsTableAdapter1;
        private System.Windows.Forms.TextBox Reasercher;
        private System.Windows.Forms.Button OpenPDFgrafics;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private FileGuardDataSet fileGuardDataSet;
        private System.Windows.Forms.BindingSource scanResultsBindingSource1;
        private FileGuardDataSetTableAdapters.ScanResultsTableAdapter scanResultsTableAdapter;
        private System.Windows.Forms.BindingSource fileGuardDataSetBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}