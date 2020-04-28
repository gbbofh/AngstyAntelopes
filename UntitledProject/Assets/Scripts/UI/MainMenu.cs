using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Core.UI;
using UnityEngine.UI;
using Core.Managers;

namespace UI
{
    public class MainMenu : UIController
    {
        private LevelManager levelManager;

        public Button playButton;
        public Button quitButton;
        private void Start() {

            levelManager = LevelManager.Instance;

            playButton.onClick.AddListener(OnPlayClicked);
            quitButton.onClick.AddListener(OnQuitClicked);
        }

        private void OnPlayClicked() {

            levelManager.LoadLevel("level_1");

        }

        private void OnQuitClicked() {

            Application.Quit();
        }
    }
}