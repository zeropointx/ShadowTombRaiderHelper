namespace TombRaiderHelper
{
    partial class Form1
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
            this.saveSettings = new System.Windows.Forms.Button();
            this.savePositionComboBox = new System.Windows.Forms.ComboBox();
            this.loadPositionComboBox = new System.Windows.Forms.ComboBox();
            this.savePositionLabel = new System.Windows.Forms.Label();
            this.loadPositionLabel = new System.Windows.Forms.Label();
            this.noclipComboBox = new System.Windows.Forms.ComboBox();
            this.noclipTextLabel = new System.Windows.Forms.Label();
            this.savePositionButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // saveSettings
            // 
            this.saveSettings.Location = new System.Drawing.Point(15, 286);
            this.saveSettings.Name = "saveSettings";
            this.saveSettings.Size = new System.Drawing.Size(151, 23);
            this.saveSettings.TabIndex = 0;
            this.saveSettings.Text = "Save Settings";
            this.saveSettings.UseVisualStyleBackColor = true;
            this.saveSettings.Click += new System.EventHandler(this.button1_Click);
            // 
            // savePositionComboBox
            // 
            this.savePositionComboBox.FormattingEnabled = true;
            this.savePositionComboBox.Location = new System.Drawing.Point(12, 68);
            this.savePositionComboBox.Name = "savePositionComboBox";
            this.savePositionComboBox.Size = new System.Drawing.Size(121, 21);
            this.savePositionComboBox.TabIndex = 1;
            // 
            // loadPositionComboBox
            // 
            this.loadPositionComboBox.FormattingEnabled = true;
            this.loadPositionComboBox.Location = new System.Drawing.Point(139, 68);
            this.loadPositionComboBox.Name = "loadPositionComboBox";
            this.loadPositionComboBox.Size = new System.Drawing.Size(121, 21);
            this.loadPositionComboBox.TabIndex = 1;
            // 
            // savePositionLabel
            // 
            this.savePositionLabel.AutoSize = true;
            this.savePositionLabel.Location = new System.Drawing.Point(12, 39);
            this.savePositionLabel.Name = "savePositionLabel";
            this.savePositionLabel.Size = new System.Drawing.Size(93, 13);
            this.savePositionLabel.TabIndex = 2;
            this.savePositionLabel.Text = "Save Position Key";
            // 
            // loadPositionLabel
            // 
            this.loadPositionLabel.AutoSize = true;
            this.loadPositionLabel.Location = new System.Drawing.Point(136, 39);
            this.loadPositionLabel.Name = "loadPositionLabel";
            this.loadPositionLabel.Size = new System.Drawing.Size(92, 13);
            this.loadPositionLabel.TabIndex = 2;
            this.loadPositionLabel.Text = "Load Position Key";
            // 
            // noclipComboBox
            // 
            this.noclipComboBox.FormattingEnabled = true;
            this.noclipComboBox.Location = new System.Drawing.Point(266, 68);
            this.noclipComboBox.Name = "noclipComboBox";
            this.noclipComboBox.Size = new System.Drawing.Size(121, 21);
            this.noclipComboBox.TabIndex = 1;
            // 
            // noclipTextLabel
            // 
            this.noclipTextLabel.AutoSize = true;
            this.noclipTextLabel.Location = new System.Drawing.Point(263, 39);
            this.noclipTextLabel.Name = "noclipTextLabel";
            this.noclipTextLabel.Size = new System.Drawing.Size(58, 13);
            this.noclipTextLabel.TabIndex = 2;
            this.noclipTextLabel.Text = "Noclip Key";
            // 
            // savePositionButton
            // 
            this.savePositionButton.Location = new System.Drawing.Point(12, 95);
            this.savePositionButton.Name = "savePositionButton";
            this.savePositionButton.Size = new System.Drawing.Size(121, 23);
            this.savePositionButton.TabIndex = 3;
            this.savePositionButton.Text = "SaveKeyBind: L";
            this.savePositionButton.UseVisualStyleBackColor = true;
            this.savePositionButton.Visible = false;
            this.savePositionButton.Click += new System.EventHandler(this.savePositionButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 321);
            this.Controls.Add(this.savePositionButton);
            this.Controls.Add(this.noclipTextLabel);
            this.Controls.Add(this.loadPositionLabel);
            this.Controls.Add(this.savePositionLabel);
            this.Controls.Add(this.noclipComboBox);
            this.Controls.Add(this.loadPositionComboBox);
            this.Controls.Add(this.savePositionComboBox);
            this.Controls.Add(this.saveSettings);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.ComboBox savePositionComboBox;
        public System.Windows.Forms.ComboBox loadPositionComboBox;
        public System.Windows.Forms.Label savePositionLabel;
        public System.Windows.Forms.Label loadPositionLabel;
        public System.Windows.Forms.Button saveSettings;
        public System.Windows.Forms.ComboBox noclipComboBox;
        public System.Windows.Forms.Label noclipTextLabel;
        private System.Windows.Forms.Button savePositionButton;
    }
}

