using System;
using System.Linq;
using R3;
using UnityEngine;

public class ThrowProjectile : MonoBehaviour
{
    private Rigidbody2D _projectileToThrow;
    [SerializeField] private Inventory _inventory;

    [SerializeField][Range(0.1f, 2.0f)] private float throw_impulse_scalar = 100;
    [SerializeField] private float max_throw_mag = 100.0f;

    [SerializeField] private RectTransform[] _regionsToIgnoreClick;

    private Rigidbody2D player;
    private LineRenderer line;
    private Vector3[] points = new Vector3[2];
    private bool _dragStarted;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        _inventory.SelectedSlot
            .Where(slot => slot)
            .Subscribe(slot => _projectileToThrow = slot.Item.Value)
            .AddTo(this);

        line = gameObject.AddComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.widthMultiplier = 1.5f;
        line.positionCount = 2;
        line.enabled = false;
    }

    public void Update()
    {
        if (!_inventory.SelectedSlot.Value || !_inventory.SelectedSlot.Value.Item.Value)
        {
            return;
        }

        Vector3 player_pos = player.transform.position;
        Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_pos.z = 0;

        Vector3 throw_displacement = (mouse_pos - player_pos);
        float throw_mag = Mathf.Clamp(throw_displacement.magnitude, 0, max_throw_mag);
        var clampedMouseDrag = throw_displacement.normalized * throw_mag;
        Vector3 throw_vector = clampedMouseDrag + (Vector3)player.velocity;

        var ignoreClick = _regionsToIgnoreClick.Any(region =>
        {
            var bounds = new Vector3[4];
            region.GetWorldCorners(bounds); // goes clockwise starting bottom-left and ending bottom-right
            return mouse_pos.x > bounds[0].x
                   && mouse_pos.x < bounds[2].x
                   && mouse_pos.y > bounds[0].y
                   && mouse_pos.y < bounds[2].y;
        });

        if (Input.GetMouseButtonDown(0) && !ignoreClick)
        {
            line.enabled = true;
            _dragStarted = true;
        }

        if (Input.GetMouseButtonUp(0) && _dragStarted)
        {
            line.enabled = false;
            _dragStarted = false;

            var itemToThrow = _inventory.SelectedSlot.Value.Item.Value;
            _inventory.SelectedSlot.Value.Item.Value = null;

            var thrownItem = Instantiate(itemToThrow, player_pos, Quaternion.identity);
            Vector2 throw_velocity = throw_vector * (throw_impulse_scalar / _projectileToThrow.mass);
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), thrownItem.GetComponent<Collider2D>());
            thrownItem.velocity = throw_velocity;
            player.velocity -= throw_velocity * (_projectileToThrow.mass / player.mass);
        }

        points[0] = player_pos;
        points[1] = player_pos + clampedMouseDrag;
        line.SetPositions(points);

        throw_mag *= 1.0f / max_throw_mag;
        line.startColor = line.endColor = new Color(throw_mag, 1.0f - throw_mag, 0.0f);
    }
}