using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Utils
{
    /// <summary>
    /// A Singleton GameObject which is derived from MonoBehaviour
    /// </summary>
    /// <typeparam name="T">A MonoBehaviour derived class to create a singleton of</typeparam>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static object instanceLock = new object();
        private static T instance;

        public static T Instance {
            get {
                lock (instanceLock) {

                    if (instance == null) {

                        GameObject go = new GameObject();
                        go.name = typeof(T).ToString() + " (Singleton)";
                        instance = go.AddComponent<T>();

                        DontDestroyOnLoad(go);
                    }

                    return instance;
                }
            }
        }

        private void Awake() {

            // We should do this even if the instance is null,
            // because that means that we have created an
            // instance of the game manager in the inspector
            // So let's not do that.
            if (instance != this && instance != null) {

                Debug.LogError($"Singleton {typeof(T)} is attached to GameObject with name \"{gameObject.name}\". Fix your scene.");
                Destroy(gameObject);
            }
        }
    }
}