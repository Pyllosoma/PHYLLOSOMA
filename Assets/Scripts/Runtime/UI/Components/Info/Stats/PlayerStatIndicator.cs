using TMPro;
using UnityEngine;

namespace Runtime.UI.Components.Info.Stats
{
    public class PlayerStatIndicator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _healthStat;
        [SerializeField] private TextMeshProUGUI _attackStat;
        [SerializeField] private TextMeshProUGUI _defenseStat;
        [SerializeField] private TextMeshProUGUI _enduranceStat;
        [SerializeField] private TextMeshProUGUI _speedStat;
        [SerializeField] private TextMeshProUGUI _faithStat;

        private void OnEnable()
        {
            //Get stat info and write to text.
        }
    }
}