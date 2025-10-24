using PieMavenPlugin.Listeners;
using PieMavenPlugin.Windows;
using plugin.Classes;
using plugin.Classes.Actions.Window;
using plugin.Classes.Context;
using plugin.Classes.UI.Containers;

namespace PieMavenPlugin
{
    public class Program : Plugin
    {
        public PluginInfo OnLoad()
        {
            PluginInfo pluginInfo = new PluginInfo();
            pluginInfo.Name = "Maven Plugin";
            pluginInfo.TaskNames = new List<string>
            {
                "New Maven Project",
                "Build Maven Project",
                "Run Maven Class"
            };
            return pluginInfo;
        }

        public Window OnInvokeTask(string taskName, PluginContext context)
        {
            if (taskName.Equals("New Maven Project"))
            {
                return new NewMavenProjectTask().GetWindow(context);
            }
            else if (taskName.Equals("Build Maven Project"))
            {
                return new BuildMavenProjectTask().GetWindow(context);
            }
            else if (taskName.Equals("Run Maven Class"))
            {
                return new RunMavenTaskClass().GetWindow(context);
            }

            return null;
        }

        public List<OnCreateFileAction> OnCreateFile(string fullFilePath, PluginContext context)
        {
            return new OnCreateFileListener().onCreateFile(fullFilePath, context);
        }

        public List<OnOpenDirectoryAction> OnOpenDirectory(string directoryPath, PluginContext context)
        {
            return new OnOpenDirectoryListener().onOpenDirectory(directoryPath, context);
        }
    }
}