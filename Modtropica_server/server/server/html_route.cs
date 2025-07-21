using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Modtropica_server.server.route_system;

using File = Modtropica_server.modtropica.core.file_system.File;
using Directory = Modtropica_server.modtropica.core.file_system.Directory;
using File_real = System.IO.File;
using dir_real = System.IO.Directory;
using Modtropica_server.poptropica_php_emu;

namespace Modtropica_server.server.server
{
    class html_route
    {        
        [Route("play.clubpenguin.com/php/login.php", "text/html")]
        public static string login_php()
        {
            return ""; // login shit
        }
        /*
        [Route("www.poptropica.com/crash-record.php", "text/html")]
        public static bool game_crash_record(string version, string stack)
        {

            Console.WriteLine($"Uh oh poptropica crashed.\n" +
                $"poptropica version: {version}\n" +
                $"{stack}");
            return false;
        }*/

        [Route("play.clubpenguin.com/", "text/html")]
        public static string game_as2_html()
        {
            string path_url = Path.Combine("play.clubpenguin.com/index.html");
            return File.ReadAllText(path_url);
        }
    }
}
