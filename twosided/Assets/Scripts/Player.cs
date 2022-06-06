using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action<int> HealthChanged;
    public event Action PlayerDied;
    public event Action CoinPickedUp;
    public event Action PlayerMoved;
    public int MaxHealth { get { return _maxHealth; } }

    [SerializeField] private float _blinkingInterval = 0.15f;
    [SerializeField] private float _blinkingDuration = 1.5f;

    [SerializeField] private int _maxHealth;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _fallMultiplier = 1.5f;
    [SerializeField] private float _velocity;
    [SerializeField] private int _ignoreMask;

    [SerializeField] private Sprite _blinkingSprite;

    private Sprite _actualSprite;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Direction _currentPosition;
    private bool _isGrounded = false;
    private Vector2 _forwardVelocity;
    private float _rayLength;
    private int _currentHealth;

    private Coroutine _blinkingCoroutine;
    private int _playerLayer = 6;
    private int _obstacleLayer = 8;

    private int _lastPosition;

    public void Initialize()
    {
        ToggleCollisions(false);

        _lastPosition = 0;

        _currentHealth = _maxHealth;
        _ignoreMask = ~LayerMask.GetMask("PlayerMask");
        _rb = GetComponent<Rigidbody2D>();
        _currentPosition = Direction.Up;

        _rayLength = transform.localScale.y / 2 + 0.1f;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _actualSprite = _spriteRenderer.sprite;
    }

    private void FixedUpdate()
    {
        MoveForward();
        ModifyFalling();
    }

    private void ModifyFalling()
    {
        if (_isGrounded == false)
        {
            _rb.velocity += Vector2.up * Physics2D.gravity.y * _fallMultiplier * Time.deltaTime;
        }
    }

    private void Update()
    {
        CheckForGround();
        PassPlayerPosition();
    }

    private void MoveForward()
    {
        _forwardVelocity = new Vector2(_velocity, _rb.velocity.y);
        _rb.velocity = _forwardVelocity;
    }

    private void PassPlayerPosition()
    {
        int currentPosition = Mathf.FloorToInt(transform.position.x);
        if (currentPosition > _lastPosition)
        {
            PlayerMoved();
        }
        _lastPosition = currentPosition;
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
        Physics2D.gravity = new Vector2(0, -Physics2D.gravity.y);
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

    private void TakeDamage()
    {
        if (_currentHealth > 0)
        {
            _currentHealth--;
            HealthChanged?.Invoke(_currentHealth);

            SetBlinkingState();

            if (_currentHealth == 0)
                Die();
        }

    }

    private void Die()
    {
        PlayerDied?.Invoke();
        _velocity = 0;
    }

    private void AddHealth()
    {
        if (_currentHealth < 3)
        {
            _currentHealth++;
            HealthChanged?.Invoke(_currentHealth);
        }
    }

    private IEnumerator StartBlinking()
    {
        while (true)
        {
            _spriteRenderer.sprite = _blinkingSprite;
            _spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(_blinkingInterval);
            _spriteRenderer.sprite = _actualSprite;
            _spriteRenderer.color = Color.blue;
            yield return new WaitForSeconds(0.2f);

        }
    }

    private IEnumerator StopBlinking()
    {
        yield return new WaitForSeconds(_blinkingDuration);
        if(_blinkingCoroutine!=null)
        StopCoroutine(_blinkingCoroutine);
        _spriteRenderer.color = Color.blue;
        _blinkingCoroutine = null;
        ToggleCollisions(false);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
            TakeDamage();

        if (collision.gameObject.tag == "Heart")
        {
            AddHealth();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Coin")
        {
            CoinPickedUp?.Invoke();
            Destroy(collision.gameObject);
        }
    }


    private void SetBlinkingState()
    {
        ToggleCollisions();
        if (_blinkingCoroutine != null)
            StopCoroutine(_blinkingCoroutine);
        _blinkingCoroutine = StartCoroutine(StartBlinking());
        StartCoroutine(StopBlinking());
    }

    private void ToggleCollisions(bool value=true)
    {
        Physics2D.IgnoreLayerCollision(_playerLayer, _obstacleLayer, value);
    }

    private void OnDestroy()
    {
        if(_currentPosition==Direction.Down)
        ChangeGravity(Direction.Up);
    }


}
