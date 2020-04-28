using Core.Managers;
using Core.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Core.Player
{
    //this class invokes the callbacks
    public class PlayerInput : MonoBehaviour
    {
        // Couldn't get InputActionMaps to work...
        // I'm sorry:(
        //private InputAction moveAction = new InputAction("move");
        //private InputAction lookAction = new InputAction("look");
        //private InputAction attackAction = new InputAction("attack");

        public PlayerCamera playerCamera;
        public PlayerController playerController;

        public UnityAction<Vector2> onMove;
        public UnityAction<Vector2> onLook;
        public UnityAction onAttack;

        GameManager gameManager;

        // getting a reference to the gameManager and 
        //  setting a callback for when the config file
        //  is loaded
        private void Awake() {

            gameManager = GameManager.Instance;
            //gameManager.onConfigLoad += OnConfigLoaded;
        }

        // Passes an instace of PlayerInput to Connect() to
        //      give access to onLook, onAttack, and onMove 
        //      functions and that will let us register our 
        //      callbacks within playerCamera and playerController
        private void Start() {
            
            // I think this call is safe here?
            // The object exists but its components
            // may not have run Start yet...
            // But we don't need that to have
            // happened for us to add events,
            // which this does.
            playerCamera.Connect(this);
            playerController.Connect(this);
        }
        
        // now we're maing sure that the key bindings loaded from conf.txt exists
        // if they are not there, this is where we add the generic key bindings
        /*private void OnConfigLoaded() {

            Config conf = gameManager.conf;

            if (!conf.HasKeys("up", "down", "left", "right", "attack")) {

                conf["up"] = "<Keyboard>/w";
                conf["down"] = "<Keyboard>/s";
                conf["left"] = "<Keyboard>/a";
                conf["right"] = "<Keyboard>/d";
                conf["attack"] = "<Mouse>/leftButton";
            }

            //moveAction = new InputAction("move", binding: "<Gamepad>/rightStick");
            //lookAction = new InputAction("look", binding: "<Gamepad>/leftStick");
            //attackAction = new InputAction("attack", binding: "<Gamepad>/buttonSouth");
            //moveAction.AddCompositeBinding("Dpad")
            //    .With("Up", "<Keyboard>/w")
            //    .With("Down", "<Keyboard>/s")
            //    .With("Left", "<Keyboard>/a")
            //    .With("Right", "<Keyboard>/d");

            //lookAction.AddBinding("<Mouse>/delta");
            //attackAction.AddBinding("<Keyboard>/space");

            //moveAction.performed += OnPlayerMove;
            //lookAction.performed += OnPlayerLook;
            //attackAction.performed += OnPlayerAttack;

            //moveAction.Enable();
            //lookAction.Enable();
            //attackAction.Enable();

            moveAction.Disable();
            lookAction.Disable();
            attackAction.Disable();

            moveAction.AddCompositeBinding("2DVector")
                .With("Up", conf["up"])
                .With("Down", conf["down"])
                .With("Left", conf["left"])
                .With("Right", conf["right"]);

            lookAction.AddBinding("<Mouse>/delta");
            attackAction.AddBinding(conf["attack"]);

            moveAction.performed += OnPlayerMove;
            lookAction.performed += OnPlayerLook;
            attackAction.performed += OnPlayerAttack;

            moveAction.Enable();
            lookAction.Enable();
            attackAction.Enable();
        }

        // called whenever the game object that has 
        //      this script is enabled and then we 
        //      enable all of the actions
        private void OnEnable() {

            moveAction.Enable();
            lookAction.Enable();
            attackAction.Enable();
        }

        // same this as above but opposite
        private void OnDisable() {

            moveAction.Disable();
            lookAction.Disable();
            attackAction.Disable();
        }*/
        
        // is called whenever someone presses one of the 
        //      keys defined in OnConfigLoaded. When it happens,
        //      we invoke the Unity action and we pass the 
        //      value associated with the input
        public void OnPlayerMove(InputAction.CallbackContext context) {

            //Vector2 value = context.action.ReadValue<Vector2>();

            Vector2 value = context.ReadValue<Vector2>().normalized;
            //Debug.Log("Move: " + value.ToString());
            if (onMove != null) {

                onMove(value);
            }
        }
       
        public void OnPlayerLook(InputAction.CallbackContext context) {
            //Vector2 value = context.action.ReadValue<Vector2>();
            //Debug.Log("Look: " + value.ToString());
            Vector2 value = context.ReadValue<Vector2>().normalized;
            if (onLook != null) {

                onLook(value);
            }
        }

        public void OnPlayerAttack(InputAction.CallbackContext context) {

            //Debug.Log("Attack");
            if (onAttack != null && context.action.triggered) {

                onAttack();
            }
        }
    }
}
