namespace Ferric.API.Wrappers.Items
{
    /// <summary>
    /// Represents an item.
    /// </summary>
    public class Item
    {
        private global::Item _baseItem;

        /// <summary>
        /// Gets the <see cref="Item"/>s ID.
        /// </summary>
        public int Id => _baseItem.info.itemid;
        
        /// <summary>
        /// Gets the <see cref="Item"/>s owner.
        /// </summary>
        public BasePlayer Owner => _baseItem.GetOwnerPlayer();
    }
}