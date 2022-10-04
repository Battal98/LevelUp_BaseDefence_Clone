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

            // 1.Enemies ontrigger exit yapýp tekrar enter tetikleyebilirler. Tekrar tetiklediklerinde listede iki
            // tane ayný objeden oluyor. Öldürdüðüm esnada da null bir obje oluyor. Bu sebeple listeden ontrigger exitte
            // çýkartmak lazým ki tekrar trigger edildiklerinde listeye alalým...

            // Bu durumda bu arkadaþlar bizim attack radius`muzun ýþýna çýkmýþ oluyorlar eðer bunlarla aramýzdaki fark attack
            // radiustan büyük ise bu arkadaþlarýn güncel pozisyonuna trigger olana kadar yürümemiz gerekiyor.
            // Chase etmek gerekiyor.
        }
    }

}