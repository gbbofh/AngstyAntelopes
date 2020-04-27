using System.Collections.Generic;

using Core.Utils;
using UnityEngine.Events;

namespace Core.Managers
{
    class BuildingManager : Singleton<BuildingManager>
    {
        private List<Entity> buildings;
        private int numBuildings;

        public UnityAction onAllBuildingsDestroyed;

        public void Awake() {

            buildings = new List<Entity>();

            numBuildings = 0;
        }

        public void AddBuilding(Entity e) {

            buildings.Add(e);

            numBuildings++;
        }

        public void RemoveBuilding(Entity e) {

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
