using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

using System.Linq;
using System.Net;

using Newtonsoft.Json;
using System.Threading;

using static Modtropica_server.server.route_system;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Security.Cryptography;
using System.Collections;
using System.Net.Mime;
using System.Security.Policy;

namespace Modtropica_server.server.server
{
    public class server_Routing
    {
        public static server_Routing POP_Server;
        public static int port_pop_server = 22500;

        public server_Routing()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://127.0.0.1:{port_pop_server}/");
            
            if (POP_server.admin)
            {
                _listener.Prefixes.Add($"http://{POP_server.GetMyHost()}:{port_pop_server}/");
                _listener.Prefixes.Add($"http://*:{port_pop_server}/");
                Console.WriteLine($"running on {POP_server.GetMyHost()}:{port_pop_server}");
            }
            POP_Server = this;

            RegisterRoutes();

            new Thread(new ThreadStart(this.Start)).Start();

            Running = true;
        }

        public static bool Running = false;

        private readonly HttpListener _listener;

        public void RefreshApplicationState(object sender, FileSystemEventArgs e)
        {
            RegisterRoutes(true);
        }

        public static void reloadRegisterRoutes()
        {
            if (POP_Server == null)
                return;
            POP_Server.RegisterRoutes(true);
        }

        #region server_handling

        private readonly List<(Regex RoutePattern, MethodInfo Handler, ParameterInfo[] Params, string ContentType, url_level Priority)> _routeHandlers = new();

