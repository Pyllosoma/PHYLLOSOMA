using System;
using Runtime.Data.ScriptableObjects;
using Runtime.Data.Structure.Items;
using Runtime.Items;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Runtime.UI
{
    public class ItemInfoWindow : UIWindow 
    {
        public static ItemInfoWindow Instance { get; private set; } = null;
        [SerializeField] private Key _closeKey = Key.E;
        [SerializeField] private TextMeshProUGUI _itemName;
        [SerializeField] private Image _itemImage;
        private Action _onItemAcquired = null;
        public ItemInfoWindow() : base() {
            Instance = this;
        }
        public void Init(ItemCode itemCode,Action onItemAcquired = null)
        {
            var item = ItemInfos.Instance.GetItem<Item>(itemCode);
            if (item == null) {
                Debug.LogError($"ItemInfoWindow.Init() : Item with code {itemCode} not found.");
                return;
            }
            _itemName.text = item.Name;
            _itemImage.sprite = item.Sprite;
            var aspectRatio = _itemImage.sprite.rect.width / _itemImage.sprite.rect.height;
            var aspectRatioFitter = _itemImage.GetComponent<AspectRatioFitter>();
            aspectRatioFitter.aspectRatio = aspectRatio;
            _onItemAcquired = onItemAcquired;
            Open(true);
        }
        public void Update()
        {
            if (gameObject.activeSelf && 
                _isOpen && 
                Keyboard.current[_closeKey].wasPressedThisFrame) {
                _onItemAcquired?.Invoke();
                _onItemAcquired = null;
                Open(false);
            }
        }
    }
}