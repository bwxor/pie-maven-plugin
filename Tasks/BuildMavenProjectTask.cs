
using PieMavenPlugin.Tasks;
using plugin.Classes.Actions;
using plugin.Classes.Context;
using plugin.Classes.UI.Containers;
using System.Net.Http.Headers;

namespace PieMavenPlugin.Windows
{
    public class BuildMavenProjectTask : PTask
    {
        public Window GetWindow(PluginContext context)
        {
            Window window = new Window("Build Maven Project");
            window.Controls.Add(new plugin.Classes.UI.Decoration.Label("pomLocationLabel", "POM Location:"));
            window.Controls.Add(
                new plugin.Classes.UI.Composite.FileBrowser("fileBrowser", "Maven Project Files (*.xml)|*.xml|All Files (*.*)|*.*", context.Custom.ContainsKey("pie-maven-plugin/pomDirectory") ?
                context.Custom["pie-maven-plugin/pomDirectory"] + "\\pom.xml" : "")
            );
            window.Controls.Add(new plugin.Classes.UI.Decoration.SpaceDelimiter());

            window.Controls.Add(new plugin.Classes.UI.Decoration.Label("phasesLabel", "Phases: "));
            window.Controls.Add(new plugin.Classes.UI.ValueContainers.TextBox("phasesTextBox", context.Custom.ContainsKey("pie-maven-plugin/phases") ? context.Custom["pie-maven-plugin/phases"] : "clean install"));

            window.OnOpen = OnOpenActions(context);
            window.OnClose = OnCloseActions(context);

            return window;
        }

        public List<UIAction> OnOpenActions(PluginContext context)
        {
            List<UIAction> onOpenActions = new List<UIAction>();

            return onOpenActions;
        }

        public List<ExitAction> OnCloseActions(PluginContext context)
        {
            List<ExitAction> onCloseActions = new List<ExitAction>();

            onCloseActions.Add(new ValidationAction("fileBrowser", s => string.IsNullOrEmpty(s.Trim()), "Input fields cannot be empty."));

            onCloseActions.Add(new ExecuteTerminalCommandAction("mvn -f \"" + "${controls.fileBrowser}" + "\" " + "${controls.phasesTextBox}"));

            onCloseActions.Add(new GeneratorAction(
                new string[] { "${controls.fileBrowser}" },
                s =>
                {
                    List<ExitAction> actions = new List<ExitAction>();
                    actions.Add(new SelectDirectoryAction(s[0].Substring(0, s[0].LastIndexOf("\\"))));
                    return actions;
                }
                ));

            onCloseActions.Add(new StoreInContextAction("pie-maven-plugin/pomDirectory", "${controls.fileBrowser}"));
            onCloseActions.Add(new StoreInContextAction("pie-maven-plugin/phases", "${controls.phasesTextBox}"));

            return onCloseActions;
        }
    }
}
