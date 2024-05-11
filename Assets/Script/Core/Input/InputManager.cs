using BulletJam.Helper;
using UnityEngine;
using UnitySingleton;

namespace BulletJam.Core.Input
{
    public class InputManager : MonoSingleton<InputManager>
    {
        private PlayerActionMap inputActions;

        [field: SerializeField] public Vector2 MoveInputs { get; private set; }
        [field: SerializeField] public Vector2 LookInputs { get; private set; }
        [field: SerializeField] public bool IsFiring { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            inputActions = new PlayerActionMap();
            inputActions.Enable();
        }

        private void OnEnable()
        {
            EnablePlayerInputs();
        }

        private void OnDisable()
        {
            DisablePlayerInputs();
        }

        private void OnDestroy()
        {
            inputActions.Disable();
        }

        private void Update()
        {
            if (!inputActions.Player.enabled)
                return;

            MoveInputs = inputActions.Player.Move.ReadValue<Vector2>();
            LookInputs = inputActions.Player.Look.ReadValue<Vector2>();
            IsFiring = System.Convert.ToBoolean(inputActions.Player.Fire.ReadValue<float>());
        }

        public void EnablePlayerInputs() => inputActions.Player.Enable();

        public void DisablePlayerInputs() => inputActions.Player.Disable();
    }
}