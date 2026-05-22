using UnityEngine;
using UnityEngine.UI;

public class UIKeyBring : MonoBehaviour
{
    [SerializeField] private Image _image;
    private Button _button;


    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _button.onClick.AddListener(KeyBrings);
        _image.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(KeyBrings);
    }

    private void KeyBrings()
    {
        _image.gameObject.SetActive(!_image.gameObject.activeSelf);
    }
}
