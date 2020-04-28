using UnityEngine;
using UnityEngine.Events;

using Core.Utils;
using Core.Player;

namespace Core.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        /*enum GameState
        {
            START,
            MENU,
            PAUSE,
            PLAY
        }*/

        enum GameState
        {
            START,
            RUNNING
        }

        public Config conf;

        public UnityAction onConfigLoad;

        private UIManager uiManager;
        private LevelManager levelManager;
        private BuildingManager buildingManager;

        private GameState gameState;

        public int playerScore;
        public PlayerController playerController;

        //called whenever the GameManager is created
        //called before anything else when a script 
        //  is added to an object
        private void Awake() {

            buildingManager = BuildingManager.Instance;
            levelManager = LevelManager.Instance;
            uiManager = UIManager.Instance;

            gameState = GameState.START;
            playerScore = 0;
        }
        
        //called after all of the other script have had 
        //  their awake method envoked
        //config.txt: gives deginitions for physical actions
        private void Start() {

            conf = Config.Reader.ReadFile(Application.dataPath + "\\config.txt");

            if(onConfigLoad != null) {

                onConfigLoad();
            }

            buildingManager.onAllBuildingsDestroyed += OnPlayerDestroyedAllBuildings;
        }

        private void Update() {
            
            /*switch(gameState) {

                case GameState.START:
                    levelManager.LoadLevel("main_menu", true, false);
                    gameState = GameState.MENU;
                    break;
                case GameState.MENU:
                    break;
                case GameState.PAUSE:
                    break;
                case GameState.PLAY:
                    break;
            }*/

            // This is a god awful hack to cope with
            // the fact that we can't order the first
            // level to be loaded in Start
            if(gameState == GameState.START) {

                gameState = GameState.RUNNING;
                //levelManager.LoadLevel("main_menu");
                //uiManager.ActivateUI("menu", true);
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

            uiManager.ActivateUI("load", true);
        }
        
        //ToDo: called when the LevelManager ends loading a 
        //  scene, this method will activate the scene from 
        //  LoadManager.
        public void OnLoadEnd() {

            // There is currently a bug here due to how LevelManager works.
            //uiManager.ActivateUI("load", false);
        }
        
        public void OnPlayerDead() {

            levelManager.LoadLevel("lose", true, false);
        }

        public void OnPlayerDestroyedAllBuildings() {

            levelManager.LoadLevel("win", true, false);
        }
    }

}