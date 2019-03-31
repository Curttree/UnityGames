using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class SimpleInv : MonoBehaviour
{


    public List<Inven> items = new List<Inven>();
    public int maxInvantorySlots = 11;
    public bool resetInvantoryOnStart = true;
    public List<GameObject> textLinks = new List<GameObject>();
    public GameObject textLinkPrefab;
    public int linkDist = -90;
    public int totalOffsetLink = 500;

    private static bool created = false;

    void Awake()
    {



    }

    // Use this for initialization
    void Start()
    {
        if (resetInvantoryOnStart) {
            ClearnInv();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool CheckToAdd() {
        if (items.Count >= maxInvantorySlots)
        {
            Debug.Log("Too many items");
            return false;
        }
        else {
            return true;
        }
    }
    public void addItem(Inven item)
    {

    
       

        GameObject linkGO = (GameObject)Instantiate(textLinkPrefab);
        linkGO.GetComponent<Image>().sprite = item.objectImage;
        linkGO.name = item.name;
       // linkGO.transform.parent = this.transform;
        linkGO.transform.SetParent(this.transform);
        linkGO.transform.localPosition = new Vector3(items.Count * 1 * linkDist - totalOffsetLink, 0, 0);
        linkGO.transform.localScale = Vector3.one;
        textLinks.Add(linkGO);
        items.Add(item);
    }


    public bool CheckItem(Inven item)
    {
        if (items.Contains(item))
        { return true; }
        else { return false; }
    }

    public void RemoveItem(Inven item)
    {


        items.Remove(item);



        for (int t = 0; t < textLinks.Count; t++)
        {
            
            if (textLinks[t].name == item.name)
            {
                Debug.Log("found " + item.name +" in textlinks");

                GameObject.Destroy(textLinks[t]);
                    textLinks.RemoveAt(t);



            }
            else {
                Debug.Log("did not find" + item.name);
              
            }
            //Debug.Log(t);
        }


        for (int t = 0; t < textLinks.Count; t++)
        {
            textLinks[t].transform.localPosition = new Vector3(t * 1 * linkDist - totalOffsetLink, 0, 0);

        }


    }

    public void ClearnInv() {
        items.Clear();
    }

}



        //public void AddItemToInvanntory(CollectableObject obj)
        //{
        //    if (objectsInInvantory.Count >= invantorySlots.Count)
        //    {
        //        Debug.Log("Too many items");
        //        return;
        //    }

        //    if (!objectsInInvantory.Find(x => x.itemLogic.name == obj.objectRefrence.name))
        //    {
        //        objectsInInvantory.Add(obj.objectRefrence);
        //        obj.objectRefrence.quantity = obj.quantity;
        //        invantorySlots[objectsInInvantory.Count - 1].GetComponent<InvantorySlot>().SetItem(obj.objectRefrence.objectImage, obj.quantity);
        //    }
        //    else
        //    {
        //        int idx = objectsInInvantory.FindIndex(x => x.itemLogic.name == obj.objectRefrence.name);
        //        objectsInInvantory[idx].quantity += obj.quantity;
        //        invantorySlots[idx].GetComponent<InvantorySlot>().SetItem(objectsInInvantory[idx].objectImage, objectsInInvantory[idx].quantity);
        //    }

        //    if (useTooltip)
        //    {
        //        if (currentlySelectedItem >= 0 && currentlySelectedItem < objectsInInvantory.Count)
        //            tooltipText.text = objectsInInvantory[currentlySelectedItem].objectTooltip;
        //        else
        //            tooltipText.text = "";
        //    }
        //}



