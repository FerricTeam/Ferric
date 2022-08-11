using System;
using System.Reflection;
using Ferric.API.Interfaces;

namespace Ferric.API.Features
{
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
    }
}