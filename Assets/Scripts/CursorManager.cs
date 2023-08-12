using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CursorManager : MonoBehaviour
{
    public float distance = 10.0f;
    public Vector3 offset = Vector3.zero;

    public Texture2D cursorTex;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorTex, new Vector2(16, 16), CursorMode.Auto);
    }

    // Update is called once per frame
    private void Update()
    {
        //Texture2D.alpha




        if (Camera.main == null)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 newPosition = ray.GetPoint(distance);
        transform.position = newPosition + offset;
    }
}
