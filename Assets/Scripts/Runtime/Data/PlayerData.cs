using System;
using System.Collections.Generic;
using Runtime.Data.Structure;
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
        public Dictionary<int, int> Inventory = new Dictionary<int, int>() {
            {0,1},
            {2000,2}
        };
        public void UseItem(int id,int count)
        {
            if (!Inventory.ContainsKey(id)) return;
            Inventory[id] -= count;
            if (Inventory[id] <= 0){
                Inventory.Remove(id);
            }
        }
        public void AddItem(int id, int count)
        {
            if (!Inventory.ContainsKey(id)) {
                Inventory.Add(id,count);
            }
            else {
                Inventory[id] += count;
            }
        }
    }
}