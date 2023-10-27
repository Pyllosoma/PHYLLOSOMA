using System;
using Runtime.Data.ScriptableObjects;
using Runtime.Data.Structure.Items;
using Runtime.Items;
using Runtime.Managers;
using Runtime.UI.Menus;
using TMPro;
using UnityEngine;

namespace Runtime.UI.Components.Info.Items
{
    public class InventoryItemButton : MonoBehaviour
    {
        [SerializeField] private ItemCode _itemCode = ItemCode.DEBUG;
        [SerializeField] private TextMeshProUGUI _itemName;
        private InventoryMenu _parent;
        public void Init(ItemCode itemCode,InventoryMenu parent)
        {
            #if UNITY_EDITOR
                Debug.Log($"{name} Item Code : {itemCode}");
            #endif
            _parent = parent;
            _itemCode = itemCode;
            var itemData = ItemInfos.Instance.GetItem<Item>(itemCode);
            _itemName.text = itemData.Name;
            gameObject.SetActive(true);
        }
        public void OnClick(){
            _parent.OnInventoryItemButtonClicked(_itemCode);
        }
        private void OnDisable()
        {
            _itemCode = ItemCode.DEBUG;
        }
    }
}