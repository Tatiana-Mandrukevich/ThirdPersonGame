using System;
using DG.Tweening;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private InputService _inputService;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _gravity;
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

    private void Start()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(_body.DOLocalRotate(new Vector3(0, 90, 0), 2));
        sequence.Append(
            _body.DOShakeScale(1f, new Vector3(0.05f, 0.05f, 0.05f))
                .SetLoops(int.MaxValue, LoopType.Restart)) // если без Append, то вместо int.MaxValue указать -1, чтобы анимация была бесконечной
            //.Join(_body.DOMoveY(_body.position.y + 0.1f, 1f)
            .Join(_body.DOLocalMoveY(_animationTarget.position.y, 1f)
                .SetEase(Ease.InOutSine)
                .SetLoops(int.MaxValue, LoopType.Yoyo));
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
        _animator.SetBool(AnimatorParameters.IsFlying, !_characterController.isGrounded);
    }
    
    private void OnJump()
    {
        if (_characterController.isGrounded)
        {
            _moveDirectionY = 1;
        }
    }

    public void OnFlyStart()
    {
        Debug.Log("OnFlyStart");
    }
    
    public void OnFlyEnd()
    {
        Debug.Log("OnFlyEnd");
    }
}