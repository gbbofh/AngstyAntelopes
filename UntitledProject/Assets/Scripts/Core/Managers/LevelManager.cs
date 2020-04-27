using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

using Core.Utils;
using UnityEngine.Events;

namespace Core.Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        public Scene Scene {
            get {

                return SceneManager.GetActiveScene();
            }
        }

        private AsyncOperation loadOp;
        private GameManager gameManager;

        public UnityAction onLevelLoaded;
        public UnityAction onLevelActivated;

        private void Start() {

            gameManager = GameManager.Instance;
        }

        public void LoadLevel(string levelName, bool activateOnLoad = true, bool loadAdditive = false) {

            gameManager.OnLoadBegin();
            StartCoroutine(LoadFromScene(levelName, activateOnLoad, loadAdditive));
        }

        IEnumerator LoadFromScene(string levelName, bool activateOnLoad, bool loadAdditive) {

            loadOp = SceneManager.LoadSceneAsync(levelName, loadAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single);
            loadOp.allowSceneActivation = activateOnLoad;

            while (!loadOp.isDone) {

                yield return null;
            }

            gameManager.OnLoadEnd();
        }

        public void ActivateLoadedScene() {

            if(onLevelActivated != null) {

                onLevelActivated();
            }

            if (loadOp != null) {

                loadOp.allowSceneActivation = true;
            }
        }

        public float GetLoadProgress() {

            return Mathf.Clamp((loadOp == null) ? 1.0f : (loadOp.progress / 0.9f), 0.0f, 1.0f);
        }
    }
}
