using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-99)]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    #region Serialized Fields
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 6f;

    [Header("Jump Settings")]
    [SerializeField] private float _jumpForce = 12f;
    [SerializeField] private float _jumpWallForce = 20f;
    [SerializeField] private float _jumpWallUpStrange = 0.3f;
    [SerializeField] private float _controlDelay = 0.1f;

    [Header("Ground & Wall Settings")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _wallLayer;

    #endregion
    [Header("Components")]
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private TriggerInsideScreen _triggerInsideScreen;

    #region Private Fields
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _inputVector;
    private Vector2 _lastMoveDirection;
    private Vector2 _jumpDirection;
    private bool _isGrounded;
    private bool _isTouchingWall;
    private int _facingDir = 1;
    private Vector3 _sceneZeroPos;
    public Vector3 SceneZeroPos => _sceneZeroPos;
    #endregion

    #region Properties
    public Vector2 FacingDirection => new Vector2(_facingDir, 0);
    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        RespawnManager.Instance?.SetPlayer(transform);
        _sceneZeroPos = transform.position;

    }
    private void Start()
    {
        transform.position = RespawnManager.Instance.GetSpawnPosition();
    }

    private void OnEnable()
    {
        GameInput.Instance.OnPlayerJumpStarted += Jump;
        GameInput.Instance.OnPunchStarted += PlayerPunchStarted;
    }
    private void OnDisable()
    {
        GameInput.Instance.OnPlayerJumpStarted -= Jump;
        GameInput.Instance.OnPunchStarted -= PlayerPunchStarted;
    }

    private void Update()
    {
        _inputVector = GameInput.Instance.GetMovementVector();
        if (_inputVector.sqrMagnitude > 0.01f)
            _lastMoveDirection = _inputVector;
        if (Mathf.Abs(_inputVector.x) > 0.1f)
        {
            _facingDir = _inputVector.x > 0 ? 1 : -1;
        }
        Flip();
    }

    private void FixedUpdate()
    {
        GroundCheck();
        WallCheck();
        WallLogic();
        MovementHandler();
    }

    #endregion
    public void SelfTeleport(Vector3 transPos)
    {
        _rigidbody.linearVelocity = Vector2.zero;
        _rigidbody.angularVelocity = 0f;
        transform.position = transPos;
    }
    private void Flip()
    {
        _spriteRenderer.flipX = _facingDir == -1;
    }

    private void MovementHandler()
    {
        float targetSpeed = _inputVector.x * _moveSpeed;
        float speedDiff = targetSpeed - _rigidbody.linearVelocity.x;

        float acceleration = 20f;

        _rigidbody.AddForce(Vector2.right * speedDiff * acceleration, ForceMode2D.Force);
    }

    private void PlayerPunchStarted(object sender, EventArgs e)
    {
        playerAnimation.PunchAnimationStarted();
    }

    private void Jump(object sender, System.EventArgs e)
    {

        if (_isGrounded)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, 0f);
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            playerAnimation.JumpStarted();
        }
        if (_isTouchingWall && _isGrounded == false)
        {
            JumpFromWall();
        }
    }

    private void GroundCheck()
    {
        bool wasGrounded = _isGrounded;
        Vector2 groundCenter = (Vector2)transform.position + Vector2.down * 1.2f;

        _isGrounded = Physics2D.OverlapBox(
            groundCenter,
            new Vector2(0.8f, 0.2f),
            0f,
            _groundLayer | _wallLayer
        );
        if (!wasGrounded && _isGrounded)
        {
            playerAnimation.JumpStopped();
        }
    }

    private void WallCheck()
    {
        Vector2 leftCenter = (Vector2)transform.position + Vector2.left * 0.5f;
        Vector2 rightCenter = (Vector2)transform.position + Vector2.right * 0.5f;

        bool rightWall = Physics2D.OverlapBox(
            rightCenter,
            new Vector2(0.2f, 1.2f),
            0f,
            _wallLayer
        );

        bool leftWall = Physics2D.OverlapBox(
            leftCenter,
            new Vector2(0.2f, 1.2f),
            0f,
            _wallLayer
        );

        if (rightWall)
        {
            _isTouchingWall = true;
            _jumpDirection = new Vector2(-1f, _jumpWallUpStrange).normalized;
            return;
        }

        if (leftWall)
        {
            _isTouchingWall = true;
            _jumpDirection = new Vector2(1f, _jumpWallUpStrange).normalized;
            return;
        }

        _isTouchingWall = false;
    }

    private void WallLogic()
    {
        if (!_isGrounded && _isTouchingWall && _inputVector.sqrMagnitude > 0.01f)
        {
            playerAnimation.WallStarted();
        }

        else
        {
            playerAnimation.WallStopped();
        }
    }

    private void JumpFromWall()
    {
        GameInput.Instance.LostControl();
        _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, 0f);
        _rigidbody.AddForce(_jumpDirection * _jumpWallForce, ForceMode2D.Impulse);
        playerAnimation.JumpStarted();
        StartCoroutine(MovePauseCoroutine());
    }

    private IEnumerator MovePauseCoroutine()
    {
        yield return new WaitForSeconds(_controlDelay);
        GameInput.Instance.ReturnControl();
    }

    #region Debug
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector2 origin = transform.position;

        float groundWidth = 0.8f;
        float groundHeight = 0.2f;
        Vector2 groundCenter = origin + Vector2.down * 1.2f;

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundCenter, new Vector3(groundWidth, groundHeight, 0f));

        float wallWidth = 0.2f;
        float wallHeight = 1.2f;
        Vector2 leftCenter = origin + Vector2.left * 0.5f;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(leftCenter, new Vector3(wallWidth, wallHeight, 0f));

        Vector2 rightCenter = origin + Vector2.right * 0.5f;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(rightCenter, new Vector3(wallWidth, wallHeight, 0f));
    }
    #endregion
}