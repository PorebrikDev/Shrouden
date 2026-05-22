using System.Collections;
using UnityEngine;

public class Trorn : MonoBehaviour
{
    [SerializeField] private float _activeTimer = 2f;
    [SerializeField] private float _hiddenTimer = 3f;

    private Animator _animator;
    private BoxCollider2D _boxCollider;
    private Coroutine _coroutine;



    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        _coroutine = StartCoroutine(ThornCycle());
    }

    private IEnumerator ThornCycle()
    {
        while (true)
        {
            ChangeAnimation(true);
            yield return new WaitForSeconds(_activeTimer);
            ChangeAnimation(false);
            yield return new WaitForSeconds(_hiddenTimer);
        }
    }

    public void FallingDown()
    {
        StopCoroutine(_coroutine);
        ChangeAnimation(true);
    }

    private void ChangeAnimation(bool x)
    {
        _animator.SetBool("Open", x);
    }

    public void OpenBoxColider()
    {
        _boxCollider.enabled = true;
    }

    public void CloseBoxColider()
    {
        _boxCollider.enabled = false;
    }

    private void OnDestroy()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }
}
