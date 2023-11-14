using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public interface ILevelCondition
    {
        bool IsCompleted { get; }
    }



    public class LevelController : SingletonBase<LevelController>
    {

        [SerializeField] private int m_ReferenceTime;
        public int ReferenceTime => m_ReferenceTime;

        [SerializeField] private UnityEvent m_EventLevelCompleted;

        private ILevelCondition[] m_Conditions;

        private bool m_IsLevelComplited;

        private float m_LevelTime;
        public float LevelTime => m_LevelTime;



        public bool completePosition;




        void Start()
        {
            m_Conditions = GetComponentsInChildren<ILevelCondition>();
        }


        void Update()
        {
            if(!m_IsLevelComplited)
            {
                m_LevelTime += Time.deltaTime;

                CheckLevelConditions();
            }
        }


        /// <summary>
        /// проверка условий прохождения лвл
        /// </summary>
        private void CheckLevelConditions()
        {
            if(m_Conditions == null || m_Conditions.Length == 0)            
                return;



            int numComplited = 0;                    



            foreach (var v in m_Conditions)
            {
                if (v.IsCompleted)
                    numComplited++;
            }



            if (numComplited == m_Conditions.Length && completePosition == true)
                {
                    m_IsLevelComplited = true;
                    m_EventLevelCompleted?.Invoke();

                    LevelSequenceController.Instance?.FinishCurrentLevel(true);
                }
            
        }


    }
}
