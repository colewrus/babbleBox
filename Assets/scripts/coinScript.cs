using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinScript : MonoBehaviour {

    bool move;
    Vector3 p;
    public float speed;
    // Use this for initialization
    void Start () {
		   
	}

    private void Awake()
    {
        p = Camera.main.ViewportToWorldPoint(new Vector3(0.9f, 0.1f, Camera.main.nearClipPlane));        
        move = true;
    }

    // Update is called once per frame
    void Update () {
		if(move)
            transform.position = Vector3.MoveTowards(transform.position, p, speed * Time.deltaTime);
        if(transform.position == p)
        {
            move = false;
            this.gameObject.SetActive(false);
        }
    }
}
