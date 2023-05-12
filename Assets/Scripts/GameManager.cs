using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LineRenderer lineRend;
    void Start()
    {
        lineRend.positionCount = 0;   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lineRend.positionCount++;
            lineRend.SetPosition(lineRend.positionCount - 1, position);
        }
    }
}
