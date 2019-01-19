using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thing : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void interact(string _name)
    {
        Debug.Log("You have touched a Master Thing! Stop it!");
    }
}
