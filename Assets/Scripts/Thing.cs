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

    public virtual bool interact(GameObject _obj)
    {
        Debug.Log("You have touched a Master Thing! Stop it!");
        return false;
    }
}
