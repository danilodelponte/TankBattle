using UnityEngine;
using UnityEngine.UI;

namespace TankBattle {
    public class PlayerTankControl : MonoBehaviour
    {
        // Start is called before the first frame update

        private CannonControl cannon;
        private CarControl car;
        private Shooter shooter;

        [SerializeField] private Joystick carJoystick;
        [SerializeField] private Joystick cannonJoystick;
        [SerializeField] private Button button;

        void Start()
        {
            cannon = GetComponent<CannonControl>();
            car = GetComponent<CarControl>();
            shooter = GetComponent<Shooter>();
            // GetComponent<Rigidbody>().centerOfMass = new Vector3(0, 0.1f, 0);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            float motor = carJoystick.Vertical;
            float steering = carJoystick.Horizontal;
            float breaks = 0;
            if(Mathf.Abs(motor) < .2f) breaks = 1;
            car.MoveCar(motor, steering, breaks);

            cannon.RotateCannon(cannonJoystick.Horizontal);
            cannon.ElevateCannon(-cannonJoystick.Vertical);
        }
    }
}