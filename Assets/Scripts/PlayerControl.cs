using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float speed = 10.0f;
    private float jumpForce = 6.0f;
    private Rigidbody rb;

    public GameObject held;
    private GameObject emptyHeld;
    private Vector3 heldPos;
    private bool hasObject = false;

    public Thingmaster masterThing;


#region blatantlyStolenMouseLookCode

    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 5F;
    public float sensitivityY = 5F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    float rotationX = 0F;
    float rotationY = 0F;
    private List<float> rotArrayX = new List<float>();
    float rotAverageX = 0F;
    private List<float> rotArrayY = new List<float>();
    float rotAverageY = 0F;
    public float tickCount = 20;
    public float maximumVelocity = 10;
    Quaternion originalRotation;

#endregion

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // blatantlyStolenMouseLookCode
        if (rb)
            rb.freezeRotation = true;
        originalRotation = transform.localRotation;
        Cursor.lockState = CursorLockMode.Locked;
        emptyHeld = held;
        heldPos = held.transform.position;
    }

    void FixedUpdate()
    {
        doMove();
        blatantlyStolenMouseLookCode();
        if (Input.GetKeyDown(KeyCode.E) && !hasObject)
            tryPickup();
        if (Input.GetKeyDown(KeyCode.E) && hasObject)
            interact();
        if (Input.GetKeyDown(KeyCode.F))
            tryThrow();
    }

    // Legit probably the best FPS movement code I've done. So simple. So clean. 
    private void doMove()
    {
        if (Input.GetKey(KeyCode.A))
            rb.velocity = new Vector3(-transform.right.x * speed, rb.velocity.y, -transform.right.z * speed);
        if (Input.GetKey(KeyCode.D))
            rb.velocity = new Vector3(transform.right.x * speed, rb.velocity.y, transform.right.z * speed);
        if (Input.GetKey(KeyCode.W))
            rb.velocity = new Vector3(transform.forward.x * speed, rb.velocity.y, transform.forward.z * speed);
        if (Input.GetKey(KeyCode.S))
            rb.velocity = new Vector3(-transform.forward.x * speed, rb.velocity.y, -transform.forward.z * speed);
        //I wonder if this works
        if (Input.GetKeyDown(KeyCode.Space))
            rb.velocity = new Vector3(rb.velocity.x, transform.up.y * jumpForce, rb.velocity.z );
        //I tried to make it do the thing...
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            rb.velocity = new Vector3(rb.velocity.x * 0.5f, rb.velocity.y, rb.velocity.z * 0.5f);
    }

    // Don't you worry about this here code.
    private void blatantlyStolenMouseLookCode()
    {
        if (axes == RotationAxes.MouseXAndY)
        {
            rotAverageY = 0f;
            rotAverageX = 0f;

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;

            rotArrayY.Add(rotationY);
            rotArrayX.Add(rotationX);

            if (rotArrayY.Count >= tickCount)
            {
                rotArrayY.RemoveAt(0);
            }
            if (rotArrayX.Count >= tickCount)
            {
                rotArrayX.RemoveAt(0);
            }

            for (int j = 0; j < rotArrayY.Count; j++)
            {
                rotAverageY += rotArrayY[j];
            }
            for (int i = 0; i < rotArrayX.Count; i++)
            {
                rotAverageX += rotArrayX[i];
            }

            rotAverageY /= rotArrayY.Count;
            rotAverageX /= rotArrayX.Count;

            rotAverageY = ClampAngle(rotAverageY, minimumY, maximumY);
            rotAverageX = ClampAngle(rotAverageX, minimumX, maximumX);

            Quaternion yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);

            transform.localRotation = originalRotation * xQuaternion * yQuaternion;
        }
        else if (axes == RotationAxes.MouseX)
        {
            rotAverageX = 0f;

            rotationX += Input.GetAxis("Mouse X") * sensitivityX;

            rotArrayX.Add(rotationX);

            if (rotArrayX.Count >= tickCount)
            {
                rotArrayX.RemoveAt(0);
            }
            for (int i = 0; i < rotArrayX.Count; i++)
            {
                rotAverageX += rotArrayX[i];
            }
            rotAverageX /= rotArrayX.Count;

            rotAverageX = ClampAngle(rotAverageX, minimumX, maximumX);

            Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);
            transform.localRotation = originalRotation * xQuaternion;
        }
        else
        {
            rotAverageY = 0f;

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

            rotArrayY.Add(rotationY);

            if (rotArrayY.Count >= tickCount)
            {
                rotArrayY.RemoveAt(0);
            }
            for (int j = 0; j < rotArrayY.Count; j++)
            {
                rotAverageY += rotArrayY[j];
            }
            rotAverageY /= rotArrayY.Count;

            rotAverageY = ClampAngle(rotAverageY, minimumY, maximumY);

            Quaternion yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);
            transform.localRotation = originalRotation * yQuaternion;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        angle = angle % 360;
        if ((angle >= -360F) && (angle <= 360F))
        {
            if (angle < -360F)
            {
                angle += 360F;
            }
            if (angle > 360F)
            {
                angle -= 360F;
            }
        }
        return Mathf.Clamp(angle, min, max);
    }

    public void tryPickup()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Debug.DrawRay(ray.origin, Camera.main.transform.forward * 1000, Color.magenta);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Pickup")
            {
                hit.transform.position = held.transform.position;
                held = hit.transform.gameObject;
                held.transform.parent = transform;
                held.GetComponent<Rigidbody>().isKinematic = true;
                hasObject = true;
            }
        }
    }

    public void tryThrow()
    {
        GameObject g = held;
        held = emptyHeld;
        g.transform.parent = null;
        g.GetComponent<Rigidbody>().isKinematic = false;
        g.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
        hasObject = false;
    }

    public void interact()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Debug.DrawRay(ray.origin, Camera.main.transform.forward * 1000, Color.magenta);
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.tag == "Thing")
            {
                Debug.Log("Attempting to use " + held.name + " on " + hit.transform.gameObject.name + ".");
                masterThing.parseThings(hit.transform.gameObject.name, held.name);
            }
        }
    }
}
