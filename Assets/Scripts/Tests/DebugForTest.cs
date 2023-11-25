using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class DebugForTest : MonoBehaviour
    {
        [SerializeField] private string _scene01;
        [SerializeField] private string _scene02;
        private void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame) {
                Debug.Log("Space Key Pressed");
                SceneManager.LoadScene(_scene02,LoadSceneMode.Additive);
            }
        }
    }
}