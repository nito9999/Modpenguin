namespace Modtropica_server.ui
{
    partial class setting_ui
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
            settingsPanel = new Panel();
            SuspendLayout();
            // 
            // settingsPanel
            // 
            settingsPanel.AutoScroll = true;
            settingsPanel.Location = new Point(12, 12);
            settingsPanel.Name = "settingsPanel";
            settingsPanel.Size = new Size(776, 426);
            settingsPanel.TabIndex = 0;
            // 
            // setting_ui
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(settingsPanel);
            Name = "setting_ui";
            Text = "setting_ui";
            Load += setting_ui_Load;
            ResumeLayout(false);
        }

        #endregion

        private Panel settingsPanel;
    }
}