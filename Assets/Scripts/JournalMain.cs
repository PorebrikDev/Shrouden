using UnityEngine;

public class JournalMain : MonoBehaviour
{
    private void Start()
    {
        GameInput.Instance.OnJurnalStarded += OnJurnalVision;
        gameObject.SetActive(false);
    }

    public void OnJurnalVision(object sender, System.EventArgs e)
    {
        SwitchVision();
    }

    private void SwitchVision()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
    private void OnDestroy()
    {
        GameInput.Instance.OnJurnalStarded -= OnJurnalVision;

    }
}