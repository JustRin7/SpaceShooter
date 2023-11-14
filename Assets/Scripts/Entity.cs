using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Базовый класс всех интерактивных игровых объектов на сцене
    /// </summary>
    public abstract class Entity : MonoBehaviour//из-за ключевого слова abstract скрипт нельзя перетащить
    //на игровой объект
    {
        /// <summary>
        /// Название объектов для пользователя
        /// </summary>
        [SerializeField]
        private string m_Nicname;

        public string Nicname => m_Nicname;
    }
}