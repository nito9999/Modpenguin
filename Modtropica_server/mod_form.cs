using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Resources;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modtropica_server.modtropica.core;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Modtropica_server
{
    public partial class mod_form : Form
    {
        public mod_form()
        {
            InitializeComponent();
        }




        private void mod_form_Load(object sender, EventArgs e)
        {
            mod_form_load();
        }
        public void mod_form_load()
        {
            mod_data.load_modtropica_mod();

            foreach (mod_data.mod_base_info item in mod_data.mods)
            {
                //byte[] bytes = Array.Empty<byte>();
                //if (File.Exists(Path.Combine("Resources",item.mod_Info.image_name)))
                int idx = 0;
                if (string.IsNullOrEmpty(item.mod_Info.image_name))
                {
                    idx = dataGridView1.Rows.Add(item.enable, item.name, null, item.mod_Info.Guid, "img://core/defaut_no_mod.png");
                }
                else
                {
                    idx = dataGridView1.Rows.Add(item.enable, item.name, null, item.mod_Info.Guid, item.mod_Info.image_name);
                }

                if (item.core_mod)
                {
                    dataGridView1.Rows[idx].Cells[0].ReadOnly = true;
                }
                else
                {
                    dataGridView1.Rows[idx].Cells[0].ReadOnly = false;

                }
                dataGridView1.Update();

            }
        }

        private const string resxFile = @".\Resource_main.resx";

        private void start_modtropica_button_Click(object sender, EventArgs e)
        {
            start_modtropica_button.Enabled = false;
            start_modtropica_button.BackColor = Color.DarkBlue;
            POP_Server = new POP_server();
        }

        public static POP_server POP_Server = null;

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            string Guid_mod = "";
            if (e.RowIndex < 0 || e.RowIndex > dataGridView1.Rows.Count)
                return;
            Guid_mod = (string)dataGridView1.Rows[e.RowIndex].Cells[3].Value;

            foreach (mod_data.mod_base_info item in mod_data.mods)
            {
                if (item.mod_Info.Guid == Guid_mod)
                {
                    mod_name.Text = item.name;

                    dev_box.Lines = item.mod_Info.Developer.ToArray();
                    if (!string.IsNullOrEmpty(item.mod_Info.description))
                    {
                        info_disc.Lines = new string[] { item.mod_Info.description };
                    }
                    else
                    {
                        info_disc.Lines = new string[] { "this mod don't have a description." };
                    }
                    string img_name = "img://core/defaut_no_mod.png";
                    if (!string.IsNullOrEmpty(item.mod_Info.image_name))
                    {
                        img_name = item.mod_Info.image_name;
                    }

                    byte[] data = file_system.File.load_image(img_name);
                    MemoryStream ms = new MemoryStream(data);
                    logo_big_Box.Image = Image.FromStream(ms);

                    break;
                }
            }
        }

        private void mod_rescan_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            mod_data.clear_modtropica_mod();
            mod_form_load();
        }
    }
}
