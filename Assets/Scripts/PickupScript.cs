using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{

    private Inventory inventory;
    private GameObject player;
    private SpriteRenderer r2d;

    [SerializeField]
    public float pickup_distance = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        player = GameObject.FindGameObjectWithTag("Player");
        r2d = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float dist = (player.transform.position - transform.position).magnitude;
        if (dist < pickup_distance)
        {
            r2d.color = Color.green;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Rigidbody2D pb = player.GetComponent<Rigidbody2D>();
                Rigidbody2D rb = this.GetComponent<Rigidbody2D>();

                inventory.AddBigRock();
                pb.velocity = ((pb.mass * pb.velocity) + (rb.mass * rb.velocity)) / (pb.mass + rb.mass);
                GameObject.Destroy(this.gameObject);
            }
        }
        else
        {
            r2d.color = Color.white;
        }
    }

}
