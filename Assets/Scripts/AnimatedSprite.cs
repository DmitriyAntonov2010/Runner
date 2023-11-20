using System;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _sprites;
    
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField] 
    private GameSettings _gameSettings;
    
    private int _frame;
    private bool _isAnimating;

    private void OnEnable()
    {
        _isAnimating = true;
        Animate();
    }

    private void OnDisable()
    {
        _isAnimating = false;
        CancelInvoke();
    }

    private async void Animate()
    {
        if (_isAnimating)
        {
            _frame++;

            if (_frame >= _sprites.Length) {
                _frame = 0;
            }

            if (_frame >= 0 && _frame < _sprites.Length) {
                _spriteRenderer.sprite = _sprites[_frame];
            }

            await Task.Delay(TimeSpan.FromSeconds(1f/_gameSettings.GameSpeed));
            Animate();
        }
    }
}