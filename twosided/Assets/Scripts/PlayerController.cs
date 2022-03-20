using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _velocity;

    private Rigidbody2D _rb;
    private Direction _currentPosition;
    private bool _isGrounded;
    private Vector2 forwardVelocity;
    public void Initialize()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentPosition = Direction.Up;

        forwardVelocity = new Vector2(_velocity, 0);
    }

    private void FixedUpdate()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        _rb.velocity += forwardVelocity*Time.fixedDeltaTime;
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
        Debug.Log(Physics2D.gravity);
        _currentPosition = direction;

        Vector2 newPos = new Vector2(transform.position.x, -transform.position.y);
        transform.position = newPos;
    }

    private void Jump(Direction direction)
    {
        Vector2 forceDirection = (direction == Direction.Up) ? Vector2.up : Vector2.down;
        _rb.AddForce(forceDirection * _jumpForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        _isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _isGrounded = false;
    }
}
