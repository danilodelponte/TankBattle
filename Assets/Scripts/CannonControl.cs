using UnityEngine;

namespace TankBattle {
    public class CannonControl : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 1f;
        [SerializeField] private float rotationAngle = 1f;
        [SerializeField] private float elevationSpeed = 1f;
        [SerializeField] private float elevationAngle = 1f;
        [SerializeField] public float elevationMaxAngle;
        [SerializeField] public float elevationMinAngle;

        [SerializeField] private GameObject cannonBase;
        [SerializeField] private GameObject cannonElevator;
        
        private Transform baseTransform;
        private Transform elevatorTransform;

        private void Start() {
            baseTransform = cannonBase.transform;
            elevatorTransform = cannonElevator.transform;
        }

        public void RotateCannon(float directionY) {
            float rotationY = rotationAngle * rotationSpeed * directionY;

            baseTransform.Rotate(new Vector3(0, rotationY));
        }
        public void RotateCannonTo(float directionY) {
            float rotationY = rotationAngle * rotationSpeed * directionY;

            baseTransform.Rotate(new Vector3(0, rotationY));
        }

        public void ElevateCannon(float directionX) {
            float elevation = elevationAngle * elevationSpeed * directionX;
            elevatorTransform.Rotate(new Vector3(elevation, 0));

            if(elevationMaxAngle != elevationMinAngle) limitElevation();
        }

        private void limitElevation() {
            Vector3 currentRotation = elevatorTransform.localRotation.eulerAngles;
            currentRotation.x = Mathf.Clamp(currentRotation.x, elevationMinAngle, elevationMaxAngle);
            elevatorTransform.localRotation = Quaternion.Euler (currentRotation);
        }

        public GameObject CannonBase() {
            return cannonBase;
        }

        public GameObject CannonElevator() {
            return cannonElevator;
        }
    }
}