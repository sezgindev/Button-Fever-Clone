using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public GameObject _moneyParticle;

    public void MoneyParticle(Vector3 spawnPos, float moneyQuantity)
    {
        GameObject obj = Instantiate(_moneyParticle, spawnPos, Quaternion.Euler(new Vector3(56.159f, 0, 0)));
        TextMeshPro particleText = obj.transform.GetChild(0).GetComponent<TextMeshPro>();
        particleText.SetText($"${moneyQuantity:F1}");
        StartCoroutine(ParticleDestroy(obj));
    }


    private IEnumerator ParticleDestroy(GameObject obj)
    {
        yield return new WaitForSeconds(.5f);
        Destroy(obj);
    }
}