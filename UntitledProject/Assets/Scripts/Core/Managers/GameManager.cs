using UnityEngine;
using UnityEngine.Events;

using Core.Utils;
using Core.Player;

namespace Core.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public Config conf;

        public UnityAction onConfigLoad;

        private LevelManager levelManager;
        private UIManager uiManager;

        private PlayerController player;

        //called whenever the GameManager is created
        //called before anything else when a script 
        //  is added to an object
        private void Awake() {

            levelManager = LevelManager.Instance;
            uiManager = UIManager.Instance;
        }
        
        //called after all of the other script have had 
        //  their awake method envoked
        //config.txt: gives deginitions for physical actions
        private void Start() {

            conf = Config.Reader.ReadFile(Application.dataPath + "\\config.txt");

            if(onConfigLoad != null) {

                onConfigLoad();
            }
        }
        
        //this default values in case the config.txt is 
        //  empty/does not exist
        private void OnDestroy() {

            if (conf.Changed) {

                Config.Writer.WriteFile(Application.dataPath + "\\config.txt", conf);
            }
        }
        
        //ToDo: when the LevelManager starts loading a scene, 
        //  OnLoadBegin is called so that the GameManager can 
        //  do whatever setup it needs to do for level (things 
        //  like cleaning up the last scene or activing the 
        //  level loading UI)
        public void OnLoadBegin() {
        }
        
        //ToDo: called when the LevelManager ends loading a 
        //  scene, this method will activate the scene from 
        //  LoadManager.
        public void OnLoadEnd() {

        }
        
        public void OnPlayerDead() {

            levelManager.LoadLevel("GameOver", true, false);
        }
    }

}
