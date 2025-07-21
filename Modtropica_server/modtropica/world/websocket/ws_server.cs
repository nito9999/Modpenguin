using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Newtonsoft.Json;
using static Modtropica_server.modtropica.world.websocket.ws_server.WebSocketHTTP_new;
using WebSocketSharp.Server;
using WebSocketSharp;

namespace Modtropica_server.modtropica.world.websocket
{
    internal class ws_server
    {
        internal class WebSocketHTTP_new
        {
            public WebSocketHTTP_new()
            {
                WebSocketServer webSocketServer = new WebSocketServer(string.Format("ws://127.0.0.1:2057", Array.Empty<object>()));
                webSocketServer.AddWebSocketService<NotificationV2>("/api/notification/v2");
                webSocketServer.AddWebSocketService<NotificationV2>("/hub/v1");
                webSocketServer.AddWebSocketService<NotificationV2>("/hub/pop/");
                webSocketServer.AddWebSocketService<NotificationV2>("/hub/pop");
                webSocketServer.Start();
                Console.WriteLine("[WebSocket.cs] has started.");
                Console.WriteLine("[WebSocket.cs] is listening.");
            }

            public class NotificationV2 : WebSocketBehavior
            {
                protected override void OnOpen()
                {
                    base.OnOpen();
                    Console.WriteLine("WebSocket.cs server connect.");
                    base.Send(JsonConvert.SerializeObject(new 
                    {
                        socketId = 0,
                        functionName = "onOpen"
                    }));
                }
                protected override void OnMessage(MessageEventArgs e)
                {
                    Console.WriteLine("WebSocket.cs called for.");
                    base.Send(ProcessRequest_data(e.Data));
                }
            }
            public static string ProcessRequest_data(string jsonData)
            {
                Console.WriteLine("WebSocket.cs data " + jsonData);

                return jsonData;
            }
            /*
            public WebSocketHTTP_new()
            {
                try
                {
                    Console.WriteLine("{ws} server started!");
                    WebSocketHTTP_new.listen.Start();
                }
                catch (Exception)
                {
                }
            }*/
            public static void ADListen()
            {
                server.Prefixes.Add("ws://localhost:20199/");
                WebSocketHTTP_new.server.Start();

                for (; ; )
                {
                    Console.WriteLine("{ws} listening");
                    HttpListenerContext context = server.GetContext();
                    string rawUrl = context.Request.RawUrl;
                    string text = "[]";
                    Console.WriteLine("{ws} requested! " + rawUrl + ".");
                    string text2;
                    using (StreamReader streamReader = new StreamReader(context.Request.InputStream))
                    {
                        text2 = streamReader.ReadToEnd();
                    }
                    if (rawUrl.StartsWith("/negotiate"))
                    {
                        text = "{\"negotiateVersion\":0,\"connectionId\":\"notif\",\"availableTransports\":[{\"transport\":\"WebSockets\",\"transferFormats\":[\"Text\", \"Binary\"]}]}";
                        goto IL_C9;
                    }
                    if (!context.Request.IsWebSocketRequest)
                    {
                        goto IL_C9;
                    }
                    Console.WriteLine("{ws} requested!");
                    Console.WriteLine(text2);
                    //ProcessRequest(context);
                IL_BA:
                    Console.WriteLine("{ws} connected!");
                    continue;
                IL_C9:
                    byte[] bytes = Encoding.UTF8.GetBytes(text);
                    context.Response.ContentLength64 = (long)bytes.Length;
                    context.Response.OutputStream.Write(bytes, 0, bytes.Length);
                    goto IL_BA;
                }
            }

