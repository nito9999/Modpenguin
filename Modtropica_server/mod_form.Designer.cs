namespace Modtropica_server
{
    partial class mod_form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            panel1 = new Panel();
            button1 = new Button();
            mod_rescan = new Button();
            start_modtropica_button = new Button();
            dataGridView1 = new DataGridView();
            enable = new DataGridViewCheckBoxColumn();
            name = new DataGridViewTextBoxColumn();
            icon = new DataGridViewImageColumn();
            mod_guid = new DataGridViewTextBoxColumn();
            image_name = new DataGridViewTextBoxColumn();
            logo_big_Box = new PictureBox();
            info_disc = new TextBox();
            dev_box = new TextBox();
            mod_name = new TextBox();
            Tag_list = new TextBox();
            date_box = new TextBox();
            title_panal = new Panel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)logo_big_Box).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(63, 63, 63);
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(button1);
            panel1.Controls.Add(mod_rescan);
            panel1.Controls.Add(start_modtropica_button);
            panel1.Controls.Add(dataGridView1);
            panel1.Location = new Point(10, 11);
            panel1.Name = "panel1";
            panel1.Size = new Size(403, 696);
            panel1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(316, 666);
            button1.Name = "button1";
            button1.Size = new Size(75, 21);
            button1.TabIndex = 4;
            button1.Text = "open setting";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // mod_rescan
            // 
            mod_rescan.Location = new Point(3, 666);
            mod_rescan.Name = "mod_rescan";
            mod_rescan.Size = new Size(103, 21);
            mod_rescan.TabIndex = 3;
            mod_rescan.Text = "refresh modtropica";
            mod_rescan.UseVisualStyleBackColor = true;
            mod_rescan.Click += mod_rescan_Click;
            // 
            // start_modtropica_button
            // 
            start_modtropica_button.Location = new Point(3, 639);
            start_modtropica_button.Name = "start_modtropica_button";
            start_modtropica_button.Size = new Size(103, 21);
            start_modtropica_button.TabIndex = 2;
            start_modtropica_button.Text = "start servers";
            start_modtropica_button.UseVisualStyleBackColor = true;
            start_modtropica_button.Click += start_modtropica_button_Click;
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle1.BackColor = Color.FromArgb(63, 63, 63);
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.BackgroundColor = SystemColors.ControlDarkDark;
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.ControlDarkDark;
            dataGridViewCellStyle2.Font = new Font("Tahoma", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { enable, name, icon, mod_guid, image_name });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.WindowFrame;
            dataGridViewCellStyle3.Font = new Font("Tahoma", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlLight;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.Location = new Point(7, 8);
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.ControlDarkDark;
            dataGridViewCellStyle4.Font = new Font("Tahoma", 9F);
            dataGridViewCellStyle4.ForeColor = Color.White;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.BackColor = SystemColors.ControlDarkDark;
            dataGridViewCellStyle5.ForeColor = Color.White;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle5;
            dataGridView1.RowTemplate.DefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
            dataGridView1.RowTemplate.DefaultCellStyle.ForeColor = Color.White;
            dataGridView1.Size = new Size(384, 625);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellContentClick_1;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick_2;
            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
            dataGridView1.CurrentCellDirtyStateChanged += dataGridView1_CurrentCellDirtyStateChanged;
            // 
            // enable
            // 
            enable.Frozen = true;
            enable.HeaderText = "enable";
            enable.Name = "enable";
            enable.ReadOnly = true;
            enable.Resizable = DataGridViewTriState.False;
            enable.Width = 50;
            // 
            // name
            // 
            name.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            name.HeaderText = "name";
            name.Name = "name";
            name.ReadOnly = true;
            name.Resizable = DataGridViewTriState.False;
            // 
            // icon
            // 
            icon.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            icon.HeaderText = "icon";
            icon.Name = "icon";
            icon.ReadOnly = true;
            icon.Resizable = DataGridViewTriState.False;
            icon.Visible = false;
            // 
            // mod_guid
            // 
            mod_guid.HeaderText = "mod_guid";
            mod_guid.Name = "mod_guid";
            mod_guid.ReadOnly = true;
            mod_guid.Visible = false;
            // 
            // image_name
            // 
            image_name.HeaderText = "image_name";
            image_name.Name = "image_name";
            image_name.Visible = false;
            // 
            // logo_big_Box
            // 
            logo_big_Box.BackColor = Color.FromArgb(63, 63, 63);
            logo_big_Box.BackgroundImageLayout = ImageLayout.Stretch;
            logo_big_Box.BorderStyle = BorderStyle.FixedSingle;
            logo_big_Box.Location = new Point(417, 71);
            logo_big_Box.Name = "logo_big_Box";
            logo_big_Box.Size = new Size(380, 424);
            logo_big_Box.SizeMode = PictureBoxSizeMode.StretchImage;
            logo_big_Box.TabIndex = 1;
            logo_big_Box.TabStop = false;
            // 
            // info_disc
            // 
            info_disc.AcceptsReturn = true;
            info_disc.BackColor = Color.FromArgb(63, 63, 63);
            info_disc.BorderStyle = BorderStyle.FixedSingle;
            info_disc.ForeColor = Color.White;
            info_disc.HideSelection = false;
            info_disc.Location = new Point(417, 500);
            info_disc.Multiline = true;
            info_disc.Name = "info_disc";
            info_disc.ReadOnly = true;
            info_disc.Size = new Size(594, 207);
            info_disc.TabIndex = 2;
            // 
            // dev_box
            // 
            dev_box.AcceptsReturn = true;
            dev_box.BackColor = Color.FromArgb(63, 63, 63);
            dev_box.BorderStyle = BorderStyle.FixedSingle;
            dev_box.ForeColor = Color.White;
            dev_box.HideSelection = false;
            dev_box.Location = new Point(802, 273);
            dev_box.Multiline = true;
            dev_box.Name = "dev_box";
            dev_box.ReadOnly = true;
            dev_box.Size = new Size(209, 221);
            dev_box.TabIndex = 3;
            // 
            // mod_name
            // 
            mod_name.AcceptsReturn = true;
            mod_name.BackColor = Color.FromArgb(63, 63, 63);
            mod_name.BorderStyle = BorderStyle.FixedSingle;
            mod_name.ForeColor = Color.White;
            mod_name.HideSelection = false;
            mod_name.Location = new Point(417, 45);
            mod_name.Name = "mod_name";
            mod_name.ReadOnly = true;
            mod_name.Size = new Size(380, 21);
            mod_name.TabIndex = 4;
            // 
            // Tag_list
            // 
            Tag_list.AcceptsReturn = true;
            Tag_list.BackColor = Color.FromArgb(63, 63, 63);
            Tag_list.BorderStyle = BorderStyle.FixedSingle;
            Tag_list.ForeColor = Color.White;
            Tag_list.HideSelection = false;
            Tag_list.Location = new Point(802, 71);
            Tag_list.Multiline = true;
            Tag_list.Name = "Tag_list";
            Tag_list.ReadOnly = true;
            Tag_list.Size = new Size(209, 197);
            Tag_list.TabIndex = 5;
            // 
            // date_box
            // 
            date_box.AcceptsReturn = true;
            date_box.BackColor = Color.FromArgb(63, 63, 63);
            date_box.BorderStyle = BorderStyle.FixedSingle;
            date_box.ForeColor = Color.White;
            date_box.HideSelection = false;
            date_box.Location = new Point(802, 45);
            date_box.Name = "date_box";
            date_box.ReadOnly = true;
            date_box.Size = new Size(209, 21);
            date_box.TabIndex = 6;
            // 
            // title_panal
            // 
            title_panal.BackColor = Color.DarkSlateGray;
            title_panal.BorderStyle = BorderStyle.Fixed3D;
            title_panal.Location = new Point(417, 11);
            title_panal.Name = "title_panal";
            title_panal.Size = new Size(595, 28);
            title_panal.TabIndex = 7;
            // 
            // mod_form
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(1022, 714);
            Controls.Add(title_panal);
            Controls.Add(date_box);
            Controls.Add(Tag_list);
            Controls.Add(mod_name);
            Controls.Add(dev_box);
            Controls.Add(info_disc);
            Controls.Add(logo_big_Box);
            Controls.Add(panel1);
            Name = "mod_form";
            Text = "mod_form";
            TransparencyKey = Color.FromArgb(64, 64, 64, 170);
            Load += mod_form_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)logo_big_Box).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion

        private Panel panel1;
        private PictureBox logo_big_Box;
        private Button start_modtropica_button;
        private DataGridView dataGridView1;
        private TextBox info_disc;
        private TextBox dev_box;
        private TextBox mod_name;
        private Button mod_rescan;
        private TextBox Tag_list;
        private TextBox date_box;
        private Panel title_panal;
        private Button button1;
        private DataGridViewCheckBoxColumn enable;
        private DataGridViewTextBoxColumn name;
        private DataGridViewImageColumn icon;
        private DataGridViewTextBoxColumn mod_guid;
        private DataGridViewTextBoxColumn image_name;
    }
}