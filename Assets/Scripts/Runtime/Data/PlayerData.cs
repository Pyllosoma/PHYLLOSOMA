using System;
using System.Collections.Generic;
using Runtime.Data.Structure;
using Runtime.Items;
using UnityEngine;

namespace Runtime.Data
{
    [Serializable]
    public class PlayerData
    {

        public long TotalPlayTime = 0;
        public long LastSaveTime = 0;
        [Header("Player Info")]
        public int Level = 1;
        public int Anima = 10000;
        [Header("Player Status")]
        public BaseStats Stats = new BaseStats();
        public Dictionary<ItemCode, int> Inventory = new Dictionary<ItemCode, int>() {
            {ItemCode.DEBUG,1},
            {ItemCode.DEFAULT,2}
        };
        public void UseItem(ItemCode itemCode,int count)
        {
            if (!Inventory.ContainsKey(itemCode)) return;
            Inventory[itemCode] -= count;
            if (Inventory[itemCode] <= 0){
                Inventory.Remove(itemCode);
            }
        }
        public void AddItem(ItemCode itemCode, int count)
        {
            #if UNITY_EDITOR
                Debug.Log($"Item Acquired : Item Code {itemCode}, Count {count}");
            #endif
            if (!Inventory.ContainsKey(itemCode)) {
                Inventory.Add(itemCode,count);
            }
            else {
                Inventory[itemCode] += count;
            }
        }
    }
}