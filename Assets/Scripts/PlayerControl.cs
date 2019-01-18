using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour 
{
	public float speed = 10.0f;
	private Rigidbody rb;
	private float xMov, zMov;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		xMov = Input.GetAxis ("Horizontal");
		zMov = Input.GetAxis ("Vertical");
		doMove ();
		doRotate ();
	}

	private void doMove()
	{
		if (xMov == 0.0f && zMov == 0.0f)
			rb.velocity = new Vector3 (rb.velocity.x * 0.5f, rb.velocity.y, rb.velocity.z * 0.5f);
		rb.AddForce (new Vector3 (xMov, 0.0f, zMov) * speed);
	}

	private void doRotate()
	{
		
	}

}
