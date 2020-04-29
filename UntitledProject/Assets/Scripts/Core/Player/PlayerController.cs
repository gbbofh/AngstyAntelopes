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
        public Rage rage;
        public Health health;

        public GameObject groundChecker;
        public GameObject attackChecker;
        public Vector3 attackBounds = new Vector3(1, 1, 1);

        public bool grounded;
        public Vector3 velocity;

        private float moveSpeed = 0.75f;
        
        // To keep rage decreasing over time
        private float tickAccum = 0.0f;
        private const float RAGE_DEC = 0.5f;
        private const float RAGE_TICK = 1.5f;

        // Camera reference
        private PlayerCamera playerCamera;

        // Controller references
        private Animator animator;
        private CharacterController controller;

        // Player move direction
        private Vector2 moveDir = Vector2.zero;

        private void Start() {

            health = GetComponent<Health>();
            rage = GetComponent<Rage>();

            animator = GetComponent<Animator>();
            controller = GetComponent<CharacterController>();

            playerCamera = GameObject.FindObjectOfType<PlayerCamera>();

            health.onEmpty += OnDead;
            health.onDecrement += OnHurt;

            rage.onFull += OnEnrage;
            rage.onEmpty += OnCalm;

            // Game Manager has already been instantiated at this point
            GameManager.Instance.playerController = this;
            UIManager.Instance.ActivateUI("hud", true);
        }

        private void FixedUpdate() {

            float speed = moveSpeed * moveDir.magnitude;
            float angle = Mathf.Atan2(moveDir.x, moveDir.y) * Mathf.Rad2Deg + playerCamera.transform.eulerAngles.y;

            // Exposed for debugging
            grounded = Physics.CheckSphere(groundChecker.transform.position, 0.75f,
                                                (1 << LayerMask.NameToLayer("Ground")),
                                                QueryTriggerInteraction.Ignore);

            velocity = transform.forward * speed * Time.deltaTime;
            velocity.y += -9.8f * Time.deltaTime;

            if(grounded && velocity.y < 0) {

                velocity.y = 0.0f;
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack")) return;

            if (moveDir != Vector2.zero) {

                transform.eulerAngles = Vector3.up * angle;
            }

            //controller.Move(transform.forward * speed * Time.deltaTime);
            controller.Move(velocity);
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

        private void OnDrawGizmos() {

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(attackChecker.transform.position, attackBounds / 2);

            Gizmos.DrawWireSphere(groundChecker.transform.position, 0.75f);
        }

        //ToDo: handle the player attacking something
        private void OnAttack() {

            animator.SetTrigger("attacking");

            Collider[] colliders = Physics.OverlapBox(attackChecker.transform.position,
                                                        attackBounds / 2,
                                                        transform.rotation,
                                                        (1 << LayerMask.NameToLayer("Attackable")));

            if (colliders != null && colliders.Length > 0) {

                Collider closest = colliders[0];

                foreach (Collider c in colliders) {

                    float distance = Vector3.Distance(transform.position, c.transform.position);
                    if (distance < Vector3.Distance(transform.position, closest.transform.position)) {

                        closest = c;
                    }
                }

                Health health = closest.gameObject.GetComponent<Health>();
                if(health != null) {

                    health.Decrement(50.0f);
                    Debug.Log(health.CurrentValue);
                }
            }
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

        }
    }

}
