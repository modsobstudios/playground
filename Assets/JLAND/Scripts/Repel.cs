using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repel : MonoBehaviour
{

    Vector3 dir;
    public float force;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>())
        {
            //Debug.Log("Hit");

            dir = gameObject.transform.position - collision.gameObject.transform.position;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(
                dir.normalized * ((force) / Vector3.Distance(collision.gameObject.transform.position,
                gameObject.transform.position) * 4));
        }
    }
}
