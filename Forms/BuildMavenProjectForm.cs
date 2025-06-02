using plugin.Classes;

namespace PieMavenPlugin
{
    public partial class BuildMavenProjectForm : Form
    {
        public PluginTaskInput pluginTaskInput;
        public List<PluginAction> actions = new List<PluginAction>();

        public BuildMavenProjectForm()
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

        private void generateButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(pomLocationTextBox.Text.Trim()) || string.IsNullOrEmpty(phasesTextBox.Text.Trim()))
            {
                MessageBox.Show("Input fields cannot be empty.", "Pie Maven Plugin");
            }
            else
            {
                RunTerminalCommandAction runTerminalCommandAction = new RunTerminalCommandAction();
                runTerminalCommandAction.Command = "mvn -f \"" + pomLocationTextBox.Text + "\" " + phasesTextBox.Text;
                actions.Add(runTerminalCommandAction);

                SelectDirectoryAction selectDirectoryAction = new SelectDirectoryAction();
                selectDirectoryAction.Path = Path.GetDirectoryName(pomLocationTextBox.Text);
                actions.Add(selectDirectoryAction);

                pluginTaskInput.Context.Map["PieMavenPlugin:pomDirectory"] = pomLocationTextBox.Text;
                pluginTaskInput.Context.Map["PieMavenPlugin:phases"] = phasesTextBox.Text;

                this.Close();
            }
        }

        private void BuildMavenProjectForm_Load(object sender, EventArgs e)
        {
            // Was another folder opened?
            if (!string.IsNullOrEmpty(pluginTaskInput.OpenedDirectory) && (!pluginTaskInput.Context.Map.ContainsKey("PieMavenPlugin:openedDirectory") || !pluginTaskInput.OpenedDirectory.Equals(pluginTaskInput.Context.Map["PieMavenPlugin:openedDirectory"])))
            {
                if (File.Exists(Path.Combine(pluginTaskInput.OpenedDirectory, "pom.xml"))) {
                    pluginTaskInput.Context.Map["PieMavenPlugin:openedDirectory"] = pluginTaskInput.OpenedDirectory;
                    pomLocationTextBox.Text = Path.Combine(pluginTaskInput.OpenedDirectory, "pom.xml");
                    pluginTaskInput.Context.Map["PieMavenPlugin:pomDirectory"] = pomLocationTextBox.Text;
                }
            }
            else if (pluginTaskInput.Context.Map.ContainsKey("PieMavenPlugin:pomDirectory"))
            {
                pomLocationTextBox.Text = pluginTaskInput.Context.Map["PieMavenPlugin:pomDirectory"].ToString();
            }

            if (pluginTaskInput.Context.Map.ContainsKey("PieMavenPlugin:phases"))
            {
                phasesTextBox.Text = (string)pluginTaskInput.Context.Map["PieMavenPlugin:phases"];
            }
            else
            {
                phasesTextBox.Text = "clean install";
            }
        }
    }
}
