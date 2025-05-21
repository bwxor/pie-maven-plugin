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
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pomDirectoryTextBox.Text = openFileDialog.FileName;
            }
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(pomDirectoryTextBox.Text.Trim()) || string.IsNullOrEmpty(phasesTextBox.Text.Trim()))
            {
                MessageBox.Show("Input fields cannot be empty.", "Pie Maven Plugin");
            }
            else
            {
                RunTerminalCommandAction runTerminalCommandAction = new RunTerminalCommandAction();
                runTerminalCommandAction.Command = "mvn -f \"" + pomDirectoryTextBox.Text + "\" " + phasesTextBox.Text;

                actions.Add(runTerminalCommandAction);

                this.Close();
            }
        }
    }
}
