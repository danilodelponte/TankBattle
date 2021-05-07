using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle {
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private GameObject shooter;
        [SerializeField] private GameObject projectile;
        [SerializeField] float projectileShootForce;
        [SerializeField] private ParticleSystem gunFire;
        [SerializeField] private float shootingDelay = .5f;
        private float lastShotTime = 0;
        
        private AudioSource audioSource;
        [SerializeField] AudioClip shootSound;
        private float volLowRange = .5f;
        private float volHighRange = 1f;
        
        private Animator anim;

        private void Start() {
            anim = gameObject.GetComponent<Animator>();
            audioSource = gameObject.GetComponent<AudioSource>();
        }

        public void Shoot() {
            if(Time.fixedTime - lastShotTime < shootingDelay) return;

            anim.Play("Shooting");

            float vol = Random.Range(volLowRange, volHighRange);
            float pitch = Random.Range(1.8f, 2f);
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(shootSound, vol);

            gunFire.Play();

            GameObject tempProjectile = Instantiate(projectile, shooter.transform.position, shooter.transform.rotation) as GameObject;
            Rigidbody tempRigidBody = tempProjectile.GetComponent<Rigidbody>();
            tempRigidBody.AddForce(shooter.transform.forward * projectileShootForce);
            Destroy(tempProjectile, 10f);
            lastShotTime = Time.fixedTime;
        }
    }
}
