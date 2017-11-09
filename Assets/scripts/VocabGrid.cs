using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VocabGrid : MonoBehaviour {

    public ObjectModal tmpModal;
    public List<GameObject> column0 = new List<GameObject>();
    public List<GameObject> column1 = new List<GameObject>();
    public List<GameObject> column2 = new List<GameObject>();
    public List<GameObject> column3 = new List<GameObject>();
    public List<GameObject> hearts = new List<GameObject>();

    public GameObject gate;

    int correct;

	// Use this for initialization
	void Start () {
		
	}
    private void Awake()
    {
        GM.instance.playerLocked = true; 
        for(int i = 0; i < hearts.Count; i++)
        {
            hearts[i].SetActive(true);
        }
        correct = 0;
    }
    // Update is called once per frame
    void Update () {		

	}

    public void WordCheck(bool rightObject)
    {
        if (rightObject)
        {
            Debug.Log("hit");
            ListColorFade(column0, "1c");            
            ListColorFade(column1, "2c");
            ListColorFade(column2, "3c");
            ListColorFade(column3, "4c");
        }else
        {
            if (hearts.Count > 0)
            {
                hearts[hearts.Count - 1].SetActive(false);
                hearts.Remove(hearts[hearts.Count - 1]);
                if(hearts.Count == 0)
                {
                    GM.instance.playerLocked = false;
                    this.gameObject.SetActive(false);
                }
            }
        }
        if(correct == 4)
        {
            this.gameObject.SetActive(false);
            GM.instance.playerLocked = false;
            gate.GetComponent<BoxCollider2D>().isTrigger = true;
            Color tempGate = gate.gameObject.GetComponent<SpriteRenderer>().color;
            tempGate.a = 0.35f;
            gate.gameObject.GetComponent<SpriteRenderer>().color = tempGate;
            //fade the gate & set inactive
        }
    }

    void ListColorFade(List<GameObject> myList, string objName)
    {
        if(EventSystem.current.currentSelectedGameObject.GetComponent<Transform>().name == objName)
        {
            correct++;
            GM.instance.AddCash(2);
            //if it's the first time then dish some cash
            for (int i = 0; i < myList.Count; i++)
            {
                Color colortemp = myList[i].gameObject.GetComponent<Image>().color;
                colortemp.a = 0;
                myList[i].gameObject.GetComponent<Image>().color = colortemp;                
            } 
        } 
    }

    /*
    IEnumerator Fader(GameObject obj)
    {
        Color tempColor = obj.GetComponent<Image>().color;
        tempColor.a -= 2 * Time.deltaTime;
        obj.GetComponent<Image>().color = tempColor;
        yield return new WaitForSeconds(0.1f);
    }
    */


}
