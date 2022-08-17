using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Task2Script : MonoBehaviour
{
    public Animator animator;
    public Texture2D theColorTexture; //fýrça texture'ý
    public Camera cam;
    Color[] Data; // % için
    public float percentTemp; // % için
    public float percent; // % için
    public Texture2D redBrush;
    public Texture2D brush;
    public Text percentText;
    private void Start()
    {
        Cursor.SetCursor(brush, Vector2.down, CursorMode.Auto);
        theColorTexture = new Texture2D(theColorTexture.height, theColorTexture.width, TextureFormat.ARGB32, false);
        GetComponent<Renderer>().material.mainTexture = theColorTexture;
        percentTemp = 0;
       
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            animator.SetBool("Painting", true);
            

            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Paint(hit.textureCoord);

                Cursor.SetCursor(redBrush, new Vector2(15 * brush.width / 16, brush.height / 2), CursorMode.Auto);

                ConvertMatTexture2D();

                for (int i = 0; i < Data.Length; i++)
                {
                    if (Data[i] == Color.red)
                    {
                        percentTemp++;
                    }

                }
                if (percent * Data.Length / 100 != percentTemp)
                {
                    StartCoroutine(TextAnim());
                }
                percent = 100 * percentTemp / Data.Length;

              
                percentTemp = 0;
                
            }
        }

        else
        {
            animator.SetBool("Painting", false);
            Cursor.SetCursor(brush, new Vector2(15 * brush.width / 16, brush.height / 2), CursorMode.Auto);
        }


    }

    private void Paint(Vector2 coordinate)
    {
        coordinate.x *= theColorTexture.width;
        coordinate.y *= theColorTexture.height;
        Color32[] texPixels = theColorTexture.GetPixels32();

        int texPoint = (int)coordinate.x + ((int)coordinate.y * theColorTexture.width);

        texPixels[texPoint] = Color.red;
        theColorTexture.SetPixels32(texPixels);
        theColorTexture.Apply();
    }

    void ConvertMatTexture2D()
    {
        Texture mainTexture = GetComponent<Renderer>().material.mainTexture;
        Texture2D texture2D = new Texture2D(mainTexture.width, mainTexture.height, TextureFormat.RGBA32, false);
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture renderTexture = new RenderTexture(mainTexture.width, mainTexture.height, 32);
        Graphics.Blit(mainTexture, renderTexture);
        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        Data = texture2D.GetPixels();

        RenderTexture.active = currentRT;
    }

    public void Home()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    private IEnumerator TextAnim()
    {
        for (float i = 1f; i < 1.2f; i += 0.05f)
        {
            percentText.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.01f);
        }

        percentText.rectTransform.localScale = new Vector3(1f, 1f, 1f);
        percentText.text = percent.ToString("0.00") + "%";
    }

}
