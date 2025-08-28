using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour 
{
    [SerializeField] private TextMeshProUGUI _textWave;
    [SerializeField] private TextMeshProUGUI _textEnemy;
    [SerializeField] private Slider _sliderHealth;
    [SerializeField] private PlayerHealth _playerHealth;

    private void OnEnable()
    {
        WaveManager.Instance.ChangeWave += OnChangeWave;
        WaveManager.Instance.ChangeEnemyCount += OnChangeEnemyCount;
        _playerHealth.ChangeHealth += OnChangeHealth;
    }

    private void OnDisable()
    {
        WaveManager.Instance.ChangeWave -= OnChangeWave;
        WaveManager.Instance.ChangeEnemyCount -= OnChangeEnemyCount;
        _playerHealth.ChangeHealth -= OnChangeHealth;
    }

    private void OnChangeHealth(float obj)
    {
        _sliderHealth.value = obj / _playerHealth.MaxHealth;
    }

    private void OnChangeEnemyCount(int obj)
    {
        _textEnemy.text = $"Count Enemy: {obj}";
    }

    private void OnChangeWave(int obj)
    {
        _textWave.text = $"Wave: {obj}";
    }
}