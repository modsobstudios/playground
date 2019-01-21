using UnityEngine;
using System.Collections.Generic;

public class Thingmaster : MonoBehaviour
{
    public Dictionary<string, Thing> things = new Dictionary<string, Thing>();

    // Pass the name of the Thing in _type to select the correct type of Thing from the collection.
    // Inherited function will be called using _name as a parameter.
    // Returns true if the object will be taken from the player.
    // Returns false if the object will stay in the player's hand.
    public bool parseThings(string _type, GameObject _obj)
    {
        if(things.ContainsKey(_type))
        {
            return things[_type].interact(_obj);
        }
        else
        {
            Debug.Log("What are you touching? That's not a Thing!");
            return false;
        }
    }

    public void addThing(string _type, Thing _thing)
    {
        things.Add(_type, _thing);
    }
}
