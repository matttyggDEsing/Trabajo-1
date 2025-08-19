namespace Trabajo_1
{
    partial class VerFactura
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblNumero;
        private Label lblCliente;
        private Label lblCuit;
        private Label lblFecha;
        private DataGridView dgvDetalle;
        private Label lblTotal;
        private Button btnImprimir;

        private void InitializeComponent()
        {
            lblNumero = new Label();
            lblCliente = new Label();
            lblCuit = new Label();
            lblFecha = new Label();
            dgvDetalle = new DataGridView();
            lblTotal = new Label();
            btnImprimir = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).BeginInit();
            SuspendLayout();

            // lblNumero
            lblNumero.AutoSize = true;
            lblNumero.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblNumero.Location = new Point(20, 20);
            lblNumero.Name = "lblNumero";
            lblNumero.Size = new Size(120, 25);
            lblNumero.TabIndex = 0;
            lblNumero.Text = "Factura Nº --";

            // lblCliente
            lblCliente.AutoSize = true;
            lblCliente.Location = new Point(20, 55);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(110, 15);
            lblCliente.TabIndex = 1;
            lblCliente.Text = "Cliente: --";

            // lblCuit
            lblCuit.AutoSize = true;
            lblCuit.Location = new Point(20, 75);
            lblCuit.Name = "lblCuit";
            lblCuit.Size = new Size(90, 15);
            lblCuit.TabIndex = 2;
            lblCuit.Text = "CUIT: --";

            // lblFecha
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(20, 95);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(85, 15);
            lblFecha.TabIndex = 3;
            lblFecha.Text = "Fecha: --";

            // dgvDetalle
            dgvDetalle.AllowUserToAddRows = false;
            dgvDetalle.AllowUserToDeleteRows = false;
            dgvDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalle.Location = new Point(20, 125);
            dgvDetalle.Name = "dgvDetalle";
            dgvDetalle.ReadOnly = true;
            dgvDetalle.Size = new Size(560, 200);
            dgvDetalle.TabIndex = 4;

            // lblTotal
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotal.Location = new Point(20, 340);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(70, 21);
            lblTotal.TabIndex = 5;
            lblTotal.Text = "Total: $0";

            // btnImprimir
            btnImprimir.Location = new Point(480, 340);
            btnImprimir.Name = "btnImprimir";
            btnImprimir.Size = new Size(100, 30);
            btnImprimir.TabIndex = 6;
            btnImprimir.Text = "Imprimir PDF";
            btnImprimir.UseVisualStyleBackColor = true;
            btnImprimir.Click += btnImprimir_Click;

            // VerFactura
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 400);
            Controls.Add(btnImprimir);
            Controls.Add(lblTotal);
            Controls.Add(dgvDetalle);
            Controls.Add(lblFecha);
            Controls.Add(lblCuit);
            Controls.Add(lblCliente);
            Controls.Add(lblNumero);
            Name = "VerFactura";
            Text = "Vista de Factura";
            Load += VerFactura_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}