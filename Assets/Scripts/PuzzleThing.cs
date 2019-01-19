using UnityEngine;

public class PuzzleThing : Thing
{
    public string puzzleName;
    public string keyName;
    public GameObject piecePlace;

    // Use this for initialization
    void Start()
    {
        GameObject.Find("Thingmaster").GetComponent<Thingmaster>().addThing(puzzleName, gameObject.GetComponent<PuzzleThing>());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void interact(string _name)
    {
        if(_name == keyName)
        {
            Debug.Log("The puzzle piece slips perfectly into place.");
        }
        else
        {
            Debug.Log("That won't fit, you neanderthal!");
        }
    }
}
