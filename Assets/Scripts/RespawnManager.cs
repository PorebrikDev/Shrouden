using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance { get; private set; }

    [SerializeField] private TriggerInsideScreen _triggerInsideScreen;
    [SerializeField] private Vector3 _currentRespawnPlase;

    private Vector3 _corestPosY = new Vector3(0f,5f,0f);

    private Dictionary<string, Vector3> _respawnTransforms = new();

    private Transform _player;
    private string _currentSpawnName;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetSpawnName(string name)
    {
        _currentSpawnName = name;
    }

    public Vector3 GetSpawnPosition()
    {
        Respawnposition[] points = Object.FindObjectsByType<Respawnposition>(FindObjectsSortMode.None);

        foreach (var point in points)
        {
            if (point.LocationName == _currentSpawnName)
            {
                return point.transform.position + _corestPosY;
            }
        }

        Debug.Log("Spawn not found: " + _currentSpawnName);
        return Player.Instance.SceneZeroPos;
    }

    public void RegisterTrigger(TriggerInsideScreen trigger)
    {
        _triggerInsideScreen = trigger;

        _triggerInsideScreen.OnPlayerIsDropped -= GetPlayerPosition;
        _triggerInsideScreen.OnPlayerIsDropped += GetPlayerPosition;
    }

    public void SetPlayer(Transform player)
    {
        _player = player;

        _currentRespawnPlase = player.position;
    }

    private void Start()
    {
        if(Player.Instance != null)
        _currentRespawnPlase = Player.Instance.transform.position;
    }

    public void GetPlayerPosition()
    {
        Player.Instance.SelfTeleport(_currentRespawnPlase);
    }

    public void SetRespawnPlase(string name, Vector3 transformPos, bool inList)
    {
        _currentRespawnPlase = transformPos + _corestPosY;

        if (!_respawnTransforms.ContainsKey(name))
        {
            _respawnTransforms.Add(name, transformPos);
        }
    }
}
