using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICSharpCode.SharpZipLib.Zip.ExtendedUnixData;
using File_IO = System.IO;

namespace Modtropica_server.modtropica.core
{
    /// <summary>
    /// a custom and easy moden file system that let you override files path
    /// </summary>
    internal class file_system
    {
        /// <summary>
        /// a list of file override that is enabled
        /// </summary>
        private static List<file_override> file_override_list = new List<file_override>();

        public static void Add_override(file_override file_override)
        {
            Console.WriteLine($"File_System: adding file override with Mod_guid: {file_override.mod_guid} for file_path: {file_override.file_path} with mode: {file_override.type.ToString()}");

            foreach (file_override item in file_override_list)
            {
                if (file_override.mod_guid == item.mod_guid)
                {
                    if (file_override.file_path == item.file_path)
                    {
                        Console.WriteLine($"File_System: Mod_guid: {file_override.mod_guid} of file_path: {file_override.file_path} got added before!");
                        return;
                    }
                }
            }
            Console.WriteLine($"File_System: added file override with Mod_guid: {file_override.mod_guid} of file_path: {file_override.file_path} with mode: {file_override.type.ToString()}");

            file_override_list.Add(file_override);
        }
        public static void Remove_override(string file_path, string mod_guid)
        {
            Console.WriteLine($"File_System: Removing file override with Mod_guid: {mod_guid} of file_path: {file_path}");

            foreach (file_override item in file_override_list)
            {
                if (mod_guid == item.mod_guid)
                {
                    if (file_path == item.file_path)
                    {
                        file_override_list.Remove(item);
                        Console.WriteLine($"File_System: Removed file override with Mod_guid: {mod_guid} of file_path: {file_path}");
                        return;
                    }
                }
            }
        }

        public static void SetEnable_override(string file_path, string mod_guid, bool flag)
        {
            foreach (file_override item in file_override_list)
            {
                if (mod_guid == item.mod_guid)
                {
                    if (file_path == item.file_path)
                    {
                        item.enable = flag;
                    }
                }
            }
        }

        public static bool IsExist_override(string file_path, string mod_guid)
        {
            bool flag = false;
            foreach (file_override item in file_override_list)
            {
                if (mod_guid == item.mod_guid)
                {
                    if (file_path == item.file_path)
                    {
                        flag = true;
                    }
                }
            }
            return flag;
        }

        public static bool IsEnable_override(string file_path, string mod_guid)
        {
            bool flag = false;
            foreach (file_override item in file_override_list)
            {
                if (mod_guid == item.mod_guid)
                {
                    if (file_path == item.file_path)
                    {
                        flag = item.enable;
                    }
                }
            }
            return flag;
        }

        public static file_override Get_override(string file_path, string mod_guid)
        {
            file_override @override = null;
            foreach (file_override item in file_override_list)
            {
                if (mod_guid == item.mod_guid)
                {
                    if (file_path == item.file_path)
                    {
                        @override = item;
                    }
                }
            }
            return @override;
        }

        public static file_override Find_high_load_order_item(List<file_override> list)
        {
            if (list.Count == 0)
            {
                return null;
            }
            int load_order = int.MinValue;
            foreach (file_override type in list)
            {
                if (type.load_order > load_order)
                {
                    load_order = type.load_order;
                }
            }
            foreach (file_override type in list)
            {
                if (type.load_order == load_order)
                {
                    return type;
                }
            }
            return null;
        }

        public static void Remove_All_override()
        {
            file_override_list.Clear();
        }

        /// <summary>
        /// a file override class
        /// </summary>
        public class file_override
        {
            public string file_path;
            public string Directory_path;
            public string? new_file_path;
            public string mod_guid;
            public bool Directory;
            public bool enable;
            public int load_order;
            public file_type type;
        }
        public enum file_type
        {
            none,
            remove,
            add,
            rename,
        }

        /// <summary>
        /// a file class
        /// </summary>
        public class File
        {
            internal static bool Exists(string path)
            {
                file_override item = Find_high_load_order_item(file_override_list);
                if (item != null)
                {
                    if (item.enable)
                    {
                        if (item.file_path == path)
                        {
                            if(item.type == file_type.remove)
                            {
                                return false;
                            }
                        }
                    }
                }
                return File_IO.File.Exists(File_IO.Path.Combine(POP_server.base_path, path));
            }

            internal static byte[] ReadAllBytes(string path)
            {
                file_override item = Find_high_load_order_item(file_override_list);
                if (item != null)
                {
                    if (item.enable)
                    {
                        if (item.file_path == path)
                        {
                            if (item.type == file_type.rename)
                            {
                                return File_IO.File.ReadAllBytes(File_IO.Path.Combine(item.Directory_path, item.new_file_path));
                            }
                        }
                    }
                }
                return File_IO.File.ReadAllBytes(File_IO.Path.Combine(POP_server.base_path, path));
            }

            internal static string ReadAllText(string path)
            {
                file_override item = Find_high_load_order_item(file_override_list);
                if (item != null)
                {
                    if (item.enable)
                    {
                        if (item.file_path == path)
                        {
                            if (item.type == file_type.rename)
                            {
                                return File_IO.File.ReadAllText(File_IO.Path.Combine(item.Directory_path, item.new_file_path));
                            }
                        }
                    }
                }
                return File_IO.File.ReadAllText(File_IO.Path.Combine(POP_server.base_path, path));
            }

            /*
             img://core/ == Resources/
            
                        poptropica.png
             
             */


            public static byte[] load_image(string path)
            {
                string temp = "";
                string mod_guid = "";
                string mod_image_name = "";
                bool is_img = false;
                bool is_core_app = false;
                foreach (char item in path)
                {
                    if (temp == "img://" && !is_img && string.IsNullOrEmpty(mod_guid))
                    {
                        is_img = true;
                        temp = "";
                    }
                    else if (item == '/' && is_img && string.IsNullOrEmpty(mod_guid))
                    {
                        if (!is_core_app && temp == "core")
                        {
                            is_core_app = true;
                            temp = "";
                            continue;
                        }
                        else
                        {
                            mod_guid = temp;
                            temp = "";
                            continue;
                        }
                    }
                    temp += item;
                }
                mod_image_name = temp;
                Console.WriteLine($"image {mod_image_name} of {mod_guid} is_core {is_core_app}");
                if (is_img)
                {
                    if (is_core_app)
                    {
                        return File_IO.File.ReadAllBytes(File_IO.Path.Combine("Resources", mod_image_name));
                    }
                    else // TODO: imperment mod image load
                    {
                        foreach (mod_data.mod_base_info mod_ in mod_data.mods)
                        {
                            if (mod_.mod_Info.Guid == mod_guid)
                            {
                                try
                                {
                                    return File_IO.File.ReadAllBytes(File_IO.Path.Combine(mod_.Directory_path, mod_image_name));
                                }
                                catch {}
                            }
                        }

                    }
                }
                return File_IO.File.ReadAllBytes(File_IO.Path.Combine("Resources", "defaut_no_mod.png"));
            }
        }

        public class Directory
        {
            internal static bool Exists(string path)
            {
                return File_IO.Directory.Exists(File_IO.Path.Combine(POP_server.base_path, path));
            }
        }
    }
}
