namespace Ferric.API.EventSystem
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Console = Ferric.API.Wrappers.Console;

    /// <summary>
    /// Extensions for events.
    /// </summary>
    public static class EventExtensions
    {
        /// <summary>
        /// Invokes a method while catching errors.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to invoke.</param>
        /// <param name="args">The action arguments.</param>
        /// <typeparam name="T">The actions type parameter.</typeparam>
        public static void InvokeSafely<T>(this Action<T> action, T args)
            where T : Ferric.API.EventSystem.EventArgs.Interfaces.IEventArg
        {
            if (action is null)
                return;
            var invocationList = action.GetInvocationList();
            if (invocationList.Length == 0)
                return;
            var eventType = EventTypeMappings.GetEventType(action);
            try
            {
                foreach (var subscriber in invocationList)
                {
                    try
                    {
                        subscriber.Method.Invoke(subscriber.Target, new object[] { args });
                    }
                    catch (Exception e)
                    {
                        Assembly assembly = subscriber.Method.DeclaringType?.Assembly;
                        if (assembly is not null && Loader.PluginAssemblies.TryGetValue(assembly, out var plugin))
                        {
                            Console.Error(
                                $"Plugin {plugin.Name} by {plugin.Author} - v{plugin.Version} threw an exception while handling the event {eventType.ToString()}: {e}");
                            continue;
                        }

                        Console.Error(
                            $"Method {subscriber.Method.Name} in class {subscriber.GetType().FullName} threw an exception while handling the event {eventType.ToString()}: {e}");
                    }
                }

                if (EventTypeMappings.ExtendedSubscribersMap.TryGetValue(eventType, out var subscribers))
                {
                    foreach (var tuple in subscribers)
                    {
                        foreach (var methodInfo in tuple.MethodInfos)
                        {
                            try
                            {
                                methodInfo.Invoke(tuple.ClassInstance, new object[] { args });
                            }
                            catch (Exception e)
                            {
                                Assembly assembly = methodInfo.DeclaringType?.Assembly;
                                if (assembly is not null && Loader.PluginAssemblies.TryGetValue(assembly, out var plugin))
                                {
                                    Console.Error(
                                        $"Plugin {plugin.Name} by {plugin.Author} - v{plugin.Version} threw an exception while handling the attribute event {eventType.ToString()}: {e}");
                                    continue;
                                }

                                Console.Error(
                                    $"Method {methodInfo.Name} in class {methodInfo.GetType().FullName} threw an exception while handling the attribute event {eventType.ToString()}: {e}");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Error($"An exception was thrown while invoking the event {eventType.ToString()}: " + e);
            }
        }
    }
}