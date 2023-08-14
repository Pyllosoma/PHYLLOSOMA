using TMPro;
using UnityEngine;

namespace Runtime.UI
{
    public class ToastWindow : UIWindow
    {
        [SerializeField] private TextMeshProUGUI _message;
        public void Init(string message)
        {
            _message.text = message;
            Open(true);
        }
    }
}