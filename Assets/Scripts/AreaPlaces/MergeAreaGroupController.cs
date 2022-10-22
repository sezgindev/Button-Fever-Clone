using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeAreaGroupController : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.AddNewButtonRequest += AddNewButton;
    }

    private void OnDisable()
    {
        EventManager.AddNewButtonRequest -= AddNewButton;
    }

    private MergeArea[] _areas;

    private void Awake()
    {
        _areas = GetComponentsInChildren<MergeArea>();
    }

    private void AddNewButton()
    {
        foreach (var area in _areas)
        {
            if (area.AreaState == MergeArea.AreaStates.Empty)
            {
                EventManager.NewButtonSpawn?.Invoke(area);
                break;
            }
        }
    }
}