using Core.Managers;
using Core.Player;
using Core.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class HUD : UIController
    {
        GameManager gameManager;
        PlayerController playerController;

        public TMP_Text playerScore;
        public TMP_Text playerHealth;

        private void Start() {

            gameManager = GameManager.Instance;
            playerController = gameManager.playerController;
        }

        private void Update() {

            playerScore.text = gameManager.playerScore.ToString();
            playerHealth.text = playerController.health.CurrentValue.ToString();
        }
    }
}