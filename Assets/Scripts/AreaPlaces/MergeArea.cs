using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeArea : MonoBehaviour
{
    public AreaStates AreaState;
    [SerializeField] private ButtonController _currentButton;
    public ButtonController CurrentButton => _currentButton;

    public enum AreaStates
    {
        Empty,
        Full
    }

    public void AreaSetButton(ButtonController button)
    {
        AreaState = AreaStates.Full;
        _currentButton = button;
    }

    public void AreaReset()
    {
        AreaState = AreaStates.Empty;
        _currentButton = null;
    }
}