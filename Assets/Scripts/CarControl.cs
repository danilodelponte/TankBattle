using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle {
    public class CarControl : MonoBehaviour
    {
        [SerializeField] private List<AxleInfo> axleInfos; // the information about each individual axle
        [SerializeField] private float maxMotorTorque; // maximum torque the motor can apply to wheel
        [SerializeField] private float maxBreakTorque; // maximum torque the motor can apply to wheel
        [SerializeField] private float maxSteeringAngle; // maximum steer angle the wheel can have
        
        public void MoveCar(float motor, float steering, float breaks) {
            motor *= maxMotorTorque;
            steering *= maxSteeringAngle;
            breaks *= maxBreakTorque;
            
            
            foreach (AxleInfo axleInfo in axleInfos) {
                if (axleInfo.steering) {
                    axleInfo.leftWheel.steerAngle = steering;
                    axleInfo.rightWheel.steerAngle = steering;
                }
                if (axleInfo.motor) {
                    axleInfo.leftWheel.motorTorque = motor;
                    axleInfo.rightWheel.motorTorque = motor;
                    axleInfo.leftWheel.brakeTorque = breaks;
                    axleInfo.rightWheel.brakeTorque = breaks;
                }
            }
        }
    }
        
    [System.Serializable]
    public class AxleInfo {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public bool motor; // is this wheel attached to motor?
        public bool steering; // does this wheel apply steer angle?
    }
}

