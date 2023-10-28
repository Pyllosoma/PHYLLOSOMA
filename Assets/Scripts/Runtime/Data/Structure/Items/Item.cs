using System;
using Runtime.Items;
using UnityEngine;
using UnityEngine.Localization;

namespace Runtime.Data.Structure.Items
{
    /// <summary>
    /// Base item data class for all items in the game.
    /// </summary>
    [Serializable]
    public class Item
    {
        public ItemCode Code => _itemCode;
        public ItemType Type => _itemType;
        public Sprite Sprite => _sprite;
        public string Name => _name.GetLocalizedString();
        public string Description => _description.GetLocalizedString();
        public int Price => _price;
        [Header("Item Info")]
        //[SerializeField] private int _id = 0;
        [SerializeField] private ItemCode _itemCode = ItemCode.DEFAULT;
        [SerializeField] private ItemType _itemType = ItemType.DEFAULT;
        [SerializeField] private Sprite _sprite = null;
        [SerializeField] private LocalizedString _name = null;
        [SerializeField] private LocalizedString _description = null;
        [SerializeField] private int _price = 0;
        public void InitItem(ItemType itemType){
            _itemType = itemType;
        }
    }
}