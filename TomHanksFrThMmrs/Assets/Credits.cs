using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Credits : MonoBehaviour, IPointerClickHandler
{
    public GameObject hanks;
    public GameObject hanksToo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (hanks.activeSelf)
        {
            Application.Quit();
        }
        else
        {
            hanks.SetActive(true);
            hanksToo.SetActive(true);
        }
    }
}
