using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PickupScript : MonoBehaviour
{

    private Inventory inventory;
    private GameObject player;
    private SpriteRenderer r2d;
    private Rigidbody2D rockPrefab;

    private Color _prevColor;

    [SerializeField] private string _thisName;

    [SerializeField]
    public float pickup_distance = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        rockPrefab = Resources.Load<Rigidbody2D>($"Projectiles/{_thisName}");
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        player = GameObject.FindGameObjectWithTag("Player");
        r2d = GetComponent<SpriteRenderer>();
        _prevColor = r2d.color;
    }

    void Update()
    {
        float dist = (player.transform.position - transform.position).magnitude;
        if (dist < pickup_distance)
        {
            r2d.color = new Color(0, .9f, 1);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Rigidbody2D pb = player.GetComponent<Rigidbody2D>();
                Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
                
                inventory.AddItem(rockPrefab);
                pb.velocity = ((pb.mass * pb.velocity) + (rb.mass * rb.velocity)) / (pb.mass + rb.mass);
                GameObject.Destroy(this.gameObject);
            }
        }
        else
        {
            r2d.color = _prevColor;
        }
    }

}
