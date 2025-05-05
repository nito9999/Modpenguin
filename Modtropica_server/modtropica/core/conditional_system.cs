using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modtropica_server.modtropica.core
{
    public class conditional_system
    {
        public static string scene = "home";
        public static string cluster = "hub";
        public static void Set_island(string test, string test2)
        {
            scene = test.ToLower();
            cluster = test2.ToLower();
        }
        /// <summary>
        /// for conditional file stuff
        /// </summary>
        /// <param name="conditional_Data"></param>
        /// <returns></returns>
        public static bool Check_if_condition_compled(List<mod_data.mod_conditional_data> conditional_Data)
        {
            bool flag = true;
            foreach (var item in conditional_Data)
            {
                switch (item.Condition_type)
                {
                    case mod_data.mod_conditional_type.none:
                        break;
                    case mod_data.mod_conditional_type.inScene:
                        flag = check_if_scene(item.data_1, item.data_2);
                        break;
                    case mod_data.mod_conditional_type.timeBefore:
                    case mod_data.mod_conditional_type.timebetween:
                    case mod_data.mod_conditional_type.timeather:
                    case mod_data.mod_conditional_type.islandWin:
                    case mod_data.mod_conditional_type.item_get:
                    case mod_data.mod_conditional_type.item_removed:
                    case mod_data.mod_conditional_type.event_hit:
                    default:
                        break;
                }
                if (item.inverted)
                    flag = !flag;
                if (item.required && !flag)
                    return false;
            }
            return true;
        }
        public static bool check_if_scene(string? test, string? test2)
        {
            if (string.IsNullOrEmpty(test) || string.IsNullOrEmpty(test2))
                return false;
            if (test.ToLower() == cluster && test2.ToLower() == scene)
                return true;
            return false;
        }
    }
}
