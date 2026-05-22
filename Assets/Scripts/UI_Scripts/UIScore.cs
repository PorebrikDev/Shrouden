using TMPro;
using UnityEngine;

public class UI_Score : MonoBehaviour
{
    [SerializeField] private Manager_Score _manager;

    [Header("UI")]
    [SerializeField] private TMP_Text _coinsText;
    [SerializeField] private TMP_Text _scoreText;

    private void OnEnable()
    {
        _manager.OnChangeScoreCoins += UpdateUI;
    }

    private void OnDisable()
    {
        _manager.OnChangeScoreCoins -= UpdateUI;
    }

    private void UpdateUI(int coins, int score)
    {
        _coinsText.text = $"Coins: {coins}";
        _scoreText.text = $"Score: {score}";
    }
}