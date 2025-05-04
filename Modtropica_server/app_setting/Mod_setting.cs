using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Modtropica_server.app_setting
{
    public class Mod_setting
    {
        /// <summary>
        /// for browser type
        /// </summary>
        public enum Browser_type
        {
            none,
            firefox,
        }

        public class Modtropica_setting
        {
            public Browser_type browser { get; set; }
            public List<Modtropica_mod_setting> mod_setting { get; set; }
        }

        public class Modtropica_mod_setting
        {
            public string mod_guid { get; set; }
            public bool Enabled { get; set; }
            public List<Modtropica_mod_setting_config> mod_setting { get; set; }
        }

        public class Modtropica_mod_setting_config
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }

        public static string SettingsPath = "Save_Data/App/Mod_setting.json";

        private static Modtropica_setting _modSettings;

        public static Modtropica_setting Mod_Settings
        {
            get
            {
                if (_modSettings == null)
                {
                    _modSettings = LoadSettings();
                }
                return _modSettings;
            }
            set
            {
                _modSettings = value;
                SaveSettings(_modSettings);
            }
        }
        public static void SetModSetting(string guid, List<Modtropica_mod_setting_config> input)
        {
            foreach (var item in input)
            {
                _SetModSetting(guid, item.Key, item.Value);
            }
            // Save the updated settings
            SaveSettings(Mod_Settings);
        }
        public static void _SetModSetting(string guid, string input, string value)
        {
            // Ensure settings are loaded
            Mod_Settings = LoadSettings();

            // Find the mod setting with the given GUID
            var modEntry = Mod_Settings.mod_setting?.Find(m => m.mod_guid == guid);

            if (modEntry == null)
            {
                // If not found, create a new entry
                modEntry = new Modtropica_mod_setting
                {
                    mod_guid = guid,
                    Enabled = false, // Default state
                    mod_setting = new List<Modtropica_mod_setting_config>()
                };

                Mod_Settings.mod_setting.Add(modEntry);
            }

            if (input.ToLower() == "enable")
            {
                // Update the Enabled flag
                modEntry.Enabled = bool.TryParse(value, out bool enabled) && enabled;
            }
            else
            {
                // Update or add key-value setting
                var existingConfig = modEntry.mod_setting.Find(c => c.Key == input);
                if (existingConfig != null)
                {
                    existingConfig.Value = value;
                }
                else
                {
                    modEntry.mod_setting.Add(new Modtropica_mod_setting_config { Key = input, Value = value });
                }
            }
        }
        public static void SetModSetting(string guid, string input, string value)
        {
            _SetModSetting(guid, input, value);
            // Save the updated settings
            SaveSettings(Mod_Settings);
        }

        public static string GetModSetting(string guid, string input)
        {
            Mod_Settings = LoadSettings();

            var modEntry = Mod_Settings.mod_setting?.Find(m => m.mod_guid == guid);
            if (modEntry == null)
            {
                if (input.ToLower() == "enable")
                    return true.ToString();
                return null;
            }

            if (input.ToLower() == "enable")
                return modEntry.Enabled.ToString();

            var configEntry = modEntry.mod_setting.Find(c => c.Key == input);
            return configEntry?.Value;
        }

        public static void EnsureSettingsFile()
        {
            if (!File.Exists(SettingsPath))
            {
                Modtropica_setting defaultSettings = new Modtropica_setting
                {
                    browser = Browser_type.none,
                    mod_setting = new List<Modtropica_mod_setting>()
                };

                File.WriteAllText(SettingsPath, JsonConvert.SerializeObject(defaultSettings));
                Console.WriteLine("Settings file created with default values.");
            }
        }

        public static Modtropica_setting LoadSettings()
        {
            EnsureSettingsFile();
            string json = File.ReadAllText(SettingsPath);
            return JsonConvert.DeserializeObject<Modtropica_setting>(json);
        }

        public static void SaveSettings(Modtropica_setting settings)
        {
            File.WriteAllText(SettingsPath, JsonConvert.SerializeObject(settings));
        }
    }
}