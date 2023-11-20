using System;
using Interfaces;
using UnityEngine;

public abstract class GameItems : MonoBehaviour
{
    [SerializeField] 
    protected GameSettings _gameSettings;

    private float _leftEdge;
    private IPool _coinPool;
    
    private void Start()
    {
        _leftEdge = Camera.main!.ScreenToWorldPoint(Vector3.zero).x - 2f;
    }
    
    private void Update()
    {
        transform.position += _gameSettings.GameSpeed * Time.deltaTime * Vector3.left;

        if (transform.position.x < _leftEdge)
        {
            ReturnToPool();
        }
    }
    
    public void SetCoinPool(IPool coinPool)
    {
        _coinPool = coinPool ?? throw new ArgumentNullException(nameof(coinPool));
    }
    
    protected void ReturnToPool()
    {
        _coinPool.ReturnToPool(gameObject);
    }
    
    protected abstract void OnTriggerEnter(Collider other);
}
