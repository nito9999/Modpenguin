using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modtropica_server.modtropica.flashtools
{
    class Jpexs_tool
    {
        private static readonly string jpexsPath = Path.Combine("game_data/tools/jpexs", "ffdec.exe");
        private static readonly string downloadUrl = "https://github.com/jindrapetrik/jpexs-decompiler/releases/latest/download/ffdec.jar";

        // Method to download JPEXS from GitHub
        public static void DownloadJpexs()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(jpexsPath));

            HttpClient client = new HttpClient();

            byte[] fileBytes = client.GetByteArrayAsync(downloadUrl).GetAwaiter().GetResult();
            File.WriteAllBytes(jpexsPath, fileBytes);
            Console.WriteLine("JPEXS downloaded successfully!");

        }

        public static void check_jpexs()
        {
            if (!File.Exists(jpexsPath))
            {
                DownloadJpexs();
            }
        }
        // Method to run JPEXS for decompilation

        public static void RunDecompiler(string swfFile, string outputDir)
        { 
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "java",
                Arguments = $"-jar \"{jpexsPath}\" -export script \"{swfFile}\" \"{outputDir}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(psi))
            {
                process.WaitForExit();
            }
        }

        // Method to import .as file
        public static void ImportScript(string inputSwf, string outputSwf, string scriptFolder)
        {
            if (!File.Exists(jpexsPath))
            {
                throw new FileNotFoundException("JPEXS Decompiler not found at " + jpexsPath);
            }
            if (!File.Exists(inputSwf))
            {
                throw new FileNotFoundException("Input SWF not found: " + inputSwf);
            }
            if (!Directory.Exists(scriptFolder))
            {
                throw new DirectoryNotFoundException("Script folder not found: " + scriptFolder);
            }
            Console.WriteLine($"\"{jpexsPath}\" -importScript \"{inputSwf}\" \"{outputSwf}\" \"{scriptFolder}\"");
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = jpexsPath,
                Arguments = $"-importScript \"{inputSwf}\" \"{outputSwf}\" \"{scriptFolder}\"",
                UseShellExecute = true,
                CreateNoWindow = false
            };

            using (Process process = Process.Start(psi))
            {
                process.WaitForExit();
            }

        }
    }
}
