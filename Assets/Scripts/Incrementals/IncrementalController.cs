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
        if (!MoneyCheck(PersistData.Instance.AddButtonPrice)) return;
        var button = Instantiate(_buttonPrefab, Vector3.zero, Quaternion.identity, _buttonHolder.transform);
        button.GetComponentInChildren<ButtonController>().DropMergeArea(spawnArea.gameObject);
        spawnArea.AreaSetButton(button.GetComponentInChildren<ButtonController>());
        Purchase(PersistData.Instance.AddButtonPrice);
    }

    private bool MoneyCheck(float price)
    {
        if (price > PersistData.Instance.Money) return false;
        return true;
    }

    private void Purchase(float price)
    {
        PersistData.Instance.Money -= price;
        PersistData.Instance.Save();
        EventManager.UpdateUIElements?.Invoke();
    }
}