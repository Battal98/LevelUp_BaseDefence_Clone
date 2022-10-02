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
using Interfaces;

namespace Managers
{
    public class EnemySpawnManager : MonoBehaviour, IGetPoolObject
    {
        #region Self Variables

        #region Serialized Variables

        #endregion

        #region Public Variables

        public int NumberOfEnemiesToSpawn = 50;

        public float SpawnDelay = 2;

        #endregion

        #endregion

        private void Start()
        {
            StartCoroutine(SpawnEnemies());
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
            if (randomType == (int)EnemyType.LargeRedEnemy)
            {
                if (randomPercentage < 30)
                {
                    randomType = (int)EnemyType.RedEnemy;
                }
            }
            var poolType = (PoolType)Enum.Parse(typeof(PoolType), ((EnemyType)randomType).ToString());
            GetObjectType(poolType);

        }

        public GameObject GetObjectType(PoolType pooltype)
        {
            return PoolSignals.Instance.onGetObjectFromPool?.Invoke(pooltype);
        }
    }
}