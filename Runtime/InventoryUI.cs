using UnityEngine.UIElements;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;

namespace InventoryManager.Core
{
    public class InventoryUI
    {
        private UIDocument root;

        private VisualElement inventoryRoot;

        private Dictionary<string, VisualElement> inventoryElements = new Dictionary<string, VisualElement>();

        public void Create(ref UIDocument root)
        {
            this.root = root;

            inventoryRoot = CreateDiv("inventoryRoot");
            this.root.rootVisualElement.Add(inventoryRoot);

            CreateUI();
        }

        private void CreateUI()
        {
            inventoryRoot.style.display = DisplayStyle.None;

            var inventoryWindow = CreateDiv("inventoryWindow");
            inventoryRoot.Add(inventoryWindow);

            var inventoryHeader = CreateDiv("inventoryHeader");
            inventoryWindow.Add(inventoryHeader);

            var inventoryTitle = new Label("Inventory");
            inventoryHeader.Add(inventoryTitle);

            var itemContainer = CreateDiv("itemContainer");
            inventoryWindow.Add(itemContainer);

            var closeButton = new Button
            {
                text = "X"
            };

            closeButton.RegisterCallback<ClickEvent>(ev =>
            {
                Close();
            });

            inventoryHeader.Add(closeButton);

            inventoryElements.Add("inventoryWindow", inventoryWindow);
            inventoryElements.Add("inventoryHeader", inventoryHeader);
            inventoryElements.Add("inventoryTitle", inventoryTitle);
            inventoryElements.Add("closeButton", closeButton);
            inventoryElements.Add("itemContainer", itemContainer);
        }

        public void Close()
        {
            inventoryRoot.style.display = DisplayStyle.None;
        }

        public void AddElements(VisualElement[] elements)
        {
            foreach (VisualElement element in elements)
            {
                inventoryRoot.Add(element);
            }
        }

        public void OpenInventory(InventoryInstance inventory)
        {
            VisualElement itemContainer = inventoryElements["itemContainer"] as VisualElement;
            itemContainer.Clear();

            inventoryRoot.style.display = DisplayStyle.Flex;

            foreach (ItemInstance item in inventory._items)
            {
                var itemImage = new Image();
                itemImage.sprite = item.item.Icon;
                itemContainer.Add(itemImage);
            }
        }

        public static VisualElement CreateDiv(string name)
        {
            var div = new VisualElement { name = name };
            return div;
        }
    }
}