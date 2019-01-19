using UnityEngine;
using System.Collections.Generic;

public class Thingmaster : MonoBehaviour
{
    public Dictionary<string, Thing> things = new Dictionary<string, Thing>();

    // Pass the name of the Thing in _type to select the correct type of Thing from the collection.
    // Inherited function will be called using _name as a parameter.
    public void parseThings(string _type, string _name)
    {
        if(things.ContainsKey(_type))
        {
            things[_type].interact(_name);
        }
        else
        {
            Debug.Log("What are you touching? That's not a Thing!");
        }
    }

    public void addThing(string _type, Thing _thing)
    {
        things.Add(_type, _thing);
    }
}
