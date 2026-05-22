using UnityEngine;
using UnityEngine.UI;

public class ToggleUI : MonoBehaviour
{
    [SerializeField] private GameObject target;

    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Toggle);
    }


    public void Toggle()
    {
        target.SetActive(!target.activeSelf);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(Toggle);
    }
}