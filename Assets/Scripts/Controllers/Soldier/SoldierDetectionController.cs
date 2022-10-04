using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using StateMachines.AIBrain.Soldier;

namespace Controllers
{
    public class SoldierDetectionController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private SoldierAIBrain soldierAIBrain;

        #endregion

        #region Private Variables

        #endregion
        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamagable damagable))
            {
                if (damagable.IsTaken) return;
                soldierAIBrain.enemyList.Add(damagable);
                if (soldierAIBrain.EnemyTarget == null)
                {
                    damagable.IsTaken = true;
                    soldierAIBrain.SetEnemyTargetTransform();
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IDamagable damagable))
            {
                if (soldierAIBrain.enemyList.Count == 0)
                {
                    soldierAIBrain.EnemyTarget = null;
                }
                damagable.IsTaken = false;
                soldierAIBrain.enemyList.Remove(damagable);
                soldierAIBrain.enemyList.TrimExcess();
            }

            // 1.Enemies ontrigger exit yap�p tekrar enter tetikleyebilirler. Tekrar tetiklediklerinde listede iki
            // tane ayn� objeden oluyor. �ld�rd���m esnada da null bir obje oluyor. Bu sebeple listeden ontrigger exitte
            // ��kartmak laz�m ki tekrar trigger edildiklerinde listeye alal�m...

            // Bu durumda bu arkada�lar bizim attack radius`muzun ���na ��km�� oluyorlar e�er bunlarla aram�zdaki fark attack
            // radiustan b�y�k ise bu arkada�lar�n g�ncel pozisyonuna trigger olana kadar y�r�memiz gerekiyor.
            // Chase etmek gerekiyor.
        }
    }

}