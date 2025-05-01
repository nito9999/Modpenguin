using Modtropica_server.app_setting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Modtropica_server.app_setting.Mod_setting;
using static Modtropica_server.mod_data;

namespace Modtropica_server.ui
{
    public partial class setting_ui : Form
    {
        private Dictionary<string, Control> controlMap = new(); // Store controls for saving values
        public setting_ui()
        {
            InitializeComponent();
        }

        private void setting_ui_Load(object sender, EventArgs e)
        {

        }

        private Control CreateControlFromConfig(setting_config config)
        {
            Control control = config.Input_Type switch
            {
                config_input_type.TextBox => new TextBox { Text = config.default_value },
                config_input_type.NumBox => new NumericUpDown { Value = decimal.TryParse(config.default_value, out var val) ? val : 0 },
                config_input_type.DateBox => new DateTimePicker { Value = DateTime.TryParse(config.default_value, out var date) ? date : DateTime.Now },
                config_input_type.CheckBox => new CheckBox { Checked = bool.TryParse(config.default_value, out var chk) && chk },
                config_input_type.ComboBox => new ComboBox { Items = { "Option 1", "Option 2", "Option 3" }, Text = config.default_value },
                config_input_type.ColorBox => new Button { BackColor = ColorTranslator.FromHtml(config.default_value) },
                _ => new Label { Text = "Unsupported Type" }
            };

            control.Name = config.Key;
            return control;
        }
        public void PopulateForm(List<mod_data.setting_config> modSettings, string mod_guid)
        {
            settingsPanel.Controls.Clear();
            controlMap.Clear();
            int y = 10;
            Label modLabel = new Label { Text = $"Mod: {mod_guid}", Location = new Point(10, y), AutoSize = true };
            settingsPanel.Controls.Add(modLabel);
            y += 20;
            foreach (var mod in modSettings)
            {
                Label label = new Label { Text = mod.Key, Location = new Point(10, y), AutoSize = true };
                var control = CreateControlFromConfig(mod);
                y += 20;
                control.Location = new Point(10, y);

                settingsPanel.Controls.Add(label);
                settingsPanel.Controls.Add(control);
                controlMap[mod.Key] = control; // Store reference for saving

                y += 30;
                
            }
            var control2 = CreateControlFromConfig(new setting_config { Key = "enabled", Input_Type = config_input_type.CheckBox, default_value = "true"});

            controlMap.Add("enabled", control2);
            control2.Location = new Point(10, y);
            control2.Text = "enabled";
            Button saveButton = new Button { Text = "Save", Location = new Point(150, y) };
            saveButton.Click += (sender, e) => SaveSettings(mod_form.slected_guid);
            settingsPanel.Controls.Add(saveButton);
            settingsPanel.Controls.Add(control2);
        }
        public void SaveSettings(string guid)
        {
            List<Modtropica_mod_setting_config> mod_setting = new List<Modtropica_mod_setting_config>();
            foreach (var config in mod_setting)
            {
                if (controlMap.TryGetValue(config.Key, out Control control))
                {
                    config.Value = control.Text;
                }
            }
            SetModSetting(guid, mod_setting);
            MessageBox.Show("Settings saved successfully!");
        }
    }
}
