using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI.Components.Settings
{
    [RequireComponent(typeof(Slider))]
    public class SliderSettingComponent : SettingComponent<float>
    {
        [SerializeField] private Slider _slider;
        protected override void OnEnable()
        {
            base.OnEnable();
            _slider.value = _settingValue;
        }

        public override void SaveToSettingId()
        {
            _settingValue = _slider.value;
            base.SaveToSettingId();
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            _slider = GetComponent<Slider>();
        }
    }
}