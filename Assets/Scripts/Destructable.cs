using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// Уничтожаемый объект на сцене. То, что может иметь ХП
    /// </summary>
    public class Destructable : Entity
    {
        #region Properties
        /// <summary>
        /// Объект игнорирует повреждения
        /// </summary>
        [SerializeField] private bool m_Indestructible;
        public bool IsIndestructible => m_Indestructible;          

        /// <summary>
        /// Стартовое кол-во ХП
        /// </summary>
        [SerializeField] private int m_HitPoints;

        /// <summary>
        /// Текущие ХП
        /// </summary>
        private int m_CurrentHitPoints;
        public int HitPoints => m_CurrentHitPoints;



        [SerializeField] private GameObject m_PredictPoint;
        public GameObject PredictPoint => m_PredictPoint;


        #endregion


        #region Unity Events
        protected virtual void Start()
        {
            m_CurrentHitPoints = m_HitPoints;

        }
        #endregion







        #region Public API
        /// <summary>
        /// Применение дамага к объекту
        /// </summary>
        /// <param name="damage"> Урон наносимый объекту</param>
        public void ApplyDamage(int damage)
        {
            if (m_Indestructible) return;

            m_CurrentHitPoints -= damage;
            if (m_CurrentHitPoints <= 0)
            {
                OnDeath();
            }
        }
        #endregion


        /// <summary>
        /// Переопределяемое событие уничтожения объекта, когда ХП ниже нуля
        /// </summary>
        protected virtual void OnDeath()
        {
            Destroy(gameObject);
            m_EventOnDeath?.Invoke();
        }






        private static HashSet<Destructable> m_AllDestructables;//HashSet - похоже на List, но в некот. ситуациях работает быстрей

        public static IReadOnlyCollection<Destructable> AllDestructables => m_AllDestructables;//специальный лист, который можно только прочитать


        protected virtual void OnEnable()//когда появляется объект
        {
            if(m_AllDestructables == null)
            {
                m_AllDestructables = new HashSet<Destructable>();
            }
            m_AllDestructables.Add(this);

            //GameObject.FindObjectOfType<Destructable>(); - долгий метод выполнения
        }


        protected virtual void OnDestroy()
        {
            m_AllDestructables.Remove(this);
        }



        public const int TeamIdNeutral = 0;
        [SerializeField] private int m_TeamID;
        public int TeamId => m_TeamID;


        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;//св-во, через которое можно достучаться до эвента m_EventOnDeath



        #region Score
        [SerializeField] private int m_ScoreValue;

        public int ScoreValue => m_ScoreValue;
        #endregion


    }
}
