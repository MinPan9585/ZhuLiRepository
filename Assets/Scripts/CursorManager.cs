using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursorTex;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorTex, new Vector2(16, 16), CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
