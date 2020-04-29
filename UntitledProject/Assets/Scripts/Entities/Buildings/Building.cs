using Core;
using Core.Managers;
using Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Entities.Buildings
{
    [RequireComponent(typeof(Entity))]
    [RequireComponent(typeof(Health))]
    public class Building : MonoBehaviour
    {
        public UnityAction<Building> onBuildingDestroyed;
        private BuildingManager buildingManager;
        private Health health;

        public List<GameObject> droppedItems;

        private bool dropPickup;

        public int Worth {

            get; private set;
        }

        void Start() {

            buildingManager = BuildingManager.Instance;
            buildingManager.AddBuilding(this);

            health = GetComponent<Health>();
            health.onEmpty += OnHealthEmpty;

            dropPickup = Random.Range(0.0f, 1.0f) > 0.8f;
            Worth = Random.Range(100, 200);
        }

        private void OnDestroy() {

            buildingManager.RemoveBuilding(this);
        }

        private void OnHealthEmpty() {

            gameObject.SetActive(false);
            if(dropPickup == true && droppedItems != null && droppedItems.Count > 0) {

                int i = Random.Range(0, droppedItems.Count - 1);
                if (i >= 0) {

                    Instantiate(droppedItems[i], transform.position, Quaternion.identity);
                }
            }

            if(onBuildingDestroyed != null) {

                onBuildingDestroyed(this);
            }
        }
    }
}