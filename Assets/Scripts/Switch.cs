using System;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private Sprite toggledSprite;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Projectile") || other.CompareTag("Player"))
        {
            Debug.Log(GetComponent<SpriteRenderer>());
            GetComponent<SpriteRenderer>().sprite = toggledSprite;
        }
    }
}