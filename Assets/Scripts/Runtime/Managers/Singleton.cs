using System;
using UnityEngine;

namespace Runtime.Managers
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                //if instance is null, find instance.
                _instance = FindObjectOfType<T>();
                if (_instance != null) return _instance;
                //if instance not found, create new instance.
                var singletonObject = new GameObject(typeof(T).Name, typeof(T));
                _instance = singletonObject.GetComponent<T>();
                return _instance;
            }
        }

        public virtual void Awake()
        {
            if (transform.parent != null && transform.root != null) {
                DontDestroyOnLoad(transform.root.gameObject);
            }
            else {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}