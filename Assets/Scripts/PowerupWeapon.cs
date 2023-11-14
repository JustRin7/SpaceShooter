using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PowerupWeapon : Powerup
    {
        [SerializeField] private TurretProperties m_Properties;

        protected override void OnPickedUp(SpaceShip ship)
        {
            ship.AssighnWeapon(m_Properties);
        }
    }
}
