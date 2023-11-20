using System;
using System.Threading.Tasks;
using Items;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] 
    private GameSettings _gameSettings;

    [SerializeField] 
    private CharacterController _characterController;

    private Vector3 _direction = Vector3.zero;
    private float _defaultSpeed;
    private float _score;
    private bool _canFly;
    private bool _isSpeedChanged;
    private IGameViewObserver _gameViewObserver;

    private void Awake()
    {
        Time.timeScale = 1;
        _defaultSpeed = _gameSettings.GameSpeed;
    }

    private void Update()
    {
        Jump();    
        Fly();
        UpdateScore();
    }
    
    public void SetObserver(IGameViewObserver gameViewObserver)
    {
        _gameViewObserver = gameViewObserver;
    }

    private void Jump()
    {
        _direction += _gameSettings.Gravity * Time.deltaTime * Vector3.down;

        if (_characterController.isGrounded)
        {
            _direction = Vector3.down;
            if (Input.GetMouseButtonDown(0))
            {
                _direction = Vector3.up * _gameSettings.PlayerForceJump;
            }
        }

        _characterController.Move(_direction * Time.deltaTime);
    }

    private void UpdateScore()
    {
        _score += _gameSettings.GameSpeed * Time.deltaTime;
        _gameViewObserver?.UpdateScore(Mathf.FloorToInt(_score).ToString("D5"));
    }
    
    private async Task WaitUntilTimeRunsOut(TimeSpan duration)
    {
        var updateInterval = TimeSpan.FromSeconds(1);
        while (duration.TotalMilliseconds > 0)
        {
            if (Time.timeScale == 0)
            {
                duration = TimeSpan.Zero;
            }
            _gameViewObserver?.UpdateTimeLeft($"Wait for {duration.TotalSeconds} seconds until the effect wears off");
            await Task.Delay(updateInterval);
            duration -= updateInterval;
        }
        
        _gameViewObserver?.UpdateTimeLeft(string.Empty);
    }

    private void Fly()
    {
        if (_canFly && transform.position.y <= _gameSettings.MaxFlyHeight)
        {
            var position = transform.position;
            position = Vector3.Lerp(position, new Vector3(position.x, _gameSettings.MaxFlyHeight, position.z), 1f);
            transform.position = position;
        }
    }
    
    public async void ApplySlowDownEffect(TimeSpan duration)
    {
        if (Math.Abs(_gameSettings.GameSpeed - _defaultSpeed) < 0.01f)
        {
            _gameSettings.GameSpeed = _gameSettings.SlowDown;
            _isSpeedChanged = true;
            await WaitUntilTimeRunsOut(duration);
            _gameSettings.GameSpeed = _defaultSpeed;
            _isSpeedChanged = false;
        }
    }
  
    public async void ApplySpeedUpEffect(TimeSpan duration)
    {
        if (Math.Abs(_gameSettings.GameSpeed - _defaultSpeed) < 0.01f)
        {
            _gameSettings.GameSpeed = _gameSettings.SpeedUp;
            _isSpeedChanged = true;
            await WaitUntilTimeRunsOut(duration);
            _gameSettings.GameSpeed = _defaultSpeed;
            _isSpeedChanged = false;
        }
    }

    public async void ApplyFlyEffect(TimeSpan duration)
    {
        if (!_isSpeedChanged)
        {
            _canFly = true;
            await WaitUntilTimeRunsOut(duration);
            _canFly = false;    
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        _gameViewObserver?.GameOver(_score);
    }
  
    public bool CanCollect()
    {
        return !_canFly && !_isSpeedChanged;
    }
}