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

        [SerializeField] private AIBehaviour m_AIBehavior;//��� ���������

        [SerializeField] private AIPointPatrol[] m_PatrolPoints;
        private int NumberOfPatrolPoint;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationLiner;//�������� �����������

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationAngular;//�������� ��������

        [SerializeField] private float m_RandomSelectMovePointTime;

        [SerializeField] private float m_FindNewTargetTime;

        [SerializeField] private float m_ShootDelay;

        [SerializeField] private float m_EnadeRayLenght;//����� ��������

        private SpaceShip m_SpaceShip;

        private Vector3 m_MovePosition;//�����, ���� ��������� �������

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
                        //���� ��������� �� ����� ���������� ���� ������ ��� ������

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
        /// ��������� ������������
        /// </summary>
        private void ActionEvadeCollision()
        {
            if( Physics2D.Raycast(transform.position, transform.up, m_EnadeRayLenght) == true)// ���� ������� � ���-�� ���������
            {
                m_MovePosition = transform.position + transform.right * 200.0f;
            }
        }


        private void ActionControlShip()
        {
            m_SpaceShip.ThrustControl = m_NavigationLiner;

            m_SpaceShip.TorqueControl = ComputeAIiginTorqueNormalized(m_MovePosition, m_SpaceShip.transform) * m_NavigationAngular;
        }


        private const float MAX_ANGLE = 45.0f;//��������� ������� ������
        /// <summary>
        /// ���������� ����
        /// </summary>
        private static float ComputeAIiginTorqueNormalized(Vector3 targetPosition, Transform ship)
        {
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);//������� ������� � ��������� ����������

            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);//��������� ���� ����� ����� ���������

            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;//������������ ���� ��������, ����� ���� ���� ��� ������ 45 ��������, �� ������� ����� ������������,
            // / 45 - ����������� ����

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
        /// ���������� ���������� �������
        /// </summary>
        private Destructable FindNearestDestructableTarget()
        {
            float maxDist = float.MaxValue;

            Destructable potentialTarget = null;

            foreach(var v in Destructable.AllDestructables )
            {
                if(v.GetComponent<SpaceShip>() == m_SpaceShip) continue;//��������� ��� �������

                if (v.TeamId == Destructable.TeamIdNeutral) continue;//��������� ���������

                if (v.TeamId == m_SpaceShip.TeamId) continue;//��������� ���� �������

                float dist = Vector2.Distance(m_SpaceShip.transform.position, v.transform.position); //��������� ����� 2-�� ���������

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
        /// ����� ������� ��������������
        /// </summary>
        public void SetPatrolBehavior(AIPointPatrol point)
        {

        }
        #endregion


    }
}

