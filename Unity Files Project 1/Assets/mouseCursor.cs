using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseCursor : MonoBehaviour
{
    public Texture2D redBrush;
    public Texture2D brush;
    public Camera m_cam;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(brush, new Vector2(15*brush.width/16, brush.height / 2), CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {


  

    }
}
