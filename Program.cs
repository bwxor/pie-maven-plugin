using PieMavenPlugin;
using plugin.Classes;
using plugin.Classes.Actions;
using plugin.Classes.UI.Containers;
using System.ComponentModel;

namespace PieMavenPlugin
{
    public class Program : Plugin
    {
        public string DisplayName()
        {
            return "Maven";
        }

        public List<Window> GetWindows()
        {
            List<Window> windows = new List<Window>();

            windows.Add(NewMavenProject());
            windows.Add(BuildMavenProject());
            windows.Add(RunMavenClass());

            return windows;
        }

        private Window NewMavenProject()
        {
            Window window = new Window("New Maven Project", 400);

            window.Controls.Add(new plugin.Classes.UI.Decoration.Label("parentDirectoryLabel", "Parent Directory:"));
            window.Controls.Add(new plugin.Classes.UI.Composite.FolderBrowser("folderBrowser"));
            window.Controls.Add(new plugin.Classes.UI.Decoration.SpaceDelimiter());
            
            window.Controls.Add(new plugin.Classes.UI.Decoration.Label("groupIdLabel", "Group ID:"));
            window.Controls.Add(new plugin.Classes.UI.ValueContainers.TextBox("groupIdTextBox"));
            window.Controls.Add(new plugin.Classes.UI.Decoration.SpaceDelimiter());

            window.Controls.Add(new plugin.Classes.UI.Decoration.Label("artifactIdLabel", "Artifact ID:"));
            window.Controls.Add(new plugin.Classes.UI.ValueContainers.TextBox("artifactIdTextBox"));
            window.Controls.Add(new plugin.Classes.UI.Decoration.SpaceDelimiter());

            window.Controls.Add(new plugin.Classes.UI.Decoration.Label("versionLabel", "Version:"));
            window.Controls.Add(new plugin.Classes.UI.ValueContainers.TextBox("versionTextBox"));
            window.Controls.Add(new plugin.Classes.UI.Decoration.SpaceDelimiter());

            window.Controls.Add(new plugin.Classes.UI.Buttons.CheckButton("includeTestingFrameworkCheckButton", "Include testing framework"));


            window.OnClose = NewMavenProjectOnCloseActions();

            return window;

        }

        private Window BuildMavenProject()
        {
            Window window = new Window("Build Maven Project", 250);
            window.Controls.Add(new plugin.Classes.UI.Decoration.Label("pomLocationLabel", "POM Location:"));
            window.Controls.Add(
                new plugin.Classes.UI.Composite.FileBrowser("fileBrowserControl", "Maven Project Files (*.xml)|*.xml|All Files (*.*)|*.*")
            );
            window.Controls.Add(new plugin.Classes.UI.Decoration.SpaceDelimiter());

            window.Controls.Add(new plugin.Classes.UI.Decoration.Label("phasesLabel", "Phases: "));
            window.Controls.Add(new plugin.Classes.UI.ValueContainers.TextBox("phasesTextBox"));

            return window;
        }

        private Window RunMavenClass()
        {
            Window window = new Window("Run Maven Class", 300);

            window.Controls.Add(new plugin.Classes.UI.Decoration.Label("pomLocationLabel", "POM Location:"));
            window.Controls.Add(
                new plugin.Classes.UI.Composite.FileBrowser("fileBrowserControl", "Maven Project Files (*.xml)|*.xml|All Files (*.*)|*.*")
            );
            window.Controls.Add(new plugin.Classes.UI.Decoration.SpaceDelimiter());

            window.Controls.Add(new plugin.Classes.UI.Decoration.Label("classNameLabel", "Class Name: "));
            window.Controls.Add(new plugin.Classes.UI.ValueContainers.TextBox("classNameTextBox"));
            window.Controls.Add(new plugin.Classes.UI.Decoration.SpaceDelimiter());

            window.Controls.Add(new plugin.Classes.UI.Decoration.Label("classLocationLabel", "Class Location: "));
            window.Controls.Add(new plugin.Classes.UI.ValueContainers.ComboBox("classLocationComboBox", new List<string>{ "main", "test"}));

            return window;
        }

        private List<ExitAction> NewMavenProjectOnCloseActions()
        {
            List<ExitAction> onCloseActions = new List<ExitAction>();

            onCloseActions.Add(new ValidationAction("folderBrowser", s => !string.IsNullOrEmpty(s.Trim()), "Input fields cannot be empty."));
            onCloseActions.Add(new ValidationAction("groupIdTextBox", s => !string.IsNullOrEmpty(s.Trim()), "Input fields cannot be empty."));
            onCloseActions.Add(new ValidationAction("artifactIdTextBox", s => !string.IsNullOrEmpty(s.Trim()), "Input fields cannot be empty."));
            onCloseActions.Add(new ValidationAction("versionTextBox", s => !string.IsNullOrEmpty(s.Trim()), "Input fields cannot be empty."));
            onCloseActions.Add(new ValidationAction("groupIdTextBox", s => s.Any(c => !Char.IsNumber(c) && !Char.IsLetter(c) && c != '.'), "Group Id can only contain letters, numbers and dots."));
            onCloseActions.Add(new ValidationAction("groupIdTextBox", s => s.StartsWith(".") || s.EndsWith("."), "Group Id cannot start or end with a dot."));
            onCloseActions.Add(new ValidationAction("artifactIdTextBox", s => s.Any(c => !Char.IsLetter(c) && !Char.IsNumber(c) && c != '-'), "Artifact Id can only contain letters, numbers and dashes."));
            onCloseActions.Add(new ValidationAction("artifactIdTextBox", s => s.StartsWith("-") || s.EndsWith("-"), "Artifact Id cannot start or end with a dash."));
            onCloseActions.Add(new ValidationAction("versionTextBox", s => s.Any(x => !Char.IsNumber(x) && !Char.IsLetter(x) && x != '.' && x != '+' && x != '-'), "Version can only contain letters, numbers, dots, dashes and plus symbols."));
            onCloseActions.Add(new ValidationAction("versionTextBox", s => s.StartsWith(".") || s.EndsWith(".") || s.StartsWith("+") || s.EndsWith("+") || s.StartsWith("-") || s.EndsWith("-"), "Version cannot start or end with a dot, dash or plus."));

            return onCloseActions;
        }
    }
}