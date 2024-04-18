using UnityEngine;

public class ThrowProjectile : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D projectile_prefab;

    [SerializeField]
    [Range(0.1f, 2.0f)]
    private float throw_impulse_scalar = 100;
    [SerializeField]
    private float inv_max_throw_mag, max_throw_mag = 100.0f;

    public Rigidbody2D selected_projectile => projectile_prefab; // TODO: later, you should be able to change your selected projectile via an inventory
    private Rigidbody2D player;
    private LineRenderer line;
    private Vector3[] points = new Vector3[2];
    private float projectile_to_player_mass_ratio, inv_projectile_mass;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        line = gameObject.AddComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.widthMultiplier = 1.5f;
        line.positionCount = 2;
        line.enabled = false;

        inv_max_throw_mag = 1.0f / max_throw_mag;
        inv_projectile_mass = 1.0f / projectile_prefab.mass;
        projectile_to_player_mass_ratio = projectile_prefab.mass / player.mass;
    }

    public void Update()
    {
        Vector3 player_pos = player.transform.position;
        Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_pos.z = 0;

        Vector3 throw_displacement = (mouse_pos - player_pos);
        float throw_mag = Mathf.Clamp(throw_displacement.magnitude, 0, max_throw_mag);
        var clampedMouseDrag = throw_displacement.normalized * throw_mag;
        Vector3 throw_vector = clampedMouseDrag + (Vector3)player.velocity;

        if (Input.GetMouseButtonDown(0))
            line.enabled = true;

        if (Input.GetMouseButtonUp(0))
        {
            line.enabled = false;

            Rigidbody2D projectile = Instantiate(selected_projectile, player_pos, Quaternion.identity);
            Vector2 throw_velocity = throw_impulse_scalar * throw_vector * inv_projectile_mass;
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), projectile.GetComponent<Collider2D>());
            projectile.velocity = throw_velocity;
            player.velocity -= throw_velocity * projectile_to_player_mass_ratio;
        }

        points[0] = player_pos;
        points[1] = player_pos + clampedMouseDrag;
        line.SetPositions(points);

        throw_mag *= inv_max_throw_mag;
        line.startColor = line.endColor = new Color(throw_mag, 1.0f - throw_mag, 0.0f);
    }
}