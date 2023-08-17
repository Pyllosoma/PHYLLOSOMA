using System;
using UnityEngine;

namespace Runtime.Data.Structure.Items
{
    [Serializable]
    public class WearableItem : Item
    {
        public int Health => _health;
        public int Damage => _damage;
        public int Defense => _defense;
        public int Endurance => _endurance;
        public int Agility => _agility;
        public int Faith => _faith;
        [Header("Wearable Item Stats")]
        [SerializeField] private int _health = 0;
        [SerializeField] private int _damage = 0;
        [SerializeField] private int _defense = 0;
        [SerializeField] private int _endurance = 0;
        [SerializeField] private int _agility = 0;
        [SerializeField] private int _faith = 0;
    }
}