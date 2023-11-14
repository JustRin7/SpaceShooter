using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelBoundary : SingletonBase<LevelBoundary>//����������� �� ������� SingletonBase. ��� LevelBoundary. ��� Singleton
    {


        /*#region Singltone
        public static LevelBoundary Instance;

        private void Awake()
        {
            if(Instance != null)
            {
                Debug.LogError(" �� ����� ��� ���������� LevelBoundary");
                Destroy(gameObject);
                return;
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);//����� ������ �� ����������� �� ������ � ��� ������� �� ����� �����
        }
        #endregion*/


        [SerializeField] private float m_Radius;//����������� ������� ����
        public float Radius => m_Radius;

        public enum Mode
        {
            Limit,
            Teleport,
            Death
        }

        [SerializeField] private Mode m_LimitMode;//����� ������ �����������
        public Mode LimitMode => m_LimitMode;


        private void OnValidate()
        {
            GetComponent<CircleCollider2D>().radius = m_Radius;
        }


#if UNITY_EDITOR
        /// <summary>
        /// ��������� �����
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, m_Radius);
        }
#endif

    }
}
