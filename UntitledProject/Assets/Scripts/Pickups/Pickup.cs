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
    }
}