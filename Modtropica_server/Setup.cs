using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Modtropica_server
{
    internal class setup
    {
        public static bool firsttime = false;
        public static void program_setup()
        {
            Console.WriteLine("Setting up... (May take a minute to download everything.)");

            Directory.CreateDirectory("Save_Data/App/");
            Directory.CreateDirectory("game_data/pop_data/");
            // for temp shit
            Directory.CreateDirectory("game_data/temp/");
            Directory.CreateDirectory("Mod_data/");
            if (!File.Exists("game_data/pop.zip"))
            {
                WebClient client = new WebClient();
                client.DownloadProgressChanged += client_DownloadProgressChanged;
                client.DownloadDataCompleted += Client_DownloadDataCompleted;
                Form1.Setup_Form.setup_text.Text = "downloading poptropica gamezip";
                Console.WriteLine("downloading poptropica gamezip from archive.org [AS3 ver]");

                client.DownloadDataAsync(new Uri("https://archive.org/download/poptropica-gamezips/AS3/83e1b5e7-4282-4bbd-868e-dcfa965e4abf.zip"));
                //File.WriteAllBytes("game_data/pop.zip", bytes);
            }
            else
            {
                Form1.Setup_Form.setup_text.Text = "found poptropica gamezip";
                program_setup_pop();
            }
        }

        static void Client_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            Form1.Setup_Form.setup_text.Text = "downloaded poptropica gamezip";
            File.WriteAllBytes("game_data/pop.zip", e.Result);
            program_setup_pop();
        }
        public static void program_setup_pop()
        {
            Form1.main_Form.Hide();
            using (ZipInputStream s = new ZipInputStream(File.OpenRead("game_data/pop.zip")))
            {
                
                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    Form1.Setup_Form.Refresh();

                    Console.WriteLine(theEntry.Name);
                    Form1.Setup_Form.setup_text.Text = $"extrating {theEntry.Name}";
                    Form1.Setup_Form.Update();
                    string directoryName = Path.GetDirectoryName(theEntry.Name);
                    string fileName = Path.GetFileName(theEntry.Name);

                    // create directory
                    if (directoryName.Length > 0)
                    {
                        Directory.CreateDirectory("game_data/pop_data/" + directoryName);
                    }

                    if (fileName != String.Empty)
                    {
                        using (FileStream streamWriter = File.Create("game_data/pop_data/" + theEntry.Name))
                        {

                            int size = 2048;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            Form1.Setup_Form.Visible = false;
        }

        static void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {

            Form1.main_Form.Hide();
            
            //Console.WriteLine($"downloading poptropica gamezip [ {e.BytesReceived} / {e.TotalBytesToReceive} ]");
            Console.WriteLine($"downloading poptropica gamezip [ {e.BytesReceived - e.TotalBytesToReceive} ]");
            Form1.Setup_Form.progressBar1.Maximum = (int)e.TotalBytesToReceive;
            Form1.Setup_Form.progressBar1.Value = (int)e.BytesReceived;
            Form1.Setup_Form.Refresh();
            Form1.Setup_Form.Update();
        }
    }
}
