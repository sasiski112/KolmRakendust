using System;
using System.Windows.Forms;

namespace Launcher
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new PeaVorm());
        }
    }
}
