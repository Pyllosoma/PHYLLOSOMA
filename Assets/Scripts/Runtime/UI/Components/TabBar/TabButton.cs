using UnityEngine;
using UnityEngine.Events;

namespace Runtime.UI.Components.TabBar
{
    public class TabButton : MonoBehaviour
    {
        public int Index = -1;
        public TabGroup Parent = null;
        [SerializeField] private UnityEvent _onToggle = new UnityEvent();
        [SerializeField] private UnityEvent _onUnToggle = new UnityEvent();
        public void Toggle() {
            OnToggle();
            _onToggle?.Invoke();
        }
        public void UnToggle() {
            OnUnToggle();
            _onUnToggle?.Invoke();
        }
        public virtual void OnClick() {
            Parent.OnTabSelected(Index);
        }
        protected virtual void OnToggle() { }
        protected virtual void OnUnToggle() { }
    }
}