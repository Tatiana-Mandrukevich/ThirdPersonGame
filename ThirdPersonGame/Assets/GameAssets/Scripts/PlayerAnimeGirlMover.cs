using DG.Tweening;
using UnityEngine;

public class PlayerAnimeGirlMover : MonoBehaviour
{
    [SerializeField] private InputService _inputService;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _gravity;
    [SerializeField] private float _moveInputDeadZone = 0.1f;
    [SerializeField] private Transform _body;
    [SerializeField] private Transform _animationTarget;
    
    private CharacterController _characterController;
    private Vector3 _moveDirection;
    private float _moveDirectionY;
    private Animator _animator;
    
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _inputService.Jump += OnJump;
    }

    private void OnDisable()
    {
        _inputService.Jump -= OnJump;
    }

    private void Update()
    {
        _moveDirectionY = Mathf.Max(_gravity, _moveDirectionY + _gravity * Time.deltaTime);
        var moveDirection = new Vector3(_inputService.MoveDirection.x, _moveDirectionY, _inputService.MoveDirection.y);
        _characterController.Move(moveDirection * (Time.deltaTime * _moveSpeed));
        _animator.SetBool(AnimatorParameters.IsMoving, _inputService.MoveDirection.magnitude > _moveInputDeadZone);
    }
    
    private void OnJump()
    {
        if (_characterController.isGrounded)
        {
            _moveDirectionY = 1;
        }
    }
}