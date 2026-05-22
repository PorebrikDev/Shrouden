using UnityEngine;
using UnityEngine.UI;

public class LevelSetUI : MonoBehaviour
{
    [SerializeField] private SceneLoadButton _loadButton;
    private string _currentName;
    [SerializeField] private Button _lvlForest;
    [SerializeField] private Button _lvlFrozen;

    private void Start()
    {
        _lvlForest.onClick.AddListener(SetForest);
        _lvlFrozen.onClick.AddListener(SetFrozen);


    }

    private void OnDestroy()
    {
        _lvlForest.onClick.RemoveListener(SetForest);
        _lvlFrozen.onClick.RemoveListener(SetFrozen);
    }

    private void SetForest()
    {
        _loadButton .ChangePointName("Start");
    }

    private void SetFrozen()
    {
        _loadButton.ChangePointName("IceHold");
    }
}
