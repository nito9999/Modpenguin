using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modtropica_server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            if (!File.Exists("Save_Data/App/server_setting.json"))
            {
                setup.firsttime = true;
                Setup_Form = new setup_form();
                Setup_Form.Show();
                setup.program_setup();
            }
            app_setting.app_setting.load_setting();
            main_Form = new mod_form();
            main_Form.Hide();
            this.Hide();
        }
        public static setup_form Setup_Form;
        public static mod_form main_Form;
    }
}
