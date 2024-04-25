
namespace V_1._2
{
    partial class HidenLayersSettingsForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.layerNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.layerNeuronCount = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.activationFunction = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.activationFunctionSettings = new System.Windows.Forms.DataGridViewButtonColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.layerNumberColumn,
            this.layerNeuronCount,
            this.activationFunction,
            this.activationFunctionSettings});
            this.dataGridView1.Location = new System.Drawing.Point(33, 47);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(876, 664);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // layerNumberColumn
            // 
            this.layerNumberColumn.Frozen = true;
            this.layerNumberColumn.HeaderText = "Номер слоя";
            this.layerNumberColumn.MinimumWidth = 6;
            this.layerNumberColumn.Name = "layerNumberColumn";
            this.layerNumberColumn.ReadOnly = true;
            this.layerNumberColumn.Width = 125;
            // 
            // layerNeuronCount
            // 
            this.layerNeuronCount.Frozen = true;
            this.layerNeuronCount.HeaderText = "Количество нейронов";
            this.layerNeuronCount.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.layerNeuronCount.MinimumWidth = 6;
            this.layerNeuronCount.Name = "layerNeuronCount";
            this.layerNeuronCount.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.layerNeuronCount.Width = 125;
            // 
            // activationFunction
            // 
            this.activationFunction.Frozen = true;
            this.activationFunction.HeaderText = "Активационная функция";
            this.activationFunction.Items.AddRange(new object[] {
            "сигмоид",
            "гиперболический тангенс",
            "ReLU",
            "leaky ReLU",
            "ELU",
            "SmoothReLU",
            "softsign",
            "ступенчатая"});
            this.activationFunction.MinimumWidth = 6;
            this.activationFunction.Name = "activationFunction";
            this.activationFunction.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.activationFunction.Width = 150;
            // 
            // activationFunctionSettings
            // 
            this.activationFunctionSettings.Frozen = true;
            this.activationFunctionSettings.HeaderText = "Свойства актиационной функции";
            this.activationFunctionSettings.MinimumWidth = 6;
            this.activationFunctionSettings.Name = "activationFunctionSettings";
            this.activationFunctionSettings.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.activationFunctionSettings.Text = "Настроить";
            this.activationFunctionSettings.UseColumnTextForButtonValue = true;
            this.activationFunctionSettings.Width = 125;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(957, 710);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 37);
            this.button1.TabIndex = 1;
            this.button1.Text = "Применить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(957, 573);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(133, 37);
            this.button3.TabIndex = 1;
            this.button3.Text = "Очистить";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(957, 616);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(133, 37);
            this.button2.TabIndex = 1;
            this.button2.Text = "По умолчанию";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // HidenLayersSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 759);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "HidenLayersSettingsForm";
            this.Text = "Изменение настроек скрытых слоев";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn layerNumberColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn layerNeuronCount;
        private System.Windows.Forms.DataGridViewComboBoxColumn activationFunction;
        private System.Windows.Forms.DataGridViewButtonColumn activationFunctionSettings;
    }
}