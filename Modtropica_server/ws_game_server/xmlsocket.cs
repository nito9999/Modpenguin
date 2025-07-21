using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModPenguin_server.ws_game_server
{
    internal class xmlsocket
    {
        private Socket? _listener;
        private bool _running;

        public async Task StartAsync(int port)
        {
            _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _listener.Bind(new IPEndPoint(IPAddress.Any, port));
            _listener.Listen(100);
            _running = true;
            Console.WriteLine($"WebSocket server started on port {port}");

            while (_running)
            {
                var client = await _listener.AcceptAsync();
                _ = HandleClientAsync(client);
            }
        }

        public void Stop()
        {
            _running = false;
            _listener?.Close();
        }

        private async Task HandleClientAsync(Socket client)
        {

            Console.WriteLine($"WebSocket server got a new client {client}");
            var buffer = new byte[4096];
            int read = 0;// await client.ReceiveAsync(buffer, SocketFlags.None);
            //var request = Encoding.ASCII.GetString(buffer, 0, read);
           // Console.WriteLine($"WebSocket server client {request}");

            // Send handshake response
            //var response = "<cross-domain-policy>\r\n\t<allow-access-from domain=\"*\" to-ports=\"2059\" secure=\"false\"/>\r\n</cross-domain-policy>";
            //await SendTextAsync(client, response);
            //response = "%xt%h%0%";
            //await SendTextAsync(client, response);
            // Echo loop
            try
            {
                while (client.Connected)
                {
                    read = await client.ReceiveAsync(buffer, SocketFlags.None);
                    //Console.WriteLine($"Received: {read}");

                    if (read == 0) continue;
                    //client.BeginSend(buffer, 0, read, SocketFlags.None, null, null);
                    var message = Encoding.UTF8.GetString(buffer, 0, read);
                    Console.WriteLine($"Received: {message}");
                    if (message == "<policy-file-request/>\0")
                    {
                        string response = "<?xml version=\"1.0\"?><!DOCTYPE cross-domain-policy SYSTEM \"http://www.adobe.com/xml/dtds/cross-domain-policy.dtd\"><cross-domain-policy><site-control permitted-cross-domain-policies=\"master-only\"/><allow-access-from domain=\"*\" to-ports=\"*\"/></cross-domain-policy>\0";
                        await SendTextAsync(client, response);
                    }
                    else if (message.Contains("<msg t='sys'><body action='verChk'"))
                    {
                        string response = "<msg t='sys'><body action='apiOK' r='0'></body></msg>\0";
                        await SendTextAsync(client, response);
                    }
                    else if (message.Contains("<msg t='sys'><body action='login'"))
                    {
                        string response = "%xt%l%-1%\0";
                        await SendTextAsync(client, response);
                    }
                    else if (message.Contains("%xt%s%js%-1%0%%%"))
                    {
                        string response = "%xt%jr%35%800%101\0";
                        await SendTextAsync(client, response);
                    }
                    else
                    {
                        string response = "%xt%e%0%402%\0";

                        // Echo back
                        await SendTextAsync(client, response);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"xml socket error: {e}");
            }
            Console.WriteLine($"WebSocket server a client closed it connection {client}");

            client.Close();
        }

        private static async Task SendTextAsync(Socket client, string message)
        {
            Console.WriteLine($"sending: {message}");

            var payload = Encoding.UTF8.GetBytes(message /*+ "\x00"*/);
            
            await client.SendAsync(payload, SocketFlags.None);
        }
    }
}