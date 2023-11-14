using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public enum TurretMode
    {
        Primary,
        Secondary
    }

    [CreateAssetMenu]
    public sealed class TurretProperties : ScriptableObject//изолир класс. от него нельзя наследоваться
    {
        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;



        [SerializeField] private GameObject m_ProjectilePrefab;
        public GameObject ProjectilePrefab => m_ProjectilePrefab;


        [SerializeField] private float m_RateOfFire;
        public float RateOfFire => m_RateOfFire;


        [SerializeField] private int m_EnergyUsage;
        public int EnergyUsage => m_EnergyUsage;


        [SerializeField] private int m_AmmoUsage;
        public int AmmoUsage => m_AmmoUsage;


        [SerializeField] private AudioClip m_LaunchSFX;
        public AudioClip LaunchSFX => m_LaunchSFX;
    }
}

