using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementalController : MonoBehaviour
{
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private GameObject _buttonHolder;
    private void OnEnable()
    {
        EventManager.NewButtonSpawn += BuyButton;
    }

    private void OnDisable()
    {
        EventManager.NewButtonSpawn -= BuyButton;
    }

    public void AddNewButtonRequest() //button Func
    {
        EventManager.AddNewButtonRequest?.Invoke();
    }

    private void BuyButton(MergeArea spawnArea)
    {
        var button = Instantiate(_buttonPrefab, Vector3.zero, Quaternion.identity, _buttonHolder.transform);
        button.GetComponentInChildren<ButtonController>().DropMergeArea(spawnArea.gameObject);
        spawnArea.AreaSetButton(button.GetComponentInChildren<ButtonController>());
    }
}