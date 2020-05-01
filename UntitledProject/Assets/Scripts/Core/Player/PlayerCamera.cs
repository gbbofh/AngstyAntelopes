using Core.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        public GameObject player;

        // Cloned from pickup to give us
        // a helicopter-like motion
        private float origYPos;
        private float yAnimPos;
        private float frequency;
        private float amp;

        private const float MAX_PLAYER_DIST = 10.0f;
        private const float MIN_PLAYER_DIST = 5.0f;

        // ToDo: this happens after getting input from mouse
        //   now we apply it to the camera
        private void OnInput(Vector2 mouseDelta) {

        }

        private void Start() {

            frequency = 1.0f;
            amp = 0.3f;

            yAnimPos = Random.Range(1.0f, 1.0f);
            origYPos = transform.position.y;
        }

        // Update is called once per frame
        // ToDo: where the actual updating 
        //   of the camera movement
        void Update() {

            float yPos = amp * Mathf.Sin(yAnimPos * frequency);

            transform.position = new Vector3(transform.position.x,
                                            origYPos + yPos,
                                            transform.position.z);

            yAnimPos += Time.deltaTime;

            //Vector3 dPos = player.transform.position - transform.position;
            //float dZ = Mathf.Abs(dPos.z);

            //if(dZ > MAX_PLAYER_DIST) {

            //    transform.Translate(Vector3.forward * Time.deltaTime);
            //}
            //if(dZ < MIN_PLAYER_DIST) {

            //    transform.Translate(Vector3.back * Time.deltaTime);
            //}

            transform.LookAt(player.transform);
        }
        
        // Listining to whenever Unity callback "onLook"
        //  is invoked
        public void Connect(PlayerInput input) {

            input.onLook += OnInput;
        }
    }
}
