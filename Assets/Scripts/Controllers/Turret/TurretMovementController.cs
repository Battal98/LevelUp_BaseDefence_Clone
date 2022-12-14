using Enums;
using Keys;
using UnityEngine;

namespace Controllers
{

    public class TurretMovementController : MonoBehaviour
    {
        private float _horizontalInput;
        private float _verticalInput;

        private Vector2 rotateDirection;

        [SerializeField] private TurretLocationType turretLocationType;

        public void SetInputParams(HorizontalInputParams input)
        {
            _horizontalInput = input.MovementVector.x;
            _verticalInput = input.MovementVector.y;
            Rotate();
        }

        private void Rotate()
        {
            rotateDirection = new Vector2(_horizontalInput, _verticalInput).normalized;
            if (rotateDirection.sqrMagnitude == 0)
                return;

            float angle = Mathf.Atan2(rotateDirection.x, rotateDirection.y) * Mathf.Rad2Deg;

            if (!(angle < 60) || !(angle > -60)) return;

            transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
        }
    }
}