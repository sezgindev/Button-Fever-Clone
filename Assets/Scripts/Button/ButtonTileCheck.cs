using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTileCheck : MonoBehaviour
{
    Collider m_Collider;
    RaycastHit m_Hit;
    float m_MaxDistance;
    bool m_HitDetect;
    [SerializeField] private LayerMask _layerMask;

    void Start()
    {
        m_Collider = GetComponent<Collider>();
        m_MaxDistance = 0.5f;
    }


    public GameObject CheckTileAvailabilty()
    {
        m_HitDetect = Physics.BoxCast(m_Collider.bounds.center, transform.localScale / 4, -transform.up, out m_Hit,
            transform.rotation, m_MaxDistance, _layerMask);
        if (m_HitDetect)
        {
            return m_Hit.collider.gameObject;
        }

        return null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (m_HitDetect)
        {
            Gizmos.DrawRay(transform.position, -transform.up * m_Hit.distance);
            Gizmos.DrawWireCube(transform.position + -transform.up * m_Hit.distance, transform.localScale / 4);
        }
        else
        {
            Gizmos.DrawRay(transform.position, -transform.up * m_MaxDistance);
            Gizmos.DrawWireCube(transform.position + -transform.up * m_MaxDistance, transform.localScale / 4);
        }
    }
}