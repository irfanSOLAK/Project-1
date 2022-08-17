using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task3Script : MonoBehaviour
{
    // Sensor
    [Header("Sensors")]
    public float sensorLength;
    public Vector3 frontSensorPosition;
    public float frontSensorAngle;

    // Movement
    private float newXPos;
    private Rigidbody rb;
    public GameObject boy;
    Vector3 starting;

    // Decision
    bool frontCenterSensorDetected;
    bool frontLeftSensorDetected;
    bool frontRightSensorDetected;
    bool leftSensorDetected;
    bool rightSensorDetected;

    //Result
    [Header("Result")]
    public int horizontalMovement;
    public float moveLeft;
    public float moveRight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        float randomNumber = Random.Range(0, 1f);
        GetComponent<Animator>().SetFloat("AnimTime", randomNumber);
        starting = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Boy").GetComponent<BoyScript>().level1)
        {
            GetComponent<Animator>().SetFloat("Speed", boy.GetComponent<BoyMoveScript>().forwardSpeed);
            newXPos = Mathf.Clamp(
                transform.position.x + horizontalMovement * boy.GetComponent<BoyMoveScript>().playerXValue,  // nothing 0, left -1 right +1
                boy.GetComponent<BoyMoveScript>().clampValues.x, 
                boy.GetComponent<BoyMoveScript>().clampValues.y); // stay between clampValues x and y
        }

    }

    private void FixedUpdate()
    {
        if (GameObject.Find("Boy").GetComponent<BoyScript>().level1)
        {
            Sensors();

            rb.MovePosition(
                new Vector3(Mathf.Lerp(transform.position.x, newXPos, boy.GetComponent<BoyMoveScript>().lerpSpeed * Time.fixedDeltaTime),
                // if lerpSpeed * Time.fixedDeltaTime = 1 returns newXpos, if lerpSpeed * Time.fixedDeltaTime = 0 returns transform.position.x
                transform.position.y,
                transform.position.z + boy.GetComponent<BoyMoveScript>().forwardSpeed * Time.fixedDeltaTime));  // for movement 
        }
    }
    void Sensors()
    {
        RaycastHit hit;
        Vector3 sensorsStartPos = transform.position + frontSensorPosition;

        // front center sensor
        if (Physics.Raycast(sensorsStartPos, transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.transform.CompareTag("Player"))
            {
                Debug.DrawLine(sensorsStartPos, hit.point);
                frontCenterSensorDetected = true;
            }
            else if (hit.collider.transform.CompareTag("Player"))
            {
                moveLeft += 0.5f;
                moveRight += 0.5f;
            }
        }



        //right sensor
        if (Physics.Raycast(sensorsStartPos, transform.right, out hit, sensorLength))
        {
            if (!hit.collider.transform.CompareTag("Player"))
            {
                Debug.DrawLine(sensorsStartPos, hit.point);
                rightSensorDetected = true;
            }
            else if (hit.collider.transform.CompareTag("Player"))
            {
                moveLeft += 0.5f;
            }
        }


        // left sensor
        if (Physics.Raycast(sensorsStartPos, -transform.right, out hit, sensorLength))
        {
            if (!hit.collider.transform.CompareTag("Player"))
            {
                Debug.DrawLine(sensorsStartPos, hit.point);
                leftSensorDetected = true;
            }
            else if (hit.collider.transform.CompareTag("Player"))
            {
                moveRight += 0.5f;
            }
        }

        sensorsStartPos.x += 0.05f;

        // front right sensor
        if (Physics.Raycast(sensorsStartPos, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.transform.CompareTag("Player"))
            {
                Debug.DrawLine(sensorsStartPos, hit.point);
                frontRightSensorDetected = true;
            }
        }

        sensorsStartPos.x -= 0.1f;
        // front left sensor
        if (Physics.Raycast(sensorsStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.transform.CompareTag("Player"))
            {
                Debug.DrawLine(sensorsStartPos, hit.point);
                frontLeftSensorDetected = true;
            }
        }

        sensorsStartPos.x += 0.05f;


        // front center bottom sensor
        if (Physics.Raycast(sensorsStartPos - new Vector3(0, 0.05f, 0), transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.transform.CompareTag("Player"))
            {
                Debug.DrawLine(sensorsStartPos - new Vector3(0, 0.05f, 0), hit.point);
                if (hit.collider.transform.parent.CompareTag("RotatorStick"))
                {
                    rightSensorDetected = true;
                }
                else
                {
                    frontCenterSensorDetected = true;
                }
            }
        }

        // right bottom sensor
        if (Physics.Raycast(sensorsStartPos - new Vector3(0, 0.05f, 0), transform.right, out hit, sensorLength))
        {
            if (!hit.collider.transform.CompareTag("Player"))
            {
                Debug.DrawLine(sensorsStartPos - new Vector3(0, 0.05f, 0), hit.point);
                if (hit.collider.transform.parent.CompareTag("RotatorStick"))
                {
                    rightSensorDetected = true;
                }
                else
                {
                    frontCenterSensorDetected = true;
                }
            }
        }

        // left bottom sensor
        if (Physics.Raycast(sensorsStartPos - new Vector3(0, 0.05f, 0), -transform.right, out hit, sensorLength))
        {
            if (!hit.collider.transform.CompareTag("Player"))
            {
                Debug.DrawLine(sensorsStartPos - new Vector3(0, 0.05f, 0), hit.point);
                if (hit.collider.transform.parent.CompareTag("RotatorStick"))
                {
                    leftSensorDetected = true;
                }
                else
                {
                    frontCenterSensorDetected = true;
                }
            }
        }

        MakeDecision();
    }

    void MakeDecision()
    {
        if (frontCenterSensorDetected)
        {
            moveLeft++;
            moveRight++;
        }

        if (frontLeftSensorDetected)
        {
            moveRight++;
        }

        if (frontRightSensorDetected)
        {
            moveLeft++;
        }

        if (leftSensorDetected)
        {
            moveRight++;
        }

        if (rightSensorDetected)
        {
            moveLeft++;
        }


        if (moveRight != moveLeft)
        {
            if (moveRight > moveLeft)
            {
                horizontalMovement = 1;
            }
            else
            {
                horizontalMovement = -1;
            }
        }
        else if(moveRight!=0)
        {
            if (transform.position.x > 0.2)
            {
                horizontalMovement = -1;
            }
            else
            {
                horizontalMovement = 1;
            }         
        }
        else
        {
            horizontalMovement = 0;
        }

        frontCenterSensorDetected = false;
        frontLeftSensorDetected = false;
        frontRightSensorDetected = false;
        leftSensorDetected = false;
        rightSensorDetected = false;
        moveLeft = 0;
        moveRight = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacles"))
        {
            transform.position = starting;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Finish")
        {
            Transform panel = GameObject.Find("Canvas").transform.GetChild(2);
            transform.position = panel.GetComponent<Task3RankingScript>().finalPos;
            panel.GetComponent<Task3RankingScript>().finalPos += new Vector3(0, 0, -1);
            GetComponent<Task3Script>().enabled = false;
        }
    }
}
