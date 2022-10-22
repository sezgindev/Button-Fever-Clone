using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    private bool _isEmpty = true;

    public bool IsEmpty
    {
         get => _isEmpty;
    }

    public void TileAvailability(bool isEmpty)
    {
        _isEmpty = isEmpty;
    }
}