
using PieMavenPlugin.Tasks;
using plugin.Classes.Actions;
using plugin.Classes.Actions.OnInvokeTask;
using plugin.Classes.Context;
using plugin.Classes.UI.Containers;

namespace PieMavenPlugin.Windows
{
    public class NewMavenProjectTask : PTask
    {
        public Window GetWindow(PluginContext context)
        {
            string groupId = context.Custom.ContainsKey("pie-maven-plugin/groupId") ? context.Custom["pie-maven-plugin/groupId"] : "com.example";
            string artifactId = context.Custom.ContainsKey("pie-maven-plugin/artifactId") ? context.Custom["pie-maven-plugin/artifactId"] : "maven-project";
            string version = context.Custom.ContainsKey("pie-maven-plugin/version") ? context.Custom["pie-maven-plugin/version"] : "1.0.0";

            Window window = new Window("New Maven Project");

            window.Controls.Add(new plugin.Classes.UI.Decoration.Label("parentDirectoryLabel", "Parent Directory:"));
            window.Controls.Add(new plugin.Classes.UI.Composite.FolderBrowser("folderBrowser"));
            window.Controls.Add(new plugin.Classes.UI.Decoration.SpaceDelimiter());

            window.Controls.Add(new plugin.Classes.UI.Decoration.Label("groupIdLabel", "Group ID:"));
            window.Controls.Add(new plugin.Classes.UI.ValueContainers.TextBox("groupIdTextBox", groupId));
            window.Controls.Add(new plugin.Classes.UI.Decoration.SpaceDelimiter());

            window.Controls.Add(new plugin.Classes.UI.Decoration.Label("artifactIdLabel", "Artifact ID:"));
            window.Controls.Add(new plugin.Classes.UI.ValueContainers.TextBox("artifactIdTextBox", artifactId));
            window.Controls.Add(new plugin.Classes.UI.Decoration.SpaceDelimiter());

            window.Controls.Add(new plugin.Classes.UI.Decoration.Label("versionLabel", "Version:"));
            window.Controls.Add(new plugin.Classes.UI.ValueContainers.TextBox("versionTextBox", version));
            window.Controls.Add(new plugin.Classes.UI.Decoration.SpaceDelimiter());

            window.Controls.Add(new plugin.Classes.UI.Buttons.CheckButton("includeTestingFrameworkCheckButton", "Include testing framework"));

            window.OnOpen = OnOpenActions(context);
            window.OnClose = OnCloseActions(context);

            return window;
        }

        public List<OnWindowOpenAction> OnOpenActions(PluginContext context)
        {
            List<OnWindowOpenAction> onOpenActions = new List<OnWindowOpenAction>();

            return onOpenActions;
        }

        public List<OnWindowCloseAction> OnCloseActions(PluginContext context)
        {
            List<OnWindowCloseAction> onCloseActions = new List<OnWindowCloseAction>();

            onCloseActions.Add(new ValidationAction("folderBrowser", s => string.IsNullOrEmpty(s.Trim()), "Input fields cannot be empty."));
            onCloseActions.Add(new ValidationAction("groupIdTextBox", s => string.IsNullOrEmpty(s.Trim()), "Input fields cannot be empty."));
            onCloseActions.Add(new ValidationAction("artifactIdTextBox", s => string.IsNullOrEmpty(s.Trim()), "Input fields cannot be empty."));
            onCloseActions.Add(new ValidationAction("versionTextBox", s => string.IsNullOrEmpty(s.Trim()), "Input fields cannot be empty."));
            onCloseActions.Add(new ValidationAction("groupIdTextBox", s => s.Any(c => !Char.IsNumber(c) && !Char.IsLetter(c) && c != '.'), "Group Id can only contain letters, numbers and dots."));
            onCloseActions.Add(new ValidationAction("groupIdTextBox", s => s.StartsWith(".") || s.EndsWith("."), "Group Id cannot start or end with a dot."));
            onCloseActions.Add(new ValidationAction("artifactIdTextBox", s => s.Any(c => !Char.IsLetter(c) && !Char.IsNumber(c) && c != '-'), "Artifact Id can only contain letters, numbers and dashes."));
            onCloseActions.Add(new ValidationAction("artifactIdTextBox", s => s.StartsWith("-") || s.EndsWith("-"), "Artifact Id cannot start or end with a dash."));
            onCloseActions.Add(new ValidationAction("versionTextBox", s => s.Any(x => !Char.IsNumber(x) && !Char.IsLetter(x) && x != '.' && x != '+' && x != '-'), "Version can only contain letters, numbers, dots, dashes and plus symbols."));
            onCloseActions.Add(new ValidationAction("versionTextBox", s => s.StartsWith(".") || s.EndsWith(".") || s.StartsWith("+") || s.EndsWith("+") || s.StartsWith("-") || s.EndsWith("-"), "Version cannot start or end with a dot, dash or plus."));

            onCloseActions.Add(new StoreInContextAction("pie-maven-plugin/groupId", "${controls.groupIdTextBox}"));
            onCloseActions.Add(new StoreInContextAction("pie-maven-plugin/artifactId", "${controls.artifactIdTextBox}"));
            onCloseActions.Add(new StoreInContextAction("pie-maven-plugin/version", "${controls.versionTextBox}"));
            onCloseActions.Add(new StoreInContextAction("pie-maven-plugin/pomDirectory", "${controls.folderBrowser}\\${controls.artifactIdTextBox}"));
            onCloseActions.Add(new StoreInContextAction("pie-maven-plugin/className", "${controls.groupIdTextBox}.Main"));

            List<OnWindowCloseAction> actionsToAdd = CreateMavenStructure();

            foreach (OnWindowCloseAction action in actionsToAdd)
            {
                onCloseActions.Add(action);
            }

            return onCloseActions;
        }

