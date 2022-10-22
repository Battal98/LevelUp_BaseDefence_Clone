using UnityEngine;
using Signals;

namespace Controllers
{
    public class SoldierCreatorController : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer backgroundSprite;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerPhysicsController playerPhysicsController))
            {
                SoldierAISignals.Instance.onSoldierAmountUpgrade?.Invoke();
                backgroundSprite.color = new Color(1,0,0,0.5f);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerPhysicsController playerPhysicsController))
            {
                backgroundSprite.color = new Color(0,0,0,0.5f);
            }
        }
    } 
}
