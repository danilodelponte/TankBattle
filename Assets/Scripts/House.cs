using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle {
    public class House : MonoBehaviour
    {
        [SerializeField] private GameObject fracturedPrefab;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void OnCollisionEnter(Collision other) {
            if(other.gameObject.GetComponent<Projectile>() != null) {
                GameObject fractured = Instantiate(fracturedPrefab, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
