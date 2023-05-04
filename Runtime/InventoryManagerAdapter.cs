using UnityEngine;
using System;
using UnityEngine.UIElements;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using InventoryManager.Utility;


namespace InventoryManager.Core
{
    public class InventoryManagerAdapter : MonoSingleton<InventoryManagerAdapter>
    {
        private Dictionary<int, InventoryInstance> _inventoryDB = new Dictionary<int, InventoryInstance>();
        private Dictionary<int, Item> _itemDB = new Dictionary<int, Item>();
        private UIDocument _uiDocument;
        private InventoryUI _inventoryUI;
        public bool useDefaultUI = true;
        public string itemResourcePath;
        public StyleSheet rootStyleSheet;
        public List<StyleSheet> customStylesheets = new List<StyleSheet>();

        private void Start()
        {
            if (itemResourcePath.Length == 0)
            {
                throw new System.Exception("Item Resource Path is empty");
            }

            CreateItemDB();

            if (useDefaultUI)
            {
                HandleUISetup();
            }
        }

        private void CreateItemDB()
        {
            List<Item> items = new List<Item>(Resources.LoadAll<Item>(itemResourcePath));

            foreach (Item item in items)
            {
                _itemDB.Add(item.ID, item);
            }
        }

        public void OpenInventory(int inventoryID)
        {
            if (_inventoryUI != null)
            {
                _inventoryUI.OpenInventory(_inventoryDB[inventoryID]);
            }
        }

        public void CloseInventory()
        {
            if (_inventoryUI != null)
            {
                _inventoryUI.Close();
            }
        }

        private void HandleUISetup()
        {
            _inventoryUI = new InventoryUI();
            _uiDocument = GetComponent<UIDocument>();

            if (_uiDocument == null)
            {
                Debug.LogError("UIDocument component not found on Inventory Manager");
                return;
            }

            _inventoryUI.Create(ref _uiDocument);

            if (rootStyleSheet != null)
            {
                _uiDocument.rootVisualElement.styleSheets.Add(rootStyleSheet);
            }

            foreach (StyleSheet sheet in customStylesheets)
            {
                _uiDocument.rootVisualElement.styleSheets.Add(sheet);
            }
        }

        public Item GetItem(int ID)
        {
            if (_itemDB.TryGetValue(ID, out Item item))
            {
                return item;
            }
            else
            {
                return null;
            }
        }

        public void AddElements(VisualElement[] elements)
        {
            _inventoryUI.AddElements(elements);
        }

        public void AddInventory(InventoryInstance inventory)
        {
            _inventoryDB.Add(inventory.ID, inventory);
        }

        public void RemoveInventory(int inventoryID)
        {
            _inventoryDB.Remove(inventoryID);
        }

        public InventoryInstance GetInventory(int inventoryID)
        {
            if (_inventoryDB.TryGetValue(inventoryID, out InventoryInstance inventory))
            {
                return inventory;
            }
            else
            {
                return null;
            }
        }
    }
}