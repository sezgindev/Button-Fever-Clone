using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoneyController : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.OnGetMoney += MoneyIncrease;
    }

    private void OnDisable()
    {
        EventManager.OnGetMoney -= MoneyIncrease;
    }


    private void MoneyIncrease(float quantity)
    {
        PersistData.Instance.Money += quantity;
        EventManager.UpdateUIElements?.Invoke();
    }
}