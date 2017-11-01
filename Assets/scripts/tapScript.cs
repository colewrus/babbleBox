using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class tapScript : MonoBehaviour {

    public GameObject floor;
    private Vector3 target;
    public float speed;
    Ray ray;
    public float yBuffer;
   
                           
    void Start () {
        //initialize position
        transform.position = new Vector3(10, floor.transform.position.y + yBuffer, 1);
        target = transform.position;
   
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }
		
        if(target != transform.position && !GM.instance.playerLocked){                
            target = new Vector3(target.x, floor.transform.position.y + yBuffer, 1);
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            this.GetComponent<Animator>().Play("playerRun");
        }else if (GM.instance.playerLocked)
        {
            target = transform.position;
            this.GetComponent<Animator>().Play("playerIdle");
        }
        else
        {
            this.GetComponent<Animator>().Play("playerIdle");
        }

	}



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "object")
        {
            Interaction tmp = collision.GetComponent<Interaction>();
            tmp.proxy = true;
            if(GM.instance.queue_String == collision.gameObject.name)
            {
                GM.instance.mStart(tmp.modal_Word, tmp.modal_Audio, tmp.mySprite);
            }
        }

        if(collision.transform.tag == "remote")
        {            
            collision.transform.parent.GetComponent<Interaction>().TriggerSingleBubble();
        }
        if(collision.transform.name == "dem_Trigger")
        {
            collision.transform.parent.gameObject.GetComponent<SpriteRenderer>().enabled = true;         
            Animator anim = collision.transform.parent.gameObject.GetComponent<Animator>();
            GM.instance.playerLocked = true;
            anim.Play("dex0");           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.tag == "object")
        {
            collision.GetComponent<Interaction>().proxy = false;
            GM.instance.queue_String = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "barrier")
        {           
            target = new Vector3(transform.position.x, floor.transform.position.y + yBuffer, 1);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.tag == "barrier")
        {
            target = new Vector3(transform.position.x, floor.transform.position.y + yBuffer, 1);
        }
    }
}
