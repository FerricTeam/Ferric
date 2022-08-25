namespace Ferric.API.Features
{
    using System;
    using System.Collections;
    using System.Reflection;
    using Interfaces;
    using Console = Wrappers.Console;

    public abstract class Plugin : IPlugin
    {
        /// <inheritdoc />
        public virtual Assembly Assembly { get; set; }
        
        /// <inheritdoc />
        public abstract string ID { get; }
        
        /// <inheritdoc />
        public virtual string Author { get; }
        
        /// <inheritdoc />
        public abstract string Name { get; }
        
        /// <inheritdoc />
        public virtual Version Version { get; }
        
        /// <inheritdoc />
        public virtual Version RequiredFerricVersion { get; }

        /// <inheritdoc />
        public virtual Config Config { get; set; } = new Config();

        /// <inheritdoc />
        public virtual bool IsModded { get; } = true;
        
        /// <inheritdoc />
        public virtual void OnEnabled()
        {
        }

        /// <inheritdoc />
        public virtual void OnDisabled()
        {
        }
        
        public void CallDelayed(float seconds, Action action)
        {
            ServerMgr.Instance.StartCoroutine(WaitForSecondsCoroutine(seconds, action));
        }
        
        private IEnumerator WaitForSecondsCoroutine(float seconds, Action action)
        {
            yield return UnityEngine.CoroutineEx.waitForSeconds(seconds);
            try
            {
                action();
            }
            catch (Exception e)
            {
                Console.Error($"Plugin {Name} by {Author} - v{Version} threw an exception in a delayed call: {e}");
            }
        }
    }
}