using Lavadero.Vistas;
using System;
using System.Reflection.Metadata;
using System.Windows.Forms;
namespace Lavadero
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormLogin());
        }
    }