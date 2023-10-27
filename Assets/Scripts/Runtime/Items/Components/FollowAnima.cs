using System;
using Runtime.Managers;
using Runtime.UI;
using UnityEngine;

namespace Runtime.Items.Components
{
    public class FollowAnima : MonoBehaviour
    {
        [SerializeField] private int _animaValue = 100;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) {
                DataManager.Instance.PlayerData.Anima += _animaValue;
                AnimaIndicateWindow.Instance.ShowAnimaChange(_animaValue);
                gameObject.SetActive(false);
            }
        }
    }
}