using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float m_Velocity;
        [SerializeField] private float m_Lifetime;
        [SerializeField] private int m_Damage;
        [SerializeField] private ImpactEffect m_ImpactEffectPrefab;


        [SerializeField] private Explosion explosionPrefab;


        [SerializeField] private bool Samonavodka;

        private float m_Timer;


        private void Update()
        {
            
            float stepLenght = Time.deltaTime * m_Velocity;
            Vector2 step = transform.up * stepLenght;


            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLenght);

            if(hit)
            {
                Destructable des = hit.collider.transform.root.GetComponent<Destructable>();

                if(des != null && des != m_Parent)
                {
                    
                    des.ApplyDamage(m_Damage);


                    if (transform != null)///если пуля существует, то наносится урон
                    {
                        if (m_Parent == Player.Instance.ActiveShip)
                        {
                            Player.Instance.AddScore(des.ScoreValue);


                            AIController AIShip = hit.collider.transform.root.GetComponent<AIController>();//килл зачтеться, только если попасть в ии
                            if (AIShip != null)
                            {
                                Player.Instance.AddKill();
                            }

                        }
                    }
                    

                }
                OnProjectileLifeEnd(hit.collider, hit.point);
            }


            m_Timer += Time.deltaTime;

            if (m_Timer > m_Lifetime)
                Destroy(gameObject);


            /*
            ///ии в этом случае тоже считается игроком
            if (m_Parent == null)//если корабль умер, то пули уничтожаются, не нанеся урона
            {
                Destroy(gameObject);
            }
            */


            if (Samonavodka == false) 
            {
                transform.position += new Vector3(step.x, step.y, 0);
            }


            if (Samonavodka == true)
            {

                Destructable findObjForScript = GameObject.FindObjectOfType<Destructable>();
                GameObject findObj = findObjForScript.gameObject;

                //Vector3 k = Vector3.Lerp(transform.position, findObj.transform.position, m_Velocity * Time.deltaTime);
                //transform.position = new Vector3(k.x, k.y, transform.position.z);

                if(findObj && findObj)  transform.up = (findObjForScript.transform.position - transform.position).normalized;
                transform.position += new Vector3(step.x, step.y, 0);

            }

        }


        private void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            Explosion explosion = Instantiate(explosionPrefab);
            explosion.Init(new Vector3(col.transform.position.x, col.transform.position.y, col.transform.position.z));
            Destroy(gameObject);
        }


        private Destructable m_Parent;

        public void SetParentShooter(Destructable parent)
        {
            m_Parent = parent;
        }

        
    }
}
