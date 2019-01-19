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

    public override void interact(string _name)
    {
        if (_name == keyName)
        {
            if (!isOpen)
            {
                // Open de duur.
                isOpen = true;
                Debug.Log("Crrrrrrrrrreeeeeaaaaaak... The door is open.");
            }
        }
        else
        {
            // Heckle the fool.
            Debug.Log("What is this? You think a " + _name + " can defeat me? Pshaw! Begone, tiny dumpling!");
        }
    }
}
