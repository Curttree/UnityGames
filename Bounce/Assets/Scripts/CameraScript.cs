using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static float offsetX;
    public static float offsetY;

    // Update is called once per frame
    void Update()
    {
        if (BallScript.instance != null && BallScript.instance.isAlive && BallScript.instance.gameObject.transform.position.x > 0)
        {
            MoveCamera();
        }
    }

    void MoveCamera()
    {
        Vector3 temp = transform.position;
        temp.x = BallScript.instance.GetPositionX() + offsetX;
        //temp.y = BallScript.instance.GetPositionY() + offsetY;
        transform.position = temp;
    }
}
