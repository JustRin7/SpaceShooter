using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Ограничитель позиции. Работает в связке со скриптом LevelBoundary если таковой есть на сцене
/// Кидается на объект, который нужно ограничить
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

            var lb = LevelBoundary.Instance;//ссылка на ограничитель урповня
            var r = lb.Radius;//ссылка на радиус ограничителя


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
