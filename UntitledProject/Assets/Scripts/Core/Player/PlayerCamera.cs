using Core.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        // ToDo: this happens after getting input from mouse
        //   now we apply it to the camera
        private void OnInput(Vector2 mouseDelta) {

        }

        // Update is called once per frame
        // ToDo: where the actual updating 
        //   of the camera movement
        void Update() {

        }
        
        // Listining to whenever Unity callback "onLook"
        //  is invoked
        public void Connect(PlayerInput input) {

            input.onLook += OnInput;
        }
    }
}
