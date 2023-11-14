using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelBoundary : SingletonBase<LevelBoundary>//наследуется от скрипта SingletonBase. Тип LevelBoundary. Это Singleton
    {


        /*#region Singltone
        public static LevelBoundary Instance;

        private void Awake()
        {
            if(Instance != null)
            {
                Debug.LogError(" На сцене уже существует LevelBoundary");
                Destroy(gameObject);
                return;
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);//чтобы объект не уничтожался на данной и мог перейти на новые сцены
        }
        #endregion*/


        [SerializeField] private float m_Radius;//ограничение радиуса мира
        public float Radius => m_Radius;

        public enum Mode
        {
            Limit,
            Teleport,
            Death
        }

        [SerializeField] private Mode m_LimitMode;//выбор режима ограничения
        public Mode LimitMode => m_LimitMode;


        private void OnValidate()
        {
            GetComponent<CircleCollider2D>().radius = m_Radius;
        }


#if UNITY_EDITOR
        /// <summary>
        /// Рисование гизмо
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, m_Radius);
        }
#endif

    }
}
