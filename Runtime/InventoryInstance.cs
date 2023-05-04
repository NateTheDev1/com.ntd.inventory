using System.Collections.Generic;
using UnityEngine;

namespace InventoryManager.Core
{
    public class InventoryInstance
    {
        public int ID;
        public List<ItemInstance> _items { get; private set; }
        public float WeightCapacity { get; private set; }
        public int MaxItemSlots { get; private set; }
        public bool useWeight { get; private set; }
        public bool useMaxItemSlots { get; private set; }
        public InventoryInstance(float weightCapacity, int maxItemSlots, bool useWeight, bool useMaxItemSlots)
        {
            this._items = new List<ItemInstance>();
            WeightCapacity = weightCapacity;
            MaxItemSlots = maxItemSlots;
            this.useWeight = useWeight;
            this.useMaxItemSlots = useMaxItemSlots;
        }

        public bool AddItem(int itemID)
        {
            Item item = InventoryManagerAdapter.Instance.GetItem(itemID);

            // Will weight be an issue?
            if (useWeight && item.Weight + GetTotalWeightOfAllItems() > WeightCapacity)
            {
                return false;
            }

            bool alreadyInInventory = CheckIfItemIsInInventory(item.ID);

            // Is it over the max item slots?
            if (useMaxItemSlots && _items.Count + 1 > MaxItemSlots && !alreadyInInventory)
            {
                return false;
            }

            // Can we stack it or  not
            if (alreadyInInventory && item.isStackable)
            {
                int currentIndex = _items.FindIndex(instance => instance.item.ID == item.ID);
                if (_items[currentIndex] != null && _items[currentIndex].amount < item.maxStackSize)
                {
                    _items[currentIndex].amount++;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                _items.Add(new ItemInstance(item));
                return true;
            }

            return false;
        }

        private bool CheckIfItemIsInInventory(int itemID)
        {
            bool alreadyInInventory = false;

            foreach (ItemInstance instance in _items)
            {
                if (instance.item.ID == itemID)
                {
                    alreadyInInventory = true;
                    break;
                }
            }

            return alreadyInInventory;
        }

        private float GetTotalWeightOfAllItems()
        {
            float totalWeight = 0;
            foreach (ItemInstance item in _items)
            {
                totalWeight += item.item.Weight;
            }

            return totalWeight;
        }

        public bool RemoveItem(int itemID, int quantity = 1)
        {
            bool alreadyInInventory = CheckIfItemIsInInventory(itemID);

            if (alreadyInInventory)
            {
                int currentIndex = _items.FindIndex(instance => instance.item.ID == itemID);

                // Check current stack size
                if (_items[currentIndex].amount > 1)
                {
                    _items[currentIndex].amount -= quantity;
                    return true;
                }
                else
                {
                    _items.RemoveAt(currentIndex);
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}