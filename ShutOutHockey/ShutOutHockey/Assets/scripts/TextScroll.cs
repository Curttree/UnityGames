using System;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TextScroll : MonoBehaviour {

    private GameObject[] svPercentUI;
    public int cutoff = 70;
    public int padding = 500;
    public float speed = 0.8f;

    // Use this for initialization
    void Start()
    {
        svPercentUI = GameObject.FindGameObjectsWithTag("SVPercent");
        //TODO: Add flexibility for number of elements.
        if (svPercentUI.Length != 2)
        {
            print("WARNING: Text scroll was designed for two labels. Visual issues may occur.");
        }
        cutoff = (int)-(svPercentUI.First().GetComponent<RectTransform>().rect.width * 1.5f);
        padding = (int)Mathf.Abs(svPercentUI[0].transform.position.x - svPercentUI[1].transform.position.x);
    }

    // Update is called once per frame
    private void Update()
    {
        for (int index = 0; index < svPercentUI.Length; index++)
        {
            if (svPercentUI[index].transform.position.x <= cutoff)
            {
                switch (index)
                {
                    case 0:
                        svPercentUI[0].transform.position = new Vector3(svPercentUI[1].transform.position.x + padding, svPercentUI[index].transform.position.y);
                        break;
                    case 1:
                        svPercentUI[1].transform.position = new Vector3(svPercentUI[0].transform.position.x + padding, svPercentUI[index].transform.position.y);
                        break;
                    default:
                        break;
                }
            }
            svPercentUI[index].transform.Translate(new Vector3(-speed * Time.unscaledDeltaTime, 0, 0));
        }
    }
}
