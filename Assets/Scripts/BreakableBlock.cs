using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    [SerializeField] private float _momentumThreshold;
    public ParticleSystem system;

    

    void Start()
    {
        system = GetComponent<ParticleSystem>();
        system.Pause();
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        var prevVel = other.relativeVelocity;
        
        // Calculate x and y components of the vector using sine and cosine
        var angleRad = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
        var x = -Mathf.Sin(angleRad);
        var y = Mathf.Cos(angleRad);
        var blockOrientation = new Vector2(x, y);

        var projectileMomentum = other.rigidbody.mass * prevVel;
        var momentumInCorrectDirection = Math.Abs(Vector3.Cross(blockOrientation, projectileMomentum).z);

        if (momentumInCorrectDirection < _momentumThreshold)
        {
            return;
        }
        
        // subtract collision from player momentum and break this
        
        var isToTheRight = Vector3.Cross(blockOrientation, projectileMomentum).z < 0;
        var rightNormalDirection = new Vector2(blockOrientation.y, -blockOrientation.x).normalized;
        
        var newMomentumAfterCollision = projectileMomentum - projectileMomentum.normalized * _momentumThreshold;
        var newVelocityAfterCollision = newMomentumAfterCollision / other.rigidbody.mass;
        other.rigidbody.velocity = newVelocityAfterCollision;

        // TODO: play destroy animation
        // var child = this.DestroyableObject;
        // var collider = GetCollider2D();
        // var sprite = GetComponent<SpriteRenderer>();

        system.Play(); // Applies the new value directly to the Particle System
        Destroy(gameObject);
    }
}
