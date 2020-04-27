using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Stats;
using UnityEngine.Events;
using Core.Managers;

namespace Core.Player
{
    [RequireComponent(typeof(Rage))]
    [RequireComponent(typeof(Health))]
    public class PlayerController : MonoBehaviour
    {
        public UnityAction onPlayerHurt;
        public UnityAction onPlayerDead;
        public UnityAction onPlayerHealed;

        public UnityAction onPlayerCalmed;
        public UnityAction onPlayerEnraged;

        private Rage rage;
        private Health health;
        private GameManager gameManager;
        
        private float tickAccum = 0.0f;
        private const float RAGE_DEC = 0.5f;
        private const float RAGE_TICK = 1.5f;

        private void Start() {

            health = GetComponent<Health>();
            rage = GetComponent<Rage>();

            health.onEmpty += OnDead;
            health.onDecrement += OnHurt;

            rage.onFull += OnEnrage;
            rage.onEmpty += OnCalm;

            gameManager = GameManager.Instance;
        }

        private void Update() {

            tickAccum += Time.deltaTime;
            if(tickAccum >= RAGE_TICK) {

                tickAccum -= RAGE_TICK;
                rage.Decrement(RAGE_DEC);
            }
        }
        
        //ToDo: be the actual player movement in 3D space
        private void OnMove(Vector2 axis) {

        }

        //ToDo: handle the player attacking something
        private void OnAttack() {

        }

        private void OnHurt() {

            if(onPlayerHurt != null) {

                onPlayerHurt();
            }
        }

        private void OnDead() {

            if(onPlayerDead != null) {

                onPlayerDead();
            }
        }

        private void OnEnrage() {

            if(onPlayerEnraged != null) {

                onPlayerEnraged();
            }
        }

        private void OnCalm() {

            if(onPlayerCalmed != null) {

                onPlayerCalmed();
            }
        }

        //listened for Unity callbacks for player input
        public void Connect(PlayerInput input) {

            input.onMove += OnMove;
            input.onAttack += OnAttack;
        }
    }

}
