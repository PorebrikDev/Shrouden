using UnityEngine;
using DG.Tweening;

public class PlatformController : MonoBehaviour
{
    [Header("Platform Settings")]
    [SerializeField] private PlatformType _platformType = PlatformType.Static;

    [Header("===Rotated===")]
    private float _rotationAngle = 0f;
    [SerializeField] private bool _rotateOnStart;

    [Header("===Movement===")]
    [SerializeField] private float _moveDistance = 10f;
    [SerializeField] private float _moveTime = 3f;

    [Header("===Controlled===")]
    [SerializeField] private float _controlledDistance = 5f;
    [SerializeField] private float _controlledDuration = 1f;

    [Header("===Visual===")]
    [SerializeField] private bool _useVisualEffects = true;

    private SpriteRenderer _spriteRenderer;
    private Tween _rotationTween;

    public bool IsBeingControlled { get; private set; }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InitializePlatformBehavior();
    }

    private void OnDestroy()
    {
        _rotationTween?.Kill();
        transform.DOKill();
        _spriteRenderer?.DOKill();
    }

    private void InitializePlatformBehavior()
    {
        switch (_platformType)
        {
            case PlatformType.Rotating:
                StartRotation();
                break;

            case PlatformType.HorizontalMoving:
                StartHorizontalMovement();
                break;

            case PlatformType.VerticalMoving:
                StartVerticalMovement();
                break;
        }
    }

    public enum PlatformType
    {
        Static = 0,
        Rotating = 1,
        HorizontalMoving = 2,
        VerticalMoving = 3,
    }

    public void SetInteractingActive(bool active)
    {
        if (IsBeingControlled == active) return;

        IsBeingControlled = active;

        if (active)
            OnInteractionStarted();
        else
            OnInteractionStopped();
    }

    private void OnInteractionStarted()
    {
        GameInput.Instance.OnArrowTouth += OnArrowInput;
    }

    private void OnInteractionStopped()
    {
        GameInput.Instance.OnArrowTouth -= OnArrowInput;
    }

    private void OnArrowInput(Vector2 dir)
    {
        transform.DOLocalMove(transform.position + (Vector3)dir * _controlledDistance, _controlledDuration)
            .OnComplete(Start_Atention);
    }

    private void StartRotation()
    {
        _rotationTween?.Kill();

        Sequence rotationSeq = DOTween.Sequence();

        Color colorA = new Color(1, 1, 1, 0.1f);
        Color colorB = new Color(0, 1, 0, 1f);

        if (_rotateOnStart)
        {
            _rotationAngle = 60f;

            colorA = new Color(0, 1, 0, 1f);
            colorB = new Color(1, 1, 1, 0.1f);
        }

        transform.rotation = Quaternion.Euler(0, 0, _rotationAngle);

        if (_useVisualEffects)
        {
            rotationSeq.Join(
                _spriteRenderer.DOColor(colorB, 1f).From(colorA)
            );
        }

        rotationSeq.Join(
            transform.DOLocalRotate(
                transform.localRotation.eulerAngles + new Vector3(0f, 0f, 60f),
                1f
            )
        );

        rotationSeq.SetLoops(-1, LoopType.Yoyo);
        rotationSeq.SetEase(Ease.InOutElastic);

        _rotationTween = rotationSeq;
    }

    private void StartHorizontalMovement()
    {
        transform.DOMoveX(transform.position.x + _moveDistance, _moveTime)
            .SetEase(Ease.InOutCubic)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void StartVerticalMovement()
    {
        transform.DOLocalMoveY(transform.position.y + _moveDistance, _moveTime)
            .SetEase(Ease.InOutCubic)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void Start_Atention()
    {
        Debug.Log("Atention");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && IsMovingPlatform())
        {
            collision.transform.SetParent(transform);

            if (_useVisualEffects)
            {
                _spriteRenderer.DOColor(
                    new Color(Color.springGreen.r, Color.springGreen.g, Color.springGreen.b, 1f),
                    1f
                );
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!gameObject.activeInHierarchy) return;

        if (collision.gameObject.CompareTag("Player") && IsMovingPlatform())
        {
            collision.transform.SetParent(null);

            if (_useVisualEffects)
            {
                _spriteRenderer.DOColor(
                    new Color(Color.white.r, Color.white.g, Color.white.b, 0.1f),
                    1f
                );
            }
        }
    }

    private bool IsMovingPlatform()
    {
        return _platformType == PlatformType.HorizontalMoving
            || _platformType == PlatformType.VerticalMoving;
    }
}