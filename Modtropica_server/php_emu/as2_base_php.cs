using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Modtropica_server.poptropica_php_emu
{
    public class as2_base_php
    {

        public class as2_base
        {
            public string key;
            public string value;
        }
        public static List<as2_base> ParseQueryString(string queryString)
        {
            if (string.IsNullOrEmpty(queryString))
                return new List<as2_base>();
            List<as2_base> keyValuePairs = new List<as2_base>();
            string[] pairs = queryString.TrimStart('?').Split('&');

            foreach (string? pair in pairs)
            {
                string[] keyValue = pair.Split('=');
                string key = keyValue[0];
                string value = keyValue[1];
                keyValuePairs.Add(new as2_base()
                {
                    key = key,
                    value = value
                });
                
            }

            return keyValuePairs;
        }


        /// <summary>
        /// this gennrate a html code 
        /// </summary>
        /// <param name="HttpContext"></param>
        /// <returns></returns>
        public static string Base_php_gen(NameValueCollection reqObj, string url, bool hack_url = false)
        {
            List<as2_base> keyValuePairs = new List<as2_base>();
            if (hack_url)
            {
                keyValuePairs = ParseQueryString(url);
            }
            string GetParam(string name, string defaultValue)
            {
                return reqObj[name] != null ? Uri.EscapeDataString(reqObj[name]) : defaultValue;
            }
            string GetParam_form(string name, string defaultValue)
            {

                foreach (as2_base item in keyValuePairs)
                {
                    if (item.key == name)
                    {
                        if (!string.IsNullOrEmpty(item.value))
                        {
                            return item.value;
                        }
                    }
                }

                return defaultValue;
            }
            /*
            string scene = GetParam("room", "Home");
            string island = GetParam("island", "Home");
            string path = GetParam("startup_path", "gameplay");
            */
            string scene = "Home";
            string island = "Home";
            string path = "gameplay";
            if (!hack_url)
            {
                scene = GetParam("room", "Home");
                island = GetParam("island", "Home");
                path = GetParam("startup_path", "gameplay");
            }
            else
            {
                scene = GetParam_form("room", "Home");
                island = GetParam_form("island", "Home");
                path = GetParam_form("startup_path", "gameplay");
            }
            return Base_php_gen(scene, island, path);
        }
        public static string Base_php_gen(string scene = "Home", string island = "Home", string path = "gameplay")
        {
            Console.WriteLine($"scene: {scene} on island: {island}");

            const string SCENE_AS3 = "GlobalAS3Embassy";
            const string SCENE_AS3_START = "FlashpointStart"; // Not a real scene.
            const string SCENE_FP_RESTART = "FlashpointMiniquestRestart"; // Not a real scene.
            const string SCENE_COMMON_EARLY = "Arcade";

            string[] SPECIAL_COMMONS = { "Coconut", "Party", "Cinema", "News", "HairClub", "Airlines", "Saltys", "Crop", "BaguetteInn", "Billiards", "BrokenBarrel", "HotelInterior", "ClubInterior" };
            string[] SPECIAL_ADS = { "AdGroundH52", "AdGroundH42" }; // Why, Poptropica! These are scenes labeled as "ads" that AREN'T ads, and just use the rectangular screen format.

            const int STATE_SCENE = 0;
            const int STATE_COMMON = 1;
            const int STATE_AS3 = 2;
            const int STATE_AD = 3;
            const int STATE_RESTART = 4;

            int pageState;

            switch (scene)
            {
                case SCENE_AS3_START:
                    scene = $"{SCENE_AS3}&amp;amp;flashpointForceStart=1";
                    goto case SCENE_AS3;

                case SCENE_AS3:
                    pageState = STATE_AS3;
                    break;

                case SCENE_FP_RESTART:
                    pageState = STATE_RESTART;
                    break;

                case SCENE_COMMON_EARLY:
                    pageState = island == "Boardwalk" ? STATE_SCENE : STATE_COMMON;
                    break;

                default:
                    if (scene.StartsWith("Ad") && Array.IndexOf(SPECIAL_ADS, scene) == -1)
                    {
                        pageState = STATE_AD;
                        break;
                    }
                    if (!scene.Contains("Common") && Array.IndexOf(SPECIAL_COMMONS, scene) == -1)
                    {
                        pageState = STATE_SCENE;
                        break;
                    }
                    pageState = STATE_SCENE;
                    break;
            }

            string width;
            string height;
            string flashVars;
            string gameState = "";

            if (scene == "Home")
            {
                pageState = STATE_SCENE;
                scene = $"{SCENE_AS3}&amp;amp;flashpointForceStart=1";
                width = "1136";
                height = "673";
            }
            else if (scene.StartsWith("Ad"))
            {
                gameState = "return_user_advertisement_1";
                width = "776";
                height = "480";
            }
            else
            {
                gameState = "return_user_standard";
                width = "1136";
                height = "673";
            }

            flashVars = $"desc={scene}&amp;island={island}&amp;startup_path={path}&amp;state={gameState}";

            StringBuilder sb = new StringBuilder();

            sb.Append("<!doctype html>");
            sb.Append("<html lang=\"en\">");
            sb.Append("<head>");
            sb.Append("<meta charset=\"utf-8\">");
            sb.Append("<title>Poptropica</title>");
            sb.Append("<style>");
            sb.Append("body { background-color: #139ffd; }");
            sb.Append("embed {");
            sb.Append("background-color: #0099ff;");
            sb.Append("outline-width: 0;");
            sb.Append("position: absolute;");
            sb.Append($"left: calc(50vw - {width}px / 2);");
            sb.Append($"top: calc(50vh - {height}px / 2);");
            sb.Append("}");
            sb.Append("</style>");
            sb.Append("</head>");
            sb.Append("<body>");
            sb.Append("<embed src=\"");
            sb.Append($"{(pageState == STATE_SCENE ? "framework.swf" : pageState == STATE_AS3 ? "flashpoint/memStatus.swf" : pageState == STATE_RESTART ? "flashpoint/restartMiniquest.swf" : "flashpoint/adSkip.swf")}");
            sb.Append($"\" width=\"{width}\" height=\"{height}\" flashvars=\"{flashVars}\" scale=\"noscale\" wmode=\"gpu\">");
            sb.Append("<form method=\"POST\">");
            sb.Append("<input type=\"hidden\" name=\"room\">");
            sb.Append("<input type=\"hidden\" name=\"island\">");
            sb.Append("<input type=\"hidden\" name=\"startup_path\">");
            sb.Append("</form>");
            sb.Append("<script type=\"text/javascript\">");
            sb.Append("function dbug(message) {");
            sb.Append("console.log(message);");
            sb.Append("}");

            sb.Append("function flashpointLoad(scene,island) {");
            sb.Append("console.log(\"Flashpoint loads a scene: \" + scene + \" on \" + island);");
            sb.Append("POSTToBase(island, scene, \"gameplay\");");
            sb.Append("}");

            sb.Append("function POSTToBase(...args) {");
            sb.Append("var form = document.forms[0];");
            sb.Append("if (form.children.length == args.length) {");
            sb.Append("for (var i = 0; i < args.length; i++)");
            sb.Append("form.children[i].setAttribute(\"value\", args[i]);");
            sb.Append("form.submit();");
            sb.Append("}");
            sb.Append("}");

            if (pageState == STATE_AS3)
            {
                sb.Append("function loadAS3Embassy() {");
                sb.Append("var origEmbed = document.querySelector(\"embed\"),");
                sb.Append("embed = document.createElement(\"embed\");");
                sb.Append("for (var i = 0; i < origEmbed.attributes.length; i++)");
                sb.Append("embed.setAttribute(origEmbed.attributes[i].name, origEmbed.attributes[i].value);");
                sb.Append("embed.setAttribute(\"src\", \"framework.swf\");");
                sb.Append("var parent = origEmbed.parentNode,");
                sb.Append("sibling = origEmbed.nextSibling;");
                sb.Append("parent.removeChild(origEmbed);");
                sb.Append("parent.insertBefore(embed, sibling);");
                sb.Append("}");
            }
            else
            {
                sb.Append("function loadTrackingPixel(url) {");
                sb.Append("if (url.startsWith(\"http://notify.maps.poptropica.com\"))");
                sb.Append("POSTToBase(\"" + SCENE_AS3_START + "\", \"Home\", \"gameplay\");");
                sb.Append("}");
            }

            sb.Append("</script>");
            sb.Append("</body>");
            sb.Append("</html>");

            return sb.ToString();
        }
    }
}
