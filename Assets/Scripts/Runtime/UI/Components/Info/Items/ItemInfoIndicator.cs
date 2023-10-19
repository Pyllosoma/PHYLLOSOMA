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
        [SerializeField] private bool _isExpanded = false;
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
                    if (!_isExpanded) {
                        _effectContainerAnimation.Play();
                        _descriptionContainerAnimation.Play();
                        _itemEffectText.text = "";
                        _isExpanded = true;
                    }
                    break;
                case ItemType.WEARABLE:
                    if (_isExpanded) {
                        _effectContainerAnimation.Rewind();
                        _descriptionContainerAnimation.Rewind();
                        _isExpanded = false;
                    }
                    break;
            }
            InternalInitItem(itemData);
        }

        private void InternalInitItem(Item itemData)
        {
            _itemImage.sprite = itemData.Sprite;
            var aspectRatio = _itemImage.sprite.rect.width / _itemImage.sprite.rect.height;
            AspectRatioFitter fitter = _itemImage.GetComponent<AspectRatioFitter>();
            fitter.aspectRatio = aspectRatio;
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