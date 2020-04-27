using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Core.Managers;

namespace Core
{
    public class Entity : MonoBehaviour
    {
        private EntityManager entityManager;

        // Start is called before the first frame update
        void Start() {

            entityManager = EntityManager.Instance;

            entityManager.AddEntity(this);
        }

        private void OnDestroy() {

            entityManager.RemoveEntity(this);
        }
    }
}