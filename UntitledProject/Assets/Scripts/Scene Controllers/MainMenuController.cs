using Core.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SceneControllers
{
    public class MainMenuController : MonoBehaviour
    {
        UIManager uiManager;

        void Start() {

            uiManager = UIManager.Instance;

            uiManager.ActivateUI("menu", true);
        }
    }
}