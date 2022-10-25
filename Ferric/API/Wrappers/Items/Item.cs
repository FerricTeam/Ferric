namespace Ferric.API.Wrappers.Items
{
    /// <summary>
    /// A wrapper class for <see cref="global::Item"/>.
    /// </summary>
    public class Item
    {
        private global::Item baseItem;

        /// <summary>
        /// Gets the item instance.
        /// </summary>
        public global::Item Base => baseItem;

        /// <summary>
        /// Gets the <see cref="Item"/>s ID.
        /// </summary>
        public int Id => baseItem.info.itemid;

        /// <summary>
        /// Gets the <see cref="Item"/>s owner.
        /// </summary>
        public BasePlayer Owner => baseItem.GetOwnerPlayer();

        /// <summary>
        /// Gets an Item from an <see cref="Item"/> instance.
        /// </summary>
        /// <param name="item">The basegame item object.</param>
        /// <returns>A new item wrapper instance.</returns>
        public static Item Get(global::Item item)
        {
            return new Item(item);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="item">The baseItem.</param>
        public Item(global::Item item)
        {
            baseItem = item;
        }
    }
}