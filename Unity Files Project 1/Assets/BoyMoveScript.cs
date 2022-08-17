using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyMoveScript : MonoBehaviour
{
    public float forwardSpeed; // forward speed
    public float lerpSpeed; // left right movement speed
    public float playerXValue; // movement unit
    public Vector2 clampValues;

    private float newXPos;
    private float startXPos;

    private Rigidbody rb;

    public bool contactRotPlat;
    private Transform rotTransform;

    private float lastContact;
    private float move;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startXPos = transform.position.x;
        contactRotPlat = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            lastContact = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            move = Input.mousePosition.x - lastContact;
            lastContact = Input.mousePosition.x;
            newXPos = Mathf.Clamp(transform.position.x + move * playerXValue, // nothing 0, left -1 right +1
                startXPos + clampValues.x,
                startXPos + clampValues.y);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            move = 0;
        }


        if (Input.GetButton("Horizontal"))  //Get the value of the Horizontal input axis.
        {
            newXPos = Mathf.Clamp(transform.position.x + Input.GetAxisRaw("Horizontal") * playerXValue, // nothing 0, left -1 right +1
                startXPos + clampValues.x,
                startXPos + clampValues.y); // stay between clampValues x and y

            if(contactRotPlat)
            transform.parent = null;
        }
        else
        {
            if (contactRotPlat)
            {
                transform.parent = rotTransform;
            }
            else
            {
                transform.parent = null;
            }
        }
    }

    private void FixedUpdate()
    {

        if (gameObject.GetComponent<BoyScript>().level1)
        {
            rb.MovePosition(
                new Vector3(Mathf.Lerp(transform.position.x, newXPos, lerpSpeed * Time.fixedDeltaTime),
                // if lerpSpeed * Time.fixedDeltaTime = 1 returns newXpos, if lerpSpeed * Time.fixedDeltaTime = 0 returns transform.position.x
                transform.position.y,
                transform.position.z + forwardSpeed * Time.fixedDeltaTime)
                );  // for movement 

        }

    }


    private void OnCollisionStay(Collision collision)
    {
        if(collision.collider.CompareTag("Finish"))
        {
            rotTransform = collision.transform;
            contactRotPlat = true;
        }
        else
        {
            transform.parent = null;
            contactRotPlat = false;
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        transform.parent = null;

    }
}
