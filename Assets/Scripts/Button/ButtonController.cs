using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private bool _isClickable = true;
    private float _multiply = 1.0f;
    private ParticleManager _particleManager;
    public ButtonTypes ButtonType;
    public ButtonStates ButtonState;
    [SerializeField] private GameObject[] _buttons;

    public enum ButtonTypes
    {
        x1,
        x2,
        x4,
        x8,
        x16
    }

    public enum ButtonStates
    {
        OnBoard,
        MergedArea
    }

    public bool Clickable
    {
        get => _isClickable;
        set => _isClickable = value;
    }


    private void Awake()
    {
        _particleManager = FindObjectOfType<ParticleManager>();
    }

    private void OnEnable()
    {
        EventManager.OnButtonClick += Click;
    }

    private void OnDisable()
    {
        EventManager.OnButtonClick -= Click;
    }

    private void Click()
    {
        if (_isClickable)
        {
            var defaultPos = transform.localPosition;
            float money = _multiply * GodManager.Income;
            transform.DOLocalMoveY(defaultPos.y - .7f, .1f).SetEase(Ease.Linear).OnComplete(() =>
            {
                transform.DOLocalMoveY(defaultPos.y, .1f).SetEase(Ease.Linear);
                _particleManager.MoneyParticle(transform.position + Vector3.up * .5f, money);
                EventManager.OnGetMoney?.Invoke(money);
            });
        }
    }

    public void MergeUpgrade()
    {
        _buttons[transform.GetSiblingIndex()].SetActive(true);
        _buttons[transform.GetSiblingIndex() - 1].SetActive(false);
    }
}