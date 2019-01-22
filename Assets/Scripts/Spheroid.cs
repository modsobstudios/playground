using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spheroid : MonoBehaviour
{
    Vector3 dir;
    public List<GameObject> targets;
    GameObject self;

    private float speed = 0.0f;
    [SerializeField]
    private float strScale = 1.0f;

    // Use this for initialization
    void Start()
    {
        self = gameObject;
        speed = gameObject.GetComponent<SphereCollider>().radius;
    }

    // FixedUpdate is not called once per frame
    void FixedUpdate()
    {
        if (targets.Count > 0)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                dir = self.transform.position - targets[i].transform.position;

                targets[i].GetComponent<Rigidbody>().AddForce(dir.normalized * ((speed * strScale) / Vector3.Distance(targets[i].transform.position, self.transform.position) * 4));//Add gravity-like force to pull objects to self. The closer the object is to self, the harder the force. the * 4 increases the range of distribution of force based on distance from self
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>())
        {
            if (!targets.Contains(other.gameObject))
            {
                targets.Add(other.gameObject);
            }
//            Debug.Log("Bound");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (targets.Contains(other.gameObject))
        {
            targets.Remove(other.gameObject);
        }
    }
}

