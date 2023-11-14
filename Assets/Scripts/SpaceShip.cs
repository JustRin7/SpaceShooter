using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructable
    {

        [SerializeField] private Sprite m_PreviewImage;
        public Sprite PreviewImage => m_PreviewImage;


        /// <summary>
        /// ����� ��� �������������� ��������� � ������
        /// </summary>
        [Header("Space ship")]
        [SerializeField] private float m_Mass;

        /// <summary>
        /// ��������� ����
        /// </summary>
        [SerializeField] private float m_Thrust;

        /// <summary>
        /// ��������� ����
        /// </summary>
        [SerializeField] private float m_Mobility;

        /// <summary>
        /// ������������ �������� ��������
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;
        public float MaxLinearVelocity => m_MaxLinearVelocity;

        /// <summary>
        /// ������������ ������������ ��������. � ��������/���
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;
        public float MaxAngularVelocity => m_MaxAngularVelocity;

        /// <summary>
        /// ����������� ������ �� �����
        /// </summary>
        private Rigidbody2D m_Rigid;



        private float m_TimerSpeed;
        private float m_TimerDamage;
        private float Mobility;
        private float Thrust;
        private float TakeDamage;
        private bool Speedup;
        private bool Damage;
        [SerializeField] private GameObject Shine;



        #region Public API
        /// <summary>
        /// ���������� �������� �����. -1.0 �� +1.0
        /// </summary>
        public float ThrustControl { get; set; }

        /// <summary>
        /// ���������� ������������ �����. -1.0 �� +1.0
        /// </summary>
        public float TorqueControl { get; set; }
        #endregion


        #region Unity Event
        protected override void Start()
        {
            base.Start();

            m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigid.mass = m_Mass;

            m_Rigid.inertia = 1;


            Mobility = m_Mobility;
            Thrust = m_Thrust;
            TakeDamage = gameObject.GetComponent<CollisionDamageApplicator>().m_DamageConstant;
            Speedup = false;
            Damage = false;

            InitOffensive();
        }


        private void FixedUpdate()
        {
            UpdateRigitBody();

            UpdateEnergyRegen();
        }
        #endregion        

        

        private void Update()
        {          
            if (m_TimerSpeed < 3 && Speedup == true)
            {
                m_TimerSpeed += Time.deltaTime;                
            }

            if (m_TimerSpeed > 3)
            {
                m_Mobility = Mobility;
                m_Thrust = Thrust;
                Speedup = false;
            }



            if (m_TimerDamage < 10 && Damage == true)
            {
                m_TimerDamage += Time.deltaTime;
            }

            if (m_TimerDamage > 10)
            {
                gameObject.GetComponent<CollisionDamageApplicator>().m_DamageConstant = TakeDamage;
                Shine.SetActive(false);
                Damage = false;
            }
        }


        /// <summary>
        /// ����� ���������� ��� ������� ��� ��������
        /// </summary>
        private void UpdateRigitBody()//���������� ��������
        {
            //////////��������� ����//////////
            m_Rigid.AddForce(ThrustControl * m_Thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);//��������� ��������� ���� � ��������� �������
            //������ up ��������� (���������������), ForceMode2D.Force - ���������� ����, � �� ����������

            //////////������ ������//////////
            m_Rigid.AddForce(-m_Rigid.velocity * (m_Thrust / m_MaxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

            //////////��������//////////
            m_Rigid.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);//AddTorque - �������� ������������ ������(�� ������� �������)

            //////////�������� ��������//////////
            m_Rigid.AddTorque(-m_Rigid.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
        }


        [SerializeField] private Turret[] m_Turrets;


        public void Fire(TurretMode mode)
        {
            for(int i = 0; i < m_Turrets.Length; i++)
            {
                if(m_Turrets[i].Mode == mode)
                {
                    m_Turrets[i].Fire();
                }
            }
        }


        [SerializeField] private int m_MaxEnergy;
        [SerializeField] private int m_MaxAmmo;
        [SerializeField] private int m_EnergyRegenPerSecond;

        private float m_PrimaryEnergy;
        private int m_SecondaryAmmo;


        public void AddEnergy(int e)
        {
            /*m_PrimaryEnergy += e;

            if (m_PrimaryEnergy > m_MaxEnergy)
                m_PrimaryEnergy = m_MaxEnergy;*/

            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + e, 0, m_MaxEnergy);//������������ m_PrimaryEnergy += e, ����� ��� ���� > 0 � < m_MaxEnergy

        }


        public void AddAmmo(int ammo)
        {
            m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo);
        }


        /// <summary>
        /// ������������� Energy � Ammo
        /// </summary>
        private void InitOffensive()
        {
            m_PrimaryEnergy = m_MaxEnergy;
            m_SecondaryAmmo = m_MaxAmmo;
        }

        /// <summary>
        /// �������������� �������
        /// </summary>
        private void UpdateEnergyRegen()
        {
            m_PrimaryEnergy += (float) m_EnergyRegenPerSecond * Time.fixedDeltaTime;

            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy, 0, m_MaxEnergy);
        }


        /// <summary>
        /// ������ �������
        /// </summary>
        public bool DrawEneggy(int count)
        {
            if (count == 0) return true;

            if (m_PrimaryEnergy >= count)
            {
                m_PrimaryEnergy -= count;
                return true;
            }

            return false;
        }


        /// <summary>
        /// ������ �������
        /// </summary>
        public bool DrawAmmo(int count)
        {
            if (count == 0) return true;

            if(m_SecondaryAmmo >= count)
            {
                m_SecondaryAmmo -= count;
                return true;
            }

            return false;
        }
             

        /// <summary>
        /// �������� ��������� ��-�� �������
        /// </summary>
        public void AssighnWeapon(TurretProperties props)
        {
            for(int i = 0; i < m_Turrets.Length; i++)
            {
                m_Turrets[i].AssignLoadout(props);
            }
        }


        /// <summary>
        /// ���������
        /// </summary>
        public void SpeedUp(float speedMobility, float speedThrust)
        {
            m_TimerSpeed = 0;
            Speedup = true;

            m_Mobility += speedMobility;
            m_Thrust += speedThrust;
            
        }


        /// <summary>
        /// �� ��������� ������
        /// </summary>
        public void TakeNotDamageUp(int damage)
        {
            m_TimerDamage = 0;
            Damage = true;

            gameObject.GetComponent<CollisionDamageApplicator>().m_DamageConstant = damage;
            Shine.SetActive(true);
        }



    }
}
