
using System.Runtime.InteropServices;

namespace Modtropica_server
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        //[STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            AllocConsole();
            //new modtropica.world.websocket.ws_server.WebSocketHTTP_new();
            //new modtropica.world.pop_server_world();
            ApplicationConfiguration.Initialize();
            Application.Run(new mod_form());

        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

    }
}