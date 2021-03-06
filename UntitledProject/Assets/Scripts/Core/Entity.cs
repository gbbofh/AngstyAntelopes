﻿using System.Collections;
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
        // all entities will have this class on it
        //      entity = (anything in the game that can move around).
        void Start() {
            
            entityManager = EntityManager.Instance;

            entityManager.AddEntity(this);
        }

        private void OnDestroy() {

            entityManager.RemoveEntity(this);
        }
    }
}
