using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] //������ �������� 2 � ����� ����������
public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour //���������
    //SingletonBase ����� ��������� �����-�� ���, �� ���� ��� ����� ���� ����������� ������ �� MonoBehaviour


     /*
     � ����������� ������� ����� �������� ���
     public class LevelBoundary : SingletonBase<LevelBoundary>//����������� �� ������� SingletonBase. ��� LevelBoundary. ��� Singleton 
     */


{
    [Header("Sigleton")]
    [SerializeField] private bool m_DoNotDestroyOnLoad;//��������� �� ������

    public static T Instance { get; private set; }// T - �������� ���� T

    protected virtual void Awake()//virtual, ����� ����� ���� �������������� Awake
    {
        if(Instance != null)
        {
            Debug.LogWarning("MonoSingleton: ogject of type exist, instance will be destroyed = " + typeof(T).Name);
            Destroy(this);
            return;
        }

        Instance = this as T;// Instance ������������ � ���� T

        if (m_DoNotDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }

}
