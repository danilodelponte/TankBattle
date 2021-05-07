using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle {
    public interface Shootable
    {
        void onHit(GameObject projectile);
    }
}