using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{

    [SerializeField]
    public float magnet_force = 150.0f;

    // Start is called before the first frame update
    void Start()
    {
        AreaEffector2D effector = GetComponent<AreaEffector2D>();
        effector.forceAngle = Mathf.Rad2Deg * -MathF.Atan2(transform.up.y, transform.up.x);
        effector.forceMagnitude = magnet_force;
    }

    // Update is called once per frame
    void Update()
    {
        AreaEffector2D effector = GetComponent<AreaEffector2D>();
        Debug.Log(-effector.forceAngle);
    }
}
