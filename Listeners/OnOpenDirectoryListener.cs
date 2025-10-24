using plugin.Classes.Actions;
using plugin.Classes.Actions.Window;
using plugin.Classes.Context;
using System.Text.RegularExpressions;

namespace PieMavenPlugin.Listeners
{
    public class OnOpenDirectoryListener
    {
        public List<OnOpenDirectoryAction> onOpenDirectory(string directoryPath, PluginContext context)
        {
            List<OnOpenDirectoryAction> actions = new List<OnOpenDirectoryAction>();

            string[] files = Directory.GetFiles(directoryPath);

            if (files.Any(f => Path.GetFileName(f) == "pom.xml"))
            {
               

            string xmlContent = File.ReadAllText(Path.Combine(directoryPath, "pom.xml"));

            // Regex to capture first <groupId>...</groupId>
            var match = Regex.Match(xmlContent, @"<groupId>\s*([^<]+)\s*</groupId>");

            if (match.Success)
            {
                string groupId = match.Groups[1].Value.Trim();
                match = Regex.Match(xmlContent, @"<artifactId>\s*([^<]+)\s*</artifactId>");

                    if (match.Success)
                    {
                        string artifactId = match.Groups[1].Value.Trim();
                        actions.Add(new StoreInContextAction("pie-maven-plugin/groupId", groupId));
                        actions.Add(new StoreInContextAction("pie-maven-plugin/artifactId", artifactId));
                        actions.Add(new StoreInContextAction("pie-maven-plugin/pomDirectory", directoryPath));
                        actions.Add(new StoreInContextAction("pie-maven-plugin/className", groupId + ".Main"));
                        return actions;
                    }
                      
            }
            }

            actions.Add(new StoreInContextAction("pie-maven-plugin/groupId", null));
            actions.Add(new StoreInContextAction("pie-maven-plugin/artifactId", null));
            actions.Add(new StoreInContextAction("pie-maven-plugin/pomDirectory", null));
            actions.Add(new StoreInContextAction("pie-maven-plugin/className", null));
            return actions;
        }
    }
}
