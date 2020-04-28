using Core.Managers;
using Core.UI;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class GameOver : UIController
    {
        private GameManager gameManager;
        private LevelManager levelManager;

        public TMP_Text score;
        public Button mainMenu;

        private void Start() {

            gameManager = GameManager.Instance;
            levelManager = LevelManager.Instance;

            mainMenu.onClick.AddListener(OnMainMenuClicked);

            score.text = gameManager.playerScore.ToString();
        }
        public void OnMainMenuClicked() {

            levelManager.LoadLevel("main_menu");
        }
    }
}