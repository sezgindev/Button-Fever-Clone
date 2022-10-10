using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{

    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EventManager.OnButtonClick?.Invoke();
        }
    }
}
