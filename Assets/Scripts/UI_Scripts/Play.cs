using UnityEngine;
using UnityEngine.UI;

public class Play : MonoBehaviour
{
    private Button _button;
    private void Awake()
    {
        _button = GetComponent<Button>();
    }
    private void Start()
    {
        _button.onClick.AddListener(StartPlay);
    }
    private void StartPlay()
    {
        PauseManager.Instance.SetPaused(false);
    }
    private void OnDestroy()
    {
        _button.onClick.RemoveListener(StartPlay);


    }
}
