using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoyScript : MonoBehaviour
{


    Vector3 starting;
    public bool level1;
    public bool level1Rot;
    // Start is called before the first frame update
    void Start()
    {
          starting = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
  
    }



    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Obstacles")) 
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex );
            }
            else
            {
                transform.position = starting;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) 
        {
            GameObject.Find("Main Camera").GetComponent<Cam>().follow = false;
            GetComponent<BoyMoveScript>().enabled = false;
            transform.position= new Vector3(-100, 3.026f, 100);
        }

        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            if (other.tag == "Finish")
            {
                Transform panel = GameObject.Find("Canvas").transform.GetChild(2);
                transform.position = panel.GetComponent<Task3RankingScript>().finalPos;
                panel.GetComponent<Task3RankingScript>().finalPos += new Vector3(0, 0, -1);
                GameObject.Find("Main Camera").GetComponent<Cam>().follow = false;
                GetComponent<BoyMoveScript>().enabled = false;

            }
        }
    }

    public void GameStart()
    {
        level1 = true;
        gameObject.GetComponent<BoyAnimationScript>().MoveAnimation();
    }


    public void Home()
    {
        int sceneNumber = SceneManager.GetActiveScene().buildIndex;
        if (sceneNumber == 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
        }
    }
}
