using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoyCountdownScript : MonoBehaviour
{

    public Text countdownText;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountdownCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CountdownCoroutine()
    {
        if (GameObject.FindGameObjectWithTag("GameController") != null)
        {
            GameObject.FindGameObjectWithTag("GameController").gameObject.SetActive(true);
        }

        countdownText.text = " ";
        yield return new WaitForSeconds(1.0f);
        countdownText.text = "3";
        yield return new WaitForSeconds(1.0f);
        countdownText.text = "2";
        yield return new WaitForSeconds(1.0f);
        countdownText.text = "1";
        yield return new WaitForSeconds(1.0f);
        countdownText.text = "Go!";
        yield return new WaitForSeconds(1.0f);
        countdownText.text = " ";

        if (GameObject.FindGameObjectWithTag("GameController") != null)
        {
            GameObject.FindGameObjectWithTag("GameController").gameObject.SetActive(false);
        }
        gameObject.GetComponent<BoyScript>().GameStart();
    }
}
