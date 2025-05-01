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

        public static string base_pop_path = $"{base_path}/www.poptropica.com";
        public static string base_quantserve_path = $"{base_path}/flash.quantserve.com";
        public static string base_path = "game_data/pop_data/content/";
        public static string mod_path = "Mod_data/";

        [Route(@"^www.poptropica.com/(?<filename>.+)\.swf$", "application/x-shockwave-flash", true)]
        public static byte[] file_path_swf(string filename)
        {
            string path_url = "www.poptropica.com/" + filename + ".swf";
            if (File.Exists(path_url))
                return File.ReadAllBytes(path_url);
            return null;

            try
            {
                byte[] bytes = new WebClient().DownloadData("https://web.archive.org/web/20120104233009/https://" + path_url);
                
                File_real.WriteAllBytes(Path.Combine(POP_server.base_path, path_url), bytes);

                if (File.Exists(path_url))
                    return File.ReadAllBytes(path_url);
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }
        /*
         http://www.poptropica.com/brain/track.php
         
         */

        [Route("www.poptropica.com/brain/track.php", "application/text")]
        public static string brain_track()
        {
            return "";
        }

        [Route("www.poptropica.com/getPrefix.php", "application/text")]
        public static string getPrefix_php()
        {
            return "http://www.poptropica.com";
        }

        [Route(@"^www.poptropica.com/(?<filename>.+)\.mp3$", "audio/mpeg", true)]
        public static byte[] file_path_mp3(string filename)
        {
            string path_url = "www.poptropica.com/" + filename + ".mp3";
            if (File.Exists(path_url))
                return File.ReadAllBytes(path_url);
            return null;

            try
            {
                byte[] bytes = new WebClient().DownloadData("https://web.archive.org/web/20120104233009/https://" + path_url);

                File_real.WriteAllBytes(Path.Combine(POP_server.base_path, path_url), bytes);
                if (File.Exists(path_url))
                    return File.ReadAllBytes(path_url);
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }

        [Route(@"^www.poptropica.com/(?<filename>.+)\.txt$", "application/text", true)]
        public static byte[] file_path_txt(string filename)
        {
            string path_url = "www.poptropica.com/" + filename + ".txt";
            if (File.Exists(path_url))
                return File.ReadAllBytes(path_url);
            return null;

            try
            {
                string bytes = new WebClient().DownloadString("https://web.archive.org/web/20120104233009/https://" + path_url);
                if (bytes.Contains(" type=\"text/javascript\">") || bytes.IsNullOrEmpty())
                {
                    bytes = new WebClient().DownloadString(path_url.Replace("www.poptropica.com/game/", "https://www.poptropica.com/cmg/play/resources/"));
                }
                File_real.WriteAllText(POP_server.base_pop_path + filename + ".txt", bytes);
                if (File.Exists(path_url))
                    return File.ReadAllBytes(path_url);
            }
            catch
            {
                string bytes = new WebClient().DownloadString(path_url.Replace("www.poptropica.com/game/", "https://www.poptropica.com/cmg/play/resources/"));

                File_real.WriteAllText(POP_server.base_pop_path + filename + ".txt", bytes);
                if (File.Exists(path_url))
                    return File.ReadAllBytes(path_url);
            }
            return null;
        }

        [Route(@"^www.poptropica.com/(?<filename>.+)\.xml$", "application/xml", true)]
        public static byte[] file_path_xml(string filename)
        {
            string path_url = "www.poptropica.com/" + filename + ".xml";
            if (File.Exists(path_url))
                return File.ReadAllBytes(path_url);
            return null;

            try
            {

                string bytes = Get_String("https://web.archive.org/web/20120104233009/https://" + "www.poptropica.com" + filename + ".xml");
                if (bytes.Contains(" type=\"text/javascript\">") || bytes.IsNullOrEmpty())
                {
                    bytes = Get_String("https://www.poptropica.com/cmg/play/resources/" + filename.Substring("game/".Length));
                }
                File_real.WriteAllText(POP_server.base_pop_path + filename + ".xml", bytes);
                if (File.Exists(path_url))
                    return File.ReadAllBytes(path_url);
            }
            catch
            {
                string bytes = Get_String("https://www.poptropica.com/cmg/play/resources/" + filename.Substring("game/".Length));
                
                File_real.WriteAllText(POP_server.base_pop_path + filename + ".xml", bytes);
                if (File.Exists(path_url))
                    return File.ReadAllBytes(path_url);
            }
            return null;
        }

        public static string Get_String(string url)
        {
            string data = string.Empty;
            HttpClient client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Version = new Version(2, 0),
            };
            client.DefaultRequestVersion = HttpVersion.Version20;
            client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/json,application/xml;q=0.9,*/*;q=0.8");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.5");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "deflate");
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:137.0) Gecko/20100101 Firefox/137.0");
            client.DefaultRequestHeaders.Add("Origin", "");
            client.DefaultRequestHeaders.Add("Referer", "");
            var x = client.SendAsync(req).GetAwaiter().GetResult();
            x.EnsureSuccessStatusCode();
            StreamReader reader = new StreamReader(x.Content.ReadAsStream());
            data = reader.ReadToEnd();
            return data;
        }

    }
}