        private void RegisterRoutes(bool reload = false)
        {
            if (reload)
            {
                Console.WriteLine("POP_Server [DEBUG]: Reregistering all of the Route");
                _routeHandlers.Clear();
            }
            else
                Console.WriteLine("POP_Server: Registering Route");

            foreach (var method in Assembly.GetExecutingAssembly().GetTypes()
                    .SelectMany(t => t.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)))
            {
                var routeAttribute = method.GetCustomAttribute<RouteAttribute>();
                if (routeAttribute != null)
                {
                    var pattern = "";
                    if (routeAttribute.IsRawRegex)
                    {
                        // Use raw regex pattern directly
                        pattern = routeAttribute.Path;
                    }
                    else
                    {
                        pattern = "^" + Regex.Escape(routeAttribute.Path)
                            .Replace("\\*", ".*")      // Match any string for wildcards
                            .Replace("\\{", "(?<")     // Start named group
                            .Replace("}", ">[^/]+)")   // End named group
                            + "$";
                    }
                    var regex = new Regex(pattern, RegexOptions.Compiled);
                    var parameters = method.GetParameters();

                    // Store the route including ContentType metadata
                    _routeHandlers.Add((regex, method, parameters, routeAttribute.ContentType, routeAttribute.Priority));
                }
            }

        }

        public void Start()
        {
            _listener.Start();
            Console.WriteLine("POP_Server: Listening...");
            while (true)
            {
                //var context = _listener.GetContext();
                try
                {
                    HttpListenerContext context = this._listener.GetContext();
                    ThreadPool.QueueUserWorkItem(o => ProcessRequest(context));
                    //ProcessRequest(context);
                }
                catch
                {
                }
            }
        }

        private void ProcessRequest(HttpListenerContext context)
        {
            var path = context.Request.RawUrl.Split('?')[0];
            var response = context.Response;
            var query = context.Request.QueryString;

            string rawurl = path;

            if (path.StartsWith("http://"))
            {
                path = path.Substring("http://".Length);
            }
            else if (path.StartsWith("https://"))
            {
                path = path.Substring("https://".Length);
            }

            string out_contentType = "application/json"; // Default Content-Type

            object response_data = null;

            string contentType = context.Request.ContentType;

            if (string.IsNullOrEmpty(contentType))
                contentType = "none";
            Console.WriteLine($"POP_Server: Start of request.");

            Console.WriteLine($"POP_Server: API Requested: {path}");
            Console.WriteLine($"POP_Server: Content-Type: {contentType}");
            Console.WriteLine($"POP_Server: Request-Method-Type: {context.Request.HttpMethod}");

            var matchedRoutes = _routeHandlers.Where(r => r.RoutePattern.IsMatch(path))
                           .OrderByDescending(r => r.Priority) // Process highest priority first
                           .ToList();

            foreach (var (regex, method, parameters, routeContentType, routePriority) in _routeHandlers)
            {
                var match = regex.Match(path);
                if (match.Success)
                {
                    out_contentType = routeContentType;

                    // Prepare arguments for handler
                    var args = new object[parameters.Length];

                    string body = "";

                    if (context.Request.ContentType == "application/json" || context.Request.ContentType == "application/x-www-form-urlencoded")
                    {
                        body = ParseRequestBody(context.Request);
                        Console.WriteLine($"POP_Server: API Data: {body}");
                    }

                    for (int i = 0; i < parameters.Length; i++)
                    {
                        var paramType = parameters[i].ParameterType;

                        if (paramType == typeof(HttpListenerContext))
                        {
                            args[i] = context; // Pass the context directly
                        }
                        else if (paramType == typeof(Dictionary<string, string>) && context.Request.ContentType == "application/x-www-form-urlencoded")
                        {
                            // Parse form data
                            body = ParseRequestBody(context.Request);
                            args[i] = ParseFormData(body);
                        }
                        else if (context.Request.ContentType == "application/x-www-form-urlencoded")
                        {
                            var formData = ParseFormData(body);
                            if (formData.TryGetValue(parameters[i].Name, out var value))
                            {
                                args[i] = value; // Form data parameter
                            }
                        }
                        else if (context.Request.ContentType == "application/json")
                        {
                            // Parse JSON body and deserialize to expected type
                            args[i] = ParseJsonBody(body, paramType);
                        }
                        else if (paramType == typeof(string) && query != null && query[parameters[i].Name] != null)
                        {
                            // Retrieve query parameter value
                            args[i] = query[parameters[i].Name];
                        }
                        else if ((
                                paramType == typeof(int) ||
                                paramType == typeof(uint) ||
                                paramType == typeof(long) ||
                                paramType == typeof(ulong) ||
                                paramType == typeof(string)) && context.Request.ContentType == "application/x-www-form-urlencoded")
                        {
                            // Extract parameter value from form data
                            var formData = ParseFormData(body);
                            var paramName = parameters[i].Name;
                            args[i] = Convert.ChangeType(formData[paramName], paramType);
                        }
                        else if (
                                paramType == typeof(int) ||
                                paramType == typeof(uint) ||
                                paramType == typeof(long) ||
                                paramType == typeof(ulong) ||
                                paramType == typeof(string))
                        {
                            // Extract URL parameters
                            var paramName = parameters[i].Name;

                            if (match.Groups[paramName]?.Success == true)
                            {
                                args[i] = Convert.ChangeType(match.Groups[paramName].Value, paramType);
                            }
                        }
                        else
                        {
                            throw new InvalidOperationException($"Unsupported parameter type: {paramType}");
                        }
                    }
                    try
                    {
                        // Invoke the handler
                        var result = method.Invoke(null, args);

                        // Handle void return types
                        if (method.ReturnType == typeof(void))
                        {

                            response.StatusCode = 200; // OK
                            response_data = "{ \"success\": \"true\" }";
                        
                        }
                        else if (method.ReturnType == typeof(bool))
                        {
                            if ((bool)result == true)
                            {
                                response.StatusCode = 200; // OK
                                response_data = "{ \"success\": \"true\" }";
                            }
                            else
                                response_data = null;

                        }
                        else
                        {
                            response_data = result;
                        }
                    }
                    catch
                    {
                        response_data = null;
                    }
                    break;
                }
            }

            if (response_data == null)
            {
                response_data = HandleNotFound(context);
                response.StatusCode = 404;
            }

            WriteResponse(response, response_data, out_contentType);
        }

        public static string GenerateETag(byte[] data)
        {
            using (var md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(data);
                return Convert.ToBase64String(hash);
            }
        }

        private static void WriteResponse(HttpListenerResponse response, object response_data, string contentType = "")
        {
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
            response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
            if (response_data is string responseString)
            {
                if (responseString.Length <= 0x1ff)
                {
                    Console.WriteLine($"POP_Server: API Response: " + responseString);
                }
                else
                {
                    Console.WriteLine($"POP_Server: API Response Length: " + responseString.Length);
                }
                var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);

                string eTag = GenerateETag(buffer);
                response.AppendHeader("ETag", eTag);
                if (string.IsNullOrEmpty(contentType))
                    response.ContentType = "application/json";
                else
                    response.ContentType = contentType;
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
            }
            else if (response_data is byte[] responseBytes)
            {
                Console.WriteLine($"POP_Server: API data Response Length: " + responseBytes.Length);

                if (string.IsNullOrEmpty(contentType))
                    response.ContentType = "application/octet-stream";
                else
                    response.ContentType = contentType;
                string eTag = GenerateETag(responseBytes);
                response.AppendHeader("ETag", eTag);

                response.ContentLength64 = responseBytes.Length;
                response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
            }
            else
            {
                throw new InvalidOperationException("Unsupported response data type.");
            }

            Console.WriteLine($"POP_Server: End of request.\n");

            response.OutputStream.Close();
        }

        private string HandleNotFound(HttpListenerContext context)
        {
            return "{\"Success\": false, \"Error\": \"404 URL Not Found: " + context.Request.Url + "\"}";
        }
        #endregion
    }
}
