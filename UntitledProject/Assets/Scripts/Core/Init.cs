using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Core.Managers;

namespace Core
{
    public class Init : MonoBehaviour
    {
        GameManager gm;
        LevelManager lm;
        UIManager um;

        void Start() {

            gm = GameManager.Instance;
            lm = LevelManager.Instance;
            um = UIManager.Instance;

            SceneManager.LoadScene("Scenes/levels/main_menu");
        }
    }
}