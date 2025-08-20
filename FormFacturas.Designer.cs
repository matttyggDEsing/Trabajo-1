namespace Trabajo_1
{
    partial class FormFacturas
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvFacturas;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.CheckBox chkFiltrarFecha;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.ComboBox cmbClientes;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblFecha;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dgvFacturas = new DataGridView();
            dtpFecha = new DateTimePicker();
            chkFiltrarFecha = new CheckBox();
            btnBuscar = new Button();
            cmbClientes = new ComboBox();
            lblCliente = new Label();
            lblFecha = new Label();
            btnSalir = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvFacturas).BeginInit();
            SuspendLayout();
            // 
            // dgvFacturas
            // 
            dgvFacturas.AllowUserToAddRows = false;
            dgvFacturas.AllowUserToDeleteRows = false;
            dgvFacturas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvFacturas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFacturas.Location = new Point(20, 80);
            dgvFacturas.Name = "dgvFacturas";
            dgvFacturas.ReadOnly = true;
            dgvFacturas.Size = new Size(760, 350);
            dgvFacturas.TabIndex = 0;
            dgvFacturas.CellDoubleClick += dgvFacturas_CellDoubleClick_1;
            // 
            // dtpFecha
            // 
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(370, 16);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(120, 23);
            dtpFecha.TabIndex = 5;
            // 
            // chkFiltrarFecha
            // 
            chkFiltrarFecha.AutoSize = true;
            chkFiltrarFecha.Location = new Point(500, 18);
            chkFiltrarFecha.Name = "chkFiltrarFecha";
            chkFiltrarFecha.Size = new Size(90, 19);
            chkFiltrarFecha.TabIndex = 4;
            chkFiltrarFecha.Text = "Filtrar Fecha";
            chkFiltrarFecha.UseVisualStyleBackColor = true;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(620, 15);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(100, 27);
            btnBuscar.TabIndex = 6;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // cmbClientes
            // 
            cmbClientes.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbClientes.Location = new Point(80, 16);
            cmbClientes.Name = "cmbClientes";
            cmbClientes.Size = new Size(220, 23);
            cmbClientes.TabIndex = 2;
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.Location = new Point(20, 20);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(44, 15);
            lblCliente.TabIndex = 1;
            lblCliente.Text = "Cliente";
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(320, 20);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(38, 15);
            lblFecha.TabIndex = 3;
            lblFecha.Text = "Fecha";
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(680, 436);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(100, 27);
            btnSalir.TabIndex = 7;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // FormFacturas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 471);
            Controls.Add(btnSalir);
            Controls.Add(btnBuscar);
            Controls.Add(dtpFecha);
            Controls.Add(chkFiltrarFecha);
            Controls.Add(cmbClientes);
            Controls.Add(lblCliente);
            Controls.Add(lblFecha);
            Controls.Add(dgvFacturas);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormFacturas";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Consulta de Facturas";
            Load += FormFacturas_Load;
            ((System.ComponentModel.ISupportInitialize)dgvFacturas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private Button btnSalir;
    }
}
