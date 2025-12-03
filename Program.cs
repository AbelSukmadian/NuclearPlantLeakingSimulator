using System;
using System.Windows.Forms;

namespace NuclearLeakSim_WinForms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Pastikan Form1 adalah nama kelas form utama Anda
            Application.Run(new Form1());
        }
    }
}