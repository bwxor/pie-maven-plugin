using PieMavenPlugin;
using plugin.Classes;

namespace SampleWinformsPlugin
{
    public class Program: IPlugin
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
                {new PluginTask("Build Project"), BuildMavenProject }
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
    }
}