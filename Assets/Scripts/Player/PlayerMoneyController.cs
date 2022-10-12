using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoneyController : MonoBehaviour
{
    private float _money = 10;

    public float Money
    {
        get => _money;
        private set { _money = value; }
    }

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
        Money += quantity;
        EventManager.UpdateUIElements?.Invoke();
    }
}