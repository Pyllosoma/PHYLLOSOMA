using System;
using Runtime.Data.Structure;
using Runtime.Managers;
using Runtime.UI.Components;
using Runtime.UI.Components.Info.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI.Menus
{
    public class CharacterMenu : MonoBehaviour
    {
        [Header("Player Status")]
        [SerializeField] private StatIndicator _currentStatIndicator;
        [SerializeField] private StatIndicator _changeStatIndicator;
        [Header("Anima Value")]
        [SerializeField] private long _currentAnima = 0;
        [SerializeField] private long _requiredAnima = 0;
        [SerializeField] private long _remainAnima = 0;
        [SerializeField] private TextMeshProUGUI _currentAnimaText;
        [SerializeField] private TextMeshProUGUI _requiredAnimaText;
        [SerializeField] private TextMeshProUGUI _remainAnimaText;
        [Header("Current Change Stat")]
        [SerializeField] private BaseStats _currentChangeStats = new BaseStats();
        [SerializeField] private ArrowNumberChanger _healthChanger;
        [SerializeField] private ArrowNumberChanger _damageChanger;
        [SerializeField] private ArrowNumberChanger _defenseChanger;
        [SerializeField] private ArrowNumberChanger _enduranceChanger;
        [SerializeField] private ArrowNumberChanger _agilityChanger;
        [SerializeField] private ArrowNumberChanger _faithChanger;
        [Header("Apply Button")]
        [SerializeField] private Button _applyButton;
        private void OnEnable()
        {
            _currentChangeStats = new BaseStats();
            //Reset Anima
            _currentAnima = DataManager.Instance.PlayerData.Anima;
            _requiredAnima = 0;
            _remainAnima = _currentAnima;
            
            _currentAnimaText.text = _currentAnima.ToString();
            _requiredAnimaText.text = _requiredAnima.ToString();
            _remainAnimaText.text = _remainAnima.ToString();
            //Reset Stats
            _currentStatIndicator.Init(DataManager.Instance.PlayerData.Stats);
            _changeStatIndicator.Init(DataManager.Instance.PlayerData.Stats);
        }
        public void UpdateMenu()
        {
            _currentChangeStats.Health = _healthChanger.Value;
            _currentChangeStats.Damage = _damageChanger.Value;
            _currentChangeStats.Defense = _defenseChanger.Value;
            _currentChangeStats.Endurance = _enduranceChanger.Value;
            _currentChangeStats.Agility = _agilityChanger.Value;
            _currentChangeStats.Faith = _faithChanger.Value;
            
            _requiredAnima = _currentChangeStats.GetTotalStat() * 100;
            _remainAnima = _currentAnima - _requiredAnima;
            _applyButton.interactable = _remainAnima >= 0 && _currentChangeStats.GetTotalStat() > 0;
            
            _currentAnimaText.text = _currentAnima.ToString();
            _requiredAnimaText.text = _requiredAnima.ToString();
            _remainAnimaText.text = _remainAnima.ToString();
        }
        public void OnStatApplyButtonClicked()
        {
            
        }
    }
}