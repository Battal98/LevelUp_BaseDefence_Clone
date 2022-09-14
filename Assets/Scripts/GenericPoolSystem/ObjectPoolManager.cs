using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

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

        private readonly Dictionary<string, AbstractObjectPool> _pools;

        
        public ObjectPoolManager()
        {
            _pools = new Dictionary<string, AbstractObjectPool>();
        }

       
        public void AddObjectPool<T>(Func<T> factoryMethod, Action<T> turnOnCallback, Action<T> turnOffCallback, int initialStock = 0, bool isDynamic = true)
        {
            if(!_pools.ContainsKey(typeof(T)+"ByType"))
                _pools.Add(typeof(T) + "ByType", new ObjectPool<T>(factoryMethod, turnOnCallback, turnOffCallback, initialStock, isDynamic));
        }

       
        public void AddObjectPool<T>(Func<T> factoryMethod, Action<T> turnOnCallback, Action<T> turnOffCallback, string poolName, int initialStock = 0, bool isDynamic = true)
        {
            if (!_pools.ContainsKey(poolName))
                _pools.Add(poolName, new ObjectPool<T>(factoryMethod, turnOnCallback, turnOffCallback, initialStock, isDynamic));
        }

        
        public void AddObjectPool<T>(Func<T> factoryMethod, Action<T> turnOnCallback, Action<T> turnOffCallback, List<T> initialStock, bool isDynamic = true) where T : AbstractObjectPool, new()
        {
            if (!_pools.ContainsKey(typeof(T) + "ByType"))
                _pools.Add(typeof(T) + "ByType", new ObjectPool<T>(factoryMethod, turnOnCallback, turnOffCallback, initialStock, isDynamic));
        }

        
        public void AddObjectPool<T>(Func<T> factoryMethod, Action<T> turnOnCallback, Action<T> turnOffCallback, List<T> initialStock, string poolName, bool isDynamic = true) where T : AbstractObjectPool, new()
        {
            if (!_pools.ContainsKey(poolName))
                _pools.Add(poolName, new ObjectPool<T>(factoryMethod, turnOnCallback, turnOffCallback, initialStock, isDynamic));
        }

      
        public void AddObjectPool(AbstractObjectPool pool)
        {
            if (!_pools.ContainsKey(pool.GetType() + "ByType"))
                _pools.Add(pool.GetType() + "ByType", pool);
        }

        
        public void AddObjectPool(AbstractObjectPool pool, string poolName)
        {
            if (_pools.ContainsKey(poolName))
                _pools.Add(poolName, pool);
        }

        public ObjectPool<T> GetObjectPool<T>()
        {
            return (ObjectPool<T>)_pools[typeof(T) + "ByType"];
        }

        
        public ObjectPool<T> GetObjectPool<T>(string poolName)
        {
            return (ObjectPool<T>)_pools[poolName];
        }

       
        public T GetObject<T>()
        {
            return ((ObjectPool<T>)_pools[typeof(T) + "ByType"]).GetObject();
        }

       
        public T GetObject<T>(string poolName)
        {
            return ((ObjectPool<T>)_pools[poolName]).GetObject();
        }

        
        public void ReturnObject<T>(T o)
        {
            ((ObjectPool<T>)_pools[typeof(T) + "ByType"]).ReturnObject(o);
        }
        
        public void ReturnObject<T>(T o, string poolName)
        {
            Debug.Log("poolName"+poolName+"T : "+o);
            ((ObjectPool<T>)_pools[poolName]).ReturnObject(o);
        }
        
        public void RemovePool<T>()
        {

            _pools[typeof(T) + "ByType"] = null;
        }

     
        public void RemovePool(string poolName)
        {
            _pools[poolName] = null;
        }
    }
}