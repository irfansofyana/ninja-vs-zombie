﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private float xMax;

    [SerializeField]
    private float xMin;
    
    [SerializeField]
    private float yMax;
    
    [SerializeField]
    private float yMin;
    
    private Transform target;
    
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("Ninja") != null){
            target = GameObject.Find("Ninja").transform;  
        }else {
            target = null;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null)
        transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(target.position.y, yMin, yMax), transform.position.z);
    }
}
