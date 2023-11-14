using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] //нельзя добавить 2 и более компонента
public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour //дженерики
    //SingletonBase может содержать какой-то тип, но этот тип может быть наследником только от MonoBehaviour


     /*
     в наследуемом скрипте нужно написать так
     public class LevelBoundary : SingletonBase<LevelBoundary>//наследуется от скрипта SingletonBase. Тип LevelBoundary. Это Singleton 
     */


{
    [Header("Sigleton")]
    [SerializeField] private bool m_DoNotDestroyOnLoad;//уничтожен ли объект

    public static T Instance { get; private set; }// T - свойство типа T

    protected virtual void Awake()//virtual, чтобы можно было переопределить Awake
    {
        if(Instance != null)
        {
            Debug.LogWarning("MonoSingleton: ogject of type exist, instance will be destroyed = " + typeof(T).Name);
            Destroy(this);
            return;
        }

        Instance = this as T;// Instance приравниваем к типу T

        if (m_DoNotDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }

}
