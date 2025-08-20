using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Trabajo_1
{
    public partial class Splash : Form
    {
        private System.Windows.Forms.Timer timerMove;
        private System.Windows.Forms.Timer timerTick;
        private int x = -100;
        private int step = 8;
        private bool tickStarted = false;

        private readonly int centerY;
        private int tickOffsetY = 0;      // animación de salto
        private int tickAlpha = 0;        // fade-in

        public Splash()
        {
            InitializeComponent();
            Size = new Size(400, 200);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;
            BackColor = Color.FromArgb(18, 18, 28);
            DoubleBuffered = true;
            centerY = ClientSize.Height / 2;

            timerMove = new System.Windows.Forms.Timer { Interval = 20 };
            timerTick = new System.Windows.Forms.Timer { Interval = 25 };

            timerMove.Tick += OnMoveTick;
            timerTick.Tick += OnTickTick;

            timerMove.Start();
        }

        private void OnMoveTick(object sender, EventArgs e)
        {
            x += step;
            if (x >= ClientSize.Width / 2 - 20 && !tickStarted)
            {
                step = 0;
                tickStarted = true;
                timerMove.Stop();
                timerTick.Start();
            }
            Invalidate();
        }

        private void OnTickTick(object sender, EventArgs e)
        {
            // Salto suave (sube y baja)
            tickOffsetY = (int)(-10 * Math.Sin(Math.PI * tickAlpha / 100));
            tickAlpha += 5;

            if (tickAlpha >= 100)
            {
                timerTick.Stop();
                // espera 500 ms y cierra
                var closeTimer = new System.Windows.Forms.Timer { Interval = 500 };
                closeTimer.Tick += (_, __) => { closeTimer.Stop(); Close(); };
                closeTimer.Start();
            }
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Carrito
            using var carBrush = new SolidBrush(Color.FromArgb(64, 0, 255, 255));
            e.Graphics.FillRectangle(carBrush, x, centerY - 20, 40, 20);
            e.Graphics.FillEllipse(carBrush, x + 5, centerY + 5, 10, 10);
            e.Graphics.FillEllipse(carBrush, x + 25, centerY + 5, 10, 10);

            if (tickStarted)
            {
                // Tilde sale del centro del carrito + salto
                using var tickPen = new Pen(Color.Lime, 4);
                float tickX = x + 20;
                float tickY = centerY - 5 + tickOffsetY;

                e.Graphics.DrawLines(tickPen, new[]
                {
                    new PointF(tickX - 8, tickY + 4),
                    new PointF(tickX, tickY + 12),
                    new PointF(tickX + 10, tickY)
                });
            }
        }
    }
}