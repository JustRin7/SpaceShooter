using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelConditionScore : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private int score;//очки для достижения

        private bool m_Reached;//для установки достижения


        bool ILevelCondition.IsCompleted
        {
            get
            {
                if(Player.Instance != null && Player.Instance.ActiveShip != null)
                {
                    if(Player.Instance.Score >= score) {
                        m_Reached = true;
                    }
                }
                return m_Reached;
            }
        }


    }
}
