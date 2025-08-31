using UnityEngine;

public class Game : MonoBehaviour 
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private WaveManager _waveManager;
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private StartMenuView _startMenuView;
    [SerializeField] private DeathMenuView _deathView;

    private void OnEnable()
    {
        _playerHealth.Death += GameOver;
        _startMenuView.StartGame += StartGame;
        _deathView.RestartGame += RestartGame;
    }

    private void OnDisable()
    {
        _playerHealth.Death -= GameOver;
        _startMenuView.StartGame -= StartGame;
        _deathView.RestartGame -= RestartGame;
    }

    private void RestartGame()
    {
        _playerView.Enable();
        _waveManager.Reset();
        _playerHealth.Reset();
        _playerMover.Reset();
        _bulletPool.Reset();
        _waveManager.Difficulty = _deathView.ChoiseDifficulty;
        Time.timeScale = 1.0f;
        _waveManager.ActiveGenerator = true;
        _deathView.DisActive();
        _playerView.Active();
    }

    private void StartGame()
    {
        _waveManager.Difficulty = _startMenuView.ChoiseDifficulty;
        _deathView.SetDifficulty( _waveManager.Difficulty);
        _waveManager.ActiveGenerator = true;
        Time.timeScale = 1.0f;
        _startMenuView.Disactive();
        _playerView.Enable();
        _playerView.Active();
    }

    private void GameOver()
    {
        Time.timeScale = 0f;
        _deathView.Active();
        _playerView.Disable();
        _playerView.Disactive();
        _waveManager.ActiveGenerator = false;
    }

    private void Awake()
    {
        Time.timeScale = 0f;
        _playerView.Disactive();
        _startMenuView.Active();
        _deathView.DisActive();
    }
}
