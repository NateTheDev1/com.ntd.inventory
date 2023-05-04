
namespace InventoryManager.Core
{
    public class ItemInstance
    {
        public Item item { get; private set; }
        public int amount;

        public ItemInstance(Item item)
        {
            this.item = item;
        }
    }
}