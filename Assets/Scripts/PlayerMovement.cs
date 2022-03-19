using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _walkSpeed = 3.0f;
    [SerializeField] private float _jumpForce = 5.0f;

    private const float JumpAnimationSpeed = 2.0f;
    private const float DefaultAnimationSpeed = 1.0f;
    private const string Horizontal = "Horizontal";
    private const string Space = "Space";
    private const string State = "State";
    private const float CircleRadius = 0.2f;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private bool _isGrounded = false;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (_isGrounded)
        {
            _animator.speed = DefaultAnimationSpeed;
            _animator.SetInteger(State, (int)MoveState.Idle);
        }

        if (Input.GetButton(Horizontal))
        {
            Walk();
        }

        if (_isGrounded && Input.GetButtonDown(Space))
        {
            Jump();
        }
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, CircleRadius);

        _isGrounded = colliders.Length > 1;

        if (_isGrounded == false)
        {
            _animator.speed = JumpAnimationSpeed;
            _animator.SetInteger(State, (int)MoveState.Jump);
        }
    }

    private void Jump()
    {
        _rigidbody2D.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void Walk()
    {
        Vector3 direction = transform.right * Input.GetAxis(Horizontal);

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, _walkSpeed * Time.deltaTime);

        _spriteRenderer.flipX = direction.x < 0.0f;

        if (_isGrounded)
        {
            _animator.speed = DefaultAnimationSpeed;
            _animator.SetInteger(State, (int)MoveState.Walk);
        }
    }

    private enum MoveState
    {
        Idle,
        Walk,
        Jump
    }
}