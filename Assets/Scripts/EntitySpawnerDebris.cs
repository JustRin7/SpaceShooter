using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class EntitySpawnerDebris : MonoBehaviour
    {
        [SerializeField] private Destructable[] m_DebrisPrefabs;
        [SerializeField] private CircleArea m_Area;
        [SerializeField] private int m_NumDerbis;        
        [SerializeField] private float m_RandomSpeed;//скорость мусора


     


        private void Start()
        {
            for(int i = 0; i < m_NumDerbis; i++)
            {
                SpawnDebris();
            }
        }


        private void SpawnDebris()
        {
            int index = Random.Range(0, m_DebrisPrefabs.Length);
            GameObject debris = Instantiate(m_DebrisPrefabs[index].gameObject);

            debris.transform.position = m_Area.GetRandomInsideZone();
            debris.GetComponent<Destructable>().EventOnDeath.AddListener(OnDebrisDead);

            Rigidbody2D rb = debris.GetComponent<Rigidbody2D>();

            if(rb != null && m_RandomSpeed > 0)
            {
                rb.velocity = (Vector2) UnityEngine.Random.insideUnitSphere * m_RandomSpeed;
            }
        }

        private void OnDebrisDead()
        {
            SpawnDebris();
        }




    }
}
