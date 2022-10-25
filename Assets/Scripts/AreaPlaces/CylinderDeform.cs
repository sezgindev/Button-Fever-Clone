using System;
using System.Collections;
using System.Collections.Generic;
using Deform;
using UnityEngine;

public class CylinderDeform : MonoBehaviour
{
    [SerializeField] private List<GameObject> moneyNodeDeformObjects;
    [SerializeField] private GameObject _nodePrefab;
    private Vector3 defaultPos;
    private Deformable _deformer;

    private void Awake()
    {
        defaultPos = moneyNodeDeformObjects[0].transform.position;
        _deformer = GetComponent<Deformable>();
    }

    private void SpawnCylinderNode()
    {
        GameObject spawnNode = null;
        foreach (var node in moneyNodeDeformObjects)
        {
            if (node.GetComponent<SpherifyDeformer>().Factor == 0)
            {
                spawnNode = node;
            }
        }

        if (spawnNode == null)
        {
            var newDeformer = Instantiate(_nodePrefab, defaultPos, Quaternion.identity, transform);
            _deformer.AddDeformer(newDeformer.GetComponent<SpherifyDeformer>());
            moneyNodeDeformObjects.Add(newDeformer);
            spawnNode = newDeformer;
        }

        spawnNode.GetComponent<Animator>().SetTrigger("isDeform");
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnCylinderNode();
        }
    }
}