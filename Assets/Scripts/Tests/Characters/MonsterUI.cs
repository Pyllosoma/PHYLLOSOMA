using Runtime.UI.Components.Sliders;
using UnityEngine;

namespace Tests.Characters
{
    public class MonsterUI : MonoBehaviour
    {
        [SerializeField] private CharacterSlider _healthSlider;
        [SerializeField] private CharacterSlider _soulGaugeSlider;
        public void ResetValue(float health,float soulGauge)
        {
            _healthSlider.Init((int)health);
            _soulGaugeSlider.Init((int)soulGauge);
        }
        public void SetHealth(float health) {
            _healthSlider.SetHealth((int)health);
        }
        public void SetSoulGauge(float soulGauge) {
            _soulGaugeSlider.SetHealth((int)soulGauge);
        }
    }
}