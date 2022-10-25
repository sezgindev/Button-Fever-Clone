using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySpawnController : MonoBehaviour
{
    [SerializeField] private GameObject _moneyPrefab;
    
    private void SpawnNewMoney()
    {
        Instantiate(_moneyPrefab, Vector3.zero, Quaternion.identity, transform);
        MoneyRePositioning();
    }

    private void MoneyRePositioning()
    {
        
    }
    
}
