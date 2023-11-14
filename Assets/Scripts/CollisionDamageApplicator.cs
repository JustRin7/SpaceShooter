using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class CollisionDamageApplicator : MonoBehaviour
    {
        public static string IgnoreTag = "WorldBoundary";

        [SerializeField] private float m_VelocityDamageModifier;//модификатор урона, зависящий от скорости столкновения

        [SerializeField] public float m_DamageConstant;//константный урон

        [SerializeField] private LevelBoundary lb;




        private void OnCollisionEnter2D(Collision2D collision)
        {

            /// if (collision.transform.tag == IgnoreTag) return;

            
                var destructable = transform.root.GetComponent<Destructable>();//root -самый главный дочерний объект

                if (destructable != null)//расчет демеджа с учетом скорости столкновения
                {
                    destructable.ApplyDamage((int)m_DamageConstant + (int)(m_VelocityDamageModifier * collision.relativeVelocity.magnitude));
                }
            
        }



        private void Update()
        {
            if (LevelBoundaryLimiter.deth == true)
            {
                var destructable = transform.root.GetComponent<Destructable>();//root -самый главный дочерний объект

                if (destructable != null)//расчет демеджа с учетом скорости столкновения
                {
                    destructable.ApplyDamage((int)m_DamageConstant);
                }
            }
        }

    }
}
