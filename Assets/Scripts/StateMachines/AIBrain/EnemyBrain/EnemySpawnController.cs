using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;
using Signals;
using Interfaces;
using Sirenix.OdinInspector;
using Data.ValueObject.AIDatas;

namespace Managers
{
    public class EnemySpawnController : MonoBehaviour, IGetPoolObject
    {
        #region Self Variables
        

        #region Serialized Variables
        [SerializeField]
        private EnemyManager _enemyManager;

        [SerializeField]
        private List<Transform> enemiesTargetsTransform;

        [SerializeField]
        private Transform enemiesSpawnTransform;

        [SerializeField]
        private Transform bossSpawnPos;

        [SerializeField]
        private GameObject spriteTarget;

        #endregion

        #region Private Variables

        [ShowInInspector]
        private List<GameObject> _enemyAIObject = new List<GameObject>();
        private bool _isSpawning;
        private EnemyAIData _enemyAIData;

        #endregion

        #endregion
        private void Awake()
        {
            _enemyAIData = EnemySignals.Instance.onGetEnemyAIData();
        }

        private void Start()
        {
            StartCoroutine(SpawnEnemies());
            SpawnBoss();
        }
        public Transform GetTargetTransform()
        {
            var random = Random.Range(0,enemiesTargetsTransform.Count);
            return enemiesTargetsTransform[random];
        }

        public Transform GetSpawnTransform()
        {
            return enemiesSpawnTransform;
        }

        private IEnumerator SpawnEnemies()
        {
            WaitForSeconds wait = new WaitForSeconds(_enemyAIData.enemySpawnData.SpawnDelay);

            int spawnedEnemies = 0;

            _isSpawning = true;

            while (spawnedEnemies < _enemyAIData.enemySpawnData.SpawnAmount)
            {
                DoSpawnEnemy();
                spawnedEnemies++;
                yield return wait;
            }

            _isSpawning = false;
        }

        public void ReleasedObjectCount(GameObject obj)
        {
            _enemyAIObject.Remove(obj);
            CheckEnemyCount();
        }

        private void CheckEnemyCount()
        {
            if (_enemyAIObject.Count > _enemyAIData.enemySpawnData.SpawnAmount / 2) return;
            if (_isSpawning) return;
            StartCoroutine(SpawnEnemies());
        }

        private void DoSpawnEnemy()
        {
            int randomType = Random.Range(0, Enum.GetNames(typeof(EnemyType)).Length-1);
            int randomPercentage = Random.Range(0, _enemyAIData.enemySpawnData.MaxRandomRange);

            if (randomType == (int)EnemyType.LargeRedEnemy)
            {
                if (randomPercentage < _enemyAIData.enemySpawnData.MaxRandomPercentage)
                    randomType = (int)EnemyType.RedEnemy;
            }

            var poolType = (PoolType)Enum.Parse(typeof(PoolType), ((EnemyType)randomType).ToString());
            var obj = GetObject(poolType);
            _enemyAIObject.Add(obj);
        }

        private void SpawnBoss()
        {
            var bossObj = GetObject(PoolType.Boss);
            bossObj.transform.position = bossSpawnPos.position;
            bossObj.GetComponentInChildren<ThrowEventController>().SpriteTarget = spriteTarget;
        }

        public GameObject GetObject(PoolType pooltype)
        {
            return PoolSignals.Instance.onGetObjectFromPool?.Invoke(pooltype);
        }
    }
}