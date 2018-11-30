using UnityEngine;

public class TextScroll : MonoBehaviour {

    //public GameObject[] scrollingTexts;
    //public int cutoff = 10;
    //public float startPos;

    //public TextMeshProUGUI TextMeshComponent;
    //public float scrollSpeed = 0.1f;

    //private TextMeshProUGUI m_cloneTextObject;
    //private RectTransform rectTransform;

    //private void Awake()
    //{
    //    rectTransform = TextMeshComponent.GetComponent<RectTransform>();

    //    m_cloneTextObject = Instantiate(TextMeshComponent) as TextMeshProUGUI;
    //    RectTransform cloneRectTransform = m_cloneTextObject.GetComponent<RectTransform>();

    //    rectTransform.anchorMin = new Vector2(0, 0.5f);
    //    cloneRectTransform.SetParent(rectTransform);
    //    cloneRectTransform.anchorMin = new Vector2(0, 0.5f);
    //    cloneRectTransform.localScale = new Vector3(1, 1, 1);
    //}
    //// Use this for initialization
    //IEnumerator Start ()
    //{
    //    //var xpos = startPos;
    //    //scrollingTexts = GameObject.FindGameObjectsWithTag("SVPercent");
    //    //foreach (var textbox in scrollingTexts)
    //    //{
    //    //    textbox.transform.position = new Vector2(xpos, textbox.transform.position.y);
    //    //    xpos += startPos;
    //    //}
    //    float width = TextMeshComponent.preferredWidth;
    //    Vector3 startPosition = rectTransform.position;

    //    float scrollPosition = 0;

    //    while (true)
    //    {
    //        if (TextMeshComponent.h)
    //        {
    //            print("changed");
    //            width = TextMeshComponent.preferredWidth;
    //            m_cloneTextObject.text = TextMeshComponent.text;
    //        }

    //        rectTransform.position = new Vector3(-scrollPosition % width ,startPosition.y, startPosition.z);

    //        scrollPosition += scrollSpeed * 20 * Time.deltaTime;

    //        yield return null;
    //    }
    //}

    private GameObject[] svPercentUI;
    public int cutoff = 150;
    public int startPos = 1400;

    // Use this for initialization
    void Start()
    {
        svPercentUI = GameObject.FindGameObjectsWithTag("SVPercent");
    }

    // Update is called once per frame
    private void Update()
    {
        foreach (var svPercent in svPercentUI)
        {
            if (svPercent.transform.position.x <= cutoff)
            {
                svPercent.transform.position = new Vector3(startPos, svPercent.transform.position.y);
            }
            svPercent.transform.Translate(new Vector3(-0.4f, 0, 0));
        }
    }
}
