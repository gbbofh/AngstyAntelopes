using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

using Core.Utils;
using UnityEngine.Events;
using Core.UI;

namespace Core.Managers
{
    public class UIManager : Singleton<UIManager>
    {

        private const string UI_PATH = "Scenes/UI/";

        public void ActivateUI(string name, bool active) {

            if (active) {

                StartCoroutine(LoadUIScene(UI_PATH + name));

            }
            else {

                StartCoroutine(UnloadUIScene(UI_PATH + name));
            }
        }

        private IEnumerator LoadUIScene(string name) {

            AsyncOperation loadOp = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);

            while (!loadOp.isDone) {

                yield return null;
            }
        }

        //basically unloads a scene that is currently loaded
        //  (reverse of LoadUIScene)
        // BUG:
        // When LevelManager loads a scene, all loaded UI's
        // are unloaded. To fix this, All scenes loaded by
        // LevelManager must be loaded ADDITIVELY (like they are here)
        // and it must manually remove the previously loaded scene.
        // Big oof.
        private IEnumerator UnloadUIScene(string name) {

            AsyncOperation unloadOP = SceneManager.UnloadSceneAsync(name);

            while (!unloadOP.isDone) {

                yield return null;
            }
        }
    }
}
