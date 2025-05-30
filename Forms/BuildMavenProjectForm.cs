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
                pluginTaskInput.Context.Map["PieMavenPlugin:className"] = "org.example.Main";

                this.Close();
            }
        }

        private void BuildMavenProjectForm_Load(object sender, EventArgs e)
        {
            if (pluginTaskInput.Context.Map.ContainsKey("PieMavenPlugin:pomDirectory"))
            {
                pomLocationTextBox.Text = (string)pluginTaskInput.Context.Map["PieMavenPlugin:pomDirectory"];
                phasesTextBox.Text = "clean install";
            }
            else if (pluginTaskInput.OpenedDirectory != null &&
                File.Exists(Path.Combine(pluginTaskInput.OpenedDirectory, "pom.xml")))
            {
                pomLocationTextBox.Text = Path.Combine(pluginTaskInput.OpenedDirectory, "pom.xml");
                pluginTaskInput.Context.Map["PieMavenPlugin:pomDirectory"] = pomLocationTextBox.Text;
                phasesTextBox.Text = "clean install";
            }

            if (pluginTaskInput.Context.Map.ContainsKey("PieMavenPlugin:phases"))
            {
                phasesTextBox.Text = (string)pluginTaskInput.Context.Map["PieMavenPlugin:phases"];
            }
        }
    }
}
