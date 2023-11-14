using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(MeshRenderer))]
    public class BackgroundElement : MonoBehaviour
    {
        [Range(0.0f, 4.0f)]
        [SerializeField] private float m_ParalaxPower;

        [SerializeField] private float m_TextureScale;

        private Material m_QuadMaterial;
        private Vector2 m_InitialOffset;//изначальная точка оффсета

        private void Start()
        {
            m_QuadMaterial = GetComponent<MeshRenderer>().material;

            m_InitialOffset = UnityEngine.Random.insideUnitCircle;//генерируется случ. точка в рамках единичной окружности

            m_QuadMaterial.mainTextureScale = Vector2.one * m_TextureScale; //new Vector2(m_TextureScale, m_TextureScale);
        }

        private void Update()
        {
            Vector2 offset = m_InitialOffset;

            offset.x += transform.position.x / transform.localScale.x / m_ParalaxPower;
            offset.y += transform.position.y / transform.localScale.y / m_ParalaxPower;

            m_QuadMaterial.mainTextureOffset = offset;
        }

    }
}
