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

    private List<Vector2> points = new List<Vector2>();
    void Start()
    {
        //drawManager = FindObjectOfType<DrawManager>();
        edgeCollider.transform.position -= transform.position;
        //lineRenderer = GetComponent<LineRenderer>();
        //lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        /*        float alpha = 1.0f;
                Gradient gradient = new Gradient();
                gradient.SetKeys(
                    new GradientColorKey[] { new GradientColorKey(Color.green, 0.0f), new GradientColorKey(Color.red, 1.0f) },
                    new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }*/
        //);

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
        yield return new WaitForSeconds(2);
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

