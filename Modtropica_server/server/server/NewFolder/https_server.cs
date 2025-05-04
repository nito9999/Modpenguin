using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;


namespace Modtropica_server.server.server.NewFolder
{
    class https_server
    {
        public static void https_Main()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 22501);
            listener.Start();
            Console.WriteLine("https Proxy server running on port 22501...");
            AppContext.SetSwitch("System.Net.Security.SslStream", true);

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                HandleClient(client);
            }
        }

        static void HandleClient(TcpClient client)
        {
            using (NetworkStream networkStream = client.GetStream())
            {
                if (!networkStream.CanRead || !networkStream.CanWrite)
                {
                    Console.WriteLine("Invalid stream.");
                    return;
                }

                // Load self-signed certificate
                X509Certificate2 cert = new X509Certificate2("mycert.pfx", "");

                using (SslStream sslStream = new SslStream(networkStream))
                {
                    sslStream.AuthenticateAsServer(cert, false, System.Security.Authentication.SslProtocols.Tls12, true);

                    Console.WriteLine("Secure HTTPS connection established.");
                }
            }
        }

        static void HandleHttpsTunnel(NetworkStream clientStream, string targetHost)
        {
            string[] hostParts = targetHost.Split(':');
            string host = hostParts[0];
            int port = hostParts.Length > 1 ? int.Parse(hostParts[1]) : 443;

            using (TcpClient targetClient = new TcpClient(host, port))
            using (NetworkStream targetStream = targetClient.GetStream())
            {
                clientStream.CopyTo(targetStream);
                targetStream.CopyTo(clientStream);
            }
        }


        static string GetDummyResponse(string url)
        {
            if (url.Contains("config.json"))
                return JsonConvert.SerializeObject(new config_data
                {
                    version = new
                    {
                        client = "1.0.5",
                        server = "2.0.0"
                    },
                    maintenance = false,
                    quests = new config_quests_data
                    {
                        useScroll = false,
                        active = new List<config_quest_data>
                        {
                            new config_quest_data
                            {
                                name = "Bloody Shrink Ray Island",
                                desc = "Play through a creepier version of Shrink Ray Island in this community-created quest.",
                                code = "shrinkScare",
                                group = "replacedIsland",
                                saves = true,
                                prize =  new {
                                    type= "noPrize",
                                    data = (object)null
                                }
                            }
                        },
                        inactive = new List<config_quest_data> { }
                    },
                    styles = (object)null,
                    specialMenu = (object)null
                });
            else if (url.Contains("hello"))
                return "{\"message\": \"Hello from the proxy!\"}";
            else
                return "{\"message\": \"Default response.\"}";
        }
        public class config_data
        {
            public object version;
            public bool maintenance;
            public config_quests_data quests;
            public object styles;
            public object specialMenu;
        }
        public class config_quests_data
        {
            public bool useScroll;
            public List<config_quest_data> active;
            public List<config_quest_data> inactive;

        }
        public class config_quest_data
        {
            public string name;
            public string desc;
            public string code;
            public string group;
            public bool saves;
            public object prize;

        }
    }
}
