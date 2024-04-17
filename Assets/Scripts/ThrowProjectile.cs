using System;
using R3;
using R3.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class ThrowProjectile : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private InertialObject _player;
    [SerializeField] private InertialObject _projectilePrefab;
    [SerializeField] private float _throwImpulse = 100;

    public InertialObject SelectedProjectile =>
        _projectilePrefab; // TODO: later, you should be able to change your selected projectile via an inventory
    

    public void OnPointerDown(PointerEventData pointerEventData)
    {
    }

    public void OnPointerUp(PointerEventData pointerData)
    {
        var clickPosition3D = pointerData.pointerCurrentRaycast.worldPosition;
        var clickPosition = new Vector2(clickPosition3D.x, clickPosition3D.y);
        var playerPosition3D = _player.transform.position;
        var playerPosition = new Vector2(playerPosition3D.x, playerPosition3D.y);
        var throwDirection = (clickPosition - playerPosition).normalized;
        var throwVelocity = _throwImpulse * throwDirection / SelectedProjectile.Mass.Value;
        var thrownProjectile = Instantiate(SelectedProjectile, playerPosition3D, Quaternion.identity);
        Physics2D.IgnoreCollision(_player.GetComponent<Collider2D>(), thrownProjectile.GetComponent<Collider2D>());
        thrownProjectile.Velocity.Value = throwVelocity;
        _player.Velocity.Value -= throwVelocity * thrownProjectile.Mass.Value / _player.Mass.Value;
    }
}