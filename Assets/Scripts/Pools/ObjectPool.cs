using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Pools
{
    public class ObjectPool : MonoBehaviour, IPool
    {
        [SerializeField] 
        protected GameObject _prefab;
    
        [SerializeField] 
        private int _poolSize = 10;

        private Queue<GameObject> _objectPool = new Queue<GameObject>();

        private void Awake()
        {
            InitializeObjectPool();
        }

        private void InitializeObjectPool()
        {
            for (int i = 0; i < _poolSize; i++)
            {
                var obj = Instantiate(_prefab, transform);
                obj.SetActive(false);
                _objectPool.Enqueue(obj);
            }
        }

        public GameObject GetFromPool()
        {
            if (_objectPool.Count == 0)
            {
                Debug.LogWarning("Object pool is empty. Consider increasing pool size.");
                return null;
            }

            var obj = _objectPool.Dequeue();
            if (obj != null)
            {
                obj.SetActive(true);    
            }

            return obj;
        }

        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
            _objectPool.Enqueue(obj);
        }
    }
}