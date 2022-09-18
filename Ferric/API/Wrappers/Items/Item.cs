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
    }
}