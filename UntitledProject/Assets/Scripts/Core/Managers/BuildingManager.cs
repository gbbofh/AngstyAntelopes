using System.Collections.Generic;

using Core.Utils;
using Entities.Buildings;
using UnityEngine.Events;

namespace Core.Managers
{
    class BuildingManager : Singleton<BuildingManager>
    {
        private List<Building> buildings;
        private int numBuildings;

        public UnityAction onAllBuildingsDestroyed;

        public void Awake() {

            buildings = new List<Building>();

            numBuildings = 0;
        }

        public void AddBuilding(Building e) {

            buildings.Add(e);

            numBuildings++;
        }

        public void RemoveBuilding(Building e) {

            buildings.Remove(e);
        }

        private void OnBuildingDestroyed(Entity e) {

            numBuildings--;
            if(numBuildings == 0 && onAllBuildingsDestroyed != null) {

                onAllBuildingsDestroyed();
            }
        }
    }
}
