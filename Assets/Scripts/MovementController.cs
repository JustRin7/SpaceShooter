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

        [SerializeField] private SpaceShip m_TargetShip;//ссылка на корабль
        public void SetTargetShip(SpaceShip ship) => m_TargetShip = ship;

        [SerializeField] private VirtualJoystick m_MobileJoustick;//ссылка на виртуальный джойстик

        [SerializeField] private ControlMode m_ControlMode;//выбор способа управления

        public static bool controlModeMobile = false;


        [SerializeField] private PointerClickHold m_MobileFirePrimary;
        [SerializeField] private PointerClickHold m_MobileFireSecondary;


        /// <summary>
        /// Проверка способа управления и/или платформы
        /// </summary>
        private void Start()
        {

            /* /////// первый способ
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
            if (m_TargetShip == null) return;//проверка на то, уничтожен ли корабль

            ///////проверка способа управления///////
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
        /// Метод, обрабатывающий управление с джойстика
        /// </summary>
        private void ControlMobile()
        {
            Vector3 dir = m_MobileJoustick.Value;//направление

            var dot = Vector2.Dot(dir, m_TargetShip.transform.up);// скалярное умножение векторов (корабля-А и джойстика-Б) (если А.вверх Б.вверх направление = 1, А.вверх и Б.влево = 0, А.вверх Б.вниз = -1)
            //1 - параллельны, 0 - перпендикулярны, -1 - когда показывают в противоположных направлениях
            var dot2 = Vector2.Dot(dir, m_TargetShip.transform.right);// то же самое, только сначала было направление Y, а теперь X



            if (m_MobileFirePrimary.IsHold == true)
            {
                m_TargetShip.Fire(TurretMode.Primary);
            }
            if (m_MobileFireSecondary.IsHold == true)
            {
                m_TargetShip.Fire(TurretMode.Secondary);
            }



            m_TargetShip.ThrustControl = Mathf.Max(0, dot);//максимальное значение, чтобы не было отрицат. значения и чтобы корабль не двиг. в противоположную сторону
            m_TargetShip.TorqueControl = -dot2;//нужно делать обратный поворот


            /* var dir = m_MobileJoustick.Value;
             m_TargetShip.ThrustControl = dir.y;
             m_TargetShip.TorqueControl = -dir.x;*/
        }


        /// <summary>
        /// Метод, обрабатывающий управление с клавиш
        /// </summary>
        private void ControlKeyboard()
        {
            float trust = 0;//тяга
            float torque = 0;//угловая тяга

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


            /////передача управление короблю/////
            m_TargetShip.ThrustControl = trust;
            m_TargetShip.TorqueControl = torque;
        }


    }
}
