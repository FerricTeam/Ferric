using System.Collections.Generic;

namespace Ferric.API.Wrappers
{
    /// <summary>
    /// Represents a player on the server.
    /// </summary>
    public class Player
    {
        private BasePlayer _basePlayer;
        
        /// <summary>
        /// A list of all online players.
        /// </summary>
        public static List<Player> List = new();

        /// <summary>
        /// Creates a <see cref="Player"/> instance via a BasePlayer.
        /// </summary>
        /// <param name="basePlayer">The BasePlayer object.</param>
        public Player(BasePlayer basePlayer)
        {
            _basePlayer = basePlayer;
        }

        /// <summary>
        /// Gets or sets the <see cref="Player"/>s health.
        /// </summary>
        public float Health
        {
            get => _basePlayer.health;
            set => _basePlayer.health = value;
        }

        /// <summary>
        /// Gets the <see cref="Player"/>s inventory.
        /// </summary>
        public PlayerInventory Inventory => _basePlayer.inventory;
    }
}