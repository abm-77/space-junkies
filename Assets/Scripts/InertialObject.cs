using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class InertialObject : MonoBehaviour
{
    [SerializeField] private Vector2 _position = Vector2.zero;
    [SerializeField] private Vector2 _velocity = Vector2.zero;
    [SerializeField] private float _mass = 1;
    
    public readonly ReactiveProperty<Vector2> Position = new(Vector2.zero);
    public readonly ReactiveProperty<Vector2> Velocity = new(Vector2.zero);
    public readonly ReactiveProperty<float> Mass = new(1);
    
    void Awake()
    {
        Position.Value = _position;
        Velocity.Value = _velocity;
        Mass.Value = _mass;
    }

    public void ApplyImpulse(Vector2 impulse)
    {
        Velocity.Value += impulse / Mass.Value;
    }

    public static Vector2 GetImpulse(Vector2 velocity, float mass)
    {
        return velocity * mass;
    }

    /// <summary>
    /// Changes the velocity of `this` to be consistent with having launched `projectile` with the projectile's mass and velocity
    /// </summary>
    /// <param name="projectile"></param>
    public void LaunchProjectile(InertialObject projectile)
    {
        Velocity.Value -= projectile.Velocity.Value * projectile.Mass.Value / Mass.Value;
    }
}
