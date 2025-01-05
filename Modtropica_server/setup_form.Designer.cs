namespace Modtropica_server
{
    partial class setup_form
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
            progressBar1 = new ProgressBar();
            setup_text = new Label();
            SuspendLayout();
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(7, 96);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(652, 23);
            progressBar1.TabIndex = 0;
            // 
            // setup_text
            // 
            setup_text.AutoSize = true;
            setup_text.ForeColor = SystemColors.Window;
            setup_text.Location = new Point(12, 9);
            setup_text.Name = "setup_text";
            setup_text.Size = new Size(38, 14);
            setup_text.TabIndex = 1;
            setup_text.Text = "label1";
            // 
            // setup_form
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(671, 131);
            Controls.Add(setup_text);
            Controls.Add(progressBar1);
            Name = "setup_form";
            Text = "setup_form";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public ProgressBar progressBar1;
        public Label setup_text;
    }
}