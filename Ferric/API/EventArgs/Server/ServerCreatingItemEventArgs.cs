namespace Ferric.API.EventArgs.Server
{
    using Ferric.API.EventArgs.Interfaces;

    /// <summary>
    /// Represents all the information when an <see cref="Item"/> is created.
    /// </summary>
    public class ServerCreatingItemEventArgs : IDenyable, IEventArg
    {
        /// <summary>
        /// Gets or sets a value indicating whether or not the item will be created
        /// </summary>
        public bool Allowed { get; set; }

        /// <summary>
        /// Gets or sets the ItemDefinition.
        /// </summary>
        public ItemDefinition ItemDefinition { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Gets or sets the skin.
        /// </summary>
        public ulong Skin { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerCreatingItemEventArgs"/> class.
        /// </summary>
        public ServerCreatingItemEventArgs(ItemDefinition definition, int amount, ulong skin)
        {
            Allowed = true;
            ItemDefinition = definition;
            Amount = amount;
            Skin = skin;
        }
    }
}