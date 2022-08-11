using System;
using Ferric.API.EventArgs.Interfaces;
using Console = Ferric.API.Wrappers.Console;

namespace Ferric.EventHandlers
{
    public static class Extensions
    {
        public static void InvokeSafely<T>(this Action<T> action, T args)
            where T : IEventArg
        {
            foreach (var subscriber in action.GetInvocationList())
            {
                try
                {
                    subscriber.Method.Invoke(subscriber.Target, new object[]{args});
                }
                catch (Exception e)
                {
                    var assembly = subscriber.Method.DeclaringType?.Assembly;
                    if (assembly is not null && Loader.PluginAssemblies.TryGetValue(assembly, out var plugin))
                    {
                        Console.Error($"Plugin {plugin.Name} by {plugin.Author} - v{plugin.Version} threw an exception while handling the event {action.GetType().GenericTypeArguments[0].FullName ?? "Unknown"}: {e}");
                        continue;
                    }
                    Console.Error($"Method {subscriber.Method.Name} in class {subscriber.Method.GetType().FullName} threw an exception while handling the event {action.GetType().GenericTypeArguments[0].FullName ?? "Unknown"}: {e}");
                }
            }
        }
    }
}