using System;
using Runtime.Managers;
using TMPro;
using UnityEngine;

namespace Runtime.UI.Menus
{
    public class EndGameMenu : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _totalPlayTimeText;
        [SerializeField] private TextMeshProUGUI _lastSaveTimeText;
        private void OnEnable()
        {
            _levelText.text = $"{DataManager.Instance.PlayerData.Level}";
            _totalPlayTimeText.text = $"{new DateTime(DataManager.Instance.PlayerData.TotalPlayTime):HH:mm:ss}";
            _lastSaveTimeText.text = $"{new DateTime(DataManager.Instance.PlayerData.LastSaveTime):yyyy-MM-dd HH:mm:ss}";
        }
    }
}