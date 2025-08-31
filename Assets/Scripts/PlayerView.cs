using TMPro;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textWave;
    [SerializeField] private TextMeshProUGUI _textEnemy;
    [SerializeField] private RectTransform _healthBar;
    [SerializeField] private RectTransform _healthBarPerent;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private Canvas _canvas;

    private float _maxWidht;

    public void Enable() 
    {
        WaveManager.Instance.ChangeWave += OnChangeWave;
        WaveManager.Instance.ChangeEnemyCount += OnChangeEnemyCount;
        _playerHealth.ChangeHealth += OnChangeHealth;
    }

    public void Disable() 
    {
        WaveManager.Instance.ChangeWave -= OnChangeWave;
        WaveManager.Instance.ChangeEnemyCount -= OnChangeEnemyCount;
        _playerHealth.ChangeHealth -= OnChangeHealth;
    }

    public void Active() => gameObject.SetActive(true);
    public void Disactive() => gameObject.SetActive(false);

    private void Awake()
    {
        _maxWidht = _healthBarPerent.rect.width;
        SetRight(0);
    }

    private void OnChangeHealth(float obj)
    {
        float value = 1 - (obj / _playerHealth.MaxHealth);
        SetRight(value);
    }

    private void SetRight(float rightValue)
    {
        Vector2 offset = _healthBar.offsetMax;
        offset.x = -rightValue * _maxWidht;
        _healthBar.offsetMax = offset;
    }

    private void OnChangeEnemyCount(int obj)
    {
        _textEnemy.text = $"Count Enemy: {obj}";
    }

    private void OnChangeWave(int obj)
    {
        _textWave.text = $"Wave: {obj}";
    }

    private void Reset()
    {
        SetRight(0);
    }
}