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
    public class Building : MonoBehaviour
    {
        private BuildingManager buildingManager;
        private Health health;

        void Start() {

            buildingManager = BuildingManager.Instance;
            buildingManager.AddBuilding(this);
        }

        private void OnDestroy() {

            buildingManager.RemoveBuilding(this);
        }
    }
}