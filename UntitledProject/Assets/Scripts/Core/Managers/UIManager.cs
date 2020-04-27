using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

using Core.Utils;

namespace Core.Managers
{
    public class UIManager : Singleton<UIManager>
    {
        public void ActivateUI(string name, bool active) {

            if (active) {

                StartCoroutine(LoadUIScene(name));

            }
            else {

                StartCoroutine(UnloadUIScene(name));
            }
        }

        private IEnumerator LoadUIScene(string name) {

            AsyncOperation loadOp = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);

            while (!loadOp.isDone) {

                yield return null;
            }
        }

        private IEnumerator UnloadUIScene(string name) {

            AsyncOperation unloadOP = SceneManager.UnloadSceneAsync(name);

            while (!unloadOP.isDone) {

                yield return null;
            }
        }
    }
}