            /*
            private static async void ProcessRequest(HttpListenerContext ctx)
            {
                HttpListenerWebSocketContext httpListenerWebSocketContext = await ctx.AcceptWebSocketAsync(null);
                CancellationTokenSource src = new CancellationTokenSource();
                ws = httpListenerWebSocketContext.WebSocket;
                while (ws.State == WebSocketState.Open)
                {
                    string temp1 = "";
                    string temp2 = "";
                    string temp3 = "";

                    temp1 = EncodeNonAsciiCharacters(temp1, '"');
                    if (temp1 == null)
                    {
                        temp1 = "{}";
                    }
                    //temp1.Replace("\"", "\\" + "u0022");
                    Console.WriteLine(temp1);
                    byte[] received = new byte[2048];
                    int offset = 0;
                    for (; ; )
                    {
                        try
                        {
                            ArraySegment<byte> arraySegment = new ArraySegment<byte>(received, offset, received.Length);
                            WebSocketReceiveResult webSocketReceiveResult = await ws.ReceiveAsync(arraySegment, src.Token);
                            offset += webSocketReceiveResult.Count;
                            if (!webSocketReceiveResult.EndOfMessage)
                            {
                                continue;
                            }
                        }
                        catch
                        {
                        }
                        break;
                    }
                    if (offset != 0)
                    {
                        string @string = Encoding.ASCII.GetString(received);
                        temp2 = JsonConvert.SerializeObject(new
                        {
                            Id = "PresenceUpdate",
                            Msg = temp1
                        });
                        byte[] array;
                        temp3 = JsonConvert.SerializeObject(new SockSignalR
                        {
                            type = MessageTypes.Invocation,
                            result = "200 OK",
                            nonblocking = true,
                            target = "Notification",
                            arguments = new object[]
                            {
                                JsonConvert.SerializeObject(new Respond
                                {
                                    Id = "",
                                    Msg = ""
                                })
                            },
                            error = "",
                            invocationId = "1",
                            item = ""
                        });
                        if (@string.Contains("version"))
                        {
                            Console.WriteLine("{ws} game request json handshake!");
                            array = Encoding.ASCII.GetBytes("{}\u001e");
                        }
                        else if (@string.Contains("SubscribeToPlayers"))
                        {
                            Console.WriteLine("{ws} game request presence!");
                            temp3 = JsonConvert.SerializeObject(new SockSignalR
                            {
                                type = MessageTypes.Invocation,
                                result = "200 OK",
                                nonblocking = true,
                                target = "Notification",
                                arguments = new object[] {
                                    JsonConvert.SerializeObject(new Respond
                                    {
                                        Id = "",
                                        Msg = ""
                                    })
                                },
                                error = null,
                                invocationId = null,
                                item = null
                            });
                        }
                        Console.WriteLine(temp3 + "\u001e");

                        array = Encoding.ASCII.GetBytes(temp3 + "\u001e");

                        await ws.SendAsync(new ArraySegment<byte>(array, 0, array.Length), WebSocketMessageType.Text, true, src.Token);
                        received = null;
                    }
                    received = null;
                }
            }
            public static WebSocket ws;
            */


            public static async void SendRequest(string json)
            {
                CancellationTokenSource src = new CancellationTokenSource();
                string temp3 = JsonConvert.SerializeObject(new SockSignalR
                {
                    type = MessageTypes.Invocation,
                    result = "200 OK",
                    nonblocking = true,
                    target = "Notification",
                    arguments = new object[] { json },
                    error = null,
                    invocationId = null,
                    item = null
                });

                byte[] array = Encoding.ASCII.GetBytes(temp3 + "\u001e");

                //await ws.SendAsync(new ArraySegment<byte>(array, 0, array.Length), WebSocketMessageType.Text, true, src.Token);


            }

            static string EncodeNonAsciiCharacters(string value, char value1)
            {
                StringBuilder sb = new StringBuilder();
                foreach (char c in value)
                {
                    if (c > 127 || value1 == c)
                    {
                        // This character is too big for ASCII
                        string encodedValue = "\\u" + ((int)c).ToString("x4");
                        sb.Append(encodedValue);
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }
                return sb.ToString();
            }

            public class Respond
            {
                public object Id { get; set; }
                public object Msg { get; set; }
            }

            public static HttpListener server = new HttpListener();
            public static Thread listen = new Thread(new ThreadStart(ADListen));

            public class SockSignalR
            {
                public MessageTypes type;
                public string invocationId;
                public bool nonblocking;
                public string target;
                public object[] arguments;
                public object item;
                public object result;
                public string error;
            }

            public enum MessageTypes
            {
                Handshake,
                Invocation,
                StreamItem,
                Completion,
                StreamInvocation,
                CancelInvocation,
                Ping,
                Close
            }
        }
    }
}
