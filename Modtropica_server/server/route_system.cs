
using System.Net;
using System.Web;
using Newtonsoft.Json;

namespace Modtropica_server.server
{
    class route_system
    {
        public static object ParseJsonBody(string body, Type targetType)
        {
            return JsonConvert.DeserializeObject(body, targetType); // Deserialize into the specified type
        }

        public static string ParseRequestBody(HttpListenerRequest request)
        {
            using var reader = new StreamReader(request.InputStream, request.ContentEncoding);
            return reader.ReadToEnd(); // Reads raw data from the body
        }

        public static Dictionary<string, string> ParseFormData(string body)
        {
            var formData = new Dictionary<string, string>();
            var parsedQuery = HttpUtility.ParseQueryString(body);
            foreach (var key in parsedQuery.AllKeys)
            {
                formData[key] = parsedQuery[key];
            }
            return formData;
        }
        public enum url_level
        {
            high = 300,
            medium = 200,
            low = 100,
            none = 0
        }

        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
        public class RouteAttribute : Attribute
        {
            public string Path { get; }
            public string ContentType { get; } // Optional second parameter
            public bool IsRawRegex { get; } // Flag for raw regex mode
            public url_level Priority { get; } // php shit
            public RouteAttribute(string path, string contentType = "application/xml", bool isRawRegex = false, url_level priority = url_level.low)
            {
                Path = path;
                ContentType = contentType;
                IsRawRegex = isRawRegex;
                Priority = priority;
            }
        }
    }
}
