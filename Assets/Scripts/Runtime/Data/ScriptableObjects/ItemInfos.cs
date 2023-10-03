using System;
using System.Collections.Generic;
using Runtime.Data.Structure.Items;
using UnityEngine;

namespace Runtime.Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ItemInfos", menuName = "Data/ItemInfos")]
    public class ItemInfos : SingletonScriptableObject<ItemInfos>
    {
        [SerializeField] private int _defaultItemStartId = 0;
        [SerializeField] private int _usableItemStartId = 1000;
        [SerializeField] private int _wearableItemStartId = 2000;
        [SerializeField] private List<Item> _defaultItems = new List<Item>();
        [SerializeField] private List<UsableItem> _usableItems = new List<UsableItem>();
        [SerializeField] private List<WearableItem> _wearableItems = new List<WearableItem>();
        private Dictionary<int, Item> _cachedItems = new Dictionary<int, Item>();
        private void InternalCreateCache(){
            foreach (Item t in _defaultItems) {
                _cachedItems.Add(t.Id, t);
            }
            foreach (UsableItem t in _usableItems) {
                _cachedItems.Add(t.Id, t);
            }
            foreach (WearableItem t in _wearableItems) {
                _cachedItems.Add(t.Id, t);
            }
        }
        public T GetItem<T> (int id) where T : Item
        {
            if (_cachedItems.Count != 0) return _cachedItems[id] as T;
            InternalCreateCache();
            return _cachedItems[id] as T;
        }
        private void OnValidate()
        {
            for (int i = 0; i < _defaultItems.Count; i++) {
                _defaultItems[i].InitItem(_defaultItemStartId + i, ItemType.DEFAULT);
            }
            for (int i = 0; i < _usableItems.Count; i++) {
                _usableItems[i].InitItem(_usableItemStartId + i, ItemType.USABLE);
            }
            for (int i = 0; i < _wearableItems.Count; i++) {
                _wearableItems[i].InitItem(_wearableItemStartId + i, ItemType.WEARABLE);
            }
        }
    }
}