using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public static class ModernTheme
{
    public static void Apply(Form form)
    {
        Color bg = Color.FromArgb(250, 248, 245); // crema cálido
        Color bgInput = Color.FromArgb(255, 255, 255); // casi blanco
        Color accent = Color.FromArgb(34, 197, 94);   // verde menta
        Color text = Color.FromArgb(51, 65, 85);    // gris suave
        Color border = Color.FromArgb(226, 232, 240); // gris claro

        form.BackColor = bg;
        form.ForeColor = text;
        form.Font = new Font("Segoe UI Variable", 10f);
        form.FormBorderStyle = FormBorderStyle.None;

        // Borde redondeado suave
        form.Paint += (s, e) =>
        {
            using var path = RoundedRect(e.ClipRectangle, 14);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using var pen = new Pen(border, 1);
            e.Graphics.DrawPath(pen, path);
        };

        foreach (Control c in form.Controls)
        {
            switch (c)
            {
                case Button btn:
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.BackColor = accent;
                    btn.ForeColor = Color.White;
                    btn.Font = new Font("Segoe UI Variable", 10f, FontStyle.Bold);
                    btn.Paint += (s, e) =>
                    {
                        var r = new Rectangle(0, 0, btn.Width, btn.Height);
                        using var path = RoundedRect(r, 12);
                        e.Graphics.FillPath(new SolidBrush(btn.BackColor), path);
                        TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, r, btn.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    };
                    btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(22, 163, 74);
                    btn.MouseLeave += (s, e) => btn.BackColor = accent;
                    break;

                case TextBox txt:
                    txt.BorderStyle = BorderStyle.None;
                    txt.BackColor = bgInput;
                    txt.ForeColor = text;
                    txt.Padding = new Padding(8);
                    txt.Margin = new Padding(4);
                    txt.Paint += (s, e) =>
                    {
                        using var path = RoundedRect(new Rectangle(0, 0, txt.Width - 1, txt.Height - 1), 10);
                        using var pen = new Pen(border, 2);
                        e.Graphics.DrawPath(pen, path);
                    };
                    break;

                case Label lbl:
                    lbl.ForeColor = lbl.Name.Contains("lblTotal") ? accent : text;
                    break;

                case ComboBox cmb:
                    cmb.FlatStyle = FlatStyle.Flat;
                    cmb.BackColor = bgInput;
                    cmb.ForeColor = text;
                    break;

                case DataGridView dgv:
                    dgv.BorderStyle = BorderStyle.None;
                    dgv.BackgroundColor = bg;
                    dgv.DefaultCellStyle.BackColor = bgInput;
                    dgv.DefaultCellStyle.ForeColor = text;
                    dgv.DefaultCellStyle.SelectionBackColor = accent;
                    dgv.DefaultCellStyle.SelectionForeColor = Color.White;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = text;
                    dgv.ColumnHeadersHeight = 32;
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.GridColor = border;
                    break;

                case NumericUpDown nud:
                    nud.BorderStyle = BorderStyle.None;
                    nud.BackColor = bgInput;
                    nud.ForeColor = text;
                    break;
            }
        }
    }

    private static GraphicsPath RoundedRect(Rectangle bounds, int radius)
    {
        var diameter = radius * 2;
        var arc = new Rectangle(bounds.Location, new Size(diameter, diameter));
        var path = new GraphicsPath();
        if (radius == 0) { path.AddRectangle(bounds); return path; }
        path.AddArc(arc, 180, 90);
        arc.X = bounds.Right - diameter;
        path.AddArc(arc, 270, 90);
        arc.Y = bounds.Bottom - diameter;
        path.AddArc(arc, 0, 90);
        arc.X = bounds.Left;
        path.AddArc(arc, 90, 90);
        path.CloseFigure();
        return path;
    }
}