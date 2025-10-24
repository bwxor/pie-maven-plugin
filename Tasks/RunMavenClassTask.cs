
using PieMavenPlugin.Tasks;
using plugin.Classes.Actions;
using plugin.Classes.Context;
using plugin.Classes.UI.Containers;

namespace PieMavenPlugin.Windows
{
    public class RunMavenTaskClass : PTask
    {
        public Window GetWindow(PluginContext context)
        {
            Window window = new Window("Run Maven Class");

            window.Controls.Add(new plugin.Classes.UI.Decoration.Label("pomLocationLabel", "POM Location:"));
            window.Controls.Add(
                new plugin.Classes.UI.Composite.FileBrowser("fileBrowser", "Maven Project Files (*.xml)|*.xml|All Files (*.*)|*.*",
                context.Custom.ContainsKey("pie-maven-plugin/pomDirectory") ? context.Custom["pie-maven-plugin/pomDirectory"] : "")
            );
            window.Controls.Add(new plugin.Classes.UI.Decoration.SpaceDelimiter());

            window.Controls.Add(new plugin.Classes.UI.Decoration.Label("classNameLabel", "Class Name: "));

            window.Controls.Add(new plugin.Classes.UI.ValueContainers.TextBox("classNameTextBox",
                context.Custom.ContainsKey("pie-maven-plugin/className") ? context.Custom["pie-maven-plugin/className"] :
                (
                    context.Custom.ContainsKey("pie-maven-plugin/groupId") ? context.Custom["pie-maven-plugin/groupId"] + ".Main" : "com.example.Main"
                )));
            window.Controls.Add(new plugin.Classes.UI.Decoration.SpaceDelimiter());

            window.Controls.Add(new plugin.Classes.UI.Decoration.Label("classLocationLabel", "Class Location: "));
            window.Controls.Add(new plugin.Classes.UI.ValueContainers.ComboBox("classLocationComboBox", new List<string> { "src/main", "src/test" },
                context.Custom.ContainsKey("pie-maven-plugin/classLocation") ? (context.Custom["pie-maven-plugin/classLocation"].Equals("src/test") ? 1 : 0) : 0)
                );

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
            onCloseActions.Add(new ValidationAction("classNameTextBox", s => string.IsNullOrEmpty(s.Trim()), "Input fields cannot be empty."));
            onCloseActions.Add(new StoreInContextAction("pie-maven-plugin/className", "${controls.fileBrowser}"));
            onCloseActions.Add(new StoreInContextAction("pie-maven-plugin/className", "${controls.classNameTextBox}"));
            onCloseActions.Add(new StoreInContextAction("pie-maven-plugin/classLocation", "${controls.classLocationComboBox}"));
            onCloseActions.Add(new GeneratorAction(
                new string[] { "${controls.fileBrowser}", "${controls.classNameTextBox}", "${controls.classLocationComboBox}" },
                s =>
                {
                    List<ExitAction> actions = new List<ExitAction>();

                    string targetClassesLocation = Path.Combine(Path.GetDirectoryName(s[0]), "target", s[2].Equals("src/main") ? "classes" : "test-classes");

                    if (s[2].Equals("src/main")) {
                        actions.Add(new ExecuteTerminalCommandAction(
                            $"java -cp \"" + targetClassesLocation + ";$(mvn -q dependency:build-classpath)\" " + s[1]
                            ));
                    }
                    else if (s[2].Equals("src/test"))
                    {
                        actions.Add(new ExecuteTerminalCommandAction(
   $"mvn -f " + s[0] + " test -Dtest=" + s[1]
    ));
                    }

                        return actions; 
                }
                ));

            return onCloseActions;
        }
    }
}
