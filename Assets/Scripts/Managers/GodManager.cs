using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GodManager : MonoBehaviour
{
    private const int _buttonLayer = 6;
    private const int _buttonMergeArea = 7;
    private const int _selectedButtonLayer = 8;
    private const int _boardTile = 9;
    private Camera _camera;
    public Vector3 DefaultButtonPos;
    public ButtonController SelectedButton;
    private bool _isDragable = false;
    [SerializeField] private LayerMask _layerMask;
    public static float Income = 1.0f;
    private bool _isClick = false;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.layer == _buttonLayer)
                {
                    _isClick = true;
                    DefaultButtonPos = hit.collider.transform.position;
                    DefaultButtonPos.y = 0.07077199f;
                    SelectedButton = hit.collider.gameObject.transform.parent.GetComponent<ButtonController>();
                    SelectedButton.ButtonSelected();
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (SelectedButton == null) return;
            DragButton();

            if (_isClick && !_isDragable && SelectedButton.Clickable)
            {
                EventManager.OnButtonClick?.Invoke();
                _isClick = false;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (SelectedButton == null) return;
            _isDragable = false;
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000.0f, _layerMask))
            {
                if (hit.collider.gameObject.layer == _buttonMergeArea)
                {
                    ButtonDropMergeArea(hit.collider.transform.position, hit.collider.gameObject);
                }

                if (hit.collider.gameObject.layer == _boardTile)
                {
                    var tile = hit.collider.GetComponent<TileController>();
                    if (tile.IsEmpty)
                    {
                        SelectedButton.DropBoardTile(tile);
                        SelectedButton = null;
                    }
                    else
                    {
                        SelectedButtonReset();
                    }
                }
            }
            else
            {
                SelectedButtonReset();
            }
        }
    }

    private void DragButton()
    {
        float planeY = 1.0f;
        Plane plane = new Plane(Vector3.up, Vector3.up * planeY); // ground plane
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        float distance;

        if (plane.Raycast(ray, out distance))
        {
            if (Mathf.Abs((ray.GetPoint(distance).x - SelectedButton.transform.position.x)) > 0.5f)
            {
                _isDragable = true;
            }

            if (Mathf.Abs((ray.GetPoint(distance).z - SelectedButton.transform.position.z)) > 0.5f)
            {
                _isDragable = true;
            }

            if (_isDragable)
            {
                SelectedButton.Clickable = false;
                SelectedButton.gameObject.layer = _selectedButtonLayer;
                SelectedButton.transform.position =
                    Vector3.Lerp(SelectedButton.transform.position, ray.GetPoint(distance), 15 * Time.deltaTime);
            }
        }
    }

    private void SelectedButtonReset()
    {
        SelectedButton.ResetPosition(DefaultButtonPos);
        SelectedButton = null;
    }

    private void ButtonDropMergeArea(Vector3 areaPos, GameObject mergeArea)
    {
        MergeArea area = mergeArea.GetComponent<MergeArea>();
        if (area.AreaState == MergeArea.AreaStates.Empty)
        {
            area.AreaSetButton(SelectedButton);
            SelectedButton.DropMergeArea(mergeArea);
            SelectedButton = null;
        }
        else
        {
            if (area.CurrentButton.ButtonMultiply == SelectedButton.ButtonMultiply &&
                area.CurrentButton != SelectedButton)
            {
                Destroy(SelectedButton.gameObject);
                area.CurrentButton.MergeUpgrade();
            }
            else
            {
                SelectedButtonReset();
            }
        }
    }
}