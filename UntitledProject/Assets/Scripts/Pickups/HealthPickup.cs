using Core;
using Core.Player;
using Stats;

using UnityEngine;
using UnityEngine.Events;

namespace Pickups
{
    [RequireComponent(typeof(Pickup))]
    public class HealthPickup : MonoBehaviour
    {
        private float value;
        private float origYPos;
        private float yAnimPos;
        private float frequency;
        private float amp;

        // Not sure if this is a good idea or not
        public UnityAction<HealthPickup> onPickup;

        private void Start() {

            value = Random.Range(15.0f, 30.0f);
            frequency = Random.Range(5.0f, 6.0f);
            amp = 0.3f;

            yAnimPos = 0.0f;
            origYPos = transform.position.y;
        }

        private void Update() {

            float yPos = Mathf.Sin(yAnimPos * frequency);

            transform.position = new Vector3(transform.position.x,
                                            origYPos + amp * yPos,
                                            transform.position.z);

            yAnimPos += Time.deltaTime;

            transform.Rotate(Vector3.up, 1.0f * (yPos + 1.0f));
        }

        void OnPickup(Entity other) {

            if(other.GetComponent<PlayerController>() != null) {

                other.GetComponent<Health>().Increment(this.value);
            }

            this.gameObject.SetActive(false);
            if(onPickup != null) {

                onPickup(this);
            }
        }
    }
}