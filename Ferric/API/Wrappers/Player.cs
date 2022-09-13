namespace Ferric.API.Wrappers
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// A wrapper class for <see cref="BasePlayer"/>.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// A list of all online players.
        /// </summary>
        public static List<Player> List = new();

        /// <summary>
        /// Gets the basePlayer instance.
        /// </summary>
        public BasePlayer Base => basePlayer;

        /// <summary>
        /// Gets a value indicating whether the player is connected.
        /// </summary>
        public bool IsConnected => basePlayer.IsConnected;

        /// <summary>
        /// Gets a value indicating whether the player is an NPC.
        /// </summary>
        public bool IsNpc => basePlayer.IsNpc;

        /// <summary>
        /// Gets a value indicating whether the player is an admin.
        /// </summary>
        public bool IsAdmin => basePlayer.IsAdmin;

        /// <summary>
        /// Gets a value indicating whether the player is an developer.
        /// </summary>
        public bool IsDeveloper => basePlayer.IsDeveloper;

        /// <summary>
        /// Gets a value indicating whether the player is an moderator.
        /// </summary>
        public bool IsModerator => ServerUsers.Is(basePlayer.userID, ServerUsers.UserGroup.Moderator);

        /// <summary>
        /// Gets a value indicating whether the player is banned.
        /// </summary>
        public bool IsBanned => ServerUsers.Is(basePlayer.userID, ServerUsers.UserGroup.Banned);

        /// <summary>
        /// Gets a value indicating whether the player is sleeping.
        /// </summary>
        public bool IsSleeping => basePlayer.IsSleeping();

        /// <summary>
        /// Gets a value indicating whether the player is wounded.
        /// </summary>
        public bool IsWounded => basePlayer.IsWounded();

        /// <summary>
        /// Gets the Player's ip address.
        /// </summary>
        public string IP => basePlayer.net.connection.ipaddress;

        /// <summary>
        /// Gets the network ping of the player.
        /// </summary>
        public int Ping => Network.Net.sv.GetAveragePing(basePlayer.net.connection);

        /// <summary>
        /// Gets the players UserId.
        /// </summary>
        public string UserId => basePlayer.UserIDString;

        /// <summary>
        /// Gets the PlayerFlags.
        /// </summary>
        public BasePlayer.PlayerFlags Flags => basePlayer.playerFlags;

        /// <summary>
        /// Gets the <see cref="Player"/>s inventory.
        /// </summary>
        public PlayerInventory Inventory => basePlayer.inventory;

        /// <summary>
        /// Gets or sets the <see cref="Player"/>s health.
        /// </summary>
        public float Health
        {
            get => basePlayer.health;
            set => basePlayer.health = value;
        }

        /// <summary>
        /// Gets or sets the players position.
        /// </summary>
        public Vector3 Position
        {
            get => basePlayer.transform.position;
            set => Teleport(value);
        }

        /// <summary>
        /// Kills the player.
        /// </summary>
        /// <param name="hitInfo">Optional HitInfo.</param>
        public void Kill(HitInfo hitInfo = null)
        {
            basePlayer.Die(hitInfo);
        }

        /// <summary>
        /// Teleports the player to a position.
        /// </summary>
        /// <param name="position">The position to teleport to.</param>
        public void Teleport(Vector3 position)
        {
            basePlayer.EnsureDismounted();
            basePlayer.Teleport(position);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class via a BasePlayer.
        /// </summary>
        /// <param name="basePlayer">The BasePlayer object.</param>
        public Player(BasePlayer basePlayer)
        {
            this.basePlayer = basePlayer;
        }

        private BasePlayer basePlayer;
    }
}