using UnityEngine;
using UnityEngine.Events;

using Core.Utils;

namespace Core.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public Config conf;

        public UnityAction onConfigLoad;

        private LevelManager levelManager;

        private void Awake() {

            levelManager = LevelManager.Instance;
        }

        private void Start() {

            conf = Config.Reader.ReadFile(Application.dataPath + "\\config.txt");

            if(onConfigLoad != null) {

                onConfigLoad();
            }
        }

        private void OnDestroy() {

            if (conf.Changed) {

                Config.Writer.WriteFile(Application.dataPath + "\\config.txt", conf);
            }
        }

        public void OnLoadBegin() {
        }

        public void OnLoadEnd() {

        }

        public void OnPlayerDead() {

            levelManager.LoadLevel("GameOver", true, false);
        }
    }

}
