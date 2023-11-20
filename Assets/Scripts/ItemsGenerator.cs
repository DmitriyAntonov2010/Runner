using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Enums;
using Interfaces;
using Pools;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemsGenerator : MonoBehaviour
{
    [SerializeField] 
    private ObjectPool _slowDownCoinPool;
    
    [SerializeField] 
    private ObjectPool _speedUpCoinPool;
    
    [SerializeField] 
    private ObjectPool _flyCoinPool;
    
    [SerializeField] 
    private ObjectPool _birdPool;
    
    [SerializeField] 
    private ObjectPool _cactusPool;

    [SerializeField] 
    private float _minSpawnInterval = 5f;
    
    [SerializeField] 
    private float _maxSpawnInterval = 10f;

    private ItemsFactory _itemsFactory;
    private Dictionary<IItemsEffect, ObjectPool> _itemPools = new Dictionary<IItemsEffect, ObjectPool>();

    private void Start()
    {
        _itemsFactory = new ItemsFactory();
        InitializeItemPools();
        GenerateRandomItem();
    }
    
    private void InitializeItemPools()
    {
        _itemPools.Add(_itemsFactory.CreateCoinEffect(CoinType.SlowDown), _slowDownCoinPool);
        _itemPools.Add(_itemsFactory.CreateCoinEffect(CoinType.SpeedUp), _speedUpCoinPool);
        _itemPools.Add(_itemsFactory.CreateCoinEffect(CoinType.Fly), _flyCoinPool);
        _itemPools.Add(_itemsFactory.CreateEnemyEffect(EnemyType.Bird), _birdPool);
        _itemPools.Add(_itemsFactory.CreateEnemyEffect(EnemyType.Cactus), _cactusPool);
    }

    private void GenerateRandomItem()
    {
        var randomCoinType = GetRandomCoinType();
        var randomEnemyType = GetRandomEnemyType();

        var randomItem = Random.Range(0, 2);
        var itemsEffect = randomItem == 0
            ? _itemsFactory.CreateCoinEffect(randomCoinType)
            : _itemsFactory.CreateEnemyEffect(randomEnemyType);

        GenerateItem(itemsEffect);
    }
    
    private EnemyType GetRandomEnemyType()
    {
        return (EnemyType)Random.Range(0, Enum.GetValues(typeof(EnemyType)).Length);
    }
    
    private CoinType GetRandomCoinType()
    {
        return (CoinType)Random.Range(0, Enum.GetValues(typeof(CoinType)).Length);
    }

    private async void GenerateItem(IItemsEffect itemsEffect)
    {
        foreach (var item in _itemPools)
        {
            if (item.Key.Equals(itemsEffect))
            {
                var currentPool = item.Value;
                var pooledItem = currentPool.GetFromPool();
                if (pooledItem != null)
                {
                    pooledItem.transform.position = transform.position;
                    pooledItem.GetComponent<GameItems>().SetCoinPool(currentPool);
                }
            }
        }

        var timeForNextItem = Random.Range(_minSpawnInterval, _maxSpawnInterval);
        await Task.Delay(TimeSpan.FromSeconds(timeForNextItem));
        GenerateRandomItem();
    }
}