using UnityEngine;

public class Boing : MonoBehaviour
{
    float boing = 5000;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision _coll)
    {
        if (_coll.transform.GetComponent<Rigidbody>() != null)
        {
            Debug.Log("Boing.");
            Debug.Log("bOing.");
            Debug.Log("boIng.");
            Debug.Log("boiNg.");
            _coll.transform.GetComponent<Rigidbody>().AddForce(transform.forward * boing);
            Debug.Log("boinG.");
            Debug.Log("BoInG.");
            Debug.Log("bOing.");
            Debug.Log("BOING.");
        }
    }
}
