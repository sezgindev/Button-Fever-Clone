using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerMoneyText;


    private void OnEnable()
    {
        EventManager.UpdateUIElements += SetMoneyText;
    }

    private void OnDisable()
    {
        EventManager.UpdateUIElements -= SetMoneyText;
    }

    private void Awake()
    {
        SetMoneyText();
    }

    private void SetMoneyText()
    {
        float money = PersistData.Instance.Money;
        _playerMoneyText.SetText("$" + money);
    }
}