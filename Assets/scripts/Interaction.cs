using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif


public enum InType
{
    word, conversation, action
}

public enum ConversationType
{
    singleSpeech, multiSpeech, singleThought, multiThought
}
[System.Serializable]
public class SpeechElement
{
    public List<String> bText = new List<string>(); //initialize in the bubbles yeah   
    //public Sprite mySprite; 
}

[System.Serializable]
public class ObjectModal
{
    public String modClass_string;
    public Sprite modClass_sprite;
}


public class Interaction : MonoBehaviour {


    public InType interactionType;

    public ObjectModal myModal;
    public List<SpeechElement> flow = new List<SpeechElement>();
    public List<GameObject> bubbles = new List<GameObject>(); //speech bubble here ya? alwya ta same
    public bool barriers; //if yes then player needs something for clicking on me to mean something

    //Variables for WORD
    public string modal_Word;
    public AudioClip modal_Audio;

    public Sprite mySprite; //variable to pass in the lookup of it's own sprite to send into the modal
    public bool proxyNeeded; 
    public bool proxy; //bool to determine if you are in proximity of the object
    bool collect_FirstTouch; //is this the first time touching
    public GameObject coinPrefab;

    //Variables for CONVERSATION 
    public GameObject trigger; //conversation & action
    public GameObject singleBubbleObj;
    public ConversationType conversationType;
    public Sprite sprite_Speech;//bubble
    public Sprite sprite_Thought;//bubble
    public Sprite sprite_Content;//the contents of the speech or thought bubble
    public string text_Content; //the contents of the speech or thought bubble

    //Variables for ACTION
    public GameObject vocabWindow; //sept demo vocab window
    public GameObject denyBubble; //speech bubble for DEM to deny if player hasn't tapped 6 objects
    public bool characterVisible; //start dem as invisible

    //enum to declare what this is
    //Conversation/options wheel
    //Return - More conversation - add to inventory - add to dictionary - set bool/variable
    //Add to inventory/dictionary
    //Check variable
    //This check the object you tap, does it need a variable? Do you have the variable?
    //i.e. door may not need a variable just needs you to tap it to open. Other doors may require that you've collected a variable


    // Use this for initialization
    void Start () {
        if (!proxyNeeded)
            proxy = false;
        else if (proxyNeeded)
            proxy = true;
        myModal.modClass_sprite = this.GetComponent<SpriteRenderer>().sprite;
        myModal.modClass_string = modal_Word;
        mySprite = this.GetComponent<SpriteRenderer>().sprite;
        collect_FirstTouch = false;
        if(interactionType == InType.conversation)
        {
            if(conversationType == ConversationType.singleSpeech)
            {
                singleBubbleObj.SetActive(false);
            }
        }
        characterVisible = false; //can't see dem at start;
	}

    private void Awake()
    {
        if(this.gameObject.name == "DEM")
        {
            Debug.Log("check");
            
        }
    }

    // Update is called once per frame
    void Update () {
  
	}

    private void OnMouseDown()
    {
        if(this.gameObject.name == "DEM")
        {
            gameObject.GetComponent<Animator>().Play("demAppear");
        }

        if(interactionType == InType.word)
        {
            if (!proxy)
                GM.instance.queue_String = this.gameObject.name;
            if (proxy)
                GM.instance.mStart(modal_Word, modal_Audio, mySprite);
            if (!collect_FirstTouch)
            {
                GM.instance.collectedObjects.Add(myModal);
                //GM.instance.cash += 2;
                GM.instance.AddCash(2);
                //play coin sound
                Instantiate(coinPrefab, this.transform.position, Quaternion.identity);
                Invoke("createCoin", 0.1f);
                collect_FirstTouch = true;
            }
        }
  
    }

    public void TriggerSingleBubble()
    {
        singleBubbleObj.SetActive(true);     
    }

    public void createCoin()
    {
        Instantiate(coinPrefab, this.transform.position, Quaternion.identity);
    }

    public void WindowOn()
    {
        if(GM.instance.collectedObjects.Count == 6)
        {           
            vocabWindow.SetActive(true);            
            denyBubble.SetActive(false);
        }else
        {
            denyBubble.SetActive(false);
        }

    }

    public void StopAnim(float t)
    {
        Debug.Log(t);
        this.gameObject.GetComponent<Animator>().Play(0);
        StartCoroutine(TimerRun(t));        
    }

    IEnumerator TimerRun(float t)
    {
        yield return new WaitForSeconds(t);
    }
}



#if UNITY_EDITOR
[CustomEditor(typeof(Interaction))]
public class Interaction_editor : Editor
{
    override public void OnInspectorGUI()
    {
        Interaction itr_Type = target as Interaction;
        //genericObjects myGui = target as genericObjects; //reference the script that has the variables we want to use

        itr_Type.interactionType = (InType)EditorGUILayout.EnumPopup(itr_Type.interactionType);
        // These guys should be visible no matter what
        //myGui.myIdentity = (objType)EditorGUILayout.EnumPopup(myGui.myIdentity);
        //myGui.mySound = (AudioClip)EditorGUILayout.ObjectField("My Sound", myGui.mySound, typeof(AudioClip), true);

        if (itr_Type.interactionType == InType.word)
        {
            itr_Type.modal_Word = EditorGUILayout.TextField("Modal Word", itr_Type.modal_Word);
            itr_Type.modal_Audio = (AudioClip)EditorGUILayout.ObjectField("Audio", itr_Type.modal_Audio,typeof(AudioClip));
            itr_Type.mySprite = (Sprite)EditorGUILayout.ObjectField("Sprite", itr_Type.mySprite, typeof(Sprite));
            itr_Type.proxyNeeded = EditorGUILayout.Toggle("Proximity Check?", itr_Type.proxyNeeded);
            itr_Type.proxy = EditorGUILayout.Toggle("Proxy", itr_Type.proxy);
            itr_Type.coinPrefab = (GameObject)EditorGUILayout.ObjectField("Coin", itr_Type.coinPrefab, typeof(GameObject));
        }

        if(itr_Type.interactionType == InType.conversation)
        {
            itr_Type.conversationType = (ConversationType)EditorGUILayout.EnumPopup(itr_Type.conversationType);
            itr_Type.trigger = (GameObject)EditorGUILayout.ObjectField("Trigger", itr_Type.trigger,typeof(GameObject));   
            itr_Type.singleBubbleObj = (GameObject)EditorGUILayout.ObjectField("Single Bubble", itr_Type.singleBubbleObj, typeof(GameObject));
            itr_Type.sprite_Speech = (Sprite)EditorGUILayout.ObjectField("Speech Bubble", itr_Type.sprite_Speech, typeof(Sprite));
            itr_Type.sprite_Thought = (Sprite)EditorGUILayout.ObjectField("Thought Bubble", itr_Type.sprite_Thought, typeof(Sprite));
            itr_Type.sprite_Content = (Sprite)EditorGUILayout.ObjectField("Image Content", itr_Type.sprite_Content, typeof(Sprite));
            //itr_Type.text_Content = EditorGUILayout.TextField("Text Content", itr_Type.text_Content);
        }
        
        if(itr_Type.interactionType == InType.action)
        {
            itr_Type.trigger = (GameObject)EditorGUILayout.ObjectField("Trigger", itr_Type.trigger, typeof(GameObject));
            itr_Type.vocabWindow = (GameObject)EditorGUILayout.ObjectField("Vocab", itr_Type.vocabWindow, typeof(GameObject));
        }
    }
}
#endif




