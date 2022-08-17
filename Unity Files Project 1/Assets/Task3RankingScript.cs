using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
public class Task3RankingScript : MonoBehaviour
{
    public Vector3 finalPos;
    public GameObject[] ranking;
    List<GameObject> sorting;
    Text[] newText;
    Color textColor;
    Color finalTextColor;
    // Start is called before the first frame update
    void Start()
    {
        newText = GetComponentsInChildren<Text>();
        finalPos = new Vector3(-100, 3.026f, 100);

        textColor = new Color(161f / 255f, 11f / 255f, 0, 185f / 255f);
        finalTextColor = new Color(0, 2f / 255f, 144f / 255f, 225f / 255f);
    }

    // Update is called once per frame
    void Update()
    {
        sorting = ranking.OrderBy(z => z.transform.position.z).ToList();

        for(int j = 0; j < 11; j++)
        {
            if(sorting[10 - j].transform.GetChild(3).GetComponent<TextMesh>() != null)
            {
                newText[j].text = sorting[10 - j].transform.GetChild(3).GetComponent<TextMesh>().text;

                if (sorting[10 - j].transform.position.z > 17)
                {
                    newText[j].color = finalTextColor;
                }
                else
                {
                    newText[j].color = textColor;
                }                
            }
            else
            {
                newText[j].text = sorting[10 - j].name;

                if (sorting[10 - j].transform.position.z > 17)
                {
                    newText[j].color = finalTextColor;
                }
                else
                {
                    newText[j].color = Color.yellow;
                }
            }
        }
       
    }
}
