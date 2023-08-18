using System;
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

        public void Init(BaseStats baseStats)
        {
            _healthStat.text = baseStats.Health.ToString();
            _attackStat.text = baseStats.Damage.ToString();
            _defenseStat.text = baseStats.Defense.ToString();
            _enduranceStat.text = baseStats.Endurance.ToString();
            _speedStat.text = baseStats.Agility.ToString();
            _faithStat.text = baseStats.Faith.ToString();
        }
        private void OnDisable()
        {
            Init(new BaseStats());
        }
    }
}