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
        protected BuildingManager buildingManager;
        protected Health health;
        private const float INV_DROP_FREQ = 0.8f;

        public List<GameObject> droppedItems;

        protected bool dropPickup;

        public int Worth {

            get; protected set;
        }

        void Start() {

            buildingManager = BuildingManager.Instance;
            buildingManager.AddBuilding(this);

            health = GetComponent<Health>();
            health.onEmpty += OnHealthEmpty;
            health.MaxValue = 50;
            health.MinValue = 0;
            health.CurrentValue = health.MaxValue;

            dropPickup = Random.Range(0.0f, 1.0f) > INV_DROP_FREQ;
            Worth = Random.Range(100, 200);
        }

        private void OnDestroy() {

            buildingManager.RemoveBuilding(this);
        }

        protected void OnHealthEmpty() {

            gameObject.SetActive(false);
            if(dropPickup == true && droppedItems != null && droppedItems.Count > 0) {

                int i = Random.Range(0, droppedItems.Count - 1);
                if (i >= 0) {

                    Instantiate(droppedItems[i], transform.position + Vector3.up, Quaternion.identity);
                }
            }

            if(onBuildingDestroyed != null) {

                onBuildingDestroyed(this);
            }
        }
    }
}