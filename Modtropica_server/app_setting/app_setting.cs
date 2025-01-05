using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Modtropica_server.app_setting
{
    internal class app_setting
    {
        public enum theme_idx
        {
            none,
            light,
            dark,
            custom
        }
        public enum setup_stage
        {
            none,
            download_gamefile,
            download_patches,
            stage_setup = 10,
            account_pop_setup,
            theme_setup,

        }

        public static Modtropica_server_setting modtropica_Setting;

        public class Modtropica_server_setting
        {
            public bool enable_mod { get; set; } = true;
            public bool enable_rpc { get; set; } = false;
            public theme_idx theme { get; set; } = theme_idx.light;
            public string? theme_name { get; set; } = null;

        }

        public static void load_setting()
        {
            try
            {
                if (File.Exists(SettingsPath))
                {
                    modtropica_Setting = JsonConvert.DeserializeObject<Modtropica_server_setting>(File.ReadAllText(SettingsPath));
                }
                else
                {
                    File.WriteAllText(SettingsPath,JsonConvert.SerializeObject(new Modtropica_server_setting()));
                    modtropica_Setting = JsonConvert.DeserializeObject<Modtropica_server_setting>(File.ReadAllText(SettingsPath));

                }
            }
            catch
            {
                File.WriteAllText(SettingsPath, JsonConvert.SerializeObject(new Modtropica_server_setting()));
                modtropica_Setting = JsonConvert.DeserializeObject<Modtropica_server_setting>(File.ReadAllText(SettingsPath));
            }
        }

        public static string SettingsPath = "Save_Data/App/server_setting.json";

    }
}
