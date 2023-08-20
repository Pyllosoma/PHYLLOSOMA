using System;
using Runtime.Data.Structure;
using Runtime.Managers;
using Runtime.UI.Components;
using Runtime.UI.Components.Info.Indicators;
using Runtime.UI.Components.Info.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI.Menus
{
    public class CharacterMenu : MonoBehaviour
    {
        [Header("Color info")]
        [SerializeField] private Color _positiveColor = Color.green;
        [SerializeField] private Color _defaultColor = Color.white;
        [SerializeField] private Color _negativeColor = Color.red;
        [Header("Player Status")]
        [SerializeField] private BaseStats _playerStats = new BaseStats();
        [SerializeField] private StatIndicator _currentStatIndicator;
        [SerializeField] private StatIndicator _changeStatIndicator;
        [Header("Anima Value")]
        [SerializeField] private int _currentAnima = 0;
        [SerializeField] private int _requiredAnima = 0;
        [SerializeField] private int _remainAnima = 0;
        [SerializeField] private ValueIndicator _currentAnimaValue;
        [SerializeField] private ValueIndicator _requiredAnimaValue;
        [SerializeField] private ValueIndicator _remainAnimaValue;
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
        public void Reset(){
            _playerStats = DataManager.Instance.PlayerData.Stats;
            _currentChangeStats = new BaseStats();
            //Reset Anima
            _currentAnima = DataManager.Instance.PlayerData.Anima;
            _requiredAnima = 0;
            _remainAnima = _currentAnima;
            
            _currentAnimaValue.UpdateValue(_currentAnima);
            _remainAnimaValue.UpdateValue(_remainAnima);
            _requiredAnimaValue.UpdateValue(_requiredAnima);
            //Reset Stats
            _currentStatIndicator.Init(_playerStats);
            _changeStatIndicator.Init(_playerStats + _currentChangeStats);
            
            //Reset Arrow Number Changer
            _healthChanger.Reset();
            _damageChanger.Reset();
            _defenseChanger.Reset();
            _enduranceChanger.Reset();
            _agilityChanger.Reset();
            _faithChanger.Reset();
        }
        public void UpdateMenu()
        {
            _currentChangeStats.Health = _healthChanger.Value;
            _currentChangeStats.Damage = _damageChanger.Value;
            _currentChangeStats.Defense = _defenseChanger.Value;
            _currentChangeStats.Endurance = _enduranceChanger.Value;
            _currentChangeStats.Agility = _agilityChanger.Value;
            _currentChangeStats.Faith = _faithChanger.Value;
            
            //Create Color profile
            Color[] colors = new Color[6];
            colors[0] = _currentChangeStats.Health > 0 ? _positiveColor : _defaultColor;
            colors[1] = _currentChangeStats.Damage > 0 ? _positiveColor : _defaultColor;
            colors[2] = _currentChangeStats.Defense > 0 ? _positiveColor : _defaultColor;
            colors[3] = _currentChangeStats.Endurance > 0 ? _positiveColor : _defaultColor;
            colors[4] = _currentChangeStats.Agility > 0 ? _positiveColor : _defaultColor;
            colors[5] = _currentChangeStats.Faith > 0 ? _positiveColor : _defaultColor;
            //Update stats indicator
            _changeStatIndicator.Init(_playerStats + _currentChangeStats,colors);
            //Change anima value
            _requiredAnima = _currentChangeStats.GetTotalStat() * 100;
            _remainAnima = _currentAnima - _requiredAnima;
            _applyButton.interactable = _remainAnima >= 0 && _currentChangeStats.GetTotalStat() > 0;
            //Show anima value
            _currentAnimaValue.UpdateValue(_currentAnima);
            _remainAnimaValue.UpdateValue(_remainAnima);
            _requiredAnimaValue.UpdateValue(_requiredAnima);
        }

        public void OnStatApplyButtonClicked()
        {
            DataManager.Instance.PlayerData.Stats += _currentChangeStats;
            DataManager.Instance.PlayerData.Anima = _remainAnima;
            //Reset data by player data
            Reset();
        }
        private void OnEnable(){
            //Start up reset
            _currentAnima = DataManager.Instance.PlayerData.Anima;
            _requiredAnima = 0;
            _remainAnima = _currentAnima;
            _currentAnimaValue.SetStartValue(_currentAnima);
            _remainAnimaValue.SetStartValue(_remainAnima);
            _requiredAnimaValue.SetStartValue(_requiredAnima);
            Reset();
        }
    }
}