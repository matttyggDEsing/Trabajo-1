using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.IO;
using System.Windows.Forms;
using Trabajo_1.DB;
namespace Trabajo_1


{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            GlobalFontSettings.UseWindowsFontsUnderWindows = true;
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var splash = new Splash())
            {
                splash.ShowDialog();
            }
            Application.Run(new Form1()); // Aquí debe estar tu formulario principal
                                          // Debe ejecutarse ANTES de crear cualquier XFont
            
        }

    }
}