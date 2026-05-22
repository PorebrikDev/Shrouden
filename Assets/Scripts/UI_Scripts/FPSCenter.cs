using UnityEngine;

public class FPSCenter : MonoBehaviour
{
    public static FPSCenter Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        QualitySettings.vSyncCount = 0;
        ApplySaved();
    }

    public void ApplySaved()
    {
        int fps = PlayerPrefs.GetInt("FPS", 120);
        SetFPS(fps);
    }

    public void SetFPS(int fps)
    {
        Application.targetFrameRate = fps;
        PlayerPrefs.SetInt("FPS", fps);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}
