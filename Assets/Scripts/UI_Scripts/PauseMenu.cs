using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private void Start()
    {
        if (PauseManager.Instance != null)
        {
            PauseManager.Instance.RegisterMenu(gameObject);
        }
        else
        {
            Debug.LogWarning("PauseManager ещё не создан!");
        }
    }
}