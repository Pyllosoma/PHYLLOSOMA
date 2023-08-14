using Runtime.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.UI.Menus
{
    public class SaveWindowMenu : MonoBehaviour
    {
        public void OnSave()
        {
            DataManager.Instance.Save();
            SceneManager.LoadScene("StartUpUI");
            
        }
    }
}