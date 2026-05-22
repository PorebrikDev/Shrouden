using UnityEngine;
using UnityEngine.UI;


public class QuitGame : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Quit_Game);
    }

    private void Quit_Game()
    {
        Application.Quit();
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Quit_Game);
    }
}
