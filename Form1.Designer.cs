namespace SampleWinformsPlugin
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            parentDirectoryTextBox = new TextBox();
            browseButton = new Button();
            label1 = new Label();
            generateButton = new Button();
            label2 = new Label();
            groupIdTextBox = new TextBox();
            artifactIdTextBox = new TextBox();
            label3 = new Label();
            versionTextBox = new TextBox();
            label4 = new Label();
            SuspendLayout();
            // 
            // parentDirectoryTextBox
            // 
            parentDirectoryTextBox.BorderStyle = BorderStyle.FixedSingle;
            parentDirectoryTextBox.Location = new Point(12, 26);
            parentDirectoryTextBox.Name = "parentDirectoryTextBox";
            parentDirectoryTextBox.ReadOnly = true;
            parentDirectoryTextBox.Size = new Size(316, 23);
            parentDirectoryTextBox.TabIndex = 0;
            // 
            // browseButton
            // 
            browseButton.Location = new Point(334, 26);
            browseButton.Name = "browseButton";
            browseButton.Size = new Size(75, 23);
            browseButton.TabIndex = 1;
            browseButton.Text = "Browse...";
            browseButton.UseVisualStyleBackColor = true;
            browseButton.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 8);
            label1.Name = "label1";
            label1.Size = new Size(95, 15);
            label1.TabIndex = 2;
            label1.Text = "Parent Directory:";
            // 
            // generateButton
            // 
            generateButton.Location = new Point(332, 212);
            generateButton.Name = "generateButton";
            generateButton.Size = new Size(75, 23);
            generateButton.TabIndex = 3;
            generateButton.Text = "Generate";
            generateButton.UseVisualStyleBackColor = true;
            generateButton.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(9, 53);
            label2.Name = "label2";
            label2.Size = new Size(56, 15);
            label2.TabIndex = 4;
            label2.Text = "Group Id:";
            // 
            // groupIdTextBox
            // 
            groupIdTextBox.BorderStyle = BorderStyle.FixedSingle;
            groupIdTextBox.Location = new Point(12, 71);
            groupIdTextBox.Name = "groupIdTextBox";
            groupIdTextBox.Size = new Size(397, 23);
            groupIdTextBox.TabIndex = 5;
            groupIdTextBox.Text = "org.example";
            // 
            // artifactIdTextBox
            // 
            artifactIdTextBox.BorderStyle = BorderStyle.FixedSingle;
            artifactIdTextBox.Location = new Point(12, 115);
            artifactIdTextBox.Name = "artifactIdTextBox";
            artifactIdTextBox.Size = new Size(397, 23);
            artifactIdTextBox.TabIndex = 7;
            artifactIdTextBox.Text = "maven-project";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(9, 97);
            label3.Name = "label3";
            label3.Size = new Size(62, 15);
            label3.TabIndex = 6;
            label3.Text = "Artifact Id:";
            // 
            // versionTextBox
            // 
            versionTextBox.BorderStyle = BorderStyle.FixedSingle;
            versionTextBox.Location = new Point(12, 159);
            versionTextBox.Name = "versionTextBox";
            versionTextBox.Size = new Size(397, 23);
            versionTextBox.TabIndex = 9;
            versionTextBox.Text = "0.0.1-SNAPSHOT";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(9, 141);
            label4.Name = "label4";
            label4.Size = new Size(48, 15);
            label4.TabIndex = 8;
            label4.Text = "Version:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(419, 247);
            Controls.Add(versionTextBox);
            Controls.Add(label4);
            Controls.Add(artifactIdTextBox);
            Controls.Add(label3);
            Controls.Add(groupIdTextBox);
            Controls.Add(label2);
            Controls.Add(generateButton);
            Controls.Add(label1);
            Controls.Add(browseButton);
            Controls.Add(parentDirectoryTextBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            Text = "New Maven Project";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox parentDirectoryTextBox;
        private Button browseButton;
        private Label label1;
        private Button generateButton;
        private Label label2;
        private TextBox groupIdTextBox;
        private TextBox artifactIdTextBox;
        private Label label3;
        private TextBox versionTextBox;
        private Label label4;
    }
}
