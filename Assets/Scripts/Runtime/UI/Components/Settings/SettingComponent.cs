using System;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.UI.Components.Settings
{
    //설정 값을 저장하는 함수
    public class SettingComponent<T> : MonoBehaviour
    {
        [SerializeField] private bool _loadOnEnable = true;
        [SerializeField] private string _settingTargetId = "NONE";
        [SerializeField] protected T _settingValue = default;

        protected virtual void OnEnable()
        {
            if (_loadOnEnable) LoadFromSettingId();
        }
        protected virtual void LoadFromSettingId()
        {
            //Get value by setting id.
            _settingValue = SettingManager.Instance.GetSettingDataById<T>(_settingTargetId);
        }
        public virtual void SaveToSettingId()
        {
            if (_settingTargetId == "NONE") return;
            //Save value by setting id.
            SettingManager.Instance.SetSettingDataById(_settingTargetId, $"{_settingValue}");
        }
        protected virtual void OnValidate()
        {
            //if gameobject is prefab then return.
            if (gameObject.scene.name == null) return;
            if (_settingTargetId == "NONE") {
                Debug.LogError($"SettingComponent : Setting target id is not set.\n{transform.gameObject.name}");
            }
        }
    }
}