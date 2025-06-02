using PieMavenPlugin;
using plugin.Classes;

namespace PieMavenPlugin
{
    public class Program : IPlugin
    {
        public string GetName()
        {
            return "Maven";
        }

        public Dictionary<PluginTask, Func<PluginTaskInput, PluginTaskOutput>> GetTaskDictionary()
        {
            return new Dictionary<PluginTask, Func<PluginTaskInput, PluginTaskOutput>>
            {
                { new PluginTask("New Maven Project"), NewMavenProject },
                {new PluginTask("Build Project"), BuildMavenProject },
                {new PluginTask("Run Maven Class"), RunMavenClass }
            };
        }

        public PluginTaskOutput NewMavenProject(PluginTaskInput input)
        {
            PluginTaskOutput output = new PluginTaskOutput();

            NewMavenProjectForm newMavenProjectForm = new NewMavenProjectForm();
            newMavenProjectForm.pluginTaskInput = input;
            newMavenProjectForm.ShowDialog();

            List<PluginAction> actions = newMavenProjectForm.actions;
            output.Actions = actions;

            return output;
        }

        public PluginTaskOutput BuildMavenProject(PluginTaskInput input)
        {
            PluginTaskOutput output = new PluginTaskOutput();

            BuildMavenProjectForm buildMavenProjectForm = new BuildMavenProjectForm();
            buildMavenProjectForm.pluginTaskInput = input;
            buildMavenProjectForm.ShowDialog();

            List<PluginAction> actions = buildMavenProjectForm.actions;
            output.Actions = actions;

            return output;
        }

        public PluginTaskOutput RunMavenClass(PluginTaskInput input)
        {
            PluginTaskOutput output = new PluginTaskOutput();

            RunMavenClassForm runMavenClassForm = new RunMavenClassForm();
            runMavenClassForm.pluginTaskInput = input;
            runMavenClassForm.ShowDialog();

            output.Actions = runMavenClassForm.actions;

            return output;
        }
    }
}