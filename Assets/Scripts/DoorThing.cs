using UnityEngine;

public class DoorThing : Thing
{
    public string keyName;
    public string doorName;
    public bool isOpen = false;
    // Use this for initialization
    void Start()
    {
        GameObject.Find("Thingmaster").GetComponent<Thingmaster>().addThing(doorName, gameObject.GetComponent<DoorThing>());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override bool interact(GameObject _obj)
    {
        if (_obj.name == keyName)
        {
            if (!isOpen)
            {
                // Open de duur.
                isOpen = true;
                Debug.Log("Crrrrrrrrrreeeeeaaaaaak... The door is open.");
                transform.Rotate(transform.right, 90.0f);
                return false;
            }
            else
            {
                Debug.Log("The door is already open, y'goof!");
                return false;
            }
        }
        else
        {
            // Heckle the fool.
            Debug.Log("What is this? You think a " + _obj.name + " can defeat me? Pshaw! Begone, tiny dumpling!");
            return false;
        }
    }
}
