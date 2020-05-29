using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static float offsetX;
    public static float offsetY;

    // Update is called once per frame
    void Update()
    {
        if (BirdScript.instance != null && BirdScript.instance.isAlive)
        {
            MoveCamera();
        }
    }

    void MoveCamera()
    {
        Vector3 temp = transform.position;
        temp.x = BirdScript.instance.GetPositionX() + offsetX;
        //temp.y = BirdScript.instance.GetPositionY() + offsetY;
        transform.position = temp;
    }
}
