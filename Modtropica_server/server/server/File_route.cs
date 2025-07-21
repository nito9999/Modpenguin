using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Modtropica_server.server.route_system;

using File = Modtropica_server.modtropica.core.file_system.File;
using Directory = Modtropica_server.modtropica.core.file_system.Directory;
using File_real = System.IO.File;
using dir_real = System.IO.Directory;
using System.IO;
using WebSocketSharp;

namespace Modtropica_server.server.server
{
    class File_route
    {

        public static string play_clubpenguin_path = $"{base_path}/play.clubpenguin.com";
        public static string media_clubpenguin_path = $"{base_path}/media.clubpenguin.com";
        public static string base_path = "game_data/game_files/";
        public static string mod_path = "Mod_data/";

        [Route(@"^play.clubpenguin.com/(?<filename>.+)\.js$", "application/javascript", true)]
        public static byte[] file_path_js(string filename)
        {
            string path_url = "play.clubpenguin.com/" + filename + ".js";
            if (File.Exists(path_url))
                return File.ReadAllBytes(path_url);
            return null;
        }

        [Route(@"^play.clubpenguin.com/(?<filename>.+)\.gif$", "image/gif", true)]
        public static byte[] file_path_gif(string filename)
        {
            string path_url = "play.clubpenguin.com/" + filename + ".gif";
            if (File.Exists(path_url))
                return File.ReadAllBytes(path_url);
            return null;
        }

        [Route(@"^play.clubpenguin.com/(?<filename>.+)\.css$", "text/css", true)]
        public static byte[] file_path_css(string filename)
        {
            string path_url = "play.clubpenguin.com/" + filename + ".css";
            if (File.Exists(path_url))
                return File.ReadAllBytes(path_url);
            return null;
        }

        [Route(@"^play.clubpenguin.com/(?<filename>.+)\.swf$", "application/x-shockwave-flash", true)]
        public static byte[] file_path_swf(string filename)
        {
            string path_url = "play.clubpenguin.com/" + filename + ".swf";
            if (File.Exists(path_url))
                return File.ReadAllBytes(path_url);
            return null;
        }

        [Route(@"^media.clubpenguin.com/(?<filename>.+)\.swf$", "application/x-shockwave-flash", true)]
        public static byte[] media_file_path_swf(string filename)
        {
            string path_url = "media.clubpenguin.com/" + filename + ".swf";
            if (File.Exists(path_url))
                return File.ReadAllBytes(path_url);
            return null;
        }

        /*
        [Route(@"^www.poptropica.com/(?<filename>.+)\.mp3$", "audio/mpeg", true)]
        public static byte[] file_path_mp3(string filename)
        {
            string path_url = "www.poptropica.com/" + filename + ".mp3";
            if (File.Exists(path_url))
                return File.ReadAllBytes(path_url);
            return null;
        }*/

        [Route("play.clubpenguin.com/news.txt", "application/text")]
        public static byte[] news_txt_file()
        {
            string path_url = "play.clubpenguin.com/news.txt";
            if (File.Exists(path_url))
                return File.ReadAllBytes(path_url);
            return null;
        }

        [Route("play.clubpenguin.com/setup.txt", "application/text")]
        public static byte[] Setup_txt_file()
        {
            string path_url = "play.clubpenguin.com/setup.txt";
            if (File.Exists(path_url))
                return File.ReadAllBytes(path_url);
            return null;
        }

        [Route(@"^play.clubpenguin.com/(?<filename>.+)\.txt$", "application/text", true)]
        public static byte[] file_path_txt(string filename)
        {
            string path_url = "play.clubpenguin.com/" + filename + ".txt";
            if (File.Exists(path_url))
                return File.ReadAllBytes(path_url);
            return null;
        }
        /*
        [Route(@"^www.poptropica.com/(?<filename>.+)\.xml$", "application/xml", true)]
        public static byte[] file_path_xml(string filename)
        {
            string path_url = "www.poptropica.com/" + filename + ".xml";
            if (File.Exists(path_url))
                return File.ReadAllBytes(path_url);
            return null;
        }*/
    }
}
