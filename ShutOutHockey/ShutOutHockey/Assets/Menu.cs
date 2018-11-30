using UnityEngine;

public class Menu : MonoBehaviour {

    public AudioSource bgSource;

    private void Start()
    {
        if (PlayerPrefs.HasKey("BGMusic"))
        {
            if (PlayerPrefs.GetInt("BGMusic") == 0)
            {
                bgSource.Pause();
            }
        }
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
