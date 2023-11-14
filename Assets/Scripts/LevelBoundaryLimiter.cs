using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ������������ �������. �������� � ������ �� �������� LevelBoundary ���� ������� ���� �� �����
/// �������� �� ������, ������� ����� ����������
/// </summary>
namespace SpaceShooter
{
    public class LevelBoundaryLimiter : MonoBehaviour
    {
        [SerializeField] private Player player;


        public static bool deth = false;

        private void Update()
        {
            if (LevelBoundary.Instance == null) return;

            var lb = LevelBoundary.Instance;//������ �� ������������ �������
            var r = lb.Radius;//������ �� ������ ������������


            if(transform.position.magnitude > r )
            {
                if(lb.LimitMode == LevelBoundary.Mode.Limit)
                {
                    transform.position = transform.position.normalized * r;
                }

                if (lb.LimitMode == LevelBoundary.Mode.Teleport)
                {
                    transform.position = -transform.position.normalized * r;
                }

                if (lb.LimitMode == LevelBoundary.Mode.Death)
                {
                    deth = true;
                }


            }

            
        }
    }
}
