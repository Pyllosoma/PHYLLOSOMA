using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tests.Utils
{
    public class GameEventHolder : MonoBehaviour
    {
        public void MoveScene(string sceneName) {
            SceneManager.LoadScene(sceneName);
        }
        public void ExitGame()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}