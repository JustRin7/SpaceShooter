using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SpaceShooter
{
    public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] private Image m_JoyBack;//фон джойстика
        [SerializeField] private Image m_Joystick;//сам стик

        public Vector3 Value { get; private set; }


        /// <summary>
        /// Перемещение стика
        /// </summary>
        public void OnDrag(PointerEventData eventData)
        {
            Vector2 position = Vector2.zero;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_JoyBack.rectTransform, eventData.position, eventData.pressEventCamera, out position);//позицианирование стика относительно фона

            //////нормализация векторов)//////
            position.x = (position.x / m_JoyBack.rectTransform.sizeDelta.x); //sizeDelta.x - wight
            position.y = (position.y / m_JoyBack.rectTransform.sizeDelta.y);

            position.x = position.x * 2 - 1;
            position.y = position.y * 2 - 1;

            //////нормализация векторов)//////
            Value = new Vector3(position.x, position.y, 0);
            if (Value.magnitude > 1)
                Value = Value.normalized;
            //Debug.Log(Value);

            float offsetX = m_JoyBack.rectTransform.sizeDelta.x / 2 - m_Joystick.rectTransform.sizeDelta.x / 2;//делим фон и джойстик пополам и отнимаем второе от первого, чтобы знать, насколько стик будет смещаться 
            float offsetY = m_JoyBack.rectTransform.sizeDelta.y / 2 - m_Joystick.rectTransform.sizeDelta.y / 2;

            m_Joystick.rectTransform.anchoredPosition = new Vector2(Value.x * offsetX, Value.y * offsetY);//задаем местоположению стика значение Value, и * offset чтобы он двигался с большим шагом
        }

        /// <summary>
        /// Когда нажимаем джойстик
        /// </summary>
        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        /// <summary>
        /// Когда отжимаем джойстик
        /// </summary>
        public void OnPointerUp(PointerEventData eventData)
        {
            Value = Vector3.zero;
            m_Joystick.rectTransform.anchoredPosition = Vector3.zero;
        }
    }
}
