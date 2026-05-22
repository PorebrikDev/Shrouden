using UnityEngine;

public class GameBootstrap
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        InstantiateFromResources("GameInput");
        InstantiateFromResources("AudioManager");
        InstantiateFromResources("RespawnManager");
        InstantiateFromResources("PauseManager");
    }

    private static void InstantiateFromResources(string name)
    {
        var prefab = Resources.Load<GameObject>(name);

        if (prefab != null)
            Object.Instantiate(prefab);
        else
            Debug.LogError($"Prefab not found in Resources: {name}");
    }
}