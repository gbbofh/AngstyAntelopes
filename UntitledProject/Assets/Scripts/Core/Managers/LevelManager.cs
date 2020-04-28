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

        private const string LEVEL_PATH = "Scenes/Levels/";

        public UnityAction onLevelLoaded;
        public UnityAction onLevelActivated;

        // I guess I could have actually just loaded the main menu from here...
        // However, I feel like that will cause issues with GameManager's callbacks
        // to listen for onLevelLoaded.
        private void Start() {

            gameManager = GameManager.Instance;

        }

        public void LoadLevel(string levelName, bool activateOnLoad = true, bool loadAdditive = false) {

            gameManager.OnLoadBegin();
            //this makes load from scene function run cuncurently with everything else
            StartCoroutine(LoadFromScene(LEVEL_PATH + levelName, activateOnLoad, loadAdditive));
        }

        IEnumerator LoadFromScene(string levelName, bool activateOnLoad, bool loadAdditive) {

            loadOp = SceneManager.LoadSceneAsync(levelName, loadAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single);
            loadOp.allowSceneActivation = activateOnLoad;

            while (!loadOp.isDone) {
                //this indicated that the coroutine is not yet done
                yield return null;
            }

            gameManager.OnLoadEnd();
        }

        public void ActivateLoadedScene() {
            //"onLevelActivated" is a unity thing that tells 
            //  other entities that a level is active
            if(onLevelActivated != null) {

                onLevelActivated();
            }

            if (loadOp != null) {

                loadOp.allowSceneActivation = true;
            }
        }
        
        //this tells you how long your waiting
        public float GetLoadProgress() {

            return Mathf.Clamp((loadOp == null) ? 1.0f : (loadOp.progress / 0.9f), 0.0f, 1.0f);
        }
    }
}
