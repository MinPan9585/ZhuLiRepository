using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Line : MonoBehaviour
{
    //public DrawManager drawManager;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private EdgeCollider2D edgeCollider;

    //public GameObject drawing;

    private List<Vector2> points = new List<Vector2>();

    public bool isVisible;
    float lineAlpha = 1;
    bool isTrans = false;
    void Start()
    {
        edgeCollider.transform.position -= transform.position;
        StartCoroutine(DestroyLine());
    }

    public void SetPosition(Vector2 pos)
    {
        if (!CanAppend(pos)) return;
        if (DrawManager.dm.currentEnergy <= 0) return;

        points.Add(pos);

        lineRenderer.positionCount++;
        DrawManager.dm.currentEnergy--;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, pos);

        //Instantiate(drawing, transform.position, Quaternion.identity);

        edgeCollider.points = points.ToArray();
    }

    private bool CanAppend(Vector2 pos)
    {
        if (lineRenderer.positionCount == 0) return true;

        return Vector2.Distance(lineRenderer.GetPosition(lineRenderer.positionCount - 1), pos) > DrawManager.RESOLUTION;
    }



    IEnumerator DestroyLine()
    {
        yield return new WaitForSeconds(4);
        //StartCoroutine(Fade());
        //for(int i=0;i<8;i++)
        //{
        //    isVisible = !isVisible;
        //    if(!isVisible)
        //    {
        //        lineRenderer.startColor = new Color(1, 0.87f, 0.41f, 0);
        //        lineRenderer.startColor = new Color(1, 0.87f, 0.41f, 0);
        //    }
        //    else
        //    {
        //        lineRenderer.startColor = new Color(1, 0.87f, 0.41f, 1);
        //        lineRenderer.startColor = new Color(1, 0.87f, 0.41f, 1);
        //    }
        //}

        while (!isTrans)
        {
            print(lineAlpha);
            lineAlpha -= Time.deltaTime * 0.6f;
            lineRenderer.startColor = new Color(1, 0.87f, 0.41f, lineAlpha);
            lineRenderer.endColor = new Color(1, 0.87f, 0.41f, lineAlpha);

            if (lineAlpha > 0.1f)
            {
                yield return null;
            }
            else
            {
                isTrans = true;
            }

        }
        

        //yield return new WaitForSeconds(0.1f);
        DrawManager.dm.currentEnergy += lineRenderer.positionCount;
        Destroy(gameObject);
    }

    /*IEnumerator Fade()
    {
        Gradient gradient = new Gradient();
        float alpha = 1.0f;
        while (alpha>0.000001f)
        {
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) };

             yield return null;
        }*
                
    }*/

    /*IEnumerator Fade()
    {
        while (lineRenderer.material.color.a > 0.1f)
        {
            lineRenderer.material.color = new Color(lineRenderer.material.color.r,
            lineRenderer.material.color.g,
            lineRenderer.material.color.b,
            Mathf.Lerp(lineRenderer.material.color.a, 0, DrawManager.dm.smoothing * Time.deltaTime));
            yield return null;
        }
    }*/
}

