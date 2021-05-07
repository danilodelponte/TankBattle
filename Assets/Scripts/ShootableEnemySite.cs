using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle {
    public class ShootableEnemySite : MonoBehaviour, Shootable
    {
        public void onHit(GameObject projectile){
            GameplayController.Singleton.SiteHit(gameObject);
        }
    }
}