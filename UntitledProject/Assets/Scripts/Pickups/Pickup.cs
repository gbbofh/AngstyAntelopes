using Core;
using Core.Player;
using Stats;
using UnityEngine;
using UnityEngine.Events;

namespace Pickups
{
    public class Pickup : MonoBehaviour
    {

        public UnityAction<Entity> onPickup;

        void OnPickup(Entity other) {

            if(onPickup != null) {

                onPickup(other);
            }
        }

        private void OnCollisionEnter(Collision collision) {

            Entity entity = collision.gameObject.GetComponent<Entity>();
            
            if(entity != null) {

                OnPickup(entity);
            }
        }
    }
}