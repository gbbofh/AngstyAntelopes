using System.Collections.Generic;

using Core.Utils;

namespace Core.Managers
{
    class EntityManager : Singleton<EntityManager>
    {
        private List<Entity> entities;

        public void Awake() {

            entities = new List<Entity>();
        }

        public void AddEntity(Entity e) {

            entities.Add(e);
        }

        public void RemoveEntity(Entity e) {

            entities.Remove(e);
        }
    }
}
