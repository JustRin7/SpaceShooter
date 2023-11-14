using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{

    [RequireComponent(typeof(CircleCollider2D))]
    public class GravityWell : MonoBehaviour
    {
        [SerializeField] private float m_Forse;//сила притяжения
        [SerializeField] private float m_Radius;

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.attachedRigidbody == null) return;

            Vector2 dir = transform.position - collision.transform.position;//направление от коллизии до трансформа

            float dist = dir.magnitude;//дистанция

            if(dist < m_Radius)
            {
                Vector2 force = dir.normalized * m_Forse * (dist / m_Radius);
                collision.attachedRigidbody.AddForce(force, ForceMode2D.Force);//добавляем силу притяжения
            }
        }

        /// <summary>
        /// При билде OnValidate может выдавать ошибку, поэтому нужно написать #if UNITY_EDITOR (автоматическое удаление кода)
        /// </summary>
#if UNITY_EDITOR
        private void OnValidate()
        {
            GetComponent<CircleCollider2D>().radius = m_Radius;
        }

    }
#endif

}
