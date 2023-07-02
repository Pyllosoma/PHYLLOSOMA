using UnityEngine;
using UnityEngine.Events;

namespace Runtime.UI.Components.TabBar
{
    public class TabButton : MonoBehaviour
    {
        public int Index = -1;
        public TabBarManager Parent;
        [SerializeField] private UnityEvent _onTabSelected;
        [SerializeField] private UnityEvent _onTabDeselected;
        public void OnTabSelected() {
            Parent.OnTabSelected(Index);
        }
        /// <summary>
        /// Change the state of the button.
        /// </summary>
        /// <param name="isAttention">The parameter that active or deactivate button.</param>
        public void Attention(bool isAttention) {
            if (isAttention) {
                _onTabSelected.Invoke();
            }
            else {
                _onTabDeselected.Invoke();
            }
        }
    }
}