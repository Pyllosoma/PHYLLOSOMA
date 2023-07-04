using System;
using UnityEngine;

namespace Runtime.UI.Components.Settings
{
    //설정 값을 저장하는 함수
    public class SettingComponent<T> : MonoBehaviour
    {
        [SerializeField] private bool _loadOnEnable = false;
        [SerializeField] private string _settingTargetId = "NONE";
        [SerializeField] protected T _settingValue = default;

        protected virtual void OnEnable()
        {
            if (_loadOnEnable) LoadFromSettingId();
        }
        protected void LoadFromSettingId()
        {
            //Get value by setting id.
            _settingValue = default;
        }
        protected void SaveToSettingId()
        {
            //Save value by setting id.
        }
    }
}