using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelConditionPosition : MonoBehaviour
    {        

        private bool m_ReachedPosition;//��� ��������� ����������


        [SerializeField] Player player;
        [SerializeField] LevelController LevelController;



        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (player.ActiveShip != null)
            {
                LevelController.completePosition = true;
            }
            else return;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (player.ActiveShip != null)
            {
                LevelController.completePosition = false;
            }
            else return;
        }


    }
}
