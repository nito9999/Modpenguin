using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
namespace Modtropica_server.modtropica.world
{
    internal class pop_server_world
    {
        public pop_server_world()
        {
            try
            {
                Console.WriteLine("[POP_server.cs] has started.");
                new Thread(new ThreadStart(this.StartListen)).Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An Exception Occurred while Listening :" + ex.ToString());
            }
        }

        private void StartListen()
        {
            try
            {
                this.listener.Prefixes.Add($"http://localhost:{port_pop_server}/");
                this.listener.Start();
                Console.WriteLine("[POP_server.cs] is listening.");

                while (true)
                {
                    HttpListenerContext context = this.listener.GetContext();
                    ThreadPool.QueueUserWorkItem(o => HandleRequest(context));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                File.WriteAllText("crashdump_world.txt", Convert.ToString(ex));
                this.listener.Close();
                new POP_server();
            }
        }

        private void HandleRequest(HttpListenerContext context)
        {
            try
            {
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;
                List<byte> list = new List<byte>();
                string Url = request.RawUrl;
                byte[] bytes = new byte[1];
                byte[] raw_data_bytes = new byte[1];
                bool raw_data = false;

                Console.WriteLine("POP_api Requested: " + Url);

                string s = "";

                string rawurl = Url;

                if (Url.StartsWith("/poptropica/api/get_client.php"))
                {
                    s = "127.0.0.1";
                }
                

                string text;
                byte[] array;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    context.Request.InputStream.CopyTo(memoryStream);
                    array = memoryStream.ToArray();
                    text = Encoding.ASCII.GetString(array);
                }

                if (text.Length > 0xffff)
                {
                    Console.WriteLine("POP_api Data: unviewable");
                }
                else
                {
                    Console.WriteLine("POP_api Data: " + text);
                }
                string temp_url1 = Url.Split('?')[0];

                string path_url = Path.Combine(base_path, temp_url1);
                Console.WriteLine("POP_api Data: " + path_url + " Exist " + File.Exists(path_url));

            send_data:
                if (s.Length > 400)
                {
                    Console.WriteLine("POP_api Response: " + s.Length);
                }
                else
                {
                    Console.WriteLine("POP_api Response: " + s);
                }

                if (raw_data)
                {
                    bytes = raw_data_bytes;
                }
                else
                {
                    bytes = Encoding.UTF8.GetBytes(s);
                }

                response.ContentLength64 = bytes.Length;
                Console.WriteLine("POP_api Response size: " + bytes.Length);

                using (Stream outputStream = response.OutputStream)
                {
                    outputStream.Write(bytes, 0, bytes.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error handling request: " + ex.ToString());
            }
        }



        public static string base_pop_path = $"{base_path}/www.poptropica.com";
        public static string base_quantserve_path = $"{base_path}/flash.quantserve.com";
        public static string base_path = "game_data/pop_data/content/";
        public static string mod_path = "Mod_data/";

        public static int port_pop_server = 20198;

        public static string BlankResponse = "";
        public static string BracketResponse = "[]";

        private HttpListener listener = new HttpListener();
    }
}
