using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Runtime.UI.Components.Buttons
{
    public class FocusButton : Button
    {
        public bool IsFocused => _isFocused;
        [Header("Focus Button Settings")]
        [SerializeField] private bool _focusOnStart = false;
        [SerializeField] private UnityEvent _onFocusIn;
        [SerializeField] private UnityEvent _onFocusOut;
        private bool _isFocused = false;
        protected override void Start()
        {
            base.Start();
            if (_focusOnStart) {
                OnFocusIn();
                Select();
            }
            _isFocused = _focusOnStart;
        }
        public virtual void OnFocusIn()
        {
            if (_isFocused) return;
            _isFocused = true;
            _onFocusIn?.Invoke();
        }
        public virtual void OnFocusOut()
        {
            if (!_isFocused) return;
            _isFocused = false;
            _onFocusOut?.Invoke();
        }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            OnFocusIn();
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            OnFocusOut();
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            OnFocusIn();
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            OnFocusOut();
        }

        protected override void OnValidate()
        {
            transition = Transition.None;
            base.OnValidate();
        }
        
    }
}