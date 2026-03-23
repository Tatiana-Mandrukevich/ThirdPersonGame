using UnityEngine;

public class PlayerAnimeGirlMover : MonoBehaviour
{
    [SerializeField] private InputService _inputService;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _jumpVelocity = 5f;
    [SerializeField] private float _groundedStickVelocity = -2f;
    [SerializeField] private float _moveInputDeadZone = 0.1f;
    [SerializeField] private Transform _body;
    [SerializeField] private Transform _animationTarget;
    [SerializeField] private float _visualGroundOffset = 0f;

    private CharacterController _characterController;
    private float _verticalVelocity;
    private Animator _animator;
    private Vector3 _bodyStartLocalPosition;
    private bool _isJumping;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        if (_body == null || _body == transform)
        {
            var armature = transform.Find("Armature");
            if (armature != null)
                _body = armature;
        }

        if (_body != null && _body != transform)
            _bodyStartLocalPosition = _body.localPosition;
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
        if (_characterController.isGrounded && _verticalVelocity < 0f)
        {
            _verticalVelocity = _groundedStickVelocity;

            if (_isJumping)
            {
                _isJumping = false;
                _animator.SetBool(AnimatorParameters.IsJumping, false);
            }
        }

        _verticalVelocity += _gravity * Time.deltaTime;

        Vector3 horizontalVelocity = new Vector3(_inputService.MoveDirection.x, 0f, _inputService.MoveDirection.y) * _moveSpeed;
        Vector3 moveVelocity = horizontalVelocity + Vector3.up * _verticalVelocity;

        _characterController.Move(moveVelocity * Time.deltaTime);

        bool isMoving = _inputService.MoveDirection.magnitude > _moveInputDeadZone;
        _animator.SetBool(AnimatorParameters.IsMoving, isMoving);
    }

    private void LateUpdate()
    {
        var localPosition = _body.localPosition;
        localPosition.y = _bodyStartLocalPosition.y + _visualGroundOffset;
        _body.localPosition = localPosition;
    }

    private void OnJump()
    {
        if (_characterController.isGrounded)
        {
            _verticalVelocity = _jumpVelocity;
            _isJumping = true;
            _animator.SetBool(AnimatorParameters.IsJumping, true);
        }
    }
}