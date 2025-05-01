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
        [Route("www.poptropica.com/game/", "text/html")]
        public static string game_as3_html()
        {
            string path_url = Path.Combine("www.poptropica.com/game/index.html");
            return File.ReadAllText(path_url);
        }
        [Route("www.poptropica.com/loader.php", "text/html")]
        public static string game_loader_html()
        {
            string scene = "Home";
            string island_path = "Home";
            string path = "gameplay";
            Console.WriteLine($"scene: {scene} on island: {island_path} with path: {path}");

            return as2_base_php.Base_php_gen(scene, island_path, path);
        }
        [Route("/uh.php", "text/html")]
        public static string friends_as2_php()
        {
            return "{}";
        }

        [Route("www.poptropica.com/crash-record.php", "text/html")]
        public static bool game_crash_record(string version, string stack)
        {

            Console.WriteLine($"Uh oh poptropica crashed.\n" +
                $"poptropica version: {version}\n" +
                $"{stack}");
            return false;
        }
        [Route("www.poptropica.com/", "text/html")]
        public static string game_as2_html()
        {
            string path_url = Path.Combine("www.poptropica.com/index.html");
            return File.ReadAllText(path_url);
        }
        [Route("www.poptropica.com/base.php", "text/html")]
        public static string base_php_code(string room = "Home", string island = "Home", string startup_path = "gameplay")
        {
            string scene = room ?? "Home";
            string island_path = island ?? "Home";
            string path = startup_path ?? "gameplay";
            Console.WriteLine($"scene: {scene} on island: {island_path} with path: {path}");

            return as2_base_php.Base_php_gen(scene, island_path, path);
        }
    }
}
