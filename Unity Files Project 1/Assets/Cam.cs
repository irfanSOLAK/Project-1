using UnityEngine;

public class Cam : MonoBehaviour
{
    public GameObject target;
    public Vector3 distance;
    public bool follow;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(0, 7, -1);
        follow = true;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (follow)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, target.transform.position + distance, Time.deltaTime);
        }
        else
        {
            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(0, 7, -1), Time.deltaTime);
        }
       
    }
}
