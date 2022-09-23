using System;
using System.Collections;
using System.Collections.Generic;
using StateMachines.AIBrain.Enemy;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using Signals;

namespace Managers
{
    public class EnemySpawnManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<GameObject> enemies = new List<GameObject>();

        #endregion

        #region Public Variables

        public int NumberOfEnemiesToSpawn = 50;

        public float SpawnDelay = 2;
        [SerializeField]
        private Transform spawnPos;

        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            LevelSignals.Instance.onLevelInitDone += InitEnemyPool;
        }
        private void UnsubscribeEvent()
        {
            LevelSignals.Instance.onLevelInitDone -= InitEnemyPool;
        }
        private void OnDisable()
        {
            UnsubscribeEvent();
        }
        #endregion

        private int _listCache;
        private void InitEnemyPool()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                _listCache = i;
                ObjectPoolManager.Instance.AddObjectPool(EnemyFac, TurnOnEnemyAI, TurnOffEnemyAI, ((EnemyType)i).ToString(), 2, true);
            }
            StartCoroutine(SpawnEnemies());
        }

        private GameObject EnemyFac()
        {
            return Instantiate(enemies[_listCache], spawnPos);
        }

        private void TurnOnEnemyAI(GameObject enemy)
        {
            if (enemy) 
                enemy.SetActive(true);
        }

        private void TurnOffEnemyAI(GameObject enemy)
        {
            if (enemy)
                enemy.SetActive(false);
        }
        private void ReleaseEnemyObject(GameObject go, Type t)
        {
            ObjectPoolManager.Instance.ReturnObject(go, t.ToString());
        }

        private IEnumerator SpawnEnemies()
        {
            WaitForSeconds wait = new WaitForSeconds(SpawnDelay);

            int spawnedEnemies = 0;

            while (spawnedEnemies < NumberOfEnemiesToSpawn)
            {
                DoSpawnEnemy();
                spawnedEnemies++;
                yield return wait;
            }
        }

        private void DoSpawnEnemy()
        {

            int randomType = Random.Range(0, Enum.GetNames(typeof(EnemyType)).Length-1);
            int randomPercentage = Random.Range(0, 101);
            _listCache = randomType;
            if (randomType == (int)EnemyType.LargeRedEnemy)
            {
                if (randomPercentage < 30)
                {
                    randomType = (int)EnemyType.RedEnemy;
                }
            }
            ObjectPoolManager.Instance.GetObject<GameObject>(((EnemyType)randomType).ToString());

        }
    }
}