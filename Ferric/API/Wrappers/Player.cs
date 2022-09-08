namespace Ferric.API.Wrappers
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a player on the server.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// A list of all online players.
        /// </summary>
        public static List<Player> List = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class via a BasePlayer.
        /// </summary>
        /// <param name="basePlayer">The BasePlayer object.</param>
        public Player(BasePlayer basePlayer)
        {
            this.basePlayer = basePlayer;
        }

        /// <summary>
        /// Gets or sets the <see cref="Player"/>s health.
        /// </summary>
        public float Health
        {
            get => basePlayer.health;
            set => basePlayer.health = value;
        }

        /// <summary>
        /// Gets the <see cref="Player"/>s inventory.
        /// </summary>
        public PlayerInventory Inventory => basePlayer.inventory;

        private BasePlayer basePlayer;
    }
}