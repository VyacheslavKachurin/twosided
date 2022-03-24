using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _velocity;
    [SerializeField] private int _ignoreMask;

    private Rigidbody2D _rb;
    private Direction _currentPosition;
    private bool _isGrounded = false;
    private Vector2 _forwardVelocity;
    private float _rayLength;

    public void Initialize()
    {
        _ignoreMask = ~LayerMask.GetMask("PlayerMask");
        _rb = GetComponent<Rigidbody2D>();
        _currentPosition = Direction.Up;

        _rayLength = transform.localScale.y / 2 + 0.1f;
    }

    private void FixedUpdate()
    {
        MoveForward();
    }

    private void Update()
    {
        CheckForGround();
    }

    private void MoveForward()
    {
        _forwardVelocity = new Vector2(_velocity,_rb.velocity.y);
        _rb.velocity = _forwardVelocity;
    }

    public void Move(Direction direction)
    {
        if (!_isGrounded)
            return;

        if (_currentPosition != direction)
        {
            ChangeGravity(direction);
            return;
        }

        Jump(direction);
    }

    private void ChangeGravity(Direction direction)
    {
        Physics2D.gravity = new Vector3(0, -Physics2D.gravity.y, 0);
        _currentPosition = direction;

        Vector2 newPos = new Vector2(transform.position.x, -transform.position.y);
        transform.position = newPos;
    }

    private void Jump(Direction direction)
    {
        Vector2 forceDirection = (direction == Direction.Up) ? Vector2.up : Vector2.down;
        _rb.AddForce(forceDirection * _jumpForce);
    }


    private void CheckForGround()
    {
        Vector2 rayDirection = (_currentPosition == Direction.Up) ? Vector2.down : Vector2.up;

        Debug.DrawRay(transform.position, rayDirection, Color.red);

        if (Physics2D.Raycast(transform.position, rayDirection, _rayLength, _ignoreMask))
            _isGrounded = true;
        else
            _isGrounded = false;
    }
}
