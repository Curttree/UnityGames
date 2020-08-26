using UnityEngine;

public class MenuBallController : MonoBehaviour
{
    [SerializeField]
    private float maxY, minY, speed;

    [SerializeField]
    private bool moveDown;

    // Update is called once per frame
    void Update()
    {
        if (moveDown && transform.position.y >= minY)
        {
            Vector3 temp = transform.position;
            temp.y -= speed * Time.deltaTime;
            transform.position = temp;
        }
        else if (moveDown && transform.position.y < minY)
        {
            moveDown = false;
        }
        else if (!moveDown && transform.position.y <= maxY)
        {
            Vector3 temp = transform.position;
            temp.y += speed * Time.deltaTime;
            transform.position = temp;
        }
        else if (!moveDown && transform.position.y > maxY)
        {
            moveDown = true;
        }
        
    }
}
