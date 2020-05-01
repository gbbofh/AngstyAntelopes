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
    public class TallBuilding : Building
    {
        private const float INV_DROP_FREQ = 0.7f;
        void Start() {

            buildingManager = BuildingManager.Instance;
            buildingManager.AddBuilding(this);

            health = GetComponent<Health>();
            health.onEmpty += OnHealthEmpty;
            health.MaxValue = 100;
            health.MinValue = 0;
            health.CurrentValue = health.MaxValue;

            dropPickup = Random.Range(0.0f, 1.0f) > INV_DROP_FREQ;
            Worth = Random.Range(200, 300);
        }
    }
}