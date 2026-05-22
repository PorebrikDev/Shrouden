using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }
    public static bool IsPaused { get; private set; }
    [SerializeField] private GameObject _menu;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        //GameInput.Instance.OnEscStarted += TogglePause;
    }

    //private void Start()
    //{
    //    _menu = GameObject.Find("MainMenu");

    //    if (_menu != null)
    //    {
    //        _menu.SetActive(false);
    //    }
    //}
    private void OnEnable()
    {
        GameInput.Instance.OnEscStarted += TogglePause;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        GameInput.Instance.OnEscStarted -= TogglePause;

    }

    //private void OnDestroy()
    //{
    //    if (Instance == this)
    //        Instance = null;
    //    if (GameInput.Instance != null)
    //        GameInput.Instance.OnEscStarted -= TogglePause;
    //}
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _menu = null;
    }

    private void TogglePause()
    {
        SetPaused(!IsPaused);
    }

    public void SetPaused(bool state)
    {
        IsPaused = state;

        if (IsPaused)
            Pause();
        else
            Resume();
    }
    public void RegisterMenu(GameObject menu)
    {
        _menu = menu;

        if (_menu != null)
            _menu.SetActive(false);
        //_menu.SetActive(IsPaused);
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        if (_menu != null) _menu.SetActive(true);
    }

    private void Resume()
    {
        Time.timeScale = 1f;
        if (_menu != null) _menu.SetActive(false);
    }
}