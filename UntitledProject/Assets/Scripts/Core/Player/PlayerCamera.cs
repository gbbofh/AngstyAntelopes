using Core.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        private void OnInput(Vector2 mouseDelta) {

        }

        // Update is called once per frame
        void Update() {

        }

        public void Connect(PlayerInput input) {

            input.onLook += OnInput;
        }
    }
}