using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class Inv_Needed : MonoBehaviour, IPointerClickHandler
{
    [TextArea(2, 5)]
    public string[] lookMessages;
    public int lookCount;
    [TextArea(2, 5)]
    public string usedUpMessage;
    ChangeCursor cam;
    public bool nearEnough;
    private Transform target;
    public bool seen = false;

    SimpleInv inv;

    [Header("Require an Object to Interact")]

    public bool needObject;
    public Inven neededObject;
    [TextArea(2, 5)]
    public string needItemMessage;
    [TextArea(2, 5)]
    public string haveItemMessage;

    public bool unLocked = false;

    [Header("Load a New Scene")]
    public bool loadScene = false;
    public int sceneToLoad = 1;

    [Header("Give the Player an Inventory Item")]
    public bool giveObject;
    public Inven objectToGive;
    [TextArea(2, 5)]
    public string gotItemMessage;
    public Sprite face;
    public string name;
    public bool setNewWalkable;
    public int newWalkable;

    [Header("Change Walkable")]
    public int currentWalkable;
    public List<GameObject> walkables;




    // Use this for initialization
    void Start()
    {
        cam = Camera.main.GetComponent<ChangeCursor>();
        inv = FindObjectOfType<SimpleInv>();
        target = GameObject.FindWithTag("Target").transform;
    }

    // Update is called once per frame
    public void OnHover()
    {
        //   Debug.Log("Hovering");
        if (seen)
        {
            cam.Interact();
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            nearEnough = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        nearEnough = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!cam.inConversation)
        {
            if (!seen)
            {
                cam.EnableDialog(lookMessages, face, name);
                seen = true;
            }
            else
            {

                if (nearEnough)
                {
                    if (needObject)
                    {

                        CheckIfHave();
                    }

                    else
                    {
                        Unlocked();
                    }
                }
                else
                {
                    target.position = transform.position;
                    cam.EnableDialog("I need to get closer first.");
                }
            }
        }
    }

    void CheckIfHave()
    {

        if (!unLocked)
        {
            if (inv.CheckItem(neededObject))
            {

                
                inv.RemoveItem(neededObject);
                unLocked = true;
                needObject = false;


                if (giveObject) {
                    GiveObject();
                    cam.EnableDialog(gotItemMessage);
                  //  Unlocked();

                }
                else
                {
                    cam.EnableDialog(haveItemMessage, face, name);
                    if (setNewWalkable)
                    {
                        currentWalkable = newWalkable;
                        for(int walkVal = 0; walkVal< walkables.Count; walkVal++)
                        {
                            if (walkVal == currentWalkable)
                            {
                                walkables[walkVal].SetActive(true);
                            }
                            else
                            {
                                walkables[walkVal].SetActive(false);
                            }
                        }
                    }
                }
            }
            else
            {
                cam.EnableDialog(needItemMessage, face, name);

            }
        }
        else {
           
            Unlocked();
        }



    }

    void GiveObject()
    {
        if (inv.CheckToAdd())
        {
            
            inv.addItem(objectToGive);
            giveObject = false;
        }

    }

    void Unlocked() {

        
        if (giveObject) {
            cam.EnableDialog(gotItemMessage, face, name);
            GiveObject();
        }


        else if (loadScene)
        {
            cam.EnableDialog(haveItemMessage, face, name);
            StartCoroutine(LoadScene());
        }

        else { cam.EnableDialog(usedUpMessage , face, name); }

        


    }

  


IEnumerator LoadScene()
{
   
    yield return new WaitForSeconds(5);
        cam.DisableDialog();
        SceneManager.LoadScene(sceneToLoad);

    }
    
}







