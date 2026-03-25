using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameAssets.Scripts.DanceBattle
{
    public class InputService : MonoBehaviour
    {
        private InputSystem_Actions _actions;

        public event Action<int> SpecialClick;

        private void Awake()
        {
            _actions = new InputSystem_Actions();
        }  
        
        private void OnEnable()
        {
            _actions.Enable();
            _actions.Player.Special1.performed += OnSpecial1;
            _actions.Player.Special2.performed += OnSpecial2;
        }

        private void OnDisable()
        {
            _actions.Disable();
            _actions.Player.Special1.performed -= OnSpecial1;
            _actions.Player.Special2.performed -= OnSpecial2;
        }

        private void OnSpecial1(InputAction.CallbackContext _) => SpecialClick?.Invoke(1);
        
        private void OnSpecial2(InputAction.CallbackContext _) => SpecialClick?.Invoke(2);
    }
}