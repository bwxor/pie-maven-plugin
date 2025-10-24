using plugin.Classes.Actions;
using plugin.Classes.Context;
using plugin.Classes.UI.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieMavenPlugin.Tasks
{
    public interface PTask
    {
        Window GetWindow(PluginContext context);
        List<UIAction> OnOpenActions(PluginContext context);
        List<ExitAction> OnCloseActions(PluginContext context);
    }
}
