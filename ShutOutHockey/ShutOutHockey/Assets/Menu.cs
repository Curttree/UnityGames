using UnityEngine;

public class Menu : MonoBehaviour {

    private void Start()
    {
        Screen.SetResolution(960, 640, true);
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
