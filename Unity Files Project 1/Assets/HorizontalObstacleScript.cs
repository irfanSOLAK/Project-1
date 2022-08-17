using UnityEngine;

public class HorizontalObstacleScript : MonoBehaviour
{
    public float xRot;
    public float yRot;
    public float zRot;
    public float movingSpeed;

    private void FixedUpdate()
    {
        Vector3 leftBorder = new Vector3(-0.77f, 3.285f, transform.position.z);
        Vector3 rigthBorder = new Vector3(0.77f, 3.285f, transform.position.z); // -0,77 ile 0,77 arasý harket etsin
        Vector3 forwardBorder = new Vector3(transform.position.x, 3.285f, 7);
        Vector3 backwardBorder = new Vector3(transform.position.x, 3.285f, 8);

        transform.eulerAngles += new Vector3(xRot, yRot, zRot);


        if (gameObject.name== "HorizontalObstacle")
        {
            transform.position = Vector3.Lerp(leftBorder, rigthBorder, Mathf.PingPong(Time.time * movingSpeed, 1.0f));
        }
        else
        {
            transform.position = Vector3.Lerp(forwardBorder, backwardBorder, Mathf.PingPong(Time.time * movingSpeed, 1.0f));
        }

    }
}
