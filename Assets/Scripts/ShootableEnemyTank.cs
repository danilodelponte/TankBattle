using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle {
    public class ShootableEnemyTank : MonoBehaviour, Shootable
    {
        [SerializeField] GameObject DestroyedTankPrefab;
        public void onHit(GameObject projectile){
            GameplayController.Singleton.TankHit(gameObject);
            GameObject destroyedTank = GameObject.Instantiate(DestroyedTankPrefab, transform.position, transform.rotation);
            Rigidbody cannonBaseRb = destroyedTank.transform.Find("CannonBase").gameObject.GetComponent<Rigidbody>();
            cannonBaseRb.AddForce(gameObject.GetComponent<Rigidbody>().angularVelocity + Vector3.up * 3, ForceMode.Impulse);
            Destroy(gameObject);
        }
    }
}