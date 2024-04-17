using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        Destroy(collider.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
