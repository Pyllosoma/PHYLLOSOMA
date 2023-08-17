using System;
using Runtime.Data.Structure.Items;
using Runtime.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI.Components.Info.Items
{
    public class ItemInfoIndicator : MonoBehaviour
    {
        [SerializeField] private Sprite _defaultSprite;
        [SerializeField] private Image _itemImage;
        [SerializeField] private TextMeshProUGUI _itemEffectText;
        [SerializeField] private TextMeshProUGUI _itemDescription;
        public void Init(int itemId)
        {
            var itemData = DataManager.Instance.Items.GetItem<Item>(itemId);
            //Need to change later
            _itemEffectText.text = "";
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