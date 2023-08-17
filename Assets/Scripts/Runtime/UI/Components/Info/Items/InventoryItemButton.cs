using System;
using Runtime.Data.Structure.Items;
using Runtime.Managers;
using Runtime.UI.Menus;
using TMPro;
using UnityEngine;

namespace Runtime.UI.Components.Info.Items
{
    public class InventoryItemButton : MonoBehaviour
    {
        [SerializeField] private int _itemId = -1;
        [SerializeField] private TextMeshProUGUI _itemName;
        private InventoryMenu _parent;
        public void Init(int itemId,InventoryMenu parent)
        {
            _parent = parent;
            _itemId = itemId;
            var itemData = DataManager.Instance.Items.GetItem<Item>(itemId);
            _itemName.text = itemData.Name;
            gameObject.SetActive(true);
        }
        public void OnClick(){
            _parent.OnInventoryItemButtonClicked(_itemId);
        }
        private void OnDisable()
        {
            _itemId = -1;
        }
    }
}