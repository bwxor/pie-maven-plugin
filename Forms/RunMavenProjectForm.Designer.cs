namespace PieMavenPlugin
{
    partial class RunMavenProjectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RunMavenProjectForm));
            generateButton = new Button();
            label1 = new Label();
            browseButton = new Button();
            pomLocationTextBox = new TextBox();
            label2 = new Label();
            classNameTextBox = new TextBox();
            SuspendLayout();
            // 
            // generateButton
            // 
            generateButton.Location = new Point(335, 124);
            generateButton.Name = "generateButton";
            generateButton.Size = new Size(75, 23);
            generateButton.TabIndex = 7;
            generateButton.Text = "Run";
            generateButton.UseVisualStyleBackColor = true;
            generateButton.Click += generateButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(86, 15);
            label1.TabIndex = 6;
            label1.Text = "POM Location:";
            // 
            // browseButton
            // 
            browseButton.Location = new Point(337, 27);
            browseButton.Name = "browseButton";
            browseButton.Size = new Size(75, 23);
            browseButton.TabIndex = 5;
            browseButton.Text = "Browse...";
            browseButton.UseVisualStyleBackColor = true;
            browseButton.Click += browseButton_Click;
            // 
            // pomLocationTextBox
            // 
            pomLocationTextBox.BorderStyle = BorderStyle.FixedSingle;
            pomLocationTextBox.Location = new Point(15, 27);
            pomLocationTextBox.Name = "pomLocationTextBox";
            pomLocationTextBox.ReadOnly = true;
            pomLocationTextBox.Size = new Size(316, 23);
            pomLocationTextBox.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 63);
            label2.Name = "label2";
            label2.Size = new Size(72, 15);
            label2.TabIndex = 9;
            label2.Text = "Class Name:";
            // 
            // classNameTextBox
            // 
            classNameTextBox.BorderStyle = BorderStyle.FixedSingle;
            classNameTextBox.Location = new Point(15, 81);
            classNameTextBox.Name = "classNameTextBox";
            classNameTextBox.Size = new Size(395, 23);
            classNameTextBox.TabIndex = 8;
            // 
            // RunMavenProjectForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(422, 155);
            Controls.Add(label2);
            Controls.Add(classNameTextBox);
            Controls.Add(generateButton);
            Controls.Add(label1);
            Controls.Add(browseButton);
            Controls.Add(pomLocationTextBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "RunMavenProjectForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Run Maven Project";
            Load += RunMavenProjectForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button generateButton;
        private Label label1;
        private Button browseButton;
        private TextBox pomLocationTextBox;
        private Label label2;
        private TextBox classNameTextBox;
    }
}