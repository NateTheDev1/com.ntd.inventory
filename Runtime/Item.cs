using UnityEngine;

namespace InventoryManager.Core
{
    [System.Serializable]
    public enum ItemRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }

    [System.Serializable]
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory System/Item", order = 1)]
    public class Item : ScriptableObject
    {
        public int ID;
        public string Name;
        public string Description;
        public ItemRarity Rarity;
        public float Weight;
        public int maxStackSize;
        public bool isStackable;
        public int SellValue;
        public int BuyValue;
        public bool isSellable;
        public bool isBuyable;
        public bool isDiscardable;
        public Sprite Icon;
    }
}