using UnityEngine;
using UnityEngine.EventSystems;

public class Interactiv_Batton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Light_Ui_Control _ui_Control;
    private UniversalAudioClip _audioClip;

    private void Awake()
    {
            _ui_Control = GetComponent<Light_Ui_Control>();
            _audioClip = GetComponent<UniversalAudioClip>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _ui_Control.DO_Interactive_Start_Button();
        _audioClip.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _ui_Control.DO_Interactive_Exit_Button();
    }
}
