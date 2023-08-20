using System;
using System.Collections.Generic;
using Runtime.Data.Structure;
using TMPro;
using UnityEngine;

namespace Runtime.UI.Components.Info.Stats
{
    public class StatIndicator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _healthStat;
        [SerializeField] private TextMeshProUGUI _attackStat;
        [SerializeField] private TextMeshProUGUI _defenseStat;
        [SerializeField] private TextMeshProUGUI _enduranceStat;
        [SerializeField] private TextMeshProUGUI _speedStat;
        [SerializeField] private TextMeshProUGUI _faithStat;
        private const int _statCount = 6;
        private Color[] _colors = new []{
            Color.white,
            Color.white,
            Color.white,
            Color.white,
            Color.white,
            Color.white,
        };
        public void Init(BaseStats baseStats, Color[] colors = null)
        {
            _healthStat.text = baseStats.Health.ToString();
            _attackStat.text = baseStats.Damage.ToString();
            _defenseStat.text = baseStats.Defense.ToString();
            _enduranceStat.text = baseStats.Endurance.ToString();
            _speedStat.text = baseStats.Agility.ToString();
            _faithStat.text = baseStats.Faith.ToString();
            if (colors == null || colors.Length != _statCount){
                _healthStat.color = _colors[0];
                _attackStat.color = _colors[1];
                _defenseStat.color = _colors[2];
                _enduranceStat.color = _colors[3];
                _speedStat.color = _colors[4];
                _faithStat.color = _colors[5];
            }
            else {
                _healthStat.color = colors[0];
                _attackStat.color = colors[1];
                _defenseStat.color = colors[2];
                _enduranceStat.color = colors[3];
                _speedStat.color = colors[4];
                _faithStat.color = colors[5];
            }
        }
        private void OnDisable()
        {
            Init(new BaseStats());
        }
    }
}