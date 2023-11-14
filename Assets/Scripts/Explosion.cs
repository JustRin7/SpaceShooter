using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private GameObject m_ExplosionPrefab;//ссылка на префаб взрыва

        private float timer;

        public void Init(Vector3 position)
        {
            m_ExplosionPrefab.transform.position = position;
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer > 2)
            {
                Destroy(gameObject);
            }
        }

    }
}
