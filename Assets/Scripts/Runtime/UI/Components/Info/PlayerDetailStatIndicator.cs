using System;
using TMPro;
using UnityEngine;

namespace Runtime.UI.Components.Info
{
    public class PlayerDetailStatIndicator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _healthStat;
        [SerializeField] private TextMeshProUGUI _staminaStat;

        private void OnEnable()
        {
            //Get stat info and write to text.
        }
    }
}