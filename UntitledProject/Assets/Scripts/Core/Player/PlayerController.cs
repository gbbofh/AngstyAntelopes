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
        // Callback actions
        public UnityAction onPlayerHurt;
        public UnityAction onPlayerDead;
        public UnityAction onPlayerHealed;

        public UnityAction onPlayerCalmed;
        public UnityAction onPlayerEnraged;

        // Player stats
        private Rage rage;
        private Health health;
        
        // To keep rage decreasing over time
        private float tickAccum = 0.0f;
        private const float RAGE_DEC = 0.5f;
        private const float RAGE_TICK = 1.5f;

        // Camera reference
        private PlayerCamera camera;

        // Controller references
        private Animator animator;
        private CharacterController controller;

        // Player move direction
        private Vector2 moveDir = Vector2.zero;

        private void Start() {

            health = GetComponent<Health>();
            rage = GetComponent<Rage>();

            animator = GetComponent<Animator>();

            camera = GameObject.FindObjectOfType<PlayerCamera>();

            health.onEmpty += OnDead;
            health.onDecrement += OnHurt;

            rage.onFull += OnEnrage;
            rage.onEmpty += OnCalm;
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

            moveDir = axis.normalized;

            animator.SetBool("walking", moveDir.magnitude > 0);
        }

        //ToDo: handle the player attacking something
        private void OnAttack() {

            animator.SetTrigger("attacking");
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

        public void OnCollisionEnter(Collision collision) {

            Debug.Log(collision.gameObject.name);
        }
    }

}
