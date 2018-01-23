using System;
using System.Threading;
using System.Windows.Forms;

namespace PinApp
{
    static class Program
    {
        // ATTRIBUTI
        private static MainForm mainForm;

        [STAThread] // ENTRY POINT
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            mainForm = new MainForm();
            Server server = new Server(mainForm);

            Thread mainThread = new Thread(server.launch);
            mainThread.Start();

            Application.Run(mainForm);
        }
    }
}