using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle {
    public class EnemyTankControl : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private Transform target;
        [SerializeField] private float distanceToTarget = Mathf.Infinity;
        [SerializeField] private float minShootingDistance = 10f;
        [SerializeField] private float maxShootingDistance = 100f;
        [SerializeField] private float chasingDistance = 80f;
        [SerializeField] private bool chasing = false;
        
        private Vector3 targetPosition;

        private CannonControl cannonControl;
        private CarControl car;
        private Shooter shooter;

        void Start()
        {
            cannonControl = GetComponent<CannonControl>();
            car = GetComponent<CarControl>();
            shooter = GetComponent<Shooter>();
            target = GameObject.Find("PlayerTank").transform;
        }

        private void FixedUpdate() {
            if(target == null) { return; }
            targetPosition = target.position;
            //if target is enemy
            
            distanceToTarget = Vector3.Distance(transform.position, targetPosition);
            if(distanceToTarget < chasingDistance) {
                chasing = true;
            } else if(!chasing) {
                return;
            }

            MoveCar();
            RotateCannon();
            ElevateCannon();
        }

        private void MoveCar() {
            if(distanceToTarget < minShootingDistance) {
                car.MoveCar(0, 0, 1);
            }

            Vector3 direction = (targetPosition - transform.position).normalized;
            float steering = Vector3.Dot(transform.right, direction);
            float motor = Vector3.Dot(transform.forward, direction);

            float breakingRange = distanceToTarget - minShootingDistance;
            if(breakingRange < minShootingDistance) {
                motor *= breakingRange / minShootingDistance;
            }
            
            car.MoveCar(motor, steering, 0);
        }

        private void RotateCannon() {
            if(distanceToTarget > maxShootingDistance) return;

            Transform cannon = cannonControl.CannonBase().transform;
            Vector3 cannonDirection = (targetPosition - cannon.position).normalized;
            float rotation = Vector3.Dot(cannon.right, cannonDirection) * 10;
            cannonControl.RotateCannon(rotation);
            if(Mathf.Abs(rotation) < .15f) shooter.Shoot();
        }

        private void ElevateCannon() {
            if(distanceToTarget > maxShootingDistance) return;

            Transform cannonElevator = cannonControl.CannonElevator().transform;

            float distanceFactor = distanceToTarget / maxShootingDistance;
            float elevationRange = cannonControl.elevationMaxAngle - cannonControl.elevationMinAngle;
            float desiredElevation = cannonControl.elevationMaxAngle - (elevationRange * distanceFactor);
            float elevationDirection = ((cannonElevator.eulerAngles.x / desiredElevation) - 1) * -20;
            cannonControl.ElevateCannon(elevationDirection);
        }
    }
}