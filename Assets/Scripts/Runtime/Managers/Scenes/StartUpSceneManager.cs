using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.Managers.Scenes
{
    public class StartUpSceneManager : MonoBehaviour
    {
        public void OnContinueClicked()
        {
            //Load Scene
            MoveToNextScene(NextSceneNames.PlayerMovement, LoadTypes.Continue);
        }
        public void OnLoadGameClicked()
        {
            DataManager.Instance.LoadSaveData(DataManager.SaveDataType.LOAD_GAME);
            
            //Load Scene
            MoveToNextScene(NextSceneNames.PlayerMovement, LoadTypes.Load);
        }

        public void OnNewGameClicked()
        {
            DataManager.Instance.LoadSaveData(DataManager.SaveDataType.NEW_GAME);
            
            //Load Scene
            MoveToNextScene(NextSceneNames.PlayerMovement, LoadTypes.New);
        }

        private void MoveToNextScene(NextSceneNames nextSceneName, LoadTypes type)
        {
            //Load
            if (type == LoadTypes.Continue)
            {
                LoadManager.Instance.LoadContinueGame(nextSceneName);
                return; //로딩 성공하면 함수 종료
            }

            //Load
            if (type == LoadTypes.Load)
            {
                LoadManager.Instance.LoadSavedGame(nextSceneName);
                return; //로딩 성공하면 함수 종료
            }

            if (type == LoadTypes.New)
            {
                LoadManager.Instance.LoadNewGame(nextSceneName);
                return;
            }

            //If Fail to Load
            Debug.Log("로딩 실패, 씬 이름과 로딩 타입을 확인해주세요");
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