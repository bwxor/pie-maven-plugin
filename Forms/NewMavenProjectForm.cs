using PieMavenPlugin.Classes;
using plugin.Classes;

namespace SampleWinformsPlugin
{
    public partial class NewMavenProjectForm : Form
    {
        public PluginTaskInput pluginTaskInput;
        public List<PluginAction> actions = new List<PluginAction>();

        public NewMavenProjectForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                parentDirectoryTextBox.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(parentDirectoryTextBox.Text.Trim())
                || string.IsNullOrEmpty(groupIdTextBox.Text.Trim())
                || string.IsNullOrEmpty(artifactIdTextBox.Text.Trim())
                || string.IsNullOrEmpty(versionTextBox.Text.Trim()))
            {
                MessageBox.Show("Input fields cannot be empty.");
            }
            else if (groupIdTextBox.Text.Any(x => !Char.IsNumber(x) && !Char.IsLetter(x) && x != '.'))
            {
                MessageBox.Show("Group Id can only contain letters, numbers and dots.", "Pie Maven Plugin");
            }
            else if (groupIdTextBox.Text.StartsWith(".") || groupIdTextBox.Text.EndsWith("."))
            {
                MessageBox.Show("Group Id cannot start or end with a dot.", "Pie Maven Plugin");
            }
            else if (artifactIdTextBox.Text.Any(x => !Char.IsLetter(x) && !Char.IsNumber(x) && x != '-'))
            {
                MessageBox.Show("Artifact Id can only contain letters, numbers and dashes.", "Pie Maven Plugin");
            }
            else if (artifactIdTextBox.Text.StartsWith('-') || artifactIdTextBox.Text.EndsWith('-'))
            {
                MessageBox.Show("Artifact Id cannot start or end with a dash.", "Pie Maven Plugin");
            }
            else if (versionTextBox.Text.Any(x => !Char.IsNumber(x) && !Char.IsLetter(x) && x != '.' && x != '+' && x != '-'))
            {
                MessageBox.Show("Version can only contain letters, numbers, dots, dashes and plus symbols.", "Pie Maven Plugin");
            }
            else if (versionTextBox.Text.StartsWith('.') || versionTextBox.Text.EndsWith('.') || versionTextBox.Text.StartsWith('+') || versionTextBox.Text.EndsWith('+') || versionTextBox.Text.StartsWith('-') || versionTextBox.Text.EndsWith('-'))
            {
                MessageBox.Show("Version cannot start or end with a dot, dash or plus.", "Pie Maven Plugin");
            }
            else
            {
                CreateMavenStructure();
                this.Close();
            }
        }

        private void CreateMavenStructure()
        {
            string artifactId = artifactIdTextBox.Text;

            CreateDirectoryFromParent(artifactId);
            CreateDirectoryFromParent(artifactId + "\\src");
            CreateDirectoryFromParent(artifactId + "\\src\\main");
            CreateDirectoryFromParent(artifactId + "\\src\\main\\java");
            CreateDirectoryFromParent(artifactId + "\\src\\main\\resources");
            CreateDirectoryFromParent(artifactId + "\\src\\test");
            CreateDirectoryFromParent(artifactId + "\\src\\test\\java");
            CreateDirectoryFromParent(artifactId + "\\src\\test\\resources");

            BuildJavaMainClassFromParent();
            CreatePomFileFromParent();
            SelectProjectDirectoryFromParent();
        }

        private void CreateDirectoryFromParent(string name)
        {
            CreateDirectoryAction src = new CreateDirectoryAction();
            src.DirectoryName = parentDirectoryTextBox.Text + "\\" + name;
            actions.Add(src);
        }

        private void BuildJavaMainClassFromParent()
        {
            string[] groupIdSplitByDot = groupIdTextBox.Text.Split('.');

            string javaPackagePath = artifactIdTextBox.Text + "\\src\\main\\java";

            for (int i = 0; i < groupIdSplitByDot.Length; i++)
            {
                javaPackagePath += "\\" + groupIdSplitByDot[i];

                CreateDirectoryFromParent(javaPackagePath);
            }

            CreateFileFromParent(javaPackagePath + "\\Main.java", FileContents.JAVA_MAIN_CLASS_CONTENT);
            OpenTabFromParent(javaPackagePath + "\\Main.java");
        }

        private void CreatePomFileFromParent()
        {
            CreateFileFromParent(artifactIdTextBox.Text + "\\pom.xml", FileContents.POM_CONTENT.Replace("REPLACE_GROUP_ID", groupIdTextBox.Text).Replace("REPLACE_ARTIFACT_ID", artifactIdTextBox.Text).Replace("REPLACE_VERSION", versionTextBox.Text));
        }

        private void CreateFileFromParent(string name, string content)
        {
            CreateFileAction createFileAction = new CreateFileAction();
            createFileAction.Path = parentDirectoryTextBox.Text + "\\" + name;
            createFileAction.Content = content;
            actions.Add(createFileAction);
        }

        private void OpenTabFromParent(string path)
        {
            OpenTabAction openTabAction = new OpenTabAction();
            openTabAction.Path = parentDirectoryTextBox.Text + "\\" + path;
            actions.Add(openTabAction);
        }

        private void SelectProjectDirectoryFromParent()
        {
            SelectDirectoryAction selectDirectoryAction = new SelectDirectoryAction();
            selectDirectoryAction.Path = parentDirectoryTextBox.Text + "\\" + artifactIdTextBox.Text;
            actions.Add(selectDirectoryAction);
        }
    }
}
