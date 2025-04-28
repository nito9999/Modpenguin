using Modtropica_server.poptropica;
using Modtropica_server.poptropica_php_emu;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using File = Modtropica_server.modtropica.core.file_system.File;
using Directory = Modtropica_server.modtropica.core.file_system.Directory;
using File_real = System.IO.File;
using dir_real = System.IO.Directory;


namespace Modtropica_server
{
    public class POP_server
    {
        public POP_server()
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
                this.listener.Prefixes.Add($"http://127.0.0.1:{port_pop_server}/");
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
                File_real.WriteAllText("crashdump.txt", Convert.ToString(ex));
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
                bool raw_data = true;

                Console.WriteLine("POP_api Requested: " + Url);
                
                string s = "";

                if (Url.StartsWith("http://detectportal.firefox.com/"))
                {
                    s = "success";
                    raw_data = false;
                    goto send_data;
                }

                string rawurl = Url;

                if (Url.StartsWith("http://"))
                {
                    Url = Url.Substring("http://".Length);
                }
                else if (Url.StartsWith("https://"))
                {
                    Url = Url.Substring("https://".Length);
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

                if (temp_url1.StartsWith("www.poptropica.com/haxe/") && !(temp_url1 == "www.poptropica.com/haxe/") && !File_real.Exists(path_url))
                {
                    Console.WriteLine("POP_api Data: " + path_url + " haxe build ");

                    string cmg_pop_url = temp_url1.Substring("www.poptropica.com/haxe/".Length);
                    cmg_pop_url = "https://www.poptropica.com/cmg/play/" + cmg_pop_url;
                    dir_real.CreateDirectory(Path.GetDirectoryName(path_url));
                    byte[] temp_file = new WebClient().DownloadData(cmg_pop_url);
                    File_real.WriteAllBytes(path_url, temp_file);
                } // for haxe build of poptropica

                if (File.Exists(temp_url1) || Directory.Exists(temp_url1))
                {
                    if (Path.HasExtension(temp_url1))
                    {
                        raw_data_bytes = File.ReadAllBytes(temp_url1);
                        string str = GetFileExtensionFromUrl(rawurl);
                        Console.WriteLine($"{str}");
                        switch (str)
                        {
                            case ".swf":
                                response.AppendHeader("Content-Type", "application/octet-stream");
                                response.AppendHeader("Content-Type", "application/x-shockwave-flash");
                                response.ContentType = "application/x-shockwave-flash";
                                break;
                            case ".gif":
                                response.AppendHeader("Content-Type", "image/gif");
                                break;
                            //.css
                            case ".xml":
                                response.AppendHeader("Content-Type", "application/xml");
                                break;
                            case ".css":
                                response.AppendHeader("Content-Type", ".css");
                                break;
                            case ".png":
                                response.AppendHeader("Content-Type", "image/png");
                                break;
                            case ".php":
                                Console.WriteLine($"POP_api url: {Url}");

                                string temp = "";
                                if(Url.Contains('?'))
                                {
                                    temp = Url.Split('?')[1];
                                }
                                string temp_url = Url.Split('?')[0];
                                Console.WriteLine($"POP_api url: {temp_url} with {temp}");
                                if (temp_url.Contains("base.php"))
                                {
                                    bool flag = context.Request.HttpMethod == "POST";
                                    NameValueCollection reqObj = context.Request.HttpMethod == "POST"
                                        ? ConvertFormToNameValueCollection(request)
                                        : context.Request.QueryString;
                                    
                                    s = as2_base_php.Base_php_gen(reqObj, text, flag);
                                    raw_data = false;
                                }
                                //Island_Names
                                //get_island_names.php
                                //Monitor.php
                                else if (temp_url.Contains("get_island_names.php"))
                                {
                                    s = "answer=ok&json=" + Uri.EscapeDataString(JsonConvert.SerializeObject(island_data.Island_Names));
                                    raw_data = false;
                                }
                                else if (temp_url.Contains("brain/track.php"))
                                {
                                    NameValueCollection reqObj = context.Request.QueryString;

                                    foreach (string item in reqObj.AllKeys)
                                    {
                                        Console.WriteLine($"key: {item} value: {reqObj.Get(item)}");
                                    }
                                    
                                    s = "";
                                    raw_data = false;
                                }
                                else if (temp_url.Contains("Monitor.php"))
                                {
                                    s = "";
                                    raw_data = false;
                                }
                                /*
                                
                                else if (temp_url.Contains("getPrefix.php"))
                                {
                                    s = "http://www.poptropica.com";
                                    raw_data = false;
                                }
                                */
                                else if (temp_url.Contains("getPrefix.php"))
                                {
                                    s = "http://www.poptropica.com";
                                    raw_data = false;
                                }
                                break;
                            default:
                                response.ContentType = "text/html";
                                break;
                        }
                    }
                    else if (File.Exists(Path.Combine(Url, "index.html")))
                    {
                        s = File.ReadAllText(Path.Combine(Url, "index.html"));
                        response.ContentType = "text/html";
                        raw_data = false;
                    }
                    else
                    {
                        raw_data_bytes = File.ReadAllBytes(Path.Combine(base_path, Url));
                    }
                }
                else
                {
                    string temp = "";
                    if (Url.Contains('?'))
                    {
                        temp = Url.Split('?')[1];
                    }
                    string temp_url = Url.Split('?')[0];
                    Console.WriteLine($"POP_api url: {temp_url} with {temp}");
                    if (temp_url.Contains("base.php"))
                    {
                        NameValueCollection reqObj = context.Request.HttpMethod == "POST"
                            ? ConvertFormToNameValueCollection(request)
                            : context.Request.QueryString;
                        s = as2_base_php.Base_php_gen(reqObj, temp);
                        raw_data = false;
                    }
                    else if (temp_url.Contains("get_island_names.php"))
                    {
                        s = "answer=ok&json=" + Uri.EscapeDataString(JsonConvert.SerializeObject(island_data.Island_Names));
                        raw_data = false;
                    }
                    else if (temp_url.Contains("brain/track.php"))
                    {
                        NameValueCollection reqObj = context.Request.QueryString;

                        foreach (string item in reqObj.AllKeys)
                        {
                            Console.WriteLine($"key: {item} value: {reqObj.Get(item)}");
                        }

                        s = "";
                        raw_data = false;
                    }
                    else if (temp_url.Contains("Monitor.php"))
                    {
                        s = "";
                        raw_data = false;
                    }
                    else
                    {
                        try
                        {

                        }
                        catch 
                        {

                        }
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                    }
                }
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

                string eTag = GenerateETag(bytes);
                response.AppendHeader("ETag", eTag);

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

        public static string GenerateETag(byte[] data)
        {
            using (var md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(data);
                return Convert.ToBase64String(hash);
            }
        }

        public static string GetFileExtensionFromUrl(string url)
        {
            Uri uri = new Uri(url);
            string path = uri.AbsolutePath;
            return Path.GetExtension(path);
        }

        public static NameValueCollection ConvertFormToNameValueCollection(HttpListenerRequest request)
        {
            NameValueCollection collection = new NameValueCollection();

            if (request.HasEntityBody)
            {
                using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
                {
                    string formData = reader.ReadToEnd();
                    string[] pairs = formData.Split('&');

                    foreach (string pair in pairs)
                    {
                        string[] keyValue = pair.Split('=');
                        if (keyValue.Length == 2)
                        {
                            string key = Uri.UnescapeDataString(keyValue[0]);
                            string value = Uri.UnescapeDataString(keyValue[1]);
                            collection.Add(key, value);
                        }
                    }
                }
            }

            return collection;
        }

        public static string base_pop_path = $"{base_path}/www.poptropica.com";
        public static string base_quantserve_path = $"{base_path}/flash.quantserve.com";
        public static string base_path = "game_data/pop_data/content/";
        public static string mod_path = "Mod_data/";

        public static int port_pop_server = 22500;

        public static string BlankResponse = "";
        public static string BracketResponse = "[]";

        private HttpListener listener = new HttpListener();
    }
}
