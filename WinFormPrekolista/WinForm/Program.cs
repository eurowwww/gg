using WinForm.Engine;
using WinForm.Forms;

namespace WinForm
{
    internal static class Program
    {
        private static UIApplicationContext _context;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            _context = new UIApplicationContext();
            _context.MainForm = new MainForm(_context);
            Application.Run(_context);

            
        }
    }
}