        private List<OnWindowCloseAction> CreateMavenStructure()
        {
            List<OnWindowCloseAction> actions = new List<OnWindowCloseAction>();

            actions.Add(CreateDirectoryFromParent("${controls.artifactIdTextBox}"));
            actions.Add(CreateDirectoryFromParent("${controls.artifactIdTextBox}" + "\\src"));
            actions.Add(CreateDirectoryFromParent("${controls.artifactIdTextBox}" + "\\src\\main"));
            actions.Add(CreateDirectoryFromParent("${controls.artifactIdTextBox}" + "\\src\\main\\java"));
            actions.Add(CreateDirectoryFromParent("${controls.artifactIdTextBox}" + "\\src\\main\\resources"));
            actions.Add(CreateDirectoryFromParent("${controls.artifactIdTextBox}" + "\\src\\test"));
            actions.Add(CreateDirectoryFromParent("${controls.artifactIdTextBox}" + "\\src\\test\\java"));
            actions.Add(CreateDirectoryFromParent("${controls.artifactIdTextBox}" + "\\src\\test\\resources"));

            actions.Add(BuildJavaMainClassFromParent());

            actions.Add(new GeneratorAction(
                new string[] { "${controls.includeTestingFrameworkCheckButton}", "${controls.groupIdTextBox}", "${controls.artifactIdTextBox}" },
                (s) =>
                {
                    List<OnWindowCloseAction> actions = new List<OnWindowCloseAction>();

                    if (s[0] == "true")
                    {
                        string javaPackagePath = s[2] + "\\src\\test\\java";
                        string[] groupIdSplitByDot = s[1].Split('.');

                        for (int i = 0; i < groupIdSplitByDot.Length; i++)
                        {
                            javaPackagePath += "\\" + groupIdSplitByDot[i];
                            actions.Add(CreateDirectoryFromParent(javaPackagePath));
                        }

                        actions.Add(CreateFileFromParent(javaPackagePath + "\\MainTest.java", "package " + s[1] + ";\n\n" + ResourceReader.ReadResource("PieMavenPlugin.Templates.MainTest.java")));
                        actions.Add(CreateTestPomFileFromParent());
                    }
                    else
                    {
                        actions.Add(CreatePomFileFromParent());
                    }

                        return actions;
                }));

            actions.Add(SelectProjectDirectoryFromParent());

            return actions;
        }

        private CreateDirectoryAction CreateDirectoryFromParent(string name)
        {
            return new CreateDirectoryAction("${controls.folderBrowser}" + "\\" + name);
        }

        private GeneratorAction BuildJavaMainClassFromParent()
        {
           return new GeneratorAction(new string[] { "${controls.groupIdTextBox}", "${controls.artifactIdTextBox}" }, (s) =>
            {
                string javaPackagePath = s[1] + "\\src\\main\\java";
                string[] groupIdSplitByDot = s[0].Split('.');

                List<OnWindowCloseAction> actions = new List<OnWindowCloseAction>();
                for (int i = 0; i < groupIdSplitByDot.Length; i++)
                {
                    javaPackagePath += "\\" + groupIdSplitByDot[i];
                    actions.Add(CreateDirectoryFromParent(javaPackagePath));
                }

                actions.Add(CreateFileFromParent(javaPackagePath + "\\Main.java", "package " + s[0] + ";\n\n" + ResourceReader.ReadResource("PieMavenPlugin.Templates.Main.java")));
                actions.Add(OpenTabFromParent(javaPackagePath + "\\Main.java"));

                return actions;
            });
        }

        private CreateFileAction CreatePomFileFromParent()
        {
            string pom = ResourceReader.ReadResource("PieMavenPlugin.Templates.pom.xml");
            return new CreateFileAction("${controls.folderBrowser}" + "\\" + "${controls.artifactIdTextBox}" + "\\pom.xml", pom.Replace("REPLACE_GROUP_ID", "${controls.groupIdTextBox}").Replace("REPLACE_ARTIFACT_ID", "${controls.artifactIdTextBox}").Replace("REPLACE_VERSION", "${controls.versionTextBox}"));
        }

        private CreateFileAction CreateTestPomFileFromParent()
        {
            string pom = ResourceReader.ReadResource("PieMavenPlugin.Templates.pom-test.xml");
            return CreateFileFromParent("${controls.artifactIdTextBox}" + "\\pom.xml", pom.Replace("REPLACE_GROUP_ID", "${controls.groupIdTextBox}").Replace("REPLACE_ARTIFACT_ID", "${controls.artifactIdTextBox}").Replace("REPLACE_VERSION", "${controls.versionTextBox}"));
        }

        private CreateFileAction CreateFileFromParent(string name, string content)
        {
            CreateFileAction createFileAction = new CreateFileAction("${controls.folderBrowser}" + "\\" + name, content);
            return createFileAction;
        }

        private OpenFileAction OpenTabFromParent(string path)
        {
            OpenFileAction openFileAction = new OpenFileAction("${controls.folderBrowser}" + "\\" + path);
            return openFileAction;
        }

        private SelectDirectoryAction SelectProjectDirectoryFromParent()
        {
            SelectDirectoryAction selectDirectoryAction = new SelectDirectoryAction("${controls.folderBrowser}" + "\\" + "${controls.artifactIdTextBox}");
            return selectDirectoryAction;
        }

    }
}
