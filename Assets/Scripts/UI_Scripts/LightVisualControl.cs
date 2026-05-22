using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Light_Ui_Control : MonoBehaviour
{
    [Header("Animtion Type")]
    [SerializeField] private VariantType _variantType;
    [SerializeField] private RenderType _renderType;

    [Header("SunSet - Dawn")]
    [SerializeField] private float _sunsetDelay = 10f;
    [SerializeField] private Color _downMaxColor = new Color(0.5f, 0.6f, 1f, 0.7f);

    [Header("Open - Close")]
    [SerializeField] private float _openCloseDelay = 3f;
    [SerializeField] private Color _fullColor = new Color(1f, 1f, 1f, 1f);
    [SerializeField] private Color _clearColor = new Color(1f, 1f, 1f, 0f);

    [Header("Interactive Button")]
    [SerializeField] private VariantColor _variantColor;
    [SerializeField] private float _interactColorDelay = 1f;
    [SerializeField] private float _scaleStartDelay = 0.3f;
    [SerializeField] private float _scaleBackDelay = 0.9f;
    [SerializeField] private float _scaleUI = 1.2f;

    private Transform _tr;

    private Image _image;
    private SpriteRenderer _spriteRenderer;

    private Color _interactColor;
    private Color _curentColor;
    private Vector3 _normalScale;

    private Tween _colorTween;
    private Tween _scaleTween;

    public bool IsActiveBool { get; private set; } = false;

    private void Awake()
    {
        _tr = transform;
        _normalScale = _tr.localScale;

        SwitchRender();

        _curentColor = GetColor();
    }

    private void OnEnable()
    {
        SetInteractColor();
        PlayAnimation();
    }

    private void OnDisable()
    {
        _colorTween?.Kill();
        _scaleTween?.Kill();

        _tr.localScale = _normalScale;
        SetColor(_curentColor);
    }

    private void SwitchRender()
    {
        _image = null;
        _spriteRenderer = null;

        switch (_renderType)
        {
            case RenderType.Image:
                _image = GetComponent<Image>();
                break;

            case RenderType.SpriteRenderer:
                _spriteRenderer = GetComponent<SpriteRenderer>();
                break;
        }
    }

    private Color GetColor()
    {
        return _renderType switch
        {
            RenderType.Image => _image != null ? _image.color : Color.white,
            RenderType.SpriteRenderer => _spriteRenderer != null ? _spriteRenderer.color : Color.white,
            _ => Color.white
        };
    }

    private void SetColor(Color color)
    {
        switch (_renderType)
        {
            case RenderType.Image:
                if (_image != null)
                    _image.color = color;
                break;

            case RenderType.SpriteRenderer:
                if (_spriteRenderer != null)
                    _spriteRenderer.color = color;
                break;
        }
    }

    private Tween DOColorTarget(Color target, float duration)
    {
        Tween t = _renderType switch
        {
            RenderType.Image => _image != null ? _image.DOColor(target, duration) : null,
            RenderType.SpriteRenderer => _spriteRenderer != null ? _spriteRenderer.DOColor(target, duration) : null,
            _ => null
        };

        return t?.SetUpdate(true);
    }

    private void PlayAnimation()
    {
        switch (_variantType)
        {
            case VariantType.Sunset_Dawn:
                DO_Sunset_Down_Long();
                break;

            case VariantType.Dawn_Fast:
                DO_Dawn_Fast();
                break;

            case VariantType.Open:
                DO_Open();
                break;

            case VariantType.Close:
                DO_Close();
                break;

            case VariantType.Interactive_Button:
                IsActiveBool = true;
                break;
        }
    }

    private void SetInteractColor()
    {
        _interactColor = _variantColor switch
        {
            VariantColor.Blue => new Color(0.35f, 0.66f, 1f, 1f),
            VariantColor.Purple => new Color(0.49f, 0.36f, 1f, 1f),
            VariantColor.White => new Color(0.90f, 0.93f, 1f, 1f),
            VariantColor.Orange => new Color(1f, 0.62f, 0.26f, 1f),
            _ => Color.white
        };
    }

    private void DO_Sunset_Down_Long()
    {
        _colorTween?.Kill();

        SetColor(_fullColor);

        _colorTween = DOColorTarget(_downMaxColor, _sunsetDelay)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void DO_Dawn_Fast()
    {
        _colorTween?.Kill();

        SetColor(_downMaxColor);

        _colorTween = DOColorTarget(_fullColor, _sunsetDelay);
    }

    private void DO_Open()
    {
        _colorTween?.Kill();

        SetColor(_fullColor);

        _colorTween = DOColorTarget(_clearColor, _openCloseDelay)
            .SetEase(Ease.InOutSine);
    }

    private void DO_Close()
    {
        _colorTween?.Kill();

        SetColor(_clearColor);

        _colorTween = DOColorTarget(_fullColor, _openCloseDelay)
            .SetEase(Ease.InOutSine);
    }

    public void DO_Interactive_Start_Button()
    {
        if (!IsActiveBool) return;

        _colorTween?.Kill();
        _scaleTween?.Kill();

        _colorTween = DOColorTarget(_interactColor, _interactColorDelay)
            .SetEase(Ease.OutQuad);

        _scaleTween = _tr.DOScale(_normalScale * _scaleUI, _scaleStartDelay)
            .SetUpdate(true);
    }

    public void DO_Interactive_Exit_Button()
    {
        if (!IsActiveBool) return;

        _colorTween?.Kill();
        _scaleTween?.Kill();

        _colorTween = DOColorTarget(_curentColor, _interactColorDelay)
            .SetEase(Ease.OutQuad);

        _scaleTween = _tr.DOScale(_normalScale, _scaleBackDelay)
            .SetUpdate(true);
    }

    public enum VariantType { None, Sunset_Dawn, Dawn_Fast, Open, Close, Interactive_Button, }
    public enum VariantColor { Blue, Purple, White, Orange }
    public enum RenderType { Image, SpriteRenderer }
}
