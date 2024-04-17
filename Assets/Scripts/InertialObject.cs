using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class InertialObject : MonoBehaviour
{
    public readonly ReactiveProperty<Vector2> Velocity = new(Vector2.zero);
    public readonly ReactiveProperty<float> Mass = new(1);

    public Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        Velocity.Subscribe(newVel => _rb.velocity = newVel).AddTo(this);
        Mass.Subscribe(newMass => _rb.mass = newMass).AddTo(this);
        Velocity.Value = _rb.velocity;
        Mass.Value = _rb.mass;
    }

    private void LateUpdate()
    {
        Velocity.Value = _rb.velocity;
    }
}