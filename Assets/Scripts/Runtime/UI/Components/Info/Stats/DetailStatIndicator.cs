using Runtime.Data.Structure;
using TMPro;
using UnityEngine;

namespace Runtime.UI.Components.Info.Stats
{
    public class DetailStatIndicator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _healthStat;
        [SerializeField] private TextMeshProUGUI _staminaStat;
        public void Init(BaseStats baseStats)
        {
            //Need to create status calculate system
        }
        private void OnDisable(){
            Init(new BaseStats());
        }
    }
}