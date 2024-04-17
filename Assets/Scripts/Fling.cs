using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fling : MonoBehaviour
{

	private Vector2 ballPosition;
	private Vector2 releasePosition;
	private bool didMouseDown;
	private bool didMouseUp;
	public float forceMulitplier = 1;
	private float ballRadius;


	void ResetMouse()
	{
		didMouseUp = false;
		didMouseDown = false;
	}

	void Start()
	{
		ResetMouse();
		//figure out how to reference the game object's circle collider
		ballRadius = 0.5f;
		ballPosition = gameObject.transform.position;
	}

    void OnMouseDown()
    {
        ballPosition = gameObject.transform.position;
        didMouseDown = true;
    }

    void OnMouseUp()
    {
        releasePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        didMouseUp = true;
    }

    void Update()
    {
	    //IF the mouse button was clicked inside and then outside of the ball's raius then add force
        if ((didMouseUp && didMouseDown) && (Vector2.Distance(ballPosition, releasePosition) > ballRadius))
        {
            GetComponent<Rigidbody2D>().AddForce((ballPosition - releasePosition) * forceMulitplier);
            ResetMouse();
        }
    }

    }