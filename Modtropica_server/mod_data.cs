using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Modtropica_server.modtropica.core;
using Newtonsoft.Json;

namespace Modtropica_server
{
    internal class mod_data
    {

        public class mod_base_info
        {
            public string name;
            public string? Directory_path;

            public bool core_mod;
            public bool enable;
            public mod_info mod_Info;
        }
        public static void clear_modtropica_mod()
        {
            file_system.Remove_All_override();
            List<mod_base_info> mods_new = new List<mod_base_info>()
            {
                new mod_base_info()
                {
                    name = "poptropica",
                    core_mod = true,
                    enable = true,
                    mod_Info = new mod_info
                    {
                        Guid = "poptropica_base_file",
                        name = "poptropica",
                        description = "Poptropica, a virtual world for kids to travel, play games,\n" +
                                      "compete in head-to-head competition, and communicate safely.\n" +
                                      "Kids can also read books, comics, and see movie clips while they play.",
                        tags = new List<string>
                        {
                            "Platformer",
                            "Educational",
                            "Puzzle",
                            "Variety",
                            "Side View"
                        },
                        Developer = new List<string>
                        {
                            "Sandbox Networks Inc.",
                            "Pearson Education"
                        },
                        type = mod_type.none,
                        image_name = "img://core/poptropica.png",
                        Release_Date = DateOnly.Parse("01/09/2007")
                    }
                },
                new mod_base_info()
                {
                    name = "modtropica",
                    core_mod = true,
                    enable = true,
                    mod_Info = new mod_info
                    {
                        Guid = "poptropica_base_mod_loader",
                        name = "modtropica",
                        description = "a mod loader for Poptropica",
                        tags = new List<string>
                        {
                            "modloader",
                        },
                        Developer = new List<string>
                        {
                            "wiiboi69"
                        },
                        type = mod_type.modtropica_mod,
                        image_name = "img://core/modtropica.png",
                    }
                }
            };

            mods = mods_new;
        }
        public static void load_modtropica_mod()
        {
            string[] strings = Directory.GetDirectories(POP_server.mod_path);
            foreach (string s in strings)
            {
                try
                {
                    mod_info mod = JsonConvert.DeserializeObject<mod_info>(File.ReadAllText(Path.Combine(s, "modinfo.json")));
                    mods.Add(new mod_base_info
                    {
                        name = mod.name,
                        core_mod = false,
                        enable = true,
                        mod_Info = mod,
                        Directory_path = s
                    });
                    if (!string.IsNullOrEmpty(mod.image_name))
                    {
                        mod.image_name = $"img://{mod.Guid}/{mod.image_name}";
                    }
                    

                    if (mod.file_def != null)
                    {
                        int load_order = 1;
                        foreach (string item in mod.file_def)
                        {
                            try
                            {
                                mod_file_def mod_file = JsonConvert.DeserializeObject<mod_file_def>(File.ReadAllText(Path.Combine(s, item)));
                                if (mod_file.file_Datas != null)
                                {
                                    foreach (mod_file_data item1 in mod_file.file_Datas)
                                    {
                                        bool flag = false;
                                        if (item1.is_dir != null && item1.is_dir == true)
                                        {
                                            flag = item1.is_dir;
                                        }
                                        file_system.Add_override(new file_system.file_override
                                        {
                                            mod_guid = mod.Guid,
                                            Directory = flag,
                                            enable = true,
                                            file_path = item1.file_name,
                                            new_file_path = item1.file_replacement_name,
                                            type = (file_system.file_type)item1.type,
                                            load_order = load_order,
                                            Directory_path = s
                                        });
                                        load_order += 1;
                                    }
                                }
                            }
                            catch {}
                        }
                    }
                }
                catch {}
            }
        }

        public static List<mod_base_info> mods = new List<mod_base_info>()
        {
            new mod_base_info()
            {
                name = "poptropica",
                core_mod = true,
                enable = true,
                mod_Info = new mod_info
                {
                    Guid = "poptropica_base_file",
                    name = "poptropica",
                    description = "Poptropica, a virtual world for kids to travel, play games,\n" +
                                  "compete in head-to-head competition, and communicate safely.\n" +
                                  "Kids can also read books, comics, and see movie clips while they play.",
                    tags = new List<string>
                    {
                        "Platformer",
                        "Educational",
                        "Puzzle",
                        "Variety",
                        "Side View"
                    },
                    Developer = new List<string>
                    {
                        "Sandbox Networks Inc.",    
                        "Pearson Education"
                    },
                    type = mod_type.none,
                    image_name = "img://core/Poptropica.png",
                    Release_Date = DateOnly.Parse("01/09/2007")
                }
            },
            new mod_base_info()
            {
                name = "modtropica",
                core_mod = true,
                enable = true,
                mod_Info = new mod_info
                {
                    Guid = "poptropica_base_mod_loader",
                    name = "modtropica",
                    description = "a mod loader for Poptropica",
                    tags = new List<string>
                    {
                        "modloader",
                    },
                    Developer = new List<string>
                    {
                        "wiiboi69"
                    },
                    type = mod_type.modtropica_mod,
                    image_name = "img://core/modtropica.png",
                }
            },
        };

        public enum mod_type
        {
            none,
            custom_quest,
            modtropica_mod,
        }
        public class mod_info
        {
            public string? Guid { get; set; } = null;
            public string name { get; set; } = "";
            public string? image_name { get; set; }
            public string? description { get; set; }
            public mod_type type { get; set; }
            public DateOnly? Release_Date { get; set; }
            public List<string> Developer { get; set; }
            public List<string>? tags { get; set; }
            public List<string>? file_def { get; set; }
        }
        public class mod_file_def
        {
            public List<mod_file_data>? file_Datas { get; set; }
            public List<mod_script_data>? scripts {  get; set; }
        }
        public class mod_file_data
        {
            public mod_file_type type { get; set; }
            public bool is_dir { get; set; } = false;
            public string file_name { get; set; }
            public string? file_replacement_name { get; set; }
        }
        public class mod_script_data
        {
            public mod_script_type type { get; set; }
            public string script_name { get; set; }
            public bool load_on_page_load { get; set; } = false;
            public string? file_replacement_name { get; set; }
        }
        public enum mod_script_type
        {
            none,
            add,
        }
        /// <summary>
        /// 
        /// </summary>
        public enum mod_file_type
        {
            none,
            remove,
            add,
            rename,
            merge, // it only work when is_dir is true 
        }
    }
}
