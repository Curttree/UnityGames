using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private ShotController shotController;
    private void Start()
    {
        shotController = gameObject.GetComponent<ShotController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UserInput();
        }

    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UserInput()
    {
        shotController.EnterShotValue();
    }
}
