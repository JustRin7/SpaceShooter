using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// ������� ����� ���� ������������� ������� �������� �� �����
    /// </summary>
    public abstract class Entity : MonoBehaviour//��-�� ��������� ����� abstract ������ ������ ����������
    //�� ������� ������
    {
        /// <summary>
        /// �������� �������� ��� ������������
        /// </summary>
        [SerializeField]
        private string m_Nicname;

        public string Nicname => m_Nicname;
    }
}