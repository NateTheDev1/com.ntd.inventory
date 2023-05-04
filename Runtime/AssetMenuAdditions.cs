using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UIElements;
using System.IO;

namespace InventoryManager.Core
{
    public class AssetMenuAdditions
    {
        [MenuItem("GameObject/Inventory System/Inventory Manager")]
        public static void CreateMyScriptableObject()
        {
            GameObject inventory_manager = new GameObject("Inventory Manager");
            inventory_manager.AddComponent<InventoryManagerAdapter>();
            inventory_manager.AddComponent<UIDocument>();
        }
    }
}