using UnityEngine;
using TMPro;

namespace Controllers
{
    public class RoomPaymentTextController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshPro remainingCostText;

        public void SetInitText(int cost) => remainingCostText.text = cost.ToString();

        public void UpdateText(int cost) => remainingCostText.text = cost.ToString();
    }

}