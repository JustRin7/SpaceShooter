using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(SpaceShip))]
    public class AIController : MonoBehaviour
    {
        public enum AIBehaviour
        {
            Null,
            Patrool
        }

        [SerializeField] private AIBehaviour m_AIBehavior;//тип поведени€

        [SerializeField] private AIPointPatrol[] m_PatrolPoints;
        private int NumberOfPatrolPoint;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationLiner;//скорость перемещени€

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationAngular;//скорость вращени€

        [SerializeField] private float m_RandomSelectMovePointTime;

        [SerializeField] private float m_FindNewTargetTime;

        [SerializeField] private float m_ShootDelay;

        [SerializeField] private float m_EnadeRayLenght;//длина рейкаста

        private SpaceShip m_SpaceShip;

        private Vector3 m_MovePosition;//точка, куда движетьс€ корабль

        private Destructable m_SelectedTarget;

        private Timer m_RamdomizeDirectionTimer;

        private Timer m_FireTimer;

        private Timer m_FindNewTargetTimer;


        private void Start()
        {
            m_SpaceShip = GetComponent<SpaceShip>();

            NumberOfPatrolPoint = 0;

            InitTimers();
        }


        private void Update()
        {
            UpdateTimers();

            UpdateAI();
        }


        private void UpdateAI()
        {
            /*if(m_AIBehavior == AIBehaviour.Null)
            {
            }*/

            if (m_AIBehavior == AIBehaviour.Patrool)
            {
                UpdateBehaviourPatrool();
            }
        }


        private void UpdateBehaviourPatrool()
        {
            ActionFindNewMovePosition();
            ActionControlShip();
            ActionFindNewAttackTarget();
            ActionFire();
            ActionEvadeCollision();
        }


        private void ActionFindNewMovePosition()
        {

            if (m_AIBehavior == AIBehaviour.Patrool)
            {

                if(m_SelectedTarget != null)
                {
                    m_MovePosition = m_SelectedTarget.PredictPoint.transform.position;
                }
                else
                {
                    if(m_PatrolPoints != null)
                    {
                        bool isInsidePatrolZone = (m_PatrolPoints[NumberOfPatrolPoint].transform.position - transform.position).sqrMagnitude < m_PatrolPoints[NumberOfPatrolPoint].Radius * m_PatrolPoints[NumberOfPatrolPoint].Radius;
                        //если дистанци€ от точки патрульной зоны меньше чем радиус

                        if(isInsidePatrolZone == true)
                        {
                            if (m_RamdomizeDirectionTimer.IsFinished == true)
                            {
                                if(NumberOfPatrolPoint < m_PatrolPoints.Length)
                                {
                                    NumberOfPatrolPoint += 1;
                                }

                                if (NumberOfPatrolPoint == m_PatrolPoints.Length)
                                {
                                    NumberOfPatrolPoint = 0;
                                }

                                Vector2 newPoint = UnityEngine.Random.onUnitSphere * m_PatrolPoints[NumberOfPatrolPoint].Radius + m_PatrolPoints[NumberOfPatrolPoint].transform.position;
                                m_MovePosition = newPoint;

                                m_RamdomizeDirectionTimer.Start(m_RandomSelectMovePointTime);
                            }
                        }
                        else
                        {
                            m_MovePosition = m_PatrolPoints[NumberOfPatrolPoint].transform.position;
                        }
                    }
                }

            }


        }


        /// <summary>
        /// »збегание столкновений
        /// </summary>
        private void ActionEvadeCollision()
        {
            if( Physics2D.Raycast(transform.position, transform.up, m_EnadeRayLenght) == true)// если рейкаст с чем-то пересекс€
            {
                m_MovePosition = transform.position + transform.right * 200.0f;
            }
        }


        private void ActionControlShip()
        {
            m_SpaceShip.ThrustControl = m_NavigationLiner;

            m_SpaceShip.TorqueControl = ComputeAIiginTorqueNormalized(m_MovePosition, m_SpaceShip.transform) * m_NavigationAngular;
        }


        private const float MAX_ANGLE = 45.0f;//константы пишутс€ капсом
        /// <summary>
        /// ¬озвращает угол
        /// </summary>
        private static float ComputeAIiginTorqueNormalized(Vector3 targetPosition, Transform ship)
        {
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);//перевод позиции в локальные координаты

            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);//получение угла между двум€ векторами

            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;//ограничивает угол поворота, чтобы если угол был больше 45 градусов, то поворот будет максимальный,
            // / 45 - нормализуем угол

            return -angle;
        }


        private void ActionFindNewAttackTarget()
        {
            if (m_FindNewTargetTimer.IsFinished == true)
            {
                m_SelectedTarget = FindNearestDestructableTarget();

                m_FindNewTargetTimer.Start(m_ShootDelay);
            }
        }


        private void ActionFire()
        {
            if(m_SelectedTarget != null)
            {
                if (m_FireTimer.IsFinished == true)
                {
                    m_SpaceShip.Fire(TurretMode.Primary);

                    m_FireTimer.Start(m_ShootDelay);
                }
            }
        }


        /// <summary>
        /// Ќахождение ближайшего таргета
        /// </summary>
        private Destructable FindNearestDestructableTarget()
        {
            float maxDist = float.MaxValue;

            Destructable potentialTarget = null;

            foreach(var v in Destructable.AllDestructables )
            {
                if(v.GetComponent<SpaceShip>() == m_SpaceShip) continue;//исключаем сам корабль

                if (v.TeamId == Destructable.TeamIdNeutral) continue;//исключаем нейтралов

                if (v.TeamId == m_SpaceShip.TeamId) continue;//исключаем свою команду

                float dist = Vector2.Distance(m_SpaceShip.transform.position, v.transform.position); //дистанци€ между 2-м€ объектами

                if(dist < maxDist)
                {
                    maxDist = dist;
                    potentialTarget = v;
                }
            }

            return potentialTarget;
        }


        #region Timers
        private void InitTimers()
        {
            m_RamdomizeDirectionTimer = new Timer(m_RandomSelectMovePointTime);
            m_FireTimer = new Timer(m_ShootDelay);
            m_FindNewTargetTimer = new Timer(m_FindNewTargetTime);
    }

        private void UpdateTimers()
        {
            m_RamdomizeDirectionTimer.RemoveTime(Time.deltaTime);
            m_FireTimer.RemoveTime(Time.deltaTime);
            m_FindNewTargetTimer.RemoveTime(Time.deltaTime);
        }

        /// <summary>
        /// √измо области патрулировани€
        /// </summary>
        public void SetPatrolBehavior(AIPointPatrol point)
        {

        }
        #endregion


    }
}

