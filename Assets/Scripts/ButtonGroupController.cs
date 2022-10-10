using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGroupController : MonoBehaviour
{
    private Animator _animator;
    private readonly int _isClick = Animator.StringToHash("isClick");

    private void OnEnable()
    {
        EventManager.OnButtonClick += Click;
    }

    private void OnDisable()
    {
        EventManager.OnButtonClick -= Click;
    }


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Click()
    {
        _animator.SetTrigger(_isClick);
    }
}