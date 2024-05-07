using System;
using UnityEngine;

public class IgnoreCollisionsWithPlayer : MonoBehaviour
{
    private void Awake()
    {
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
}