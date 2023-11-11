using Runtime.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.Managers
{
    public class LoadSceneManager : Singleton<LoadSceneManager>
    {
        public enum LoadSceneNames
        {
            MainScene,
            PlayerMovement
        }

        private string _nextSceneName;

        public void LoadNextScene(LoadSceneNames nextScene)
        {
            _nextSceneName = nextScene.ToString();
            SceneManager.LoadScene("LoadingScene");
        }
    }
}
