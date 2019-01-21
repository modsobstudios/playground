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

    public override bool interact(GameObject _obj)
    {
        if(_obj.name == keyName)
        {
            Debug.Log("The puzzle piece slips perfectly into place.");
            _obj.transform.position = piecePlace.transform.position;
            piecePlace = _obj;
            _obj.transform.parent = transform;
            _obj.GetComponent<Rigidbody>().isKinematic = true;
            return true;
        }
        else
        {
            Debug.Log("That won't fit, you neanderthal!");
            return false;
        }
    }
}
