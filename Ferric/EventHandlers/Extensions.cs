using System;
using Ferric.API.EventArgs.Interfaces;
using Console = Ferric.API.Wrappers.Console;

namespace Ferric.EventHandlers
{
    /// <summary>
    /// Extensions for events.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Invokes a method while catching errors.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to invoke.</param>
        /// <param name="args">The action arguments.</param>
        /// <typeparam name="T">The actions type parameter.</typeparam>
        public static void InvokeSafely<T>(this Action<T> action, T args)
            where T : IEventArg
        {
            if (action is null || action.GetInvocationList().Length == 0)
                return;
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