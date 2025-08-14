namespace Trabajo_1
{
    partial class FormFacturas
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            cmbClientes = new ComboBox();
            dtpFecha = new DateTimePicker();
            btnBuscar = new Button();
            dgvFacturas = new DataGridView();
            lblCliente = new Label();
            lblFecha = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvFacturas).BeginInit();
            SuspendLayout();
            // 
            // cmbClientes
            // 
            cmbClientes.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbClientes.FormattingEnabled = true;
            cmbClientes.Location = new Point(80, 20);
            cmbClientes.Name = "cmbClientes";
            cmbClientes.Size = new Size(220, 23);
            cmbClientes.TabIndex = 0;
            // 
            // dtpFecha
            // 
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(80, 60);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(220, 23);
            dtpFecha.TabIndex = 1;
            dtpFecha.ValueChanged += dtpFecha_ValueChanged;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(320, 20);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(140, 63);
            btnBuscar.TabIndex = 2;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // dgvFacturas
            // 
            dgvFacturas.AllowUserToAddRows = false;
            dgvFacturas.AllowUserToDeleteRows = false;
            dgvFacturas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvFacturas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFacturas.Location = new Point(20, 110);
            dgvFacturas.Name = "dgvFacturas";
            dgvFacturas.ReadOnly = true;
            dgvFacturas.Size = new Size(600, 300);
            dgvFacturas.TabIndex = 3;
            dgvFacturas.CellContentClick += dgvFacturas_CellContentClick;
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.Location = new Point(20, 23);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(47, 15);
            lblCliente.TabIndex = 4;
            lblCliente.Text = "Cliente:";
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(20, 63);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(41, 15);
            lblFecha.TabIndex = 5;
            lblFecha.Text = "Fecha:";
            // 
            // FormFacturas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(650, 440);
            Controls.Add(lblFecha);
            Controls.Add(lblCliente);
            Controls.Add(dgvFacturas);
            Controls.Add(btnBuscar);
            Controls.Add(dtpFecha);
            Controls.Add(cmbClientes);
            Name = "FormFacturas";
            Text = "Listado de Facturas";
            Load += FormFacturas_Load;
            ((System.ComponentModel.ISupportInitialize)dgvFacturas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox cmbClientes;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView dgvFacturas;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblFecha;
    }
}
