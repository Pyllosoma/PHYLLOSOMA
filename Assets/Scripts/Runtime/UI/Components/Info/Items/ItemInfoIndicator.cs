using System;
using Runtime.Data.ScriptableObjects;
using Runtime.Data.Structure.Items;
using Runtime.Managers;
using Runtime.UI.Components.Animations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI.Components.Info.Items
{
    /// <summary>
    /// 주석
    /// </summary>
    public class ItemInfoIndicator : MonoBehaviour
    {
        [SerializeField] private Sprite _defaultSprite;
        [SerializeField] private Image _itemImage;
        [SerializeField] private UIAnimationSet _effectContainerAnimation;
        [SerializeField] private UIAnimation _descriptionContainerAnimation;
        [SerializeField] private TextMeshProUGUI _itemEffectText;
        [SerializeField] private TextMeshProUGUI _itemDescription;
        public void Init(int itemId)
        {
            #if UNITY_EDITOR
                Debug.Log($"{name} Item Id : {itemId}");
            #endif
            var itemData = ItemInfos.Instance.GetItem<Item>(itemId);
            switch (itemData.Type)
            {
                case ItemType.DEFAULT: case ItemType.USABLE:
                    _effectContainerAnimation.Play();
                    _descriptionContainerAnimation.Play();
                    break;
                case ItemType.WEARABLE:
                    _effectContainerAnimation.Rewind();
                    _descriptionContainerAnimation.Rewind();
                    break;
            }
        }

        private void InternalInitItem(Item itemData)
        {
            _itemImage.sprite = itemData.Sprite;
            _itemEffectText.text = itemData.Description;
            _itemDescription.text = itemData.Description;
        }
        private void OnDisable()
        {
            _itemImage.sprite = _defaultSprite;
            _itemEffectText.text = "";
            _itemDescription.text = "";
        }
    }
}