using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{

    [RequireComponent(typeof(CircleCollider2D))]
    public class GravityWell : MonoBehaviour
    {
        [SerializeField] private float m_Forse;//���� ����������
        [SerializeField] private float m_Radius;

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.attachedRigidbody == null) return;

            Vector2 dir = transform.position - collision.transform.position;//����������� �� �������� �� ����������

            float dist = dir.magnitude;//���������

            if(dist < m_Radius)
            {
                Vector2 force = dir.normalized * m_Forse * (dist / m_Radius);
                collision.attachedRigidbody.AddForce(force, ForceMode2D.Force);//��������� ���� ����������
            }
        }

        /// <summary>
        /// ��� ����� OnValidate ����� �������� ������, ������� ����� �������� #if UNITY_EDITOR (�������������� �������� ����)
        /// </summary>
#if UNITY_EDITOR
        private void OnValidate()
        {
            GetComponent<CircleCollider2D>().radius = m_Radius;
        }

    }
#endif

}
