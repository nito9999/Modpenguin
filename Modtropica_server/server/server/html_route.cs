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
        [Route("www.poptropica.com/game/show.php", "text/html")]
        public static string show_php()
        {
            return "";
        }
        [Route("www.poptropica.com/show.php", "text/html")]
        public static string show1_php()
        {
            return "";
        }
        [Route("www.poptropica.com/set_user_field.php", "text/html")]
        public static string set_user_field_php()
        {
            return "";
        }
        [Route("www.poptropica.com/login.php", "text/html")]
        public static string login_php()
        {
            return "answer=ok&json=%7B%22pass_hash%22%3A%22%22%2C%22age%22%3A%226%22%2C%22gender%22%3A%22M%22%2C%22firstname%22%3A%22barefoot%22%2C%22lastname%22%3A%22bean%22%2C%22has_parent_email%22%3Anull%2C%22parent_email%22%3Anull%2C%22login%22%3A%22testne%22%2C%22dbid%22%3A%221%22%2C%22look%22%3A%221%2C0xeaa16e%2C0xffc255%2C13158%2C100%2C3%2C1%2Clightningknight%2C4%2Clightningknight%2C1%2C2%2Clightningknightsword%2Clightningknight%2Clightningknight%2C1%2C1%2Cnone%3A%22%2C%22map%22%3A%221%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%2C1%22%2C%22last_room%22%3A%22GlobalAS3Embassy%22%2C%22island%22%3A%22Hub%22%2C%22lastx%22%3A%222350%22%2C%22lasty%22%3A%22832%22%2C%22scores%22%3A%22%22%2C%22userData%22%3A%22%5B%5D%22%2C%22memstatus%22%3A%22member%22%2C%22memdate%22%3A%22%22%7D";
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
        [Route("www.poptropica.com/haxe", "text/html")]
        public static string game_haxe_html()
        {
            string path_url = Path.Combine("www.poptropica.com/haxe/index.html");
            return File.ReadAllText(path_url);
        }
        [Route("www.poptropica.com/haxe/play", "text/html")]
        public static string game_haxe_play_html()
        {
            string path_url = Path.Combine("www.poptropica.com/haxe/play/index.html");
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
