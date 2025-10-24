using plugin.Classes.Actions.OnInvokeTask;
using plugin.Classes.Context;
using plugin.Classes.UI.Containers;

namespace PieMavenPlugin.Tasks
{
    public interface PTask
    {
        Window GetWindow(PluginContext context);
        List<OnWindowOpenAction> OnOpenActions(PluginContext context);
        List<OnWindowCloseAction> OnCloseActions(PluginContext context);
    }
}
