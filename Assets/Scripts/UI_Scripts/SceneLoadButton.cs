using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class SceneLoadButton : MonoBehaviour
{
    [Header("Scene Settings")]
    [Tooltip("Scene name from Build Settings")]
    [SerializeField] private string _sceneName;
    [Header("Player position")]
    [SerializeField] private string _spawnPointName;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(LoadScene);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(LoadScene);
    }

    public void ChangePointName(string name)
    {
        _spawnPointName = name;
        Debug.Log(_spawnPointName);
    }

    private void LoadScene()
    {
        if (string.IsNullOrWhiteSpace(_sceneName))
        {
            Debug.LogError($"Scene name is empty on {gameObject.name}");
            return;
        }

        AudioManager.Instance.StopMusic();

        if (_spawnPointName != null)
            RespawnManager.Instance.SetSpawnName(_spawnPointName);

        SceneManager.LoadSceneAsync(_sceneName);
        PauseManager.Instance.SetPaused(false);
    }
}
