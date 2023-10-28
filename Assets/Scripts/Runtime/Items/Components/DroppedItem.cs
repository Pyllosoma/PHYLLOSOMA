using System;
using Runtime.Data.ScriptableObjects;
using Runtime.Managers;
using Runtime.UI;
using UnityEngine;

namespace Runtime.Items.Components
{
    public class DroppedItem : MonoBehaviour
    {
        [SerializeField] private int _count = 1;
        [SerializeField] private ItemCode _itemCode = 0;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) {
                ItemInfoWindow.Instance.Init(_itemCode, () => {
                    DataManager.Instance.PlayerData.AddItem(_itemCode, _count);
                    gameObject.SetActive(false);
                });
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player")) {
                ItemInfoWindow.Instance.Open(false);
            }
        }
    }
}