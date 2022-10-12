using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerMoneyText;
    private PlayerMoneyController _playerMoneyController;

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
        _playerMoneyController = FindObjectOfType<PlayerMoneyController>();
        SetMoneyText();
    }

    private void SetMoneyText()
    {
        float money = _playerMoneyController.Money;
        _playerMoneyText.SetText("$" + money);
    }
}