using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.Managers.Scenes
{
    public class StartUpSceneManager : MonoBehaviour
    {
        public void OnContinueClicked()
        {
            Debug.Log("Doing nothing");
        }
        public void OnLoadGameClicked()
        {
            DataManager.Instance.LoadSaveData(DataManager.SaveDataType.LOAD_GAME);
            SceneManager.LoadScene("MainScene");
        }
        public void OnNewGameClicked()
        {
            DataManager.Instance.LoadSaveData(DataManager.SaveDataType.NEW_GAME);
            SceneManager.LoadScene("MainScene");
        }
        public void OnExitClicked()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
            
        }
    }
}