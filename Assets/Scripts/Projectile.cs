using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle {
    public class Projectile : MonoBehaviour
    {

        [SerializeField] private ParticleSystem groundHitParticles;
        [SerializeField] private ParticleSystem enemyHitParticles;

        private Rigidbody rb;
        private Collider cc;

        private void Awake() {
            rb = GetComponent<Rigidbody>();
            cc = GetComponent<SphereCollider>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void OnCollisionEnter(Collision other) {
            ParticleSystem particles;
            var shootable = other.gameObject.GetComponent<Shootable>();

            if(shootable != null){
                particles = enemyHitParticles;
                shootable.onHit(gameObject);
            } else {
                particles = groundHitParticles;
            }
            
            ParticleSystem particlesObject = Instantiate(particles, transform.position, transform.rotation) as ParticleSystem;
            particlesObject.Play();
            Destroy(particlesObject.gameObject, 2f);
            Destroy(gameObject);
        }
    }
}
