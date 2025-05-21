namespace PieMavenPlugin
{
    partial class BuildMavenProjectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuildMavenProjectForm));
            phasesTextBox = new TextBox();
            label2 = new Label();
            generateButton = new Button();
            label1 = new Label();
            browseButton = new Button();
            pomDirectoryTextBox = new TextBox();
            SuspendLayout();
            // 
            // phasesTextBox
            // 
            phasesTextBox.BorderStyle = BorderStyle.FixedSingle;
            phasesTextBox.Location = new Point(15, 84);
            phasesTextBox.Name = "phasesTextBox";
            phasesTextBox.Size = new Size(397, 23);
            phasesTextBox.TabIndex = 11;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 66);
            label2.Name = "label2";
            label2.Size = new Size(46, 15);
            label2.TabIndex = 10;
            label2.Text = "Phases:";
            // 
            // generateButton
            // 
            generateButton.Location = new Point(337, 130);
            generateButton.Name = "generateButton";
            generateButton.Size = new Size(75, 23);
            generateButton.TabIndex = 9;
            generateButton.Text = "Build";
            generateButton.UseVisualStyleBackColor = true;
            generateButton.Click += generateButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 21);
            label1.Name = "label1";
            label1.Size = new Size(88, 15);
            label1.TabIndex = 8;
            label1.Text = "POM Directory:";
            // 
            // browseButton
            // 
            browseButton.Location = new Point(337, 39);
            browseButton.Name = "browseButton";
            browseButton.Size = new Size(75, 23);
            browseButton.TabIndex = 7;
            browseButton.Text = "Browse...";
            browseButton.UseVisualStyleBackColor = true;
            browseButton.Click += browseButton_Click;
            // 
            // pomDirectoryTextBox
            // 
            pomDirectoryTextBox.BorderStyle = BorderStyle.FixedSingle;
            pomDirectoryTextBox.Location = new Point(15, 39);
            pomDirectoryTextBox.Name = "pomDirectoryTextBox";
            pomDirectoryTextBox.ReadOnly = true;
            pomDirectoryTextBox.Size = new Size(316, 23);
            pomDirectoryTextBox.TabIndex = 6;
            // 
            // BuildMavenProjectForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(427, 166);
            Controls.Add(phasesTextBox);
            Controls.Add(label2);
            Controls.Add(generateButton);
            Controls.Add(label1);
            Controls.Add(browseButton);
            Controls.Add(pomDirectoryTextBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "BuildMavenProjectForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Maven Build";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox phasesTextBox;
        private Label label2;
        private Button generateButton;
        private Label label1;
        private Button browseButton;
        private TextBox pomDirectoryTextBox;
    }
}