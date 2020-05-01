using System.Collections.Generic;

using Core.Utils;
using Entities.Buildings;

using UnityEngine;
using UnityEngine.Events;

namespace Core.Managers
{
    public class BuildingManager : Singleton<BuildingManager>
    {
        private List<Building> buildings;

        [SerializeField]
        private int numBuildings;

        public UnityAction onAllBuildingsDestroyed;
        public UnityAction<Building> onBuildingDestroyed;

        public void Awake() {

            buildings = new List<Building>();

            numBuildings = 0;
        }

        public void AddBuilding(Building e) {

            buildings.Add(e);

            e.onBuildingDestroyed += OnBuildingDestroyed;

            numBuildings++;
        }

        public void RemoveBuilding(Building e) {

            buildings.Remove(e);
        }

        private void OnBuildingDestroyed(Building e) {

            numBuildings--;
            if(onBuildingDestroyed != null) {

                onBuildingDestroyed(e);
            }

            if(numBuildings == 0 && onAllBuildingsDestroyed != null) {

                onAllBuildingsDestroyed();
            }
        }
    }
}
