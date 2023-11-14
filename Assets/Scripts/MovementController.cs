using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class MovementController : MonoBehaviour
    {
        public enum ControlMode
        {
            Keyboard,
            Mobile
        }

        [SerializeField] private SpaceShip m_TargetShip;//������ �� �������
        public void SetTargetShip(SpaceShip ship) => m_TargetShip = ship;

        [SerializeField] private VirtualJoystick m_MobileJoustick;//������ �� ����������� ��������

        [SerializeField] private ControlMode m_ControlMode;//����� ������� ����������

        public static bool controlModeMobile = false;


        [SerializeField] private PointerClickHold m_MobileFirePrimary;
        [SerializeField] private PointerClickHold m_MobileFireSecondary;


        /// <summary>
        /// �������� ������� ���������� �/��� ���������
        /// </summary>
        private void Start()
        {

            /* /////// ������ ������
            if (Application.isMobilePlatform)
            {
                m_ControlMode = ControlMode.Mobile;
                m_MobileJoustick.gameObject.SetActive(true);
            } else
            {
                m_ControlMode = ControlMode.Keyboard;
                m_MobileJoustick.gameObject.SetActive(false);
            }*/

            if (m_ControlMode == ControlMode.Keyboard)
            {
                m_MobileJoustick.gameObject.SetActive(false);

                m_MobileFirePrimary.gameObject.SetActive(false);
                m_MobileFireSecondary.gameObject.SetActive(false);
            }
            else
            {
                m_MobileJoustick.gameObject.SetActive(true);

                m_MobileFirePrimary.gameObject.SetActive(true);
                m_MobileFireSecondary.gameObject.SetActive(true);
            } 

        }


        private void Update()
        {
            if (m_TargetShip == null) return;//�������� �� ��, ��������� �� �������

            ///////�������� ������� ����������///////
            if (m_ControlMode == ControlMode.Keyboard)
            {
                ControlKeyboard();
            }

            if (m_ControlMode == ControlMode.Mobile)
            {
                ControlMobile();
                controlModeMobile = true;

            }
        }


        /// <summary>
        /// �����, �������������� ���������� � ���������
        /// </summary>
        private void ControlMobile()
        {
            Vector3 dir = m_MobileJoustick.Value;//�����������

            var dot = Vector2.Dot(dir, m_TargetShip.transform.up);// ��������� ��������� �������� (�������-� � ���������-�) (���� �.����� �.����� ����������� = 1, �.����� � �.����� = 0, �.����� �.���� = -1)
            //1 - �����������, 0 - ���������������, -1 - ����� ���������� � ��������������� ������������
            var dot2 = Vector2.Dot(dir, m_TargetShip.transform.right);// �� �� �����, ������ ������� ���� ����������� Y, � ������ X



            if (m_MobileFirePrimary.IsHold == true)
            {
                m_TargetShip.Fire(TurretMode.Primary);
            }
            if (m_MobileFireSecondary.IsHold == true)
            {
                m_TargetShip.Fire(TurretMode.Secondary);
            }



            m_TargetShip.ThrustControl = Mathf.Max(0, dot);//������������ ��������, ����� �� ���� �������. �������� � ����� ������� �� ����. � ��������������� �������
            m_TargetShip.TorqueControl = -dot2;//����� ������ �������� �������


            /* var dir = m_MobileJoustick.Value;
             m_TargetShip.ThrustControl = dir.y;
             m_TargetShip.TorqueControl = -dir.x;*/
        }


        /// <summary>
        /// �����, �������������� ���������� � ������
        /// </summary>
        private void ControlKeyboard()
        {
            float trust = 0;//����
            float torque = 0;//������� ����

            if (Input.GetKey(KeyCode.UpArrow))
                trust = 1.0f;

            if (Input.GetKey(KeyCode.DownArrow))
                trust = -1.0f;

            if (Input.GetKey(KeyCode.LeftArrow))
                torque = 1.0f;

            if (Input.GetKey(KeyCode.RightArrow))
                torque = -1.0f;



            if (Input.GetKey(KeyCode.Space))
            {
                m_TargetShip.Fire(TurretMode.Primary);
            }
            if (Input.GetKey(KeyCode.X))
            {
                m_TargetShip.Fire(TurretMode.Secondary);
            }


            /////�������� ���������� �������/////
            m_TargetShip.ThrustControl = trust;
            m_TargetShip.TorqueControl = torque;
        }


    }
}
