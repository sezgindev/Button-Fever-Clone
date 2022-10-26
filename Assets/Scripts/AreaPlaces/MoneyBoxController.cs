using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MoneyBoxController : MonoBehaviour
{
    private int _moneyCount = 1;
    [SerializeField] private GameObject _defaultMoney;
    [SerializeField] private GameObject _moneyPrefab;
    private float xPosOffset = 0.35f;
    private float zPosOffset = 0.18f;


    private void SpawnMoney()
    {
        if (_moneyCount == 132)
        {
          //destroy all
        }

        if (_moneyCount % 11 == 0)
        {
            var pos = _defaultMoney.transform.localPosition + new Vector3(0, 0, zPosOffset);
            GameObject money = Instantiate(_moneyPrefab, pos, Quaternion.Euler(-16, 0, 0), transform);
            money.transform.localPosition = pos;
            _defaultMoney = money;
            _moneyCount += 1;
            xPosOffset = -xPosOffset;
        }
        else
        {
            var pos = _defaultMoney.transform.localPosition - new Vector3(xPosOffset, 0, 0);
            GameObject money = Instantiate(_moneyPrefab, pos, Quaternion.Euler(-16, 0, 0), transform);
            money.transform.localPosition = pos;
            _defaultMoney = money;
            _moneyCount += 1;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnMoney();
        }
    }
}