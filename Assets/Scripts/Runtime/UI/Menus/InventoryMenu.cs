using System;
using System.Collections.Generic;
using Runtime.Data.Structure.Items;
using Runtime.Managers;
using Runtime.UI.Components.Info.Items;
using UnityEngine;

namespace Runtime.UI.Menus
{
    public class InventoryMenu : MonoBehaviour
    {
        [SerializeField] private ItemInfoIndicator _itemInfoIndicator;
        [SerializeField] private Transform _inventoryItemContainer;
        [SerializeField] private GameObject _inventoryItemPrefab;
        [SerializeField] private List<InventoryItemButton> _cache = new List<InventoryItemButton>();
        public void OnInventoryItemButtonClicked(int itemId)
        {
            //Debug.Log($"Item {itemId} clicked.");
            _itemInfoIndicator.Init(itemId);
        }
        private void OnEnable()
        {
            int count = 0;
            
            foreach (var data in DataManager.Instance.PlayerData.Inventory) {
                //Need to change later
                var itemData = DataManager.Instance.Items.GetItem<WearableItem>(data.Key);
                if (itemData == default) {
                    Debug.LogError("Data type is not matched.");
                    continue;
                }
                int i = 0;
                for (i = 0; i < data.Value && count < _cache.Count; i++, count++) {
                    _cache[count].Init(data.Key,this);
                }
                for (; count < data.Value; count++) {
                    InventoryItemButton button = Instantiate(_inventoryItemPrefab, _inventoryItemContainer).GetComponent<InventoryItemButton>();
                    button.Init(data.Key,this);
                    _cache.Add(button);
                }
            }
            for (int i = count; i < _cache.Count; i++) {
                _cache[i].gameObject.SetActive(false);
            }
        }
        private void OnDisable()
        {
            foreach (var item in _cache) {
                item.gameObject.SetActive(false);
            }
        }
    }
}