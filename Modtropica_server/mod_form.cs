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
using Modtropica_server.server.server;
using Modtropica_server.server;
using System.Drawing.Drawing2D;
using Modtropica_server.ui;
using Modtropica_server.app_setting;

namespace Modtropica_server
{
    public partial class mod_form : Form
    {
        public mod_form()
        {
            if (!File.Exists("Save_Data/App/server_setting.json"))
            {
                setup.firsttime = true;
                Form1.main_Form = this;
                Form1.Setup_Form = new setup_form();
                Form1.Setup_Form.Show();
                this.Hide();
                setup.program_setup();

            }

            app_setting.app_setting.load_setting();
            InitializeComponent();
        }

        private void mod_form_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            Panel titleBar = this.title_panal;

            titleBar.MouseDown += titleBar_MouseDown;
            titleBar.MouseMove += titleBar_MouseMove;
            titleBar.MouseUp += titleBar_MouseUp;


            Button minimizeButton = new Button
            {
                Text = "-",
                ForeColor = Color.White,
                BackColor = Color.Gray,
                FlatStyle = FlatStyle.Flat,
                Width = 30,
                Height = 30,
                Dock = DockStyle.Right
            };
            titleBar.Controls.Add(minimizeButton);

            Button closeButton = new Button
            {
                Text = "X",
                ForeColor = Color.White,
                BackColor = Color.Red,
                FlatStyle = FlatStyle.Flat,
                Width = 30,
                Height = 30,
                Dock = DockStyle.Right
            };
            titleBar.Controls.Add(closeButton);
            closeButton.Click += (s, e) => Environment.Exit(0);

            minimizeButton.Click += (s, e) => this.WindowState = FormWindowState.Minimized;
            if (!File.Exists("game_data/pop.zip"))
            {
                this.Hide();
            }
            else
                mod_form_load();

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, 20, 20, 180, 90);
            path.AddArc(this.Width - 20, 0, 20, 20, 270, 90);
            path.AddArc(this.Width - 20, this.Height - 20, 20, 20, 0, 90);
            path.AddArc(0, this.Height - 20, 20, 20, 90, 90);
            path.CloseFigure();
            this.Region = new Region(path);
        }

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private void titleBar_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void titleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, (Size)dragCursorPoint);
                this.Location = Point.Add(dragFormPoint, (Size)diff);
            }
        }

        private void titleBar_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        public void mod_form_load()
        {
            mod_data.setting_UI = new setting_ui();
            mod_data.clear_modtropica_mod(); // for clearing data
            mod_data.load_modtropica_mod();

            foreach (mod_data.mod_base_info item in mod_data.mods)
            {
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



        private void start_modtropica_button_Click(object sender, EventArgs e)
        {
            start_modtropica_button.Enabled = false;
            start_modtropica_button.BackColor = Color.DarkGray;
            string temp = "game_data/pop_data/content/www.poptropica.com/game/shell.swf";
            if (!File.Exists(temp))
                goto start_server_now;
            goto start_server_now;
            modtropica.flashtools.Jpexs_tool.check_jpexs();
            string temp2 = "game_data/temp/tmp0.swf";
            File.Copy(temp, temp2, true);

            Directory.CreateDirectory("game_data/temp/game/");
            Console.WriteLine("Pop_patcher as3: starting to patch shell.swf");

            for (int idx = 0; idx < mod_data.mods.Count; idx++)
            {
                temp = "game_data/temp/tmp1.swf";

                mod_data.mod_base_info? item = mod_data.mods[idx];
                if (item.enable)
                {
                    if (!string.IsNullOrEmpty(item.Directory_path))
                    {
                        if (Directory.Exists(Path.Combine(item.Directory_path, "script/as3")))
                        {
                            Console.WriteLine("Pop_patcher as3: patching shell.swf from dir " + Path.Combine(item.Directory_path, "script/as3"));

                            modtropica.flashtools.Jpexs_tool.ImportScript(
                                temp,
                                temp2,
                                Path.Combine(item.Directory_path, "script/as3")
                                );
                        }
                    }
                }

                File.Copy(temp2, temp, true);
            }
            File.Move(temp2, "game_data/temp/game/shell.swf", true);
            Console.WriteLine("Pop_patcher as3: Patched shell.swf");
        start_server_now:
            start_server();
            //POP_Server_old = new POP_server();
        }
        private static void start_server()
        {
            POP_Server = new POP_server_Routing();

            //new Thread(server.server.NewFolder.https_server.https_Main).Start();
        }

        public static POP_server POP_Server_old;
        public static POP_server_Routing POP_Server = null;

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }


        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string Guid_mod = "";
            bool mod_enable = true;
            if (e.RowIndex < 0 || e.RowIndex > dataGridView1.Rows.Count)
                return;
            Guid_mod = (string)dataGridView1.Rows[e.RowIndex].Cells[3].Value;
            mod_enable = (bool)dataGridView1.Rows[e.RowIndex].Cells[0].Value;

            foreach (mod_data.mod_base_info item in mod_data.mods)
            {
                if (item.mod_Info.Guid == Guid_mod)
                {
                    file_system.SetEnable_override(item.mod_Info.Guid, mod_enable);
                    item.enable = mod_enable;
                    Mod_setting.SetModSetting(item.mod_Info.Guid, "enable", mod_enable.ToString());
                }
            }
        }
        public static string slected_guid;

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            string Guid_mod = "";
            bool mod_enable = true;
            if (e.RowIndex < 0 || e.RowIndex > dataGridView1.Rows.Count)
                return;
            if (dataGridView1.Rows[e.RowIndex].Cells[0] is null)
                return;
            if (string.IsNullOrEmpty((string)dataGridView1.Rows[e.RowIndex].Cells[3].Value))
                return;
            Guid_mod = (string)dataGridView1.Rows[e.RowIndex].Cells[3].Value;
            mod_enable = (bool)dataGridView1.Rows[e.RowIndex].Cells[0].Value;

            foreach (mod_data.mod_base_info item in mod_data.mods)
            {
                if (item.mod_Info.Guid == Guid_mod)
                {
                    slected_guid = item.mod_Info.Guid;
                    if (item.mod_Info.config != null)
                        mod_data.setting_UI.PopulateForm(item.mod_Info.config, slected_guid);
                    else
                        mod_data.setting_UI.PopulateForm(new List<mod_data.setting_config> (), slected_guid);
                    file_system.SetEnable_override(item.mod_Info.Guid, mod_enable);
                    mod_name.Text = item.name;

                    date_box.Text = item.mod_Info.Release_Date.ToString();
                    if (item.mod_Info.tags != null && item.mod_Info.tags.Count > 0)
                    {
                        Tag_list.Lines = item.mod_Info.tags.ToArray();
                    }
                    else
                    {
                        Tag_list.Lines = new string[0];
                    }

                    dev_box.Lines = item.mod_Info.Developer.ToArray();
                    if (!string.IsNullOrEmpty(item.mod_Info.description))
                    {
                        info_disc.Text = item.mod_Info.description;
                    }
                    else
                    {
                        info_disc.Text = "this mod don't have a description.";
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
            POP_server_Routing.reloadRegisterRoutes();
        }

        private void dataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //mod_data.setting_UI.Show();
        }
    }
}
