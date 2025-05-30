using plugin.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PieMavenPlugin
{
    public partial class RunMavenProjectForm : Form
    {
        public PluginTaskInput pluginTaskInput;
        public List<PluginAction> actions = new List<PluginAction>();
        public RunMavenProjectForm()
        {
            InitializeComponent();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Maven Project Files (*.xml)|*.xml|All Files (*.*)|*.*",
                Title = "Select a Maven Project File"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pomLocationTextBox.Text = openFileDialog.FileName;
            }
        }

        private void RunMavenProjectForm_Load(object sender, EventArgs e)
        {
            if (pluginTaskInput.Context.Map.ContainsKey("PieMavenPlugin:pomDirectory"))
            {
                pomLocationTextBox.Text = pluginTaskInput.Context.Map["PieMavenPlugin:pomDirectory"].ToString();
            }
            else if (pluginTaskInput.OpenedDirectory != null &&
               File.Exists(Path.Combine(pluginTaskInput.OpenedDirectory, "pom.xml")))
            {
                pomLocationTextBox.Text = Path.Combine(pluginTaskInput.OpenedDirectory, "pom.xml");
                pluginTaskInput.Context.Map["PieMavenPlugin:pomDirectory"] = pomLocationTextBox.Text;
            }

            if (pluginTaskInput.Context.Map.ContainsKey("PieMavenPlugin:className"))
            {
                classNameTextBox.Text = pluginTaskInput.Context.Map["PieMavenPlugin:className"].ToString();
            }
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            pluginTaskInput.Context.Map["PieMavenPlugin:className"] = classNameTextBox.Text;

            string pomLocation = pomLocationTextBox.Text.Trim();
            string outputFileLocation = Path.Combine(Path.GetDirectoryName(pomLocation), "classpath");
            string targetClassesLocation = Path.Combine(Path.GetDirectoryName(pomLocation), "target", "classes");

            RunTerminalCommandAction runTerminalCommandAction = new RunTerminalCommandAction
            {
                Command = $"java -cp \"" + targetClassesLocation + ";$(mvn -q dependency:build-classpath)\" " + classNameTextBox.Text
            };

            actions.Add(runTerminalCommandAction);

            this.Close();
        }
    }
}
