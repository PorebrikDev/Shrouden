using UnityEngine;
using DG.Tweening;

public class UIMenuMove : MonoBehaviour
{
    [SerializeField] private Vector2 _moveOffset = new Vector2(15f, -15f);
    [SerializeField] private float _moveTime = 2f;
    [SerializeField] private bool _isLooping = false;

    private RectTransform _rectTransform;
    private Sequence _sequence;

    private Vector2 _startPosition;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _startPosition = _rectTransform.anchoredPosition;
    }

    private void OnEnable()
    {
        PlayAnimation();
    }

    private void OnDisable()
    {
        _sequence?.Kill();

        _rectTransform.anchoredPosition = _startPosition;
    }

    private void PlayAnimation()
    {
        _sequence?.Kill();

        _rectTransform.anchoredPosition = _startPosition;

        _sequence = DOTween.Sequence().SetUpdate(true);

        _sequence.Append(
            _rectTransform.DOAnchorPos(
                _startPosition + _moveOffset,
                _moveTime
            ).SetEase(Ease.OutQuad)
        );

        if (_isLooping)
        {
            _sequence.SetLoops(-1, LoopType.Yoyo);
        }
    }
}