using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject[] _buttons;
    private const int _buttonLayer = 6;
    private const int _tile = 9;
    private float _defaultYPos = 0.07077199f;
    private bool _isClickable = true;
    private ParticleManager _particleManager;
    public ButtonStates ButtonState;
    public int ButtonMultiply = 1;
    public TileController CurrentTile = null;
    public MergeArea CurrentMergeArea = null;


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
        _particleManager = FindObjectOfType<ParticleManager>();
    }





    private void Click()
    {
        if (_isClickable && ButtonState == ButtonStates.OnBoard)
        {
            var defaultPos = transform.localPosition;
            defaultPos.y = _defaultYPos;
            transform.localPosition = defaultPos;
            float money = ButtonMultiply * GodManager.Income;
            transform.DOLocalMoveY(defaultPos.y - .3f, .1f).SetEase(Ease.Linear).OnComplete(() =>
            {
                transform.DOLocalMoveY(defaultPos.y, .1f).SetEase(Ease.Linear);
                _particleManager.MoneyParticle(transform.position + Vector3.up * .5f, money);
                EventManager.OnGetMoney?.Invoke(money);
            });
        }
    }

    public void MergeUpgrade()
    {
        int newActiveIndex = 0;
        for (int i = 0; i < _buttons.Length; i++)
        {
            if (_buttons[i].activeSelf)
            {
                newActiveIndex = i + 1;
                if (newActiveIndex > _buttons.Length) newActiveIndex = _buttons.Length;
            }
        }

        _buttons[newActiveIndex].SetActive(true);
        _buttons[newActiveIndex - 1].SetActive(false);
        ButtonMultiply *= 2;
    }

    public void DropMergeArea(GameObject area)
    {
        CurrentMergeArea = area.GetComponent<MergeArea>();
        transform.position = area.transform.position;
        gameObject.layer = _buttonLayer;
        Clickable = false;
        ButtonState = ButtonStates.MergedArea;
    }

    public void DropBoardTile(TileController tile)
    {
        transform.position = tile.transform.position;
        var pos = transform.localPosition;
        pos.y = _defaultYPos;
        transform.localPosition = pos;
        gameObject.layer = _buttonLayer;
        tile.TileAvailability(false);
        CurrentTile = tile;
        Clickable = true;
        ButtonState = ButtonStates.OnBoard;
    }

    public void ResetPosition(Vector3 pos)
    {
        transform.localPosition = pos;
        gameObject.layer = _buttonLayer;
        Clickable = true;
    }

    public void ButtonSelected()
    {
        if (CurrentTile != null)
        {
            CurrentTile.TileAvailability(true);
            CurrentTile = null;
        }

        if (CurrentMergeArea != null)
        {
            CurrentMergeArea.AreaReset();
            CurrentMergeArea = null;
        }
    }
}