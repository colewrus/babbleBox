﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camScript : MonoBehaviour {



    public GameObject player;
    public float zValue;
    public float yOffset;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(player.transform.position.x, yOffset, zValue);
	}
}
