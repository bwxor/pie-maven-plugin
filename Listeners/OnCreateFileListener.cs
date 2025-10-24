using plugin.Classes.Actions.Window;
using plugin.Classes.Context;

namespace PieMavenPlugin.Listeners
{
    public class OnCreateFileListener
    {
        public List<OnCreateFileAction> onCreateFile(string fullFilePath, PluginContext context)
        {
            List<OnCreateFileAction> actions = new List<OnCreateFileAction>();

            if (context.Custom.ContainsKey("pie-maven-plugin/groupId"))
            {
                if (fullFilePath.EndsWith(".java"))
                {
                    string content = ResourceReader.ReadResource("PieMavenPlugin.Templates.NewClass.java");

                    actions.Add(new AppendFileContentAction(content.Replace("REPLACE_PACKAGE", context.Custom["pie-maven-plugin/groupId"]).Replace("REPLACE_CLASS", Path.GetFileNameWithoutExtension(fullFilePath))));
                }
            }

            return actions;
        }
    }
}
