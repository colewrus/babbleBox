using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour {

    public static GM instance = null;

    public GameObject modal_Panel;
    public AudioClip chambered_Audio;
   
    public string queue_String; //name of the object so we can check this on entering the trigger proximity

    public bool demoAug;//below variables affect the august demo
    //timer to make tap icon appear
    float da_timer;
    public GameObject da_tapIcon;
    float tapIconTimer;
    public List<ObjectModal> collectedObjects = new List<ObjectModal>();

    public int cash;

    public Text text_Cash;
    public bool playerLocked;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    // Use this for initialization
    void Start () {
        modal_Panel.SetActive(false);
        queue_String = null;
        if (demoAug)
        {
            da_tapIcon.SetActive(false);
        }
        text_Cash.text = "" + cash;
        playerLocked = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (demoAug)
        {
            Demo_Aug();
        }
		
	}

    void Demo_Aug()
    {
        if (da_timer < 2)
        {
            da_timer += 0.95f*Time.deltaTime;
        }else
        {
            da_tapIcon.SetActive(true); 
        }           
        if(tapIconTimer < 6)
        {
            tapIconTimer += 1 * Time.deltaTime;
        }
        else
        {
            da_tapIcon.SetActive(false);
        }
    }

    public void AddInt()
    {

    }
    
    public void AddCash(int amount)
    {
        cash += amount;
        text_Cash.text = "" + cash;
    }

    public void mStart(string mString, AudioClip mAudio, Sprite mSprite)
    {
        modal_Panel.SetActive(true);
        playerLocked = true;
        modal_Panel.transform.GetChild(0).GetComponent<Image>().sprite = mSprite;
        modal_Panel.transform.GetChild(1).GetComponent<Text>().text = "This is a "  + mString; //set the text
        chambered_Audio = mAudio;
    }

    public void mAudio_Play()
    {
        AudioSource myAudio = this.GetComponent<AudioSource>();
        myAudio.clip = chambered_Audio;
        myAudio.Play();
        //this.GetComponent<AudioSource>()
    }

    public void mClose()
    {
        //play animation
        modal_Panel.SetActive(false);
        chambered_Audio = null;
        queue_String = null;
        playerLocked = false;
    }
}
