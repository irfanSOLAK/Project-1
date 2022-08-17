using UnityEngine;

public class HalfDonutScript : MonoBehaviour
{
    public float xRot;
    public float yRot;
    public float zRot;
    public float movingSpeedDonut;
    // Start is called before the first frame update

    private void FixedUpdate()
    {
        Vector3 leftBorderDonut = new Vector3(transform.position.x + -0.13f, transform.position.y, transform.position.z);
        Vector3 rigthBorder = new Vector3(transform.position.x + 0.15f, transform.position.y, transform.position.z); // -0,13 ile 0,15 arasý harket etsin

        if (movingSpeedDonut != 0)
        {
            gameObject.transform.GetChild(0).transform.position = Vector3.Lerp(leftBorderDonut, rigthBorder, Mathf.PingPong(Time.time * movingSpeedDonut, 1.0f));
        }


        transform.GetChild(0).transform.rotation *= Quaternion.Euler(xRot, yRot, zRot);

    }
}
