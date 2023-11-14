using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class CollisionDamageApplicator : MonoBehaviour
    {
        public static string IgnoreTag = "WorldBoundary";

        [SerializeField] private float m_VelocityDamageModifier;//����������� �����, ��������� �� �������� ������������

        [SerializeField] public float m_DamageConstant;//����������� ����

        [SerializeField] private LevelBoundary lb;




        private void OnCollisionEnter2D(Collision2D collision)
        {

            /// if (collision.transform.tag == IgnoreTag) return;

            
                var destructable = transform.root.GetComponent<Destructable>();//root -����� ������� �������� ������

                if (destructable != null)//������ ������� � ������ �������� ������������
                {
                    destructable.ApplyDamage((int)m_DamageConstant + (int)(m_VelocityDamageModifier * collision.relativeVelocity.magnitude));
                }
            
        }



        private void Update()
        {
            if (LevelBoundaryLimiter.deth == true)
            {
                var destructable = transform.root.GetComponent<Destructable>();//root -����� ������� �������� ������

                if (destructable != null)//������ ������� � ������ �������� ������������
                {
                    destructable.ApplyDamage((int)m_DamageConstant);
                }
            }
        }

    }
}
