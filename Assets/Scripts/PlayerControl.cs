using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour 
{
	private float speed = 10.0f;
    private float lookSpeed = 5.0f;
	private Rigidbody rb;
    private float xRot, zzRot;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		doMove ();
		doRotate ();
	}

	private void doMove()
	{
        if (Input.GetKey(KeyCode.A))
            rb.AddForce(-transform.right * speed);
        if (Input.GetKey(KeyCode.D))
            rb.AddForce(transform.right * speed);
        if (Input.GetKey(KeyCode.W))
            rb.AddForce(transform.forward * speed);
        if (Input.GetKey(KeyCode.S))
            rb.AddForce(-transform.forward * speed);

        if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            rb.velocity = new Vector3(rb.velocity.x * 0.5f, rb.velocity.y, rb.velocity.z * 0.5f);
	}

	private void doRotate()
	{
        xRot = Input.GetAxis("Mouse X");
        zzRot = Input.GetAxis("Mouse Y");
        // This is currently very bad.
        transform.Rotate(new Vector3(-zzRot, 0, 0) * lookSpeed);
        transform.Rotate(new Vector3(0, xRot, 0) * lookSpeed);
    }

}
