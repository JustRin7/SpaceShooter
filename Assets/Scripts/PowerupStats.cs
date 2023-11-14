using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PowerupStats : Powerup
    {
        public enum EffectType
        {
            AddAmmo,
            AddEnergy,
            SpeedUp,
            TakeNotDamage
        }

        [SerializeField] private EffectType m_EffectType;

        [SerializeField] private float m_Value;

        protected override void OnPickedUp(SpaceShip ship)
        {
            if (m_EffectType == EffectType.AddEnergy)
                ship.AddEnergy( (int) m_Value );

            if (m_EffectType == EffectType.AddAmmo)
                ship.AddAmmo((int) m_Value);

            if (m_EffectType == EffectType.AddAmmo)
                ship.AddAmmo((int)m_Value);

            if (m_EffectType == EffectType.SpeedUp)
                ship.SpeedUp(m_Value, m_Value);

            if (m_EffectType == EffectType.TakeNotDamage)
                ship.TakeNotDamageUp((int)m_Value);


        }
    }
}
