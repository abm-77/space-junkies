using System;
using R3;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private Sprite toggledSprite;
    public ReactiveProperty<bool> Hit = new(false);

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (Hit.Value || !(other.CompareTag("Projectile") || other.CompareTag("Player")))
        {
            return;
        }
        Hit.Value = true;
        GetComponent<SpriteRenderer>().sprite = toggledSprite;
    }
}