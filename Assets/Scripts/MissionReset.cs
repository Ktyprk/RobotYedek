using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MissionReset : MonoBehaviour
{
    [SerializeField] private GameObject missionsPrefab;
    [SerializeField] private Transform spawnedMissionsParent;
    private List<GameObject> missionObjects;

    private void Start()
    {
        missionObjects = new List<GameObject>();
        missionObjects = GetAllChildren(spawnedMissionsParent.gameObject);
    }

    private void Reset()
    {
        foreach (GameObject destroy in missionObjects)
        {
            Destroy(destroy);
        }

        spawnedMissionsParent = Instantiate(missionsPrefab, new Vector3(80.1299973f,13.9300003f,168.970001f), Quaternion.identity).transform;
        
        missionObjects = new List<GameObject>();
        missionObjects = GetAllChildren(spawnedMissionsParent.gameObject);
    }
    
    List<GameObject> GetAllChildren(GameObject obj) {
        List<GameObject> children = new List<GameObject>();
    
        foreach(Transform child in obj.transform) {
            children.Add(child.gameObject);
            children.AddRange(GetAllChildren(child.gameObject));
        }
    
        return children;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Reset();
        }
    }
}
