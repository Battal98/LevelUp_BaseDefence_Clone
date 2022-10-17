using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Enums;

namespace Managers
{
    public class ObjectPoolManager
    {
        private static ObjectPoolManager _instance;
        public static ObjectPoolManager Instance
        {
            get
            {
                if (_instance == null) _instance = new ObjectPoolManager();
                return _instance;
            }
        }

        private readonly Dictionary<PoolType, AbstractObjectPool> _pools;

        
        public ObjectPoolManager()
        {
            _pools = new Dictionary<PoolType, AbstractObjectPool>();
        }


       
        public void AddObjectPool<T>(Func<T> factoryMethod, Action<T> turnOnCallback, Action<T> turnOffCallback, PoolType poolName, int initialStock = 0, bool isDynamic = true)
        {
            if (!_pools.ContainsKey(poolName))
                _pools.Add(poolName, new ObjectPool<T>(factoryMethod, turnOnCallback, turnOffCallback, initialStock, isDynamic));
        }

        
        public void AddObjectPool(AbstractObjectPool pool, PoolType poolName)
        {
            if (_pools.ContainsKey(poolName))
                _pools.Add(poolName, pool);
        }

        
        public ObjectPool<T> GetObjectPool<T>(PoolType poolName)
        {
            return (ObjectPool<T>)_pools[poolName];
        }

       
        public T GetObject<T>(PoolType poolName)
        {
            return ((ObjectPool<T>)_pools[poolName]).GetObject();
        }
        
        public void ReturnObject<T>(T o, PoolType poolName)
        {
            ((ObjectPool<T>)_pools[poolName]).ReturnObject(o);
        }
     
        public void RemovePool(PoolType poolName)
        {
            _pools[poolName] = null;
        }
    }
}