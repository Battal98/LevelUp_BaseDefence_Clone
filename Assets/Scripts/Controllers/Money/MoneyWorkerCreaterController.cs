using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

namespace Controllers
{
    public class MoneyWorkerCreaterController : MonoBehaviour
    {
        [SerializeField]
        private MoneyWorkerManager moneyWorkerManager;
        [SerializeField]
        private SpriteRenderer backgroundSprite;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerPhysicsController controller))
            {
                moneyWorkerManager.CreateMoneyWorker(this.transform);
                backgroundSprite.color = new Color(1,0,0,0.5f);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerPhysicsController controller))
            {
                backgroundSprite.color = new Color(0, 0, 0, 0.5f);
            }
        }
    } 
}